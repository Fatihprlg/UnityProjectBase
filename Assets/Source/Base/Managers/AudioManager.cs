using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using DG.Tweening;

public class AudioManager : MonoSingleton<AudioManager>, ICrossSceneObject
{
    public bool IsAudioOn => isAudioOn;
    [SerializeField] Audios audios;
    [SerializeField] AudioSource sfxStandartSource, sfxInreasingSource, musicSource;
    private AudioModel currentMusic;
    bool isAudioOn;
    [Range(0, 1)] float volumeMutliplier;

    public override void Initialize()
    {
        base.Initialize();
        isAudioOn = SettingsDataModel.Data.isAudioOn;
        volumeMutliplier = SettingsDataModel.Data.audioVolume;
    }

    public void SetAudioState(bool state)
    {
        isAudioOn = state;
        SettingsDataModel.Data.isAudioOn = isAudioOn;
    }

    public void SetVolume(float volume)
    {
        volume = Mathf.Clamp01(volume);
        volumeMutliplier = volume;
        SettingsDataModel.Data.audioVolume = volumeMutliplier;
    }

    public void PlayMusic(string name, bool fadeOut = true, float fadeTime = 1f)
    {
        var music = audios.Musics.FirstOrDefault(x => x.name == name);
        PlayMusic(music, fadeOut, fadeTime);
    }

    public void PlayMusic(int id, bool fadeOut = true, float fadeTime = 1f)
    {
        var music = audios.Musics.FirstOrDefault(x => x.id == id);
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
        if(!isAudioOn) return;
        if (sfx != null)
        {
            sfxStandartSource.pitch = sfx.pitch;
            sfxStandartSource.PlayOneShot(sfx.clip, sfx.volume * volumeMutliplier);
        }
    }

    private void PlayMusic(AudioModel music, bool fadeOut, float fadeTime)
    {
        if(!isAudioOn) return;
        if (fadeOut)
        {
            StartCoroutine(BlendMusics(currentMusic, music, fadeTime));
        }
        else
        {
            currentMusic = music;
            musicSource.clip = music.clip;
            musicSource.pitch = music.pitch;
            musicSource.volume = music.volume * volumeMutliplier;
            musicSource.loop = true;
            musicSource.Play();
        }
       
    }
    private IEnumerator BlendMusics(AudioModel first, AudioModel second, float time)
    {
        yield return null;

        int loopCount = Mathf.FloorToInt(time / 0.1f);
        float firstVolDecreaseAmt = -(first.volume * volumeMutliplier * 2) / loopCount;
        float secondVolIncrAmt = (second.volume * volumeMutliplier * 2) / loopCount;
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