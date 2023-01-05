using System.Collections;
using UnityEngine;
using System.Linq;
using System.Collections.Generic;
using System.Reflection;
#if UNITY_EDITOR
using UnityEditor;


    [CanEditMultipleObjects]
    [CustomEditor(typeof(Object), editorForChildClasses: true)]
    public class BaseInspectorEditor : Editor
    {
        public List<AttributeViewModel> Views;

        private void OnEnable()
        {
            Views = new List<AttributeViewModel>
        {
            new EditorButtonView(),
            new CustomEditorView()
        };

            chechMethodAttribute();
            chechFieldAttribute(target);
        }

        private void chechMethodAttribute()
        {
            MethodInfo[] methods = target.GetType().GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly);

            for (int i = 0; i < methods.Length; i++)
            {
                for (int j = 0; j < Views.Count; j++)
                {
                    if (Views[j].GetTargetType() == TargetAttributeTypes.Method)
                    {
                        Views[j].CheckMethod(methods[i]);
                    }
                }
            }
        }

        private void chechFieldAttribute(object target)
        {
            FieldInfo[] fields = target.GetType().GetFields(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly | BindingFlags.Static | BindingFlags.NonPublic);

            for (int i = 0; i < fields.Length; i++)
            {
                for (int j = 0; j < Views.Count; j++)
                {
                    if (Views[j].GetTargetType() == TargetAttributeTypes.Field)
                    {
                        Views[j].CheckField(fields[i], target);
                    }
                }
            }
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            for (int i = 0; i < Views.Count; i++)
            {
                Views[i].Draw(target);
            }
        }

    }

#endif
