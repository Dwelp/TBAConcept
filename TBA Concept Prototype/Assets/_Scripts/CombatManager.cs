using UnityEngine;
using System.Collections;
using System.Linq;

public class CombatManager : Manager<CombatManager> {

    public enum TurnController
    {
        None,
        Player,
        AI
    }

    TurnController turnController;
    CombatInfo combatInfo;

    void Awake()
    {
        
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void InitCombat()
    {
        combatInfo = new CombatInfo();

        combatInfo.combatUnits = (GameObject.FindObjectsOfType<Unit>()).ToList();
        print("found " + combatInfo.combatUnits.Count + " units");
    }

    void StarTurn(Unit unit)
    {

    }

    void EndTurn(Unit unit)
    {

    }
}
