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

public class UnitAction : MonoBehaviour {

    public string actionName;
    protected Unit actionOwner;

    protected ActionState actionState;
    public TargetSelectionDecal targetSelectionDecal;

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

    }    

    protected virtual void EndAction()
    {

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
        return null;
    }
}
