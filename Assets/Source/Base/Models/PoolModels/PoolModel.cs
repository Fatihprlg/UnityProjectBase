using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class PoolModel : MonoBehaviour
{
    [SerializeField] List<GameObject> items;

    public virtual T GetDeactiveItem<T>()
    {
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i].gameObject.activeInHierarchy == false)
            {
                return (T)((object)items[i].GetComponent<T>());
            }
        }

        return default(T);
    }
    
    public void ResetPool()
    {
        foreach (var item in items)
        {
            item.SetActive(false);
        }
    }

    private void getItemsFromChildren()
    {
        if (items == null)
            items = new List<GameObject>();

        for (int i = 0; i < transform.childCount; i++)
        {
            GameObject item = transform.GetChild(i).gameObject;
            if (item != null)
            {
                item.gameObject.SetActive(false);
                items.Add(item);
            }
        }
    }

    [EditorButton]
    public void InitializeOnEditor()
    {
#if UNITY_EDITOR
        Undo.RecordObject(this, "GetItems");
        if (items != null)
            items.Clear();

        getItemsFromChildren();
#endif
    }

    private void Reset()
    {
        transform.ResetLocal();
    }
}
