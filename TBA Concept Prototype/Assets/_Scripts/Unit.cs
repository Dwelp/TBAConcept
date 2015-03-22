using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Unit : MonoBehaviour {

    public string objectID;
    public string name;
    public UnitOwner unitOwner;
    public List<UnitAction> unitActions;
    
    // -- Unit Stats
    public float speed;
    public float moveRange;
    public float maxHealth;
    public float currentHealth; 

    // -- Actions
    public bool hasMoved;
    public bool hasActed;
    protected UnitAction activeAction;

    // -- Combo Structure
    public float comboStep;
    public float defaultComboStep;

    // -- Pathfinding
    protected NavMeshAgent agent;

    // -- State
    protected bool initDone;
    public UnitState unitState;
    public bool speedGougeReady;
    public GameObject DeathGFX;

    // -- Info
    protected float speedGouge;

    // --- UI
    protected Canvas canvas;

    protected virtual void Awake()
    {
        canvas = transform.FindChild("Canvas").GetComponent<Canvas>();
        canvas.enabled = false;
        unitState = UnitState.NotReady;

        unitActions = transform.FindChild("UnitActions").GetComponents<UnitAction>().ToList();
        print("actions: " + unitActions.Count);

        foreach(UnitAction action in unitActions)
        {
            print(action.GetType());
        }

        agent = GetComponent<NavMeshAgent>();

        // Stats
        currentHealth = maxHealth;
        defaultComboStep = 1;
    }

	// Use this for initialization
	protected virtual void Start () {
        initDone = true;
	}
	
	// Update is called once per frame
    protected virtual void Update() 
    {
        if (!initDone)
            return;

        UpdateUI();

        UpdateState();
	}

    public void UpdateSpeedGouge(float amount)
    {
        speedGouge += amount * speed;
        speedGouge = Mathf.Min(speedGouge, 100);

        if(speedGouge == 100)
        {
            speedGougeReady = true;
            unitState = UnitState.Ready;
        }
    }

    public void ResetSpeedGouge()
    {
        speedGouge = 0;
        speedGougeReady = false;
        unitState = UnitState.NotReady;
    }

    public void TurnStarted()
    {
        unitState = UnitState.Active;
        hasMoved = false;
        comboStep = defaultComboStep;
    }

    public void TurnEnded()
    {
        unitState = UnitState.NotReady;        
    }

    // Unit State

    public void UpdateState()
    {
        if(currentHealth <= 0)
        {
            KillUnit();
        }
    }

    protected void KillUnit()
    {
        StartCoroutine(CKillUnit());
    }

    protected IEnumerator CKillUnit()
    {
        CombatManager.Instance.UnitDied(this);
        gameObject.SetActive(false);

        GameObject obj = ParticleSystem.Instantiate(DeathGFX);
        obj.transform.position = transform.position;
        ParticleSystem particleSystem = obj.GetComponent<ParticleSystem>();
        particleSystem.startSize *= 2;
        particleSystem.Play();

        //yield return new WaitForSeconds(particleSystem.duration);

        Destroy(gameObject);
        yield return null;
    }

    // Actions 

    public void ActivateAction(string actionName)
    {
        activeAction = GetAction(actionName);

        UIManager.Instance.EnterTargetSelection(activeAction);

        unitState = UnitState.TargetSelection;
    }

    public void DeactivateAction()
    {
        activeAction = null;

        UIManager.Instance.ExitTargetSelection();

        unitState = UnitState.Active;
    }

    public virtual void ValidatedActionTarget(Vector3 targetPos)
    {
        UIManager.Instance.ExitTargetSelection();
        unitState = UnitState.Active;

        activeAction.ActivateAction(targetPos);
    }

    public virtual void ValidatedMoveLocation(Vector3 movePos)
    {
        hasMoved = true;

        UIManager.Instance.ExitTargetSelection();
        unitState = UnitState.Active;

        activeAction.ActivateAction(movePos);
    }

    public virtual void ActionFinished(string actionName)
    {
        UIManager.Instance.ExitTargetSelection();
        UnitAction action = GetAction(actionName);

        if (action.AllowCombo())
        {
            comboStep = action.ComboStepRequired();
            comboStep++;
        }
        else
            comboStep = defaultComboStep;

        activeAction = null;
    }

    public UnitAction GetAction(string action)
    {
        return unitActions.Find(p => p.actionName == action);
    }

    public UnitAction GetActiveAction()
    {
        return activeAction;
    }

    public void DoDamage(float amount)
    {
        currentHealth -= amount;
        currentHealth = Mathf.Max(0, currentHealth);
    }

    // Pathfinding
    public NavMeshAgent GetNavAgent()
    {
        return agent;
    }

    // UI
    void UpdateUI()
    {
        if (UIManager.Instance == null)
            return;

        if(UIManager.Instance.unitCombatUIEnabled)
        {
            canvas.enabled = true;

            Transform[] children = canvas.GetComponentsInChildren<Transform>();
            Transform[] labels = children.Where(p => p.tag == "Label").ToArray();
            List<Text> uiLabels = (labels.ToList()).Select(p => p.GetComponent<Text>()).ToList();

            Text text;
            text = uiLabels.Find(p => p.name == "SpeedGougeLabel");
            text.text = Mathf.Ceil(speedGouge).ToString();
        }
        else
        {
            canvas.enabled = false;
        }
    }

}
