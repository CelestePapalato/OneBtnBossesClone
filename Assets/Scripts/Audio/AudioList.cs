using System;
using UnityEngine;

[CreateAssetMenu(fileName = "AudioList", menuName = "Audio/Audio List", order = 1)]
public class AudioList : ScriptableObject
{
    [Serializable]
    public class AudioClipInfo
    {
        public string name;
        public AudioClip audioClip;
    }

    public AudioClipInfo[] audio;

    public AudioClip GetAudio(string name)
    {
        AudioClip audioClip = null;
        foreach (AudioClipInfo info in audio)
        {
            if (info.name == name)
            {
                audioClip = info.audioClip; break;
            }
        }
        return audioClip;
    }

    public bool TryGetAudio(string name, out AudioClip audioClip) {
        audioClip = GetAudio(name);
        return audioClip != null;
    }
}