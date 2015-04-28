﻿using UnityEngine;
using System.Collections;
using System;

public class PrefabFactory : MonoBehaviour
{
    public static GameObject Instantiate(string pType)
    {
        return Instantiate(pType, null);
    }

    public static GameObject Instantiate(Type pType)
    {
        return Instantiate(pType, null);
    }

    public static GameObject Instantiate(Type pType, MonoBehaviour pParent)
    {
        return Instantiate(pType.Name, pParent);
    }

    public static GameObject Instantiate(Type pType, Vector2 pPosition)
    {
        return Instantiate(pType.Name, pPosition);
    }

    public static GameObject Instantiate(string pType, Vector2 pPosition)
    {
        return Instantiate(pType, null, pPosition);
    }

    public static GameObject Instantiate(string pType, MonoBehaviour pParent)
    {
        var prefab = Instantiate(Resources.Load(pType)) as GameObject;
        SetParent(prefab, pParent);
        return prefab;
    }

    public static GameObject Instantiate(string pType, MonoBehaviour pParent, Vector2 pPosition)
    {
        var prefab = Instantiate(Resources.Load(pType), pPosition, Quaternion.identity) as GameObject;
        SetParent(prefab, pParent);
        return prefab;
    }

    private static void SetParent(GameObject pObject, MonoBehaviour pParent)
    {
        if (pParent != null)
        {
            pObject.transform.parent = pParent.transform;
        }
    }

}