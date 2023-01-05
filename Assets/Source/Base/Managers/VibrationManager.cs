using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.NiceVibrations;

public class VibrationManager : NonMonoBase
{
    public static bool IsVibrationOn => isVibrationOn;
    static float lastVibrationTime;
    static bool isVibrationOn;

    public VibrationManager()
    {
        Initialize();
    }
    public override void Initialize()
    {
        base.Initialize();
        isVibrationOn = SettingsDataModel.Data.isVibrationOn;
    }

    public static void SetVibrationState(bool state)
    {
        isVibrationOn = state;
        SettingsDataModel.Data.isVibrationOn = isVibrationOn;
    }

    public static void SetHaptic(VibrationTypes type)
    {
        if (!isVibrationOn) return;
        switch (type)
        {
            case VibrationTypes.Light:
                MMVibrationManager.Haptic(HapticTypes.LightImpact);
                break;
            case VibrationTypes.Medium:
                MMVibrationManager.Haptic(HapticTypes.MediumImpact);
                break;
            case VibrationTypes.Heavy:
                MMVibrationManager.Haptic(HapticTypes.HeavyImpact);
                break;
            case VibrationTypes.Succes:
                MMVibrationManager.Haptic(HapticTypes.Success);
                break;
            case VibrationTypes.Fail:
                MMVibrationManager.Haptic(HapticTypes.Failure);
                break;
            case VibrationTypes.RigidImpact:
                MMVibrationManager.Haptic(HapticTypes.RigidImpact);
                break;
            case VibrationTypes.Soft:
                MMVibrationManager.Haptic(HapticTypes.SoftImpact);
                break;
            case VibrationTypes.Warning:
                MMVibrationManager.Haptic(HapticTypes.Warning);
                break;
            default:
                break;
        }
    }

    public static void SetHaptic(VibrationTypes type, float threshold)
    {
        if (!isVibrationOn) return;
        if (Time.time < lastVibrationTime + threshold)
            return;
        lastVibrationTime = Time.time;
        SetHaptic(type);
    }
}
