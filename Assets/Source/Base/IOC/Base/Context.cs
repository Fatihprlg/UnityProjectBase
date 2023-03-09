using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Context : MonoBehaviour, IContext, IInitializable
{
    [SerializeField] protected Builder mainBuilder;
    protected Container container;
    public void Initialize()
    {
        container = new Container();
        if (mainBuilder == null) CreateBuilder();
        mainBuilder.Build(container);
        
        IOCExtensions.SetDependencyInjector(container);
    }

    public void CreateBuilder()
    {
        mainBuilder = FindObjectOfType<Builder>();
        if(mainBuilder != null) return;
        Debug.LogError("There is no builder in scene. Creating temporary instance.");
        var gObj = new GameObject("TemporaryBuilder", typeof(Builder));
        mainBuilder = gObj.GetComponent<Builder>();
    }
}