using System;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;
using System.Linq;
using System.Runtime.CompilerServices;

public class TypeData
{
    public object Instance { get; set; }

    public List<KeyValuePair<DependencyAttribute, PropertyInfo>> Properties { get; private set; }

    public List<KeyValuePair<DependencyAttribute, FieldInfo>> Fields { get; private set; }

    public bool IsSingleton { get; private set; }

    private TypeData()
    {
        this.Properties = new List<KeyValuePair<DependencyAttribute, PropertyInfo>>();
        this.Fields = new List<KeyValuePair<DependencyAttribute, FieldInfo>>();
    }

    public static TypeData Create(Type type, bool isSingleton = false, object instance = null)
    {
        var typeData = new TypeData { IsSingleton = isSingleton, Instance = instance };

        foreach (var field in type.GetFields(BindingFlags.Public | BindingFlags.Instance | BindingFlags.NonPublic))
        {
            var dependency =
                (DependencyAttribute)field.GetCustomAttributes(typeof(DependencyAttribute), true).FirstOrDefault();
            if (dependency == null) continue;
            
            typeData.Fields.Add(new KeyValuePair<DependencyAttribute, FieldInfo>(dependency, field));
        }

        foreach (var property in type.GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.NonPublic))
        {
            var dependency =
                (DependencyAttribute)property.GetCustomAttributes(typeof(DependencyAttribute), true).FirstOrDefault();
            if (dependency == null) continue;

            typeData.Properties.Add(new KeyValuePair<DependencyAttribute, PropertyInfo>(dependency, property));
        }

        return typeData;
    }
}