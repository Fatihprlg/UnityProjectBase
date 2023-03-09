using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Audios", menuName = "Create Audios File")]
public class Audios : ScriptableObject
{
    public List<AudioModel> SFXs;
    public List<AudioModel> Musics;
}
