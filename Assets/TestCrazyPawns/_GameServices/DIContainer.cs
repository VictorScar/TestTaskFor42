using System;
using System.Collections.Generic;
using UnityEngine;

public class DIContainer : MonoBehaviour
{
    private Dictionary<Type, MonoBehaviour> _registeredComponents = new Dictionary<Type, MonoBehaviour>();
    public static DIContainer I { get; private set; }

    public void Init()
    {
        if (I == null)
        {
            I = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void RegisterComponent<T>(T component) where T : MonoBehaviour
    {
        var type = typeof(T);
        
        if (!_registeredComponents.ContainsKey(type))
        {
            _registeredComponents.Add(type, component);
        }
        else
        {
            Debug.LogWarning($"Component of type {type} is already registered.");
        }
    }

    public T GetInstance<T>() where T : MonoBehaviour
    {
        var type = typeof(T);
        
        if (_registeredComponents.TryGetValue(type, out MonoBehaviour component))
        {
            return component as T;
        }
        Debug.LogError($"No component of type {type} is registered.");
        return null;
    }

    public void UnregisterComponent<T>() where T : MonoBehaviour
    {
        var type = typeof(T);
        
        if (_registeredComponents.ContainsKey(type))
        {
            _registeredComponents.Remove(type);
        }
        else
        {
            Debug.LogWarning($"No component of type {type} is registered.");
        }
    }
}