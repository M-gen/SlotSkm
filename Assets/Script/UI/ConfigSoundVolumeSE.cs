using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfigSoundVolumeSE : MonoBehaviour
{
    [SerializeField]
    ConfigSheet ConfigSheet;

    [SerializeField]
    AudioManager AudioManager;

    [SerializeField]
    UnityEngine.UI.Slider Slider;
    public void UpdateValue()
    {
        AudioManager.SetVolume(Slider.value, "SE");
        ConfigSheet.UpdateConfig();
    }
}
