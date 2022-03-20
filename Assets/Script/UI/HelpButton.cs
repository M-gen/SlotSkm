using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelpButton : MonoBehaviour
{
    [SerializeField]
    GameObject ImageHelpSheet;

    public void OnClick()
    {
        ImageHelpSheet.SetActive(true);
    }
}
