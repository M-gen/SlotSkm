using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyController : MonoBehaviour
{
    [SerializeField]
    SoundResource soundResource; 

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DoBet()
    {
        soundResource.PlayAudio("game_01_bet", new string[] { "SE" });
    }
    public void DoLeverOn()
    {
        soundResource.PlayAudio("game_02_lever", new string[] { "SE" });
    }
}
