using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[Serializable]
public class SettingsDataModel : DataModel
{
    public static SettingsDataModel Data;
    public bool isAudioOn = true;
    public bool isVibrationOn = true;
    public float audioVolume = 1;

    public SettingsDataModel Load()
    {
        if (Data == null)
        {
            Data = this;
            object data = LoadData();

            if (data != null)
            {
                Data = (SettingsDataModel)data;
            }
        }

        return Data;
    }


    public void Save()
    {
        Save(Data);
    }
}