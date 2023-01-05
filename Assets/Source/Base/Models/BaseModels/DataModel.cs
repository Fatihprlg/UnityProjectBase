using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

[Serializable]
public class DataModel : NonMonoBase
{
    protected virtual void Save(object data)
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = new FileStream(Application.persistentDataPath + "/" + GetType().Name + ".dat",
            FileMode.Create, FileAccess.Write, FileShare.None);
        bf.Serialize(file, data);
        file.Close();
        file.Dispose();
    }

    public virtual void Delete()
    {
        if (File.Exists(Application.persistentDataPath + "/" + GetType().Name + ".dat"))
        {
            File.Delete(Application.persistentDataPath + "/" + GetType().Name + ".dat");
        }
    }

    public bool DataExists()
    {
        return File.Exists(Application.persistentDataPath + "/" + GetType().Name + ".dat");
    }

    protected object LoadData()
    {
        string path = Application.persistentDataPath + "/" + GetType().Name + ".dat";
        if (File.Exists(path))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = new FileStream(Application.persistentDataPath + "/" + GetType().Name + ".dat",
                FileMode.Open, FileAccess.Read, FileShare.None);
            if (!(file.CanSeek && file.Length == 0L))
            {
                object data = bf.Deserialize(file);
                file.Close();
                file.Dispose();
                return data;
            }
            else return null;
        }
        else
        {
            return null;
        }
    }
}