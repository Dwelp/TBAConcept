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

        unit.ActivateAction("Move");

        Vector3 heading = activeTarget.transform.position - unit.transform.position;
        Vector3 dir = heading / heading.magnitude;
        float distance = Vector3.Distance(activeTarget.transform.position, unit.transform.position);

        Vector3 movePoint;
        if(distance - 3f > unit.moveRange)
            movePoint = unit.transform.position + dir * unit.moveRange;
        else
            movePoint = unit.transform.position + dir * (distance - 3f);

        DebugX.DrawPoint(movePoint, Color.red, 0.5f, 1.0f);

        yield return new WaitForSeconds(2);

        UnitAction action = unit.GetActiveAction();
        if(action != null)
            action.ValidateTarget(movePoint);

        waitingForAction = true;
        yield return StartCoroutine(WaitForAction());

        AIManager.Instance.EndAITurn();
        yield return null;
    }

    protected override IEnumerator WaitForAction()
    {
        while (waitingForAction)
        {
            //print("waiting for action...");
            yield return 0;
        }
    }

    public override void ActionFinished(UnitAction action)
    {
        waitingForAction = false;
    }
}
