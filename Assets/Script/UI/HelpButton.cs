using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelpButton : MonoBehaviour
{
    [SerializeField]
    GameObject ImageHelpSheet;

    [SerializeField]
    AudioManager AudioManager;

    [SerializeField]
    AudioClip OnClickSE;


    public void OnClick()
    {
        ImageHelpSheet.SetActive(true);
        AudioManager.PlayAudio(OnClickSE, 0.3f, new string[] { "SE" });
    }
}
