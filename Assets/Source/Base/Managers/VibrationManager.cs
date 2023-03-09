using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.NiceVibrations;

public class VibrationManager : IInitializable
{
    public static bool IsVibrationOn { get; private set; }
    private static float lastVibrationTime;

    public VibrationManager()
    {
        Initialize();
    }
    public void Initialize()
    {
        IsVibrationOn = SettingsDataModel.Data.isVibrationOn;
    }

    public static void SetVibrationState(bool state)
    {
        IsVibrationOn = state;
        SettingsDataModel.Data.isVibrationOn = IsVibrationOn;
    }

    public static void SetHaptic(VibrationTypes type)
    {
        if (!IsVibrationOn) return;
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
            case VibrationTypes.None:
            default:
                break;
        }
    }

    public static void SetHaptic(VibrationTypes type, float threshold)
    {
        if (!IsVibrationOn) return;
        if (Time.time < lastVibrationTime + threshold)
            return;
        lastVibrationTime = Time.time;
        SetHaptic(type);
    }
}
