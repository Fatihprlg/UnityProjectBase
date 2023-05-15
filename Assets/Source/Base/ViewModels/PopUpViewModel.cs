using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class PopUpViewModel : ScreenElement
{
    public bool IsPopUpActive => _activePopUp == null;
    [SerializeField] private List<PopUpModel> popUps;
    [SerializeField] private PopUpModel dummyPopUp;
    [SerializeField] private float timeBetweenPopUps;
    
    private PopUpModel _activePopUp;
    private List<PopUpDataModel> _popUpDatas;
    private Queue<PopUpDataModel> _popUpQueue;
    public override void Initialize()
    {
        base.Initialize();
        GetAllPopUpDatas();
    }

    public void ShowPopUp(int index, string header, string text, string button1Text, string button2Text, UnityAction onButton1Clicked, UnityAction onButton2Clicked)
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
        ShowPopUp(data);
    }
    
    public void ShowPopUp(int index, string header, string text, string buttonText, UnityAction onButtonClicked)
    {
        PopUpDataModel data = new PopUpDataModel()
        {
            Index = index,
            Header = header,
            Text = text,
            Button1Text = text,
            Button1Action = onButtonClicked,
        };
        ShowPopUp(data);
    }

    public PopUpDataModel ShowPopUp(int index)
    {
        var data = _popUpDatas.FirstOrDefault(p => p.Index == index);
        if(data == null)
            return null;
        _popUpQueue.Enqueue(data);
        if(_popUpQueue.Count <= 1) StartCoroutine(N_ShowPopUp());
        return data;
    }
    public void ShowPopUp(PopUpDataModel popUp)
    {
        _popUpQueue.Enqueue(popUp);
    }

    public void HideActivePopUp()
    {
        _activePopUp.Hide();
        _activePopUp = null;
    }
    
    public void HidePopUp(int index)
    {
        var popUp = popUps.FirstOrDefault((p) => p.GetData().Index == index);
        if (popUp != null) popUp.Hide();
        _activePopUp = null;
    }

    public void HidePopUp(PopUpDataModel data)
    {
        var popUp = popUps.FirstOrDefault((p) => p.GetData().Index == data.Index);
        if (popUp != null) popUp.Hide();
        _activePopUp = null;
    }

    private IEnumerator N_ShowPopUp()
    {
        yield return null;
        while (_popUpQueue.Count > 0)
        {
            var data = _popUpQueue.Dequeue();
            var popUp = popUps.FirstOrDefault(p => p.GetData().Index == data.Index);
            if(popUp is null)
            {
                dummyPopUp.SetData(data);
                popUp = dummyPopUp;
            }
            _activePopUp = popUp;
            _activePopUp.Show();
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
        _popUpDatas = new List<PopUpDataModel>();
        _popUpDatas.Clear();
        foreach (PopUpModel popUpModel in popUps)
        {
            _popUpDatas.Add(popUpModel.GetData());
        }
    }
    
}