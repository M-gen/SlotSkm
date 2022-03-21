using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfigSoundVolumeBGM : MonoBehaviour
{
    [SerializeField]
    ConfigSheet ConfigSheet;

    [SerializeField]
    AudioManager AudioManager;

    [SerializeField]
    UnityEngine.UI.Slider Slider;

    public void UpdateValue()
    {
        AudioManager.SetVolume( Slider.value, "BGM");
        ConfigSheet.UpdateConfig();
    }
}
