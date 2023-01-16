using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerBase : MonoBehaviour, IInitializable
{
    public virtual void ControllerUpdate(GameStates currentState)
    {

    }
    public virtual void ControllerFixedUpdate(GameStates currentState)
    {

    }
    public virtual void ControllerLateUpdate(GameStates currentState)
    {

    }

    public virtual void OnStateChanged(GameStates state)
    {

    }

    public virtual void Initialize()
    {
        
    }
}
