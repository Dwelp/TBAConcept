using UnityEngine;
using System.Collections;

public class UA_Skill : UnitAction {

    public GameObject skillGFX;
    public ActionEffect actionEffect;

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
        base.EndAction();
    }

    public override float GetTSDecalSize()
    {
        UpdateActionOwner();

        return targetSelectionDecal.decalSize;
    }

    public override TargetSelectionDecalObject CrateTSDecal()
    {
        return base.CrateTSDecal();
    }

    public override bool CanBeUsed()
    {
        UpdateActionOwner();

        if (actionOwner == null)
        {
            return false;
        }

        if (CanCombo())
        {
            if (actionOwner.comboStep == ComboStepRequired() || actionOwner.defaultComboStep == ComboStepRequired())
                return true;
        }

        return false;
    }
}
