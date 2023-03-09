using UnityEngine;

[global::System.Serializable]
public class AssemblyField : ISerializationCallbackReceiver
{
#if UNITY_EDITOR
    public UnityEditorInternal.AssemblyDefinitionAsset assemblyAsset;
#endif

#pragma warning disable 414
    [SerializeField, HideInInspector] private string assemblyName = "";
#pragma warning restore 414

    public static implicit operator string(AssemblyField assemblyField)
    {
#if UNITY_EDITOR
        return assemblyField.assemblyAsset.name;
#else
            return assemblyField.assemblyName;
#endif
    }

    public void OnBeforeSerialize()
    {
#if UNITY_EDITOR
        assemblyName = assemblyAsset.name;
#endif
    }

    public void OnAfterDeserialize()
    {
    }
}