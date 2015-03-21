using UnityEngine;
using System.Collections;

public class UA_Move : UnitAction {

    Vector3 movePos;

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
        if(actionState == ActionState.InProgress)
        {
            if(actionOwner.GetNavAgent().remainingDistance < 0.5f)
            {
                EndAction();
            }
        }
    }

    public override void ActivateAction(Vector3 targetPos)
    {
        movePos = targetPos;
        DoAction();
    }

    protected override void DoAction()
    {
        UpdateActionOwner();

        DebugX.DrawPoint(movePos, Color.red, 0.5f, 1.0f);
        actionOwner.GetNavAgent().SetDestination(movePos);

        actionState = ActionState.InProgress;
        print("move action STARTED on " + actionOwner);
    }

    protected override void EndAction()
    {
        actionState = ActionState.None;
        print("move action ENDED on " + actionOwner);

        actionOwner.ActionFinished();
    }

    public override float GetTSDecalSize()
    {
        UpdateActionOwner();

        return actionOwner.moveRange * 2;
    }

    public override TargetSelectionDecalObject CrateTSDecal()
    {
        return base.CrateTSDecal();
    }

    public override bool ValidateTarget(Vector3 targetPos)
    {
        if (Vector3.Distance(transform.position, targetPos) <= actionOwner.moveRange)
        {
            actionOwner.ValidatedMoveLocation(targetPos);
            return true;
        }
        else
        {
            //print("OUT OF RANGE!");
            return false;
        }
    }
}
