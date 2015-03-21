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

        yield return new WaitForSeconds(particleSystem.duration);

        EndAction();
        yield return null;
    }

    protected override void EndAction()
    {
        actionState = ActionState.None;
        print("Finished Action: " + actionName + " on " + actionOwner);

        actionOwner.ActionFinished();
    }
}
