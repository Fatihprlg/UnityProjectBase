using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExampleClass : ControllerBase
{
    public string text = "Dependency successful!!!";
    [SerializeField] private bool updateInput;
    
    [SerializeField] private InputManager _inputManager;

    public override void ControllerUpdate(GameStates currentState)
    {
        if(currentState == GameStates.Game)
            _inputManager.PointerUpdate();
    }


    public void HelloDear(string name)
    {
        print("Hi " + name + "!");
    }
}
