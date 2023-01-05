using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[Serializable]
public class PlayerDataModel : DataModel
{
    public static PlayerDataModel Data;
    public int Level = 1;
    public int LevelIndex;
    public int OpenedLevelsCount = 1;
    public int Currency;

    public PlayerDataModel Load()
    {
        if (Data == null)
        {
            Data = this;
            object data = LoadData();

            if (data != null)
            {
                Data = (PlayerDataModel)data;
            }
        }

        return Data;
    }


    public void Save()
    {
        Save(Data);
    }
}