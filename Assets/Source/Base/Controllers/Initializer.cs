using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Initializer : MonoBehaviour
{
    [SerializeField] IInitializable[] sceneItems;
    [SerializeField] bool initializeOnAwake;
    private void Awake()
    {
        if (initializeOnAwake)
            Initialize();
    }

    public void Initialize()
    {
        foreach (var item in sceneItems)
        {
            item.Initialize();
        }
    }
}
