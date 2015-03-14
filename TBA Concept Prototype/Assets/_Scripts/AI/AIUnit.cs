using UnityEngine;
using System.Collections;

public class AIUnit : Unit {

    public AIBehavior aiBehavior;

    protected override void Awake()
    {
        canvas = transform.FindChild("Canvas").GetComponent<Canvas>();
        canvas.enabled = false;

        initDone = true;
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
}
