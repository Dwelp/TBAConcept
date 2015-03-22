using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class AIBehavior : MonoBehaviour {

    protected AIUnit unit;
    protected Unit activeTarget;
    protected bool waitingForAction;
    protected UnitAction pendingAction;

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

    protected virtual IEnumerator WaitForAction(UnitAction action)
    {
        yield return null;
    }

    protected virtual void FindTarget()
    {
        Unit[] combatUnits = GameObject.FindObjectsOfType<Unit>();
        combatUnits = combatUnits.Where(p => p.unitOwner == UnitOwner.Player).ToArray();

        activeTarget = combatUnits[0];
    }
    
    public virtual void ActionFinished(UnitAction action)
    {

    }

    public virtual List<Unit> GetAllTargetsInRange(float range)
    {
        List<Unit> unitsInRange = CombatManager.Instance.GetCombatUnits();
        unitsInRange = unitsInRange.Where(p => Vector3.Distance(transform.position, p.transform.position) <= range).ToList();

        return unitsInRange;
    }

    public virtual Unit GetClosestTarget(List<Unit> targetList)
    {
        float nearestDistanceSqr = Mathf.Infinity;
        Unit nearestUnit = null;

        foreach(Unit unit in targetList)
        {
             Vector3 unitPos = unit.transform.position;
             float distanceSqr = (unitPos - transform.position).sqrMagnitude;
 
             if (distanceSqr < nearestDistanceSqr) 
             {
                 nearestUnit = unit;
                 nearestDistanceSqr = distanceSqr;
             }
        }

        return nearestUnit;
    }
}
