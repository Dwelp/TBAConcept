using UnityEngine;
using System.Collections;

public enum ActionState
{
    None,
    InProgress,
    Paused,
    Stopped,
    Ended
}

public class UnitAction : MonoBehaviour, IComboStructure {

    public string actionName;
    protected Unit actionOwner;

    protected ActionState actionState;
    public TargetSelectionDecal targetSelectionDecal;
    public TargetSelectionDecalObject targetSelectionDecalObject;

    // Combo Structure
    public ComboStructure comboStructure;

    protected virtual void Awake()
    {

    }

	// Use this for initialization
    protected virtual void Start()
    {
	
	}
	
	// Update is called once per frame
    protected virtual void Update()
    {
	
	}

    public virtual void ActivateAction(Vector3 targetPos)
    {

    }

    protected virtual void DoAction()
    {
        StartCoroutine(ProcessAction());
    }    

    protected virtual IEnumerator ProcessAction()
    {
        EndAction();
        yield return null;
    }

    protected virtual void EndAction()
    {
        actionState = ActionState.None;
        print("Finished Action: " + actionName + " on " + actionOwner);

        actionOwner.ActionFinished(actionName);
    }

    public Unit UpdateActionOwner()
    {
        actionOwner = transform.parent.gameObject.GetComponent<Unit>();

        return actionOwner;
    }

    public virtual float GetTSDecalSize()
    {
        return 0;
    }

    public virtual TargetSelectionDecalObject CrateTSDecal()
    {
        UpdateActionOwner();

        targetSelectionDecalObject = targetSelectionDecal.CreateTSDecal(GetTSDecalSize(), actionOwner);
        return targetSelectionDecalObject;
    }

    public virtual bool ValidateTarget(Vector3 targetPos)
    {
        actionOwner.ValidatedActionTarget(targetPos);
        return true;
    }

    // IComboStructure Interface

    public bool CanCombo()
    {
        return comboStructure.canCombo;
    }

    public bool AllowCombo()
    {
        return comboStructure.allowCombo;
    }

    public int ComboStepRequired()
    {
        return comboStructure.comboStepRequired;
    }

    public virtual bool CanBeUsed()
    {
        return false;
    }
}
