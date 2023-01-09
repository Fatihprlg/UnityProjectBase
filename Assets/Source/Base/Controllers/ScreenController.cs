using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenController : ControllerBase
{

    [SerializeField] ScreenModel[] screens;
    [SerializeField] int mainScreenIndex;
    private ScreenModel currentScreen;
    private int currentScreenIndex;

    public override void Initialize()
    {
        base.Initialize();
        for (int index = mainScreenIndex; index < screens.Length; index++)
        {
            ScreenModel item = screens[index];
            item.Initialize();
            item.Hide();
        }

        currentScreen = screens[mainScreenIndex];
        currentScreenIndex = mainScreenIndex;
        currentScreen.Show();
    }

    public void NextScreen()
    {
        var index = (currentScreenIndex + 1) < screens.Length ? currentScreenIndex : mainScreenIndex;
        ChangeScreen(index);
    }

    public void ChangeScreen(int index)
    {
        currentScreen.Hide();
        if(index < screens.Length && index >= mainScreenIndex)
        {
            currentScreen = screens[index];
            currentScreen.Show();
            currentScreenIndex = index;
        }
        else
        {
            currentScreen = screens[mainScreenIndex];
            currentScreenIndex = mainScreenIndex;
            Debug.LogWarning($"There is no screen with given index {index}! Main screen loaded instead.");
        }
    }

    public override void OnStateChanged(GameStates state)
    {
        base.OnStateChanged(state);
        ChangeScreen((int)state + mainScreenIndex);
    }
}
