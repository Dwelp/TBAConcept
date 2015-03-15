using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class AIBehavior : MonoBehaviour {

    protected AIUnit unit;
    protected Unit activeTarget;
    protected bool waitingForAction;

    protected virtual void Awake()
    {
        unit = gameObject.GetComponent<AIUnit>();
    }

	// Use this for initialization
	protected virtual void Start () 
    {
	
	}
	
	// Update is called once per frame
    protected virtual void Update()
    {
	
	}

    public virtual void ProcessTurn()
    {
        FindTarget();

        StartCoroutine(CProcessTurn());
    }

    protected virtual IEnumerator CProcessTurn()
    {
        yield return null;
    }

    protected virtual IEnumerator WaitForAction()
    {
        yield return null;
    }

    protected void FindTarget()
    {
        Unit[] combatUnits = GameObject.FindObjectsOfType<Unit>();
        combatUnits = combatUnits.Where(p => p.unitOwner == UnitOwner.Player).ToArray();

        activeTarget = combatUnits[0];
    }
    
    public virtual void ActionFinished(UnitAction action)
    {

    }
}
