using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfigReelSpeed : MonoBehaviour
{
    [SerializeField]
    ConfigSheet ConfigSheet;

    [SerializeField]
    LineScript LineScript;

    [SerializeField]
    UnityEngine.UI.Slider Slider;

    public void UpdateValue()
    {
        LineScript.SetReelSpeed(Slider.value);
        ConfigSheet.UpdateConfig();
    }
}
