using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossSceneCanvas : MonoSingleton<CrossSceneCanvas>, ICrossSceneObject, IInitializable
{
    public void Initialize()
    {
        destroyGameObjectOnDuplicate = true;
        base.Init();
        if(destroyed) return;
        HandleDontDestroy();
    }

    public void HandleDontDestroy()
    {
        transform.SetParent(null);
        DontDestroyOnLoad(gameObject);
    }
}