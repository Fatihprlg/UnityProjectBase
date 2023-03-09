using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class PopUpViewModel : ScreenElement
{
    public bool IsPopUpActive => activePopUp == null;
    [SerializeField] private List<PopUpModel> popUps;
    [SerializeField] private PopUpModel dummyPopUp;
    [SerializeField] private float timeBetweenPopUps;
    
    private PopUpModel activePopUp;
    private List<PopUpDataModel> popUpDatas;
    private Queue<PopUpDataModel> popUpQueue;
    public override void Initialize()
    {
        base.Initialize();
        GetAllPopUpDatas();
    }

    public void ShowPopUp(int index, string header, string text, string button1Text, string button2Text, UnityAction onButton1Clicked, UnityAction onButton2Clicked)
    {
        CreatePopUp(index, header, text, button1Text, onButton1Clicked, button2Text, onButton2Clicked);
        ShowPopUp(index);
    }
    
    public void ShowPopUp(int index, string header, string text, string buttonText, UnityAction onButtonClicked)
    {
        CreatePopUp(index, header, text, buttonText, onButtonClicked);
        ShowPopUp(index);
    }

    public PopUpDataModel ShowPopUp(int index)
    {
        var data = popUpDatas.FirstOrDefault(p => p.Index == index);
        if(data == null)
            return null;
        popUpQueue.Enqueue(data);
        if(popUpQueue.Count <= 1) StartCoroutine(N_ShowPopUp());
        return data;
    }
    public void ShowPopUp(PopUpDataModel popUp)
    {
        popUpQueue.Enqueue(popUp);
    }

    public void HideActivePopUp()
    {
        activePopUp.Hide();
        activePopUp = null;
    }
    
    public void HidePopUp(int index)
    {
        var popUp = popUps.FirstOrDefault(p => p.GetData().Index == index).GetData();
        HidePopUp(popUp);
    }

    public void HidePopUp(PopUpDataModel data)
    {
        var popUp = popUps.FirstOrDefault((p) => p.GetData().Index == data.Index);
        popUp.Hide();
        activePopUp = null;
    }

    IEnumerator N_ShowPopUp()
    {
        yield return null;
        while (popUpQueue.Count > 0)
        {
            var data = popUpQueue.Dequeue();
            var popUp = popUps.FirstOrDefault(p => p.GetData().Index == data.Index);
            if(popUp == null)
            {
                dummyPopUp.SetData(data);
                popUp = dummyPopUp;
            }
            activePopUp = popUp;
            activePopUp.Show();
            yield return new WaitUntil(() => !IsPopUpActive);
            yield return new WaitForSeconds(timeBetweenPopUps);
        }
    }

    private PopUpModel GetPopUp(int index)
    {
        return popUps.FirstOrDefault(p => p.GetData().Index == index);
    }

    private void GetAllPopUpDatas()
    {
        popUpDatas = new List<PopUpDataModel>();
        popUpDatas.Clear();
        foreach (PopUpModel popUpModel in popUps)
        {
            popUpDatas.Add(popUpModel.GetData());
        }
    }
    
    public PopUpDataModel CreatePopUp(int index, string header, string text, string button1Text, UnityAction onButton1Clicked, string button2Text = "", UnityAction onButton2Clicked = null)
    {
        PopUpDataModel data = new PopUpDataModel()
        {
            Index = index,
            Header = header,
            Text = text,
            Button1Text = button1Text,
            Button2Text = button2Text,
            Button1Action = onButton1Clicked,
            Button2Action = onButton2Clicked
        };
        data.SetData(data);
        return data;
    }
}