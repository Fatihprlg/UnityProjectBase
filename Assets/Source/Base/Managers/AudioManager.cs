using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using DG.Tweening;

public class AudioManager : MonoSingleton<AudioManager>, ICrossSceneObject, IInitializable
{
    public bool IsAudioOn { get; private set; }
    [SerializeField] private Audios audios;
    [SerializeField] private AudioSource sfxStandardSource, musicSource;
    private AudioModel currentMusic;
    [Range(0, 1)] private float volumeMultiplier;

    public void Initialize()
    {
        destroyGameObjectOnDuplicate = true;
        base.Init();
        if(destroyed) return;
        HandleDontDestroy();
        IsAudioOn = SettingsDataModel.Data.isAudioOn;
        volumeMultiplier = SettingsDataModel.Data.audioVolume;
    }

    public void SetAudioState(bool state)
    {
        IsAudioOn = state;
        SettingsDataModel.Data.isAudioOn = IsAudioOn;
    }

    public void SetVolume(float volume)
    {
        volume = Mathf.Clamp01(volume);
        volumeMultiplier = volume;
        SettingsDataModel.Data.audioVolume = volumeMultiplier;
    }

    public void PlayMusic(string name, bool fadeOut = false, float fadeTime = 1f)
    {
        AudioModel music = audios.Musics.FirstOrDefault(x => x.name == name);
        PlayMusic(music, fadeOut, fadeTime);
    }

    public void PlayMusic(int id, bool fadeOut = false, float fadeTime = 1f)
    {
        AudioModel music = audios.Musics.FirstOrDefault(x => x.id == id);
        PlayMusic(music, fadeOut, fadeTime);
    }

    public void PlaySFX(string name)
    {
        var sfx = audios.SFXs.FirstOrDefault(x => x.name == name);
        PlaySFX(sfx);
    }

    public void PlaySFX(int id)
    {
        var sfx = audios.SFXs.FirstOrDefault(x => x.id == id);
        PlaySFX(sfx);
    }

    private void PlaySFX(AudioModel sfx)
    {
        if(!IsAudioOn) return;
        if (sfx != null)
        {
            sfxStandardSource.pitch = sfx.pitch;
            sfxStandardSource.PlayOneShot(sfx.clip, sfx.volume * volumeMultiplier);
        }
    }

    private void PlayMusic(AudioModel music, bool fadeOut, float fadeTime)
    {
        if(!IsAudioOn) return;
        if (fadeOut)
        {
            StartCoroutine(BlendMusics(currentMusic, music, fadeTime));
        }
        else
        {
            currentMusic = music;
            musicSource.clip = music.clip;
            musicSource.pitch = music.pitch;
            musicSource.volume = music.volume * volumeMultiplier;
            musicSource.loop = true;
            musicSource.Play();
        }
       
    }
    private IEnumerator BlendMusics(AudioModel first, AudioModel second, float time)
    {
        yield return null;

        int loopCount = Mathf.FloorToInt(time / 0.1f);
        float firstVolDecreaseAmt = -(first.volume * volumeMultiplier * 2) / loopCount;
        float secondVolIncrAmt = (second.volume * volumeMultiplier * 2) / loopCount;
        float volDelta = firstVolDecreaseAmt;
        while (loopCount > 0)
        {
            musicSource.volume += volDelta;
            if (Mathf.Approximately(musicSource.volume, 0))
            {
                musicSource.clip = second.clip;
                currentMusic = second;
                musicSource.pitch = second.pitch;
                musicSource.loop = true;
                volDelta = secondVolIncrAmt;
            }

            loopCount--;
            yield return new WaitForSeconds(0.1f);
        }
    }

    public void HandleDontDestroy()
    {
        transform.SetParent(null);
        DontDestroyOnLoad(this);
    }
}