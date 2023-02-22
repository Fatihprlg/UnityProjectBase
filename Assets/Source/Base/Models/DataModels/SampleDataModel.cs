using System; 

[Serializable] 
public class SampleDataModel : DataModel 
{ 
    public static SampleDataModel Data; 
    public SampleDataModel Load() 
    {
        if (Data == null)
        {
            Data = this;
            object data = LoadData();
            if (data != null)
            {
                Data = (SampleDataModel)data;
            }
        }
        return Data;
    }
    public void Save()
    {
        Save(Data);
    }
}