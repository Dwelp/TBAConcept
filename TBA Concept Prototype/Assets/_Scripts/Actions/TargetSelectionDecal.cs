using UnityEngine;
using System.Collections;
using System;

public enum TargetSelectionType
{
    Circle,
    Cone
}

[Serializable]
public class TargetSelectionDecal {

    public TargetSelectionType targetSelectionType;
    public float decalSize;
    public bool followMouse;
    
    public TargetSelectionDecalObject CreateTSDecal(float size, Unit unit)
    {
        GameObject targetSelectionDecal = null;
        Vector3 pos;
        TargetSelectionDecalObject tsObject = null;

        switch (targetSelectionType)
        {
            case TargetSelectionType.Circle:
                targetSelectionDecal = GameObject.Instantiate(Resources.Load("MoveRangeDecal")) as GameObject;

                pos = unit.transform.position;
                pos.y = 0;

                targetSelectionDecal.transform.position = pos;
                targetSelectionDecal.transform.localScale = new Vector3(size, 1, size);
                tsObject = targetSelectionDecal.AddComponent<TargetSelectionDecalObject>();
                tsObject.targetSelectionDecal = this;
                break;

            case TargetSelectionType.Cone:
                targetSelectionDecal = GameObject.Instantiate(Resources.Load("AttackRangeDecal")) as GameObject;

                pos = unit.transform.position;
                pos.y = 0;

                targetSelectionDecal.transform.position = pos;
                targetSelectionDecal.transform.localScale = new Vector3(decalSize, 1, decalSize);
                tsObject = targetSelectionDecal.AddComponent<TargetSelectionDecalObject>();
                tsObject.targetSelectionDecal = this;
                break;
        }

        return tsObject;
    }
}
