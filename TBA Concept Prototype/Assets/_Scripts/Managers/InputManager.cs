using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class InputManager : Manager<InputManager> {

    void Awake()
    {

    }

	// Use this for initialization
	void Start () 
    {
	
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 pos = Utilities.GetMouseWorldLocation();
            //print(Utilities.GetMouseHitObject().name);
            //DebugX.DrawPoint(pos, Color.red, 0.5f, 1.0f);

            //Unit[] units = GameObject.FindObjectsOfType<Unit>();
            //Unit[] playerUnits = units.Where(p => p.unitOwner == UnitOwner.Player).ToArray();
            //playerUnits[0].GetNavAgent().SetDestination(pos);

            if (CombatManager.Instance.turnController == CombatManager.TurnController.Player)
            {
                Unit unit = CombatManager.Instance.GetActiveUnit();

                if (unit.unitState == UnitState.TargetSelection)
                {
                    UnitAction action = unit.GetActiveAction();
                    if (action != null)
                        action.ValidateTarget(pos);
                }
            }
        }

        if (Input.GetMouseButtonUp(1))
        {
            if(CombatManager.Instance.turnController == CombatManager.TurnController.Player)
            {
                Unit unit = CombatManager.Instance.GetActiveUnit();

                if(unit.unitState == UnitState.TargetSelection)
                {
                    unit.DeactivateAction();
                }
            }
        }

        // Keyboard Hotkeys

        if(Input.GetKeyUp(KeyCode.M))
        {
            UIManager.Instance.OnClickButton("MoveBtn");
        }

        if (Input.GetKeyUp(KeyCode.Alpha1))
        {
            UIManager.Instance.OnClickButton("AttackBtn11");
        }

        if (Input.GetKeyUp(KeyCode.Alpha2))
        {
            UIManager.Instance.OnClickButton("AttackBtn21");
        }

        if (Input.GetKeyUp(KeyCode.Alpha3))
        {
            UIManager.Instance.OnClickButton("AttackBtn31");
        }
	}
}
