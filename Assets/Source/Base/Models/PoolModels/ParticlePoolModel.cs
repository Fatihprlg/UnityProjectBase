using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlePoolModel : PoolModel
{
    public void SetParticle(Vector3 pos, VibrationTypes type)
    {
        ParticleBaseModel particle = GetDeactiveItem<ParticleBaseModel>();
        if(particle != null)
        {
            particle.transform.position = pos;
            particle.SetActiveGameObject(true);
            particle.Play();
            VibrationManager.SetHaptic(type);
        }

    }

    public void SetParticle(Vector3 pos, Quaternion rotation, VibrationTypes type)
    {
        ParticleBaseModel particle = GetDeactiveItem<ParticleBaseModel>();
        if (particle != null)
        {
            particle.transform.position = pos;
            particle.transform.rotation = rotation;
            particle.SetActiveGameObject(true);
            particle.Play();
            VibrationManager.SetHaptic(type);
        }
    }

    [EditorButton]
    public void GetDeactiveItems()
    {
        InitializeOnEditor();
    }
}
