using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }
    public static AudioClip AudioSource2DPrefab { get; private set; }

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

    public void PlayBGM(string audioClip)
    {
        if (!BGMAudioList || !BGMAudioSource) { return; }

        AudioClip toPlay;
        if (!BGMAudioList.TryGetAudio(audioClip, out toPlay))
        {
            return;
        }
        BGMAudioSource.Stop();
        BGMAudioSource.clip = toPlay;
        BGMAudioSource.loop = true;
        BGMAudioSource.Play();
    }

    public void BGMStop()
    {
        BGMAudioSource.Stop();
    }

    public void PlaySE(string audioClip)
    {
        Vector2 point = BGMAudioSource.transform.position;
        PlaySE(audioClip, point);
    }

    public void PlaySE(string audioClip, Vector2 point)
    {
        if (!SEAudioList) { return; }
        AudioClip toPlay;
        if(!SEAudioList.TryGetAudio(audioClip, out toPlay)) { return; }
        AudioSource.PlayClipAtPoint(toPlay, point);
    }
}
