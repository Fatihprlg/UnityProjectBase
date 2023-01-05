using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class DependentClass : MonoBase
{
    [Dependency] private ExampleClass example1;
   // [Dependency] private ExampleSingleton singleton;
    public override void Initialize()
    {
    }

    [EditorButton]
    public void TestIt()
    {
        print(example1.text);
        example1.HelloDear("the great developer");
    }

    /*[EditorButton]
    public void SingletonIsHere()
    {
        singleton.Instance.IAmASingleton();
    }*/
}
