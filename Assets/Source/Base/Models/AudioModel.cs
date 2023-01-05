using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class AudioModel
{
    public int id;
    public string name;
    public AudioClip clip;
    public float volume = 1;
    public float pitch = 1;
}
