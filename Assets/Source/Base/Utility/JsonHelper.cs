using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class JsonHelper
{
    public static void SaveJson(object data, string path)
    {
        var json = JsonUtility.ToJson(data,true);
         File.WriteAllText(path ,json );
    }

    public static T LoadJson<T>(string jsonString)
    {
        T json = JsonUtility.FromJson<T>(jsonString);
        return json;
    }

    public static string ToJson(object data)
    {
        var json = JsonUtility.ToJson(data);
        return json;
    }

}
