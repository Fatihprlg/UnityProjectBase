using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LevelModel
{
    public int index;
    public string name;
    public List<PoolItemDataModel> poolItems;
    public List<WorldItemDataModel> worldItems;
}
