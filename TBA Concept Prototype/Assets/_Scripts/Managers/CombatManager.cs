using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class CombatManager : Manager<CombatManager> {

    public enum TurnController
    {
        None,
        Player,
        AI
    }

    public enum CombatState
    {
        Init,
        PassTime,
        ActiveTurn
    }

    public TurnController turnController;
    CombatState combatState;
    CombatInfo combatInfo;

    // -- Tracking
    List<Unit> readyUnits;
    Unit activeUnit;

    void Awake()
    {
        readyUnits = new List<Unit>();
        combatState = CombatState.Init;
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    if(combatState == CombatState.PassTime)
        {
            if(!CheckForReadyUnits())
                ProcessTime();
        }
	}

    public void InitCombat()
    {
        combatInfo = new CombatInfo();

        combatInfo.combatUnits = (GameObject.FindObjectsOfType<Unit>()).ToList();
        combatInfo.combatUnits = combatInfo.combatUnits.OrderBy(p => p.speed).ToList();
        print("found " + combatInfo.combatUnits.Count + " units");

        combatState = CombatState.PassTime;
    }

    void ProcessTime()
    {
        for (int i = 0; i < combatInfo.combatUnits.Count; i++)
        {
            Unit unit = combatInfo.combatUnits[i];

            unit.UpdateSpeedGouge(Time.deltaTime);

            if(unit.speedGougeReady)
            {
                readyUnits.Add(unit);
            }
        }

        readyUnits = readyUnits.OrderBy(p => p.speed).ToList();
    }

    bool CheckForReadyUnits()
    {
        readyUnits.RemoveAll(p => p == null);

        if(readyUnits.Count > 0)
        {
            SetupTurn(readyUnits[0]);
            return true;
        }

        return false;
    }

    void SetupTurn(Unit unit)
    {
        combatState = CombatState.ActiveTurn;
        readyUnits.Remove(unit);
        activeUnit = unit;

        StarTurn();             
    }

    void StarTurn()
    {
        if (activeUnit.unitOwner == UnitOwner.Player)
        {
            turnController = TurnController.Player;

            activeUnit.TurnStarted();
        }
        else
        {
            turnController = TurnController.AI;

            AIManager.Instance.HandleAITurn(activeUnit);
        }
    }

    public void EndTurn()
    {
        activeUnit.TurnEnded();
        activeUnit = null;
        turnController = TurnController.None;
        combatState = CombatState.PassTime;
    }

    // Unit States

    public void UnitDied(Unit unit)
    {
        print("Unit died " + unit.name);
    }

    // Actions

    public void UseActionButton(string actionName)
    {
        if(activeUnit.GetAction(actionName).CanBeUsed())
            activeUnit.ActivateAction(actionName);
    }

    // References

    public Unit GetActiveUnit()
    {
        return activeUnit;
    }

    public List<Unit> GetCombatUnits()
    {
        return combatInfo.combatUnits;
    }
}
