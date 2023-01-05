using System.Collections.Generic;
using UnityEngine;
using System.Reflection;
#if UNITY_EDITOR
using UnityEditor;


    public class ButtonView
    {
        public MethodInfo Method;
        public List<ParameterView> Parameters;

        public ButtonView(MethodInfo method)
        {
            Method = method;
            ParameterInfo[] parameters = method.GetParameters();
            Parameters = new List<ParameterView>();

            for (int i = 0; i < parameters.Length; i++)
            {
                Parameters.Add(new ParameterView(parameters[i]));
            }
        }

        public void Draw(object target)
        {
            EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);

            if (Parameters.Count > 0)
            {
                for (int i = 0; i < Parameters.Count; i++)
                {
                    Parameters[i].Draw();
                }
            }

            if (GUILayout.Button(Method.Name))
            {
                Method.Invoke(target, Parameters.GetParameterValues());
            }

            EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
        }

    }

#endif
