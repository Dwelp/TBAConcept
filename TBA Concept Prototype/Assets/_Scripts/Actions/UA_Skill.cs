using UnityEngine;
using System.Collections;

public class UA_Skill : UnitAction {

    // Combo Structure
    public int comboStepRequired;
    public bool allowCombo;
    public bool canCombo;

    protected override void Awake()
    {

    }

    // Use this for initialization
    protected override void Start()
    {

    }

    // Update is called once per frame
    protected override void Update()
    {

    }

    protected override void DoAction()
    {

    }

    protected override void EndAction()
    {

    }

    public override float GetTSDecalSize()
    {
        UpdateActionOwner();

        return actionOwner.moveRange * 2;
    }

    public override TargetSelectionDecalObject CrateTSDecal()
    {
        UpdateActionOwner();

        return targetSelectionDecal.CreateTSDecal(GetTSDecalSize(), actionOwner);
    }
}
