using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyController : MonoBehaviour
{
    [SerializeField]
    SlotCore slotCore;

    [SerializeField]
    SoundResource soundResource;

    [SerializeField]
    Lamp LampLeft;

    [SerializeField]
    Lamp LampRight;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void DoBet()
    {
        soundResource.PlayAudio("game_01_bet", new string[] { "SE" });
    }

    public void DoReelStart()
    {
        soundResource.PlayAudio("game_02_lever", new string[] { "SE" });
    }

    public void DoReelStopButton( SlotCore.ButtonType buttonType )
    {
        soundResource.PlayAudio("game_03_reel_stop", new string[] { "SE" });

        LampLeft.SetAnimation("def");
        LampRight.SetAnimation("def");
    }

    public void FixOneGame()
    {
        switch( slotCore.oneGame.hit )
        {
            case "rep":
                soundResource.PlayAudio("game_hit_koyaku_rep", new string[] { "SE" });
                LampLeft.SetAnimation("ao");
                LampRight.SetAnimation("ao");
                break;
            case "bell":
                soundResource.PlayAudio("game_hit_koyaku_bell", new string[] { "SE" });
                LampLeft.SetAnimation("ki");
                LampRight.SetAnimation("ki");
                break;
            case "suika":
                soundResource.PlayAudio("game_hit_koyaku_suika", new string[] { "SE" });
                LampLeft.SetAnimation("midori");
                LampRight.SetAnimation("midori");
                break;
            case "chery":
                soundResource.PlayAudio("game_hit_koyaku_chery", new string[] { "SE" });
                LampLeft.SetAnimation("aka");
                LampRight.SetAnimation("aka");
                break;
            case "Big-r7":
                soundResource.PlayAudio("game_hit_bonus", new string[] { "SE" });
                LampLeft.SetAnimation("shiro");
                LampRight.SetAnimation("shiro");
                break;
            case "Big-b7":
                soundResource.PlayAudio("game_hit_bonus", new string[] { "SE" });
                LampLeft.SetAnimation("shiro");
                LampRight.SetAnimation("shiro");
                break;
            case "Reg":
                soundResource.PlayAudio("game_hit_bonus", new string[] { "SE" });
                LampLeft.SetAnimation("shiro");
                LampRight.SetAnimation("shiro");
                break;
        }
    }
}
