using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[System.Serializable]
public class PopUpDataModel
{
    public int Index;
    public string Header;
    public string Text;
    public string Button1Text;
    public string Button2Text;
    public UnityAction Button1Action;
    public UnityAction Button2Action;
    [SerializeField] private TextMeshProUGUI headerTxt;
    [SerializeField] private TextMeshProUGUI plainTxt;
    [SerializeField] private Button[] Buttons;

    public void SetData(PopUpDataModel data)
    {
        Index = data.Index;
        Header = data.Header;
        Text = data.Text;
        Button1Text = data.Button1Text;
        Button2Text = data.Button2Text;
        Button1Action = data.Button1Action;
        Button2Action = data.Button2Action;
        UpdateValues();
    }

    private void UpdateValues()
    {
        headerTxt.text = Header;
        plainTxt.text = Text;
        Buttons[0].SetText(Button1Text);
        Buttons[0].onClick.RemoveAllListeners();
        Buttons[0].onClick.AddListener(Button1Action);
        if(string.IsNullOrEmpty(Button2Text)) return;
        Buttons[1].SetText(Button2Text);
        Buttons[1].onClick.RemoveAllListeners();
        Buttons[1].onClick.AddListener(Button2Action);
    }
    
}