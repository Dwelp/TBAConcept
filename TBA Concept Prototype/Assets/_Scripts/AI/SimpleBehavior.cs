using UnityEngine;
using System.Collections;

public class SimpleBehavior : AIBehavior {

    protected override void Awake()
    {
        base.Awake();
    }

    // Use this for initialization
    protected override void Start()
    {

    }

    // Update is called once per frame
    protected override void Update()
    {

    }

    public override void ProcessTurn()
    {
        base.ProcessTurn();
    }

    protected override IEnumerator CProcessTurn()
    {
        print("AI Behavior is being handled");

        UnitAction activeAction;
        activeAction = unit.ActivateAction("Move");

        UA_Skill attackAction = unit.GetAction("Attack1") as UA_Skill;
        float maxDistanceFromTarget = 2.5f;
        float moveOffset = Random.Range(maxDistanceFromTarget, attackAction.maxRange);

        Vector3 heading = activeTarget.transform.position - unit.transform.position;
        Vector3 dir = heading / heading.magnitude;
        float distance = Vector3.Distance(activeTarget.transform.position, unit.transform.position);

        Vector3 movePoint;
        if (distance - moveOffset > unit.moveRange)
            movePoint = unit.transform.position + dir * unit.moveRange;
        else
            movePoint = unit.transform.position + dir * (distance - moveOffset);

        movePoint.y = transform.position.y;
        DebugX.DrawPoint(movePoint, Color.red, 0.5f, 1.0f);

        yield return new WaitForSeconds(2);

        UnitAction action = unit.GetActiveAction();
        if(action == null)
        {
            print("AI Action is null");
        }
        else
            action.ValidateTarget(movePoint);
        yield return StartCoroutine(WaitForAction(action));

        if (attackAction.IsTargetInRange(activeTarget.gameObject))
        {
            activeAction = unit.ActivateAction("Attack1");
            attackAction.targetSelectionDecal.AimAt(activeTarget.transform.position);
            yield return new WaitForSeconds(1);
            activeAction.ValidateTarget(Vector3.zero);
            yield return StartCoroutine(WaitForAction(activeAction));
        }

        yield return new WaitForSeconds(0.5f);
        AIManager.Instance.EndAITurn();
        yield return null;
    }

    protected override IEnumerator WaitForAction(UnitAction action)
    {
        waitingForAction = true;
        pendingAction = action;

        while (waitingForAction)
        {
            //print("waiting for action...");
            yield return 0;
        }
    }

    public override void ActionFinished(UnitAction action)
    {
        if(action == pendingAction)
            waitingForAction = false;
    }
}
