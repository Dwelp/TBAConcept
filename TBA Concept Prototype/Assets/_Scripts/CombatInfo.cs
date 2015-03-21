using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class CombatInfo {

    public List<Unit> combatUnits;

    public CombatInfo()
    {
        combatUnits = new List<Unit>();
    }    

    public bool RemoveUnit(Unit unit)
    {
        if (combatUnits.Find(p => p.GetInstanceID() == unit.GetInstanceID()))
        {
            combatUnits.Remove(unit);
            return true;
        }
        else
            return false;
    }
}
