using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public interface IGameService
{
}

public static class ServiceLocator
{
    
    static readonly Dictionary<Type, object> services = new();

    public static T Get<T>()
    {
        var key = typeof(T);
        if (!services.ContainsKey(key))
        {
            Debug.LogError($"{key} not registered");
            return default;
        }

        return (T)services[key];
    }

    public static bool TryGet<T>(out T result)
    {
        result = default;
        var key = typeof(T);
        if (!services.TryGetValue(key, out var r))
            return false;
        result = (T)r;
        return true;
    }

    public static void Register(object service)
    {
        Register(service.GetType(), service);
        var interfaces = service.GetType().GetInterfaces();
        foreach (var t in interfaces)
            if (typeof(IGameService).IsAssignableFrom(t) && t != typeof(IGameService))
                Register(t, service);
    }

    public static void Register(Type type, object service)
    {
        if (services.ContainsKey(type))
        {
            Debug.LogError($"Attempted to register service of type {type} which is already registered.");
            return;
        }

        services.Add(type, service);
    }

    public static void Unregister(object service)
    {
        var registeredTypes = services.Where(kvp => kvp.Value == service)
            .Select(kvp => kvp.Key).ToList();
        foreach (var t in registeredTypes)
        {
            Unregister(t);
        }
    }
    
    public static void Unregister<T>()
    {
        Unregister(typeof(T));
    }
    
    private static void Unregister(Type type)
    {
        if (!services.ContainsKey(type))
        {
            Debug.LogError($"Attempted to unregister service of type {type} which is not registered.");
            return;
        }

        services.Remove(type);
    }

    public static void Clear()
    {
        services.Clear();
    }

    public static IReadOnlyDictionary<Type, object> AllInstalledServices => services;
}
