using UnityEngine;
using System.Collections;

public class UA_Skill : UnitAction {

    // Combo Structure
    public int comboStepRequired;
    public bool allowCombo;
    public bool canCombo;

    public GameObject skillGFX;

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

        return targetSelectionDecal.decalSize;
    }

    public override TargetSelectionDecalObject CrateTSDecal()
    {
        return base.CrateTSDecal();
    }
}
