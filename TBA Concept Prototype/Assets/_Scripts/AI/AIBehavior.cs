using UnityEngine;
using System.Collections;

public class AIBehavior : MonoBehaviour {

    protected virtual void Awake()
    {

    }

	// Use this for initialization
	protected virtual void Start () 
    {
	
	}
	
	// Update is called once per frame
    protected virtual void Update()
    {
	
	}

    public virtual void ProcessTurn()
    {
        StartCoroutine(CProcessTurn());
    }

    protected virtual IEnumerator CProcessTurn()
    {
        yield return null;
    }
}
