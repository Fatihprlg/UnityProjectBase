using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class MultiplePoolModel : MonoBehaviour
{
    [SerializeField] private List<PoolModel> pools;

    public T GetDeactiveItem<T>(int poolIndex)
    {
        return pools[poolIndex].GetDeactiveItem<T>();
    }

    public T GetRandomPoolItem<T>()
    {
        return pools.GetRandom().GetDeactiveItem<T>();
    }

    [EditorButton]
    public void GetPools()
    {
        if (pools != null)
            pools.Clear();
        else
            pools = new List<PoolModel>();

        for (int i = 0; i < transform.childCount; i++)
        {
            PoolModel pool = transform.GetChild(i).GetComponent<PoolModel>();
            if (pool != null)
            {
                pools.Add(pool);
            }
        }
    }

    private void Reset()
    {
        transform.ResetLocal();
    }
}
