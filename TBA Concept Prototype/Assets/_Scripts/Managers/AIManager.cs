using UnityEngine;
using System.Collections;

public class AIManager : Manager<AIManager> {

    AIUnit activeUnit;

    void Awake()
    {

    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void HandleAITurn(Unit unit)
    {
        activeUnit = (AIUnit)unit;

        if (activeUnit.aiBehavior == null)
        {
            print("Unit has no AI set, crating base AI");
            activeUnit.aiBehavior = activeUnit.gameObject.AddComponent<SimpleBehavior>();
            activeUnit.aiBehavior.ProcessTurn();
        }
        else
        {
            activeUnit.aiBehavior.ProcessTurn();
        }
    }

    public void EndAITurn()
    {
        CombatManager.Instance.EndTurn();
    }
}
