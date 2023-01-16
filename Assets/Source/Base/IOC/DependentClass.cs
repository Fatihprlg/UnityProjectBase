using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class DependentClass : MonoBehaviour
{
    [Dependency] private ExampleClass example1;

   [EditorButton]
    public void TestIt()
    {
        print(example1.text);
        example1.HelloDear("the great developer");
    }
}
