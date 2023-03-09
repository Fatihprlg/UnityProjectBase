using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WorldItemModel : ItemModel<WorldItemDataModel>
{
    public override void SetValues(WorldItemDataModel data)
    {
        id = transform.GetSiblingIndex();
        transform.position = data.Position;
        transform.rotation = data.Rotation;
        transform.localScale = data.Scale;
    }
    public override WorldItemDataModel GetData()
    {
        WorldItemDataModel dataModel = new WorldItemDataModel();
        dataModel.Id = id;
        dataModel.Position = transform.position;
        dataModel.Rotation = transform.rotation;
        dataModel.Scale = transform.localScale;
        return dataModel;
    }
}

[System.Serializable]
public class WorldItemDataModel : ItemDataModel
{
    public Vector3 Position;
    public Vector3 Scale;
    public Quaternion Rotation;
}
