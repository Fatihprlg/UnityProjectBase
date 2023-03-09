using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using UnityEditor;
using UnityEngine.Rendering;


[CustomEditor(typeof(PostProcessor))]
public class PostProcessorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        PostProcessor cubePostProcessor = target as PostProcessor;
        Volume volume = cubePostProcessor.volume;
        if (cubePostProcessor == null || volume == null || cubePostProcessor.Profiles == null) return;
        for (int i = 0; i < cubePostProcessor.Profiles.Count; i++)
        {
            if (cubePostProcessor.Profiles[i] != null)
                if (GUILayout.Button(cubePostProcessor.Profiles[i].name))
                {
                    cubePostProcessor.SetVolumeProfile(i);
                }
        }
        
    }
}
