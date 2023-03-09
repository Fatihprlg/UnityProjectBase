using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PopUpHandler : MonoBehaviour
{
    public bool IsPopUpActive => _popUpView.IsPopUpActive;
    [SerializeField] private PopUpViewModel _popUpView;
    

    public void ShowPopUp(int index, string header, string text, string buttonText, UnityAction onButtonClicked)
    {
        _popUpView.ShowPopUp(index, header, text, buttonText, onButtonClicked);
    }
    
    public void ShowPopUp(int index, string header, string text, string button1Text,string button2Text, UnityAction onButton1Clicked, UnityAction onButton2Clicked)
    {
        _popUpView.ShowPopUp(index,  header,  text,  button1Text, button2Text,  onButton1Clicked, onButton2Clicked);
    }
    
    public void ShowPopUp(int index)
    {
        _popUpView.ShowPopUp(index);
    }
    public void ShowPopUp(PopUpDataModel popUp)
    {
        _popUpView.ShowPopUp(popUp);
    }
    public void HidePopUp(int index)
    {
        _popUpView.HidePopUp(index);
    }
    
    public void HidePopUp(PopUpDataModel data)
    {
        _popUpView.HidePopUp(data);
    }

    public void HideActivePopUp() => _popUpView.HideActivePopUp();

}
