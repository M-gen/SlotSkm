using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfigButton : MonoBehaviour
{
    [SerializeField]
    GameObject ConfigSheet;

    [SerializeField]
    AudioClip OnClickSE;

    [SerializeField]
    AudioManager AudioManager;

    public void OnClick()
    {
        ConfigSheet.SetActive(true);
        AudioManager.PlayAudio(OnClickSE, 0.3f, new string[] { "SE" });
    }
}
