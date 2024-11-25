using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{    public static SoundManager Instance { get; private set; }

    [SerializeField]
    AudioSource BGMAudioSource;
    [SerializeField]
    AudioList SEAudioList;
    [SerializeField]
    AudioList BGMAudioList;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    public void PlayBGM(string audioClip, bool loop)
    {
        if (!BGMAudioList || !BGMAudioSource) { return; }

        AudioClip toPlay;
        if (!BGMAudioList.TryGetAudio(audioClip, out toPlay))
        {
            return;
        }
        BGMAudioSource.Stop();
        BGMAudioSource.clip = toPlay;
        BGMAudioSource.loop = loop;
        BGMAudioSource.Play();
    }

    public void BGMStop()
    {
        BGMAudioSource.Stop();
    }

    public void PlaySE(string audioClip, Vector2 point)
    {
        if (!SEAudioList) { return; }
        AudioClip toPlay;
        if(!SEAudioList.TryGetAudio(audioClip, out toPlay)) { return; }
        AudioSource.PlayClipAtPoint(toPlay, point);
    }
}
