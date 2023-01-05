using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlesController : ControllerBase
{
    static ParticlesController instance;
    [SerializeField] ParticlePoolModel[] particlePools;

    public override void Initialize()
    {
        base.Initialize();

        if (instance != null)
        {
            Destroy(instance);
        }
        instance = this;
    }

    public static void SetParticle(int index, Vector3 pos, VibrationTypes type)
    {
        instance.particlePools[index].SetParticle(pos, type);
    }

    public static void SetParticle(int index, Vector3 pos, Quaternion rotation, VibrationTypes type)
    {
        instance.particlePools[index].SetParticle(pos, rotation, type);
    }

}