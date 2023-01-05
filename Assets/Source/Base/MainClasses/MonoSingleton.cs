using System;
using UnityEngine;

/// <summary>
/// Mono singleton Class. Extend this class to make singleton component.
/// Example: 
/// <code>
/// public class Foo : MonoSingleton<Foo>
/// </code>. To get the instance of Foo class, use <code>Foo.instance</code>
/// Override <code>Init()</code> method instead of using <code>Awake()</code>
/// from this class.
/// </summary>
public abstract class MonoSingleton<T> : MonoBase where T : MonoSingleton<T>
{
    public static bool IsTemporaryInstance { private set; get; }
    protected bool destroyGameObjectOnDuplicate;
    protected bool destroyed;
    protected static bool IsInitialized { get; set; }
    protected static bool isShuttingDown = false;
    protected static T MInstance = null;

    public static T Instance
    {
        get
        {
            // Instance requiered for the first time, we look for it
            if (MInstance == null)
            {
                MInstance = GameObject.FindObjectOfType(typeof(T)) as T;

                // Object not found, we create a temporary one
                if (MInstance == null)
                {
                    Debug.LogWarning("No instance of " + typeof(T).ToString() + ", a temporary one is created.");

                    IsTemporaryInstance = true;
                    MInstance = new GameObject("Temp Instance of " + typeof(T).ToString(), typeof(T)).GetComponent<T>();

                    // Problem during the creation, this should not happen
                    if (MInstance == null)
                    {
                        Debug.LogError("Problem during the creation of " + typeof(T).ToString());
                    }
                }

                if (!IsInitialized)
                {
                    IsInitialized = true;
                    MInstance.Initialize();
                }
            }

            return MInstance;
        }
    }

    // If no other monobehaviour request the instance in an awake function
    // executing before this one, no need to search the object.
    public override void Initialize()
    {
        if (MInstance == null)
        {
            MInstance = this as T;
        }
        else if (MInstance != this)
        {
            Debug.LogWarning("Another instance of " + GetType() + " is already exist! Destroying self...");
            DestroyImmediate(destroyGameObjectOnDuplicate ? this.gameObject : this);
            destroyed = true;
            return;
        }

        if (!IsInitialized)
        {
            IsInitialized = true;
            MInstance.Initialize();
        }
    }

    /// Make sure the instance isn't referenced anymore when the user quit, just in case.
    protected virtual void OnApplicationQuit()
    {
        isShuttingDown = true;
        MInstance = null;
    }

    protected virtual void OnDestroy()
    {
        isShuttingDown = true;
        DeInit();
    }

    public static T Instantiate(bool dontDestroyOnLoad = false)
    {
        if (dontDestroyOnLoad)
        {
            T instance = Instance;
            if (instance)
                DontDestroyOnLoad(instance.gameObject);
        }

        return Instance;
    }


    protected virtual void DeInit()
    {
    }
}