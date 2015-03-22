using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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

    }

    public override bool IsTargetInRange(GameObject obj)
    {
        UpdateActionOwner();

        if(Vector3.Distance(obj.transform.position, actionOwner.transform.position) <= maxRange)
            return true;
        else
            return false;
    }

    public override void ActivateAction(Vector3 targetPos)
    {
        DoAction();
    }

    protected override void DoAction()
    {
        UpdateActionOwner();

        actionState = ActionState.InProgress;
        print("Started Action: " + actionName + " on " + actionOwner);

        StartCoroutine(ProcessAction());
    }

    protected override IEnumerator ProcessAction()
    {
        GameObject obj = ParticleSystem.Instantiate(skillGFX);

        if (targetSelectionDecalObject != null)
            obj.transform.position = targetSelectionDecalObject.GFXSlot.transform.position;
                
        ParticleSystem particleSystem = obj.GetComponent<ParticleSystem>();
        particleSystem.startSize *= 2;
        particleSystem.Play();

        List<Unit> registeredTargets = targetSelectionDecalObject.RegisteredUnits;

        print("Found Targets: " + registeredTargets.Count);

        foreach(Unit target in registeredTargets)
        {
            if (target.GetInstanceID() == actionOwner.GetInstanceID())
                continue;

            target.DoDamage(actionEffect.damage);
        }

        yield return new WaitForSeconds(particleSystem.duration);

        EndAction();
        yield return null;
    }

    protected override void EndAction()
    {
        base.EndAction();
    }
}
