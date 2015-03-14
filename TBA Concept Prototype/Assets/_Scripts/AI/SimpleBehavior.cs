using UnityEngine;
using System.Collections;

public class SimpleBehavior : AIBehavior {

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

    public override void ProcessTurn()
    {
        base.ProcessTurn();
    }

    protected override IEnumerator CProcessTurn()
    {
        print("AI Behavior is being handled");
        yield return new WaitForSeconds(3);
        AIManager.Instance.EndAITurn();
        yield return null;
    }
}
