using System;

[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
public class DependencyAttribute : Attribute
{
    public string Key { get; set; }

    public DependencyAttribute()
    {
        
    }
    public DependencyAttribute(string key)
    {
        this.Key = key;
    }
}