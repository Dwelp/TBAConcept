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
    public bool followMouse;
    float decalSize;
    TargetSelectionDecalObject tsObject;

    public float DecalSize
    {
        get { return decalSize; }
    }
    
    public TargetSelectionDecalObject CreateTSDecal(float size, Unit unit)
    {
        GameObject targetSelectionDecal = null;
        Vector3 pos;
        decalSize = size;

        switch (targetSelectionType)
        {
            case TargetSelectionType.Circle:
                targetSelectionDecal = GameObject.Instantiate(Resources.Load("MoveRangeDecal")) as GameObject;

                pos = unit.transform.position;
                pos.y = 0;

                targetSelectionDecal.transform.position = pos;
                targetSelectionDecal.transform.localScale = new Vector3(size, targetSelectionDecal.transform.localScale.y, size);
                tsObject = targetSelectionDecal.AddComponent<TargetSelectionDecalObject>();
                tsObject.targetSelectionDecal = this;
                break;

            case TargetSelectionType.Cone:
                targetSelectionDecal = GameObject.Instantiate(Resources.Load("AttackRangeDecal")) as GameObject;

                pos = unit.transform.position;
                pos.y = 0;

                targetSelectionDecal.transform.position = pos;
                targetSelectionDecal.transform.localScale = new Vector3(size, targetSelectionDecal.transform.localScale.y, size);
                tsObject = targetSelectionDecal.AddComponent<TargetSelectionDecalObject>();
                tsObject.targetSelectionDecal = this;
                break;
        }

        return tsObject;
    }

    public void AimAt(Vector3 aimTarget)
    {
        aimTarget.y = tsObject.transform.position.y;
        tsObject.transform.LookAt(aimTarget);
    }
}
