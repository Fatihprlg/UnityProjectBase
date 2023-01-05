using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Initializer : MonoBase
{
    [SerializeField] MonoBase[] sceneItems;
    [SerializeField] bool initializeOnAwake;
    private void Awake()
    {
        if (initializeOnAwake)
            Initialize();
    }

    public override void Initialize()
    {
        foreach (var item in sceneItems)
        {
            item.Initialize();
        }
    }
}
