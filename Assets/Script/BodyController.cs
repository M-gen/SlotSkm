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

    [SerializeField]
    GameObject betButtonOn;

    [SerializeField]
    GameObject betButtonOff;

    [SerializeField]
    GameObject lever;

    [SerializeField]
    GameObject buttonLightL1;

    [SerializeField]
    GameObject buttonLightL2;

    [SerializeField]
    GameObject buttonLightL3;

    [SerializeField]
    GameObject stageStartScreen;

    Vector3 leverStartPostion;

    void Start()
    {
        buttonLightL1.SetActive( false );
        buttonLightL2.SetActive( false );
        buttonLightL3.SetActive( false );
        betButtonOn.SetActive( false );
        leverStartPostion = lever.transform.position;

    }

    void Update()
    {
        
    }

    public void DoBet()
    {
        soundResource.PlayAudio("game_01_bet", new string[] { "SE" });

        StartCoroutine("DoBetAction");
    }

    private IEnumerator DoBetAction()
    {
        betButtonOff.SetActive(false);
        betButtonOn.SetActive(true);
        // 1•b‘Ò‚Â  
        yield return new WaitForSeconds(0.5f);

        betButtonOff.SetActive(true);
        betButtonOn.SetActive(false);
    }

    public void DoLeverOn()
    {
        StartCoroutine("DoLeverOnAction");
        stageStartScreen.SetActive(false);
    }

    private IEnumerator DoLeverOnAction()
    {
        lever.transform.position += new Vector3(-0.01f, -0.12f);

        buttonLightL1.SetActive(true);
        buttonLightL2.SetActive(true);
        buttonLightL3.SetActive(true);

        // 1•b‘Ò‚Â  
        yield return new WaitForSeconds(0.5f);

        lever.transform.position = leverStartPostion;
    }

    public void DoReelStart()
    {
        soundResource.PlayAudio("game_02_lever", new string[] { "SE" });
    }

    public void DoReelStopButton( SlotCore.ButtonType buttonType )
    {
        soundResource.PlayAudio("game_03_reel_stop", new string[] { "SE" });

        switch (buttonType)
        {
            case SlotCore.ButtonType.ReelStopL1:
                buttonLightL1.SetActive(false);
                break;
            case SlotCore.ButtonType.ReelStopL2:
                buttonLightL2.SetActive(false);
                break;
            case SlotCore.ButtonType.ReelStopL3:
                buttonLightL3.SetActive(false);
                break;
        }

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
