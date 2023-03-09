using System.Collections.Generic;
using UnityEngine;

public class Initializer : MonoBehaviour
{
    [SerializeField] private bool initializeOnAwake = true;
    [SerializeField] private GameObject[] initializableObjects;

    private List<IInitializable> initializables;

    private void Awake()
    {
        if (initializeOnAwake)
            Initialize();
    }

    private void Initialize()
    {
        if (initializables == null || initializables.Count < 1) initializables = new List<IInitializable>();
        foreach (GameObject initializableObj in initializableObjects)
        {
            var initables = initializableObj.GetComponents(typeof(IInitializable));
            if (initables.Length < 1)
            {
                Debug.Log(
                    $"GameObject {initializableObj.name} is can not initialize because have not a component derived from IInitializable interface.",
                    initializableObj);
                continue;
            }

            initializables.Add(initables[0] as IInitializable);
        }

        foreach (var item in initializables)
        {
            item.Initialize();
        }
    }

    public void InsertInitializableObject(GameObject instantiatedObj)
    {
        initializableObjects = initializableObjects.Insert(0, instantiatedObj);
    }
}