using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AudioInfo
{
    public string audioName;
    public AudioClip audio;
}

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/AudioPrefab", order = 1)]
public class AudioPrefab : ScriptableObject
{
    public AudioInfo[] audioInfos;
}
