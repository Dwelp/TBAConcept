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
}
