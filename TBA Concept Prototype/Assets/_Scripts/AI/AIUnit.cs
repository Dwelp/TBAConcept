using UnityEngine;
using System.Collections;

public class AIUnit : Unit {

    public AIBehavior aiBehavior;

    protected override void Awake()
    {
        base.Awake();
    }

	// Use this for initialization
	protected override void Start () 
    {
        base.Start();
	}
	
	// Update is called once per frame
	protected override void Update () 
    {
        base.Update();
	}

    public override void ActionFinished(string actionName)
    {
        aiBehavior.ActionFinished(activeAction);

        UIManager.Instance.ExitTargetSelection();
        activeAction = null;
    }
}
