using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelpSheet : MonoBehaviour
{
    [SerializeField]
    AudioManager AudioManager;

    [SerializeField]
    AudioClip OnClickSE;

    public void OnClick()
    {
        gameObject.SetActive(false);
        AudioManager.PlayAudio(OnClickSE, 0.3f, new string[] { "SE" });
    }
}
