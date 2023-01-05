using UnityEngine;
using System.Reflection;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;


    public class FieldView
    {
        public int SelectedIndex;
        FieldInfo field;
        public object Value;
        bool isNonSerializeable;
        bool isEnum;
        bool showContent;
        bool isConst;

        public FieldView(FieldInfo field, object target)
        {
            this.field = field;
            Value = field.GetValue(target);
            isEnum = field.FieldType.IsEnum;
            isNonSerializeable = !field.FieldType.IsSerializable;
        }

        public void SetCanEdit(bool canEdit)
        {

        }


        public void Draw(object target, bool readOnly)
        {
            EditorGUI.BeginDisabledGroup(readOnly);

            if (isEnum)
            {
                drawEnumField(readOnly);
            }
            else if (isNonSerializeable)
            {
                drawNonSerializeField(target);
            }
            else
            {
                drawBasicField(readOnly);
            }

            if (!readOnly)
                field.SetValue(target, Value);
            
            EditorGUI.EndDisabledGroup();
        }

        private void drawNonSerializeField(object target)
        {
            showContent = EditorGUILayout.Foldout(showContent, field.Name);
            int level = EditorGUI.indentLevel;

            Debug.Log(field.GetValue(target));

            EditorGUI.indentLevel = level + 1;
            if (showContent)
            {
                FieldInfo[] fields = field.FieldType.GetFields();

                for (int i = 0; i < fields.Length; i++)
                {
                    drawNonSerializeField(fields[i]);
                }
            }

            EditorGUI.indentLevel = level;
        }

        private void drawEnumField(bool isReadonly)
        {
            string[] names = System.Enum.GetNames(field.FieldType);
            SelectedIndex = EditorGUILayout.Popup(field.Name, SelectedIndex, names);
            if (!isReadonly)
            {
                Value = System.Enum.GetValues(field.FieldType).GetValue(SelectedIndex);
            }
        }

        private void drawBasicField(bool isReadonly)
        {
            if (field.FieldType == typeof(int))
            {
                if (isReadonly)
                {
                    EditorGUILayout.IntField(field.Name, (int)Value);
                }
                else
                {
                    Value = EditorGUILayout.IntField(field.Name, (int)Value);
                }
            }
            else if (field.FieldType == typeof(float))
            {
                if (isReadonly)
                {
                    EditorGUILayout.FloatField(field.Name, (float)Value);
                }
                else
                {
                    Value = EditorGUILayout.FloatField(field.Name, (float)Value);
                }
            }
            else if (field.FieldType == typeof(double))
            {
                if (isReadonly)
                {
                    EditorGUILayout.DoubleField(field.Name, (double)Value);
                }
                else
                {
                    Value = EditorGUILayout.DoubleField(field.Name, (double)Value);
                }
            }
            else if (field.FieldType == typeof(string))
            {
                if (isReadonly)
                {
                    EditorGUILayout.TextField(field.Name, (string)Value);
                }
                else
                {
                    Value = EditorGUILayout.TextField(field.Name, (string)Value);
                }
            }
            else if (field.FieldType == typeof(bool))
            {
                if (isReadonly)
                {
                    EditorGUILayout.Toggle(field.Name, (bool)Value);
                }
                else
                {
                    Value = EditorGUILayout.Toggle(field.Name, (bool)Value);
                }
            }
            else if (field.FieldType == typeof(Vector2))
            {
                if (isReadonly)
                {
                    EditorGUILayout.Vector2Field(field.Name, (Vector2)Value);
                }
                else
                {
                    Value = EditorGUILayout.Vector2Field(field.Name, (Vector2)Value);
                }
            }
            else if (field.FieldType == typeof(Vector3))
            {
                if (isReadonly)
                {
                    EditorGUILayout.Vector3Field(field.Name, (Vector3)Value);
                }
                else
                {
                    Value = EditorGUILayout.Vector3Field(field.Name, (Vector3)Value);
                }
            }
            else if (field.FieldType == typeof(Vector4))
            {
                if (isReadonly)
                {
                    EditorGUILayout.Vector4Field(field.Name, (Vector4)Value);
                }
                else
                {
                    Value = EditorGUILayout.Vector4Field(field.Name, (Vector4)Value);
                }
            }
            else if (field.FieldType == typeof(Vector2Int))
            {
                if (isReadonly)
                {
                    EditorGUILayout.Vector2IntField(field.Name, (Vector2Int)Value);
                }
                else
                {
                    Value = EditorGUILayout.Vector2IntField(field.Name, (Vector2Int)Value);
                }
            }
            else if (field.FieldType == typeof(Vector3Int))
            {
                if (isReadonly)
                {
                    EditorGUILayout.Vector3IntField(field.Name, (Vector3Int)Value);
                }
                else
                {
                    Value = EditorGUILayout.Vector3IntField(field.Name, (Vector3Int)Value);
                }
            }
            else if (field.FieldType == typeof(Quaternion))
            {
                if (isReadonly)
                {
                    Vector3 val = ((Quaternion)Value).eulerAngles;
                    EditorGUILayout.Vector3Field(field.Name, val);
                }
                else
                {
                    Vector3 val = ((Quaternion)Value).eulerAngles;
                    val = EditorGUILayout.Vector3Field(field.Name, val);
                    Value = Quaternion.Euler(val);
                }
            }
            else if (field.FieldType == typeof(Color))
            {
                if (isReadonly)
                {
                    EditorGUILayout.ColorField(field.Name, (Color)Value);
                }
                else
                {
                    Value = EditorGUILayout.ColorField(field.Name, (Color)Value);
                }
            }
            else
            {
                if (isReadonly)
                {
                    EditorGUILayout.ObjectField(label: field.Name, obj: (Object)Value, field.FieldType, true);
                }
                else
                {
                    Value = EditorGUILayout.ObjectField(label: field.Name, obj: (Object)Value, field.FieldType, true);
                }
            }
        }

        private void drawNonSerializeField(FieldInfo targetField)
        {
            if (targetField.FieldType == typeof(int))
            {
                EditorGUILayout.IntField(targetField.Name, default(int));
            }
            else if (targetField.FieldType == typeof(float))
            {
                EditorGUILayout.FloatField(targetField.Name, default(float));
            }
            else if (targetField.FieldType == typeof(double))
            {
                EditorGUILayout.DoubleField(targetField.Name, default(float));
            }
            else if (targetField.FieldType == typeof(string))
            {
                EditorGUILayout.TextField(targetField.Name, default(string));
            }
            else if (targetField.FieldType == typeof(bool))
            {
                EditorGUILayout.Toggle(targetField.Name, default(bool));
            }
            else if (targetField.FieldType == typeof(Vector2))
            {
                EditorGUILayout.Vector2Field(targetField.Name, (Vector2)Value);
            }
            else if (targetField.FieldType == typeof(Vector3))
            {
                EditorGUILayout.Vector3Field(targetField.Name, (Vector3)Value);
            }
            else if (targetField.FieldType == typeof(Vector4))
            {
                EditorGUILayout.Vector4Field(targetField.Name, (Vector4)Value);
            }
            else if (targetField.FieldType == typeof(Vector2Int))
            {
                EditorGUILayout.Vector2IntField(targetField.Name, (Vector2Int)Value);
            }
            else if (targetField.FieldType == typeof(Vector3Int))
            {
                EditorGUILayout.Vector3IntField(targetField.Name, (Vector3Int)Value);
            }
            else if (targetField.FieldType == typeof(Quaternion))
            {
                EditorGUILayout.Vector3Field(targetField.Name, Vector3.zero);
            }
            else if (targetField.FieldType == typeof(Color))
            {
                EditorGUILayout.ColorField(field.Name, (Color)Value);
            }
            else
            {
                EditorGUILayout.ObjectField(label: field.Name, obj: (Object)Value, field.FieldType, true);
            }
        }
    }


#endif