using UnityEngine;
using System.Reflection;
#if UNITY_EDITOR
using UnityEditor;


    public class ParameterView
    {
        public int SelectedIndex;
        ParameterInfo Parameter;
        public object Value;
        bool isEnum;

        public ParameterView(ParameterInfo parameter)
        {
            Parameter = parameter;
            Value = default(int);

            if (Parameter.ParameterType.IsEnum)
            {
                isEnum = true;
                Value = System.Enum.GetValues(Parameter.ParameterType).GetValue(SelectedIndex);
            }
            else
            {
                if (Parameter.ParameterType == typeof(int))
                {
                    Value = default(int);
                }
                else if (Parameter.ParameterType == typeof(float))
                {
                    Value = default(float);
                }
                else if (Parameter.ParameterType == typeof(double))
                {
                    Value = default(double);
                }
                else if (Parameter.ParameterType == typeof(string))
                {
                    Value = default(string);
                }
                else if (Parameter.ParameterType == typeof(bool))
                {
                    Value = default(bool);
                }
                else if (Parameter.ParameterType == typeof(Vector2))
                {
                    Value = default(Vector2);
                }
                else if (Parameter.ParameterType == typeof(Vector3))
                {
                    Value = default(Vector3);
                }
                else if (Parameter.ParameterType == typeof(Vector4))
                {
                    Value = default(Vector4);
                }
                else if (Parameter.ParameterType == typeof(Vector2Int))
                {
                    Value = default(Vector2Int);
                }
                else if (Parameter.ParameterType == typeof(Vector3Int))
                {
                    Value = default(Vector3Int);
                }
                else if (Parameter.ParameterType == typeof(Quaternion))
                {
                    Value = default(Quaternion);
                }
                else if (Parameter.ParameterType == typeof(Color))
                {
                    Value = default(Color);
                }
                else
                {
                    Value = default(Object);
                }
            }
        }


        public void Draw()
        {
            if (isEnum)
            {
                string[] names = System.Enum.GetNames(Parameter.ParameterType);
                SelectedIndex = EditorGUILayout.Popup(Parameter.Name, SelectedIndex, names);
                Value = System.Enum.GetValues(Parameter.ParameterType).GetValue(SelectedIndex);
            }
            else
            {
                if (Parameter.ParameterType == typeof(int))
                {
                    Value = EditorGUILayout.IntField(Parameter.Name, (int)Value);
                }
                else if (Parameter.ParameterType == typeof(float))
                {
                    Value = EditorGUILayout.FloatField(Parameter.Name, (float)Value);
                }
                else if (Parameter.ParameterType == typeof(double))
                {
                    Value = EditorGUILayout.DoubleField(Parameter.Name, (double)Value);
                }
                else if (Parameter.ParameterType == typeof(string))
                {
                    Value = EditorGUILayout.TextField(Parameter.Name, (string)Value);
                }
                else if (Parameter.ParameterType == typeof(bool))
                {
                    Value = EditorGUILayout.Toggle(Parameter.Name, (bool)Value);
                }
                else if (Parameter.ParameterType == typeof(Vector2))
                {
                    Value = EditorGUILayout.Vector2Field(Parameter.Name, (Vector2)Value);
                }
                else if (Parameter.ParameterType == typeof(Vector3))
                {
                    Value = EditorGUILayout.Vector3Field(Parameter.Name, (Vector3)Value);
                }
                else if (Parameter.ParameterType == typeof(Vector4))
                {
                    Value = EditorGUILayout.Vector4Field(Parameter.Name, (Vector4)Value);
                }
                else if (Parameter.ParameterType == typeof(Vector2Int))
                {
                    Value = EditorGUILayout.Vector2IntField(Parameter.Name, (Vector2Int)Value);
                }
                else if (Parameter.ParameterType == typeof(Vector3Int))
                {
                    Value = EditorGUILayout.Vector3IntField(Parameter.Name, (Vector3Int)Value);
                }
                else if (Parameter.ParameterType == typeof(Quaternion))
                {
                    Vector3 val = ((Quaternion)Value).eulerAngles;
                    val = EditorGUILayout.Vector3Field(Parameter.Name, val);
                    Value = Quaternion.Euler(val);
                }
                else if (Parameter.ParameterType == typeof(Color))
                {
                    Value = EditorGUILayout.ColorField(Parameter.Name, (Color)Value);
                }
                else
                {
                    Value = EditorGUILayout.ObjectField(label: Parameter.Name, obj: (Object)Value, Parameter.ParameterType, true);
                }
            }
        }
    }

#endif
