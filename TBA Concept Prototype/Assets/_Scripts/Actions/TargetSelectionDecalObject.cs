using UnityEngine;
using System.Collections;
using System;

public class TargetSelectionDecalObject : MonoBehaviour {

    public TargetSelectionDecal targetSelectionDecal;
    public GameObject GFXSlot;

    public void Awake()
    {
        if(transform.FindChild("GFXSlot") != null)
            GFXSlot = transform.FindChild("GFXSlot").gameObject;
    }
}
