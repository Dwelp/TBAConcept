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
        if (Input.GetMouseButtonUp(0))
        {
            Vector3 pos = Utilities.GetMouseWorldLocation();
            DebugX.DrawPoint(pos, Color.red, 0.5f, 2.0f);

            Unit[] units = GameObject.FindObjectsOfType<Unit>();
            Unit[] playerUnits = units.Where(p => p.unitOwner == UnitOwner.Player).ToArray();
            playerUnits[0].GetNavAgent().SetDestination(pos);
        }
	}
}
