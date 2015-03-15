using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Unit : MonoBehaviour {

    public string name;
    public UnitOwner unitOwner;

    // -- Unit Stats
    public float speed;
    public float moveRange;

    // -- Actions
    bool hasMoved;

    // -- Pathfinding
    NavMeshAgent agent;

    // -- State
    protected bool initDone;
    public bool speedGougeReady;

    // -- Info
    protected float speedGouge;

    // --- UI
    protected Canvas canvas;

    protected virtual void Awake()
    {
        canvas = transform.FindChild("Canvas").GetComponent<Canvas>();
        canvas.enabled = false;

        agent = GetComponent<NavMeshAgent>();
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
        }
    }

    public void ResetSpeedGouge()
    {
        speedGouge = 0;
        speedGougeReady = false;
    }

    // Actions 




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
