using UnityEngine;
using System.Collections;
using System;

public static class Utilities {

    public static Vector3 GetMouseWorldLocation(int layer = 31, bool allLayers = true)
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        LayerMask layerMask;

        layerMask = 1 << layer;

        if (allLayers)
        {
            layerMask = ~layerMask;
        }

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
            return hit.point;
        else
            return Vector3.zero;
    }

    public static GameObject GetMouseHitObject(int layer = 31, bool allLayers = true)
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        LayerMask layerMask;

        layerMask = 1 << layer;

        if (allLayers)
        {
            layerMask = ~layerMask;
        }

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
            return hit.collider.gameObject;
        else
            return null;
    }

    public static GameObject[] GetAllMouseHitObjects()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit[] hits;


        hits = Physics.RaycastAll(ray, Mathf.Infinity);

        GameObject[] hitObjects = new GameObject[hits.Length];
        for (int i = 0; i < hits.Length; i++)
		{
            hitObjects[i] = hits[i].collider.gameObject;
		}

        return hitObjects;
    }

    public static bool InRange(int numberToCheck, int bottom, int top)
    {
        return (numberToCheck > bottom && numberToCheck < top);
    }

    public static bool InRange(float numberToCheck, float bottom, float top)
    {
        return (numberToCheck > bottom && numberToCheck < top);
    }

    public static string GenerateID()
    {
        return Guid.NewGuid().ToString("N");
    }

    public static bool IsSameOrSubclass(Type potentialBase, Type potentialDescendant)
    {
        return potentialDescendant.IsSubclassOf(potentialBase)
               || potentialDescendant == potentialBase;
    }
}
