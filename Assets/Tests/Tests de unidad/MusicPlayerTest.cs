using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

public class MusicPlayerTest
{
    private SoundManager soundManager = Resources.Load<SoundManager>("Managers/SoundManager");
    private AudioList audioList = Resources.Load<AudioList>("Scriptables/Audio/BGM");

    private AudioClip audioClip = Resources.Load<AudioClip>("Audio/BGM/Spiral Strike");
    private string audioClipname = "level_1";

    [Test]
    public void MusicPlayerBGMPlay()
    {
        if (SceneManager.GetActiveScene().name != "TESTING")
        {
            SceneManager.LoadScene("TESTING");
        }
        soundManager = GameObject.Instantiate(soundManager);
        soundManager.PlayBGM(audioClipname);
        AudioSource source = soundManager.GetComponent<AudioSource>();
        Assert.That(source.clip, Is.EqualTo(audioClip));
    }
}
