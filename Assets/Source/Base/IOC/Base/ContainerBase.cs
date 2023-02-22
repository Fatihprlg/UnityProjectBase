using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;

[System.Serializable]
public class Container
{
    protected Dictionary<Type, Dictionary<string, GameObject>> implementations = new Dictionary<Type, Dictionary<string, GameObject>>();
    protected readonly Dictionary<Type, Dictionary<string, Type>>
    types = new Dictionary<Type, Dictionary<string, Type>>();
    protected readonly Dictionary<Type, TypeData> typeDatas = new Dictionary<Type, TypeData>();

    public virtual void Register(Type interfaceType, Type type, ClassInfo info)
    {
        try
        {
            TypeData typeData = TypeData.Create(type, info.isSingleton, type.IsSubclassOf(typeof(MonoBehaviour)) ? info.implementation : null);
            string key = "";
            object implementation = new();
            if (type.IsSubclassOf(typeof(MonoBehaviour)))
            {
                key = info.implementation.gameObject.name;
                implementation = info.implementation.gameObject;
            }
            else
            {
                implementation = ConstructClass(type);
            }
            if (this.types.ContainsKey(interfaceType ?? type))
            {
                key = RemoveSpecialCharacters(key);
                var regex = new Regex($"{key}[0-9]*$");
                
                while (types[interfaceType ?? type].ContainsKey(key ?? string.Empty)) 
                    key = $"{key} {types[interfaceType ?? type].Count(type => regex.IsMatch(RemoveSpecialCharacters(type.Key)))}";
                this.types[interfaceType ?? type].Add(key ?? string.Empty, type);
            }
            else
            {
                this.types.Add(interfaceType ?? type, new Dictionary<string, Type> { { key ?? string.Empty, type } });
            }
            if (!this.typeDatas.ContainsKey(type))
            {
                this.typeDatas.Add(type, typeData);
            }
            if (!this.implementations.ContainsKey(type))
            {
                this.implementations.Add(type, new Dictionary<string, GameObject> { { key ?? string.Empty, implementation as GameObject} });
            }
            else
            {
                this.implementations[type].Add(key ?? string.Empty, implementation as GameObject);
            }

            
        }
        catch (Exception ex)
        {
            throw new Exception("Register type failed. ", ex);
        }
    }
    public static string RemoveSpecialCharacters(string str)
    {
        return Regex.Replace(str, "[^a-zA-Z0-9_.]+", "", RegexOptions.Compiled);
    }
    public T Inject<T>(object obj)
    {
        return (T)this.Inject(typeof(T), obj);
    }

    private object ConstructClass(Type type)
    {
        var implementation = Activator.CreateInstance(type);
        return implementation;
    } 

    private static void Guard(bool failed, string format, params object[] args)
    {
        if (failed) throw new Exception(string.Format(format, args), null);
    }

    private TypeData GetTypeData(Type type)
    {
        if (!this.typeDatas.ContainsKey(type))
        {
            var typeData = TypeData.Create(type);
            this.typeDatas.Add(type, typeData);
            if (this.types.ContainsKey(type))
            {
                this.types[type].Add(string.Empty, type);
            }
            else
            {
                this.types.Add(type, new Dictionary<string, Type> { { string.Empty, type } });
            }
        }

        return this.typeDatas[type];
    }

    public object Inject(Type type, object obj)
    {
        var typeData = this.GetTypeData(type);
        
        typeData.Fields.ForEach(x => x.Value.SetValue(obj, this.Resolve(x.Value.FieldType, string.IsNullOrEmpty(x.Key.Key) ? GetImplementation(x.Value.FieldType).gameObject.name : x.Key.Key)));
        typeData.Properties.ForEach(x => x.Value.SetValue(obj, this.Resolve(x.Value.PropertyType, x.Key.Key), null));

        return obj;
    }

    public object Resolve(Type type, string key = null)
    {
        Guard(!this.types[type.BaseType].ContainsKey(key ?? string.Empty),
            "There is no implementation registered with the key {0} for the type {1}.", key, type.Name);

        var foundType = this.types[type.BaseType][key ?? string.Empty];
        var implementationOfType = this.implementations[type][key ?? string.Empty].GetComponent(type) ?? Inject(foundType, implementations[type][key ?? string.Empty].AddComponent(foundType));
        var typeData = this.typeDatas[foundType];

        if (foundType.IsSubclassOf(typeof(MonoBehaviour)))
        {
            typeData.Instance = implementationOfType;
            return implementationOfType;
        }

        if (typeData.IsSingleton)
        {
            return typeData.Instance ?? (typeData.Instance = this.Setup(foundType));
        }

        return this.Setup(foundType);
    }
    private object Setup(Type type)
    {
        var instance = Activator.CreateInstance(type);
        this.Inject(type, instance);
        return instance;
    }

    private Component GetImplementation(Type type, string key = null, GameObject sibling = null)
    {
        Guard(!this.implementations.ContainsKey(type), "There is no implementation of {0} for the {1}", type.Name, key ?? string.Empty);
        GameObject imp;
        if (string.IsNullOrEmpty(key))
        {
            imp = this.implementations[type].FirstOrDefault().Value;
        }
        else
        {
            imp = this.implementations[type][key];
        }
        var implementationOfType = imp.GetComponent(type);
        return implementationOfType;
    }

}
[System.Serializable]
public struct ClassInfo
{
    public MonoBehaviour implementation;
    public bool isSingleton;
}
