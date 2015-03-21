using UnityEngine;
using System.Collections;

public class UA_Attack1 : UA_Skill {

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
        if (actionState == ActionState.InProgress)
        {
            if (actionOwner.GetNavAgent().remainingDistance < 0.5f)
            {
                EndAction();
            }
        }
    }

    public override void ActivateAction(Vector3 targetPos)
    {
        DoAction();
    }

    protected override void DoAction()
    {
        UpdateActionOwner();

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
