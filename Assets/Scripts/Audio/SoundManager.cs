using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [Serializable]
    private class AudioClipData
    {
        public string tag;
        public AudioClip audioClip;
    }


}
