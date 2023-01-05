using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class ParticleBaseModel : MonoBase
{
    public ParticleSystem Particle;
   
    public void SetStartColor(Color color)
    {
        Particle.SetStartColor(color);
    }

    public void OnParticleSystemStopped()
    {
        gameObject.SetActive(false);
    }

    public virtual void Play()
    {
        Particle.Play();
    }

    public virtual void Stop()
    {
        Particle.Stop();
    }

    private void Reset()
    {
        if (Particle == null)
        {
            Particle = GetComponent<ParticleSystem>();
            var main = Particle.main;
            main.stopAction = ParticleSystemStopAction.Callback;
        }
    }
}
