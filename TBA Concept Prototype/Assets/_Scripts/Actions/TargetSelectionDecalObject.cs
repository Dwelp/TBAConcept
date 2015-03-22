using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class TargetSelectionDecalObject : MonoBehaviour {

    public TargetSelectionDecal targetSelectionDecal;
    public GameObject GFXSlot;

    List<Unit> registeredUnits;

    public List<Unit> RegisteredUnits
    {
        get { return registeredUnits; }
    }

    public void Awake()
    {
        if(transform.FindChild("GFXSlot") != null)
            GFXSlot = transform.FindChild("GFXSlot").gameObject;

        registeredUnits = new List<Unit>();

        Decal decal = GetComponentInChildren<Decal>();
        if (decal != null)
        {
            Vector3 posOffset = decal.transform.position - transform.position;
            Vector3 newPos = transform.position;
            newPos.y -= transform.localScale.y - (decal.transform.localScale.y / 2);
            decal.transform.position = newPos + posOffset;
        }
    }

    public void Update()
    {

    }

    public void OnCollisionEnter(Collision collision)
    {
    }

    public void OnCollisionExit(Collision collision)
    {
    }

    public void OnTriggerEnter(Collider other)
    {
        UnitCollision unitCollision = other.GetComponent<UnitCollision>();

        if (unitCollision != null)
        {
            Unit unit = other.GetComponent<UnitCollision>().unit;
            if (registeredUnits.Find(p => p.GetInstanceID() == unit.GetInstanceID()) == null)
            {
                registeredUnits.Add(unit);
            }
        }
    }

    public void OnTriggerExit(Collider other)
    {
        UnitCollision unitCollision = other.GetComponent<UnitCollision>();

        if (unitCollision != null)
        {
            Unit unit = other.GetComponent<UnitCollision>().unit;
            if (registeredUnits.Find(p => p.GetInstanceID() == unit.GetInstanceID()) != null)
            {
                registeredUnits.Remove(unit);
            }
        }
    }

    public void VibrateCollision()
    {

    }
}
