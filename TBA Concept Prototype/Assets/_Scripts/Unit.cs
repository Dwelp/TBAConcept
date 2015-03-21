using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Unit : MonoBehaviour {

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
    protected UnitAction activeAction;

    // -- Pathfinding
    protected NavMeshAgent agent;

    // -- State
    protected bool initDone;
    public UnitState unitState;
    public bool speedGougeReady;

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

        agent = GetComponent<NavMeshAgent>();

        // Stats
        currentHealth = maxHealth;
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
    }

    public void TurnEnded()
    {
        unitState = UnitState.NotReady;
    }

    // Actions 

    public void ActivateAction(string action)
    {
        activeAction = GetAction(action);

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

    public virtual void ActionFinished()
    {
        UIManager.Instance.ExitTargetSelection();
        activeAction = null;
    }

    UnitAction GetAction(string action)
    {
        return unitActions.Find(p => p.actionName == action);
    }

    public UnitAction GetActiveAction()
    {
        return activeAction;
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
