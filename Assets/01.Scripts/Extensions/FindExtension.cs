using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class FindExtension 
{
    public static T FindChild<T>(this Transform transform, string name) where T : Component
    {
        T[] children = transform.GetComponentsInChildren<T>(includeInactive: true);
        foreach (T child in children)
        {
            if (child.gameObject.name == name)
            {
                return child;
            }
        }
        return null;
    }
}
