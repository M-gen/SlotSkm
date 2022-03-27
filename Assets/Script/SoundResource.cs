using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundResource : MonoBehaviour
{
    [System.Serializable]
    public class AudioClipItems
    {
        public string Key;
        public AudioClip Clip;
    }

    [SerializeField]
    AudioManager audioManager;

    [SerializeField]
    AudioClipItems[] audioClips;

    public void PlayAudio(string key, string[] tags, bool isLoop = false, float delay = 0f, float volume = 1.0f)
    {
        foreach (var i in audioClips)
        {
            if (key == i.Key)
            {
                audioManager.PlayAudio(i.Clip, volume, tags, isLoop, delay);
            }
        }
    }
    public void DeleteAudios(string tag)
    {
        audioManager.DeleteAudios(tag);
    }
}
