using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public abstract class ItemModel<T> : MonoBehaviour
{
    public int id;
    public abstract void SetValues(T data);
    public abstract T GetData();
}
[System.Serializable]
public class ItemDataModel
{
    public int Id;
}
