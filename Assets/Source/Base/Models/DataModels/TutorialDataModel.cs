using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[Serializable]
public class TutorialDataModel : DataModel
{
    public static TutorialDataModel Data;
    public int TutorialIndex;

    public TutorialDataModel Load()
    {
        if (Data == null)
        {
            Data = this;
            object data = LoadData();

            if (data != null)
            {
                Data = (TutorialDataModel)data;
            }
        }

        return Data;
    }


    public void Save()
    {
        Save(Data);
    }
}