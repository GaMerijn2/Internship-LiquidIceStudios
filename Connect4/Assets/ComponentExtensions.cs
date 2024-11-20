using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ComponentExtensions
{
    public static T AddComponent<T>(this Component component) where T : Component
    {
        return component.gameObject.AddComponent<T>();
    }
    
    public static T GetOrAddComponent<T>(this Component component) where T : Component
    {
        if (component.HasComponent<T>()) { return component.GetComponent<T>(); }
        else {return component.AddComponent<T>();}
    }
    
    public static bool HasComponent<T>(this Component component) where T : Component
    {
        return component.GetComponent<T>() != null;
    }
}
