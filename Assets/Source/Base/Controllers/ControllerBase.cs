using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerBase : MonoBase
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
}
