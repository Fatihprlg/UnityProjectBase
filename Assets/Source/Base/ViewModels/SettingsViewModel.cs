using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsViewModel : ScreenElement
{
    [SerializeField] GameObject audioOn, audioOff, vibrationOn, vibrationOff;
    [SerializeField] GameObject settingsMenu;
    private bool settingsMenuState;
    private bool soundState;
    private bool vibrationState;
    public override void Initialize()
    {
        base.Initialize();
        soundState = AudioManager.Instance.IsAudioOn;
        vibrationState = VibrationManager.IsVibrationOn;
        SetVisuals();
    }
    
    public void SoundButtonClicked()
    {
        ToggleSound(!soundState);
    }

    public void VibrationButtonClicked()
    {
        ToggleVibration(!vibrationState);
    }

    public void ToggleSound(bool state)
    {
        AudioManager.Instance.SetAudioState(state);
        soundState = state;
        SetVisuals();
    }

    public void ToggleVibration(bool state)
    {
        VibrationManager.SetVibrationState(state);
        vibrationState = state;
        SetVisuals();
    }

    public void SettingsMenuTrigger(bool state)
    {
        settingsMenuState = state;
        settingsMenu.SetActive(state);
    }

    public void SettingsMenuTrigger()
    {
        SettingsMenuTrigger(!settingsMenuState);
    }

    private void SetVisuals()
    {
        audioOn?.SetActive(soundState);
        audioOff?.SetActive(!soundState);
        vibrationOn?.SetActive(vibrationState);
        vibrationOff?.SetActive(!vibrationState);
    }
}
