using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Documentation/Module Description")]
[System.Serializable]
public class ModuleDescription : ScriptableObject
{
    int id;
    public string moduleName;
    [TextArea(3, 15)]
    public string shortDescription;
    [TextArea(5, 15)]
    public string fullDescription;
    public List<ModuleDescription> subPages;
    public List<Texture2D> images;
}

public static class ModuleDescriptionHelper
{
    public static List<string> SplitTextBySeperator(string txt, char seperator)
    {
        List<string> response = new List<string>();

        var strs = txt.Split(seperator);
        response.AddRange(strs);

        return response;
    }

    public static List<int> GetImageIndices(List<string> str)
    {
        List<int> indices = new List<int>();
        for (int i = 1; i < str.Count; i++)
        {
            indices.Add(Convert.ToInt32(Char.GetNumericValue(str[i][0])));
            str[i] = str[i].TrimStart(str[i][0], '}', ' ');
        }
        return indices;
    }
}
