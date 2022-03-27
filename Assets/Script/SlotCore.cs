using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotCore : MonoBehaviour
{
    public enum ButtonType
    {
        None,
        Bet,
        Lever,
        ReelStopLeft,
        ReelStopCenter,
        ReelStopRight,
    }

    public SlotCoreOneGame oneGame = new SlotCoreOneGame();
    public SlotCoreLongGame longGame = new SlotCoreLongGame();

    public enum SceneStatus
    {
        Normal, // ÉQÅ[ÉÄíÜ
        Help,   // ê‡ñæâÊñ ï\é¶íÜ
        Config, // ê›íËâÊñ ï\é¶íÜ
    }
    SceneStatus sceneStatus = SceneStatus.Normal;

    [SerializeField]
    SlotLotDatas slotLotDatas;

    [SerializeField]
    LineScript LineScript;

    [SerializeField]
    SoundResource soundResource;

    [SerializeField]
    BodyController bodyController;

    void Start()
    {
        
    }

    void Update()
    {
        switch (sceneStatus)
        {
            case SceneStatus.Normal:
                switch (oneGame.status)
                {
                    case SlotCoreOneGame.Status.BetWait:
                        oneGame.oneGameWaitTimer += Time.deltaTime;
                        if (Input.GetKeyDown(KeyCode.Backspace))
                        {
                            DoBet();
                        }
                        break;
                    case SlotCoreOneGame.Status.LeverOnWait:
                        oneGame.oneGameWaitTimer += Time.deltaTime;
                        if (Input.GetKeyDown(KeyCode.Return))
                        {
                            DoLeverOn();
                        }
                        break;
                    case SlotCoreOneGame.Status.ReelStartWait:
                        oneGame.oneGameWaitTimer += Time.deltaTime;
                        if (SlotCoreOneGame.oneGameWaitTimerMax <= oneGame.oneGameWaitTimer)
                        {
                            DoReelStart();
                        }
                        break;
                    case SlotCoreOneGame.Status.ButtonStep1Wait:
                        oneGame.oneButtonWaitTimer += Time.deltaTime;
                        oneGame.oneGameWaitTimer += Time.deltaTime;
                        if (SlotCoreOneGame.oneButtonWaitTimerMax <= oneGame.oneButtonWaitTimer)
                        {
                            if (Input.GetKeyDown(KeyCode.LeftArrow))
                            {
                                DoReelStopButtonDown(ButtonType.ReelStopLeft);
                            }
                            else if (Input.GetKeyDown(KeyCode.DownArrow))
                            {
                                DoReelStopButtonDown(ButtonType.ReelStopCenter);
                            }
                            else if (Input.GetKeyDown(KeyCode.RightArrow))
                            {
                                DoReelStopButtonDown(ButtonType.ReelStopRight);
                            }
                        }
                        break;
                    case SlotCoreOneGame.Status.ButtonStep2Wait:
                        oneGame.oneButtonWaitTimer += Time.deltaTime;
                        oneGame.oneGameWaitTimer += Time.deltaTime;
                        if (SlotCoreOneGame.oneButtonWaitTimerMax <= oneGame.oneButtonWaitTimer)
                        {
                            if (Input.GetKeyDown(KeyCode.LeftArrow))
                            {
                                DoReelStopButtonDown( ButtonType.ReelStopLeft );
                            }
                            else if (Input.GetKeyDown(KeyCode.DownArrow))
                            {
                                DoReelStopButtonDown(ButtonType.ReelStopCenter );
                            }
                            else if (Input.GetKeyDown(KeyCode.RightArrow))
                            {
                                DoReelStopButtonDown(ButtonType.ReelStopRight );
                            }
                        }
                        break;
                    case SlotCoreOneGame.Status.ButtonStep3Wait:
                        oneGame.oneButtonWaitTimer += Time.deltaTime;
                        oneGame.oneGameWaitTimer += Time.deltaTime;
                        if (SlotCoreOneGame.oneButtonWaitTimerMax <= oneGame.oneButtonWaitTimer)
                        {
                            if (Input.GetKeyDown(KeyCode.LeftArrow))
                            {
                                DoReelStopButtonDown(ButtonType.ReelStopLeft);
                            }
                            else if (Input.GetKeyDown(KeyCode.DownArrow))
                            {
                                DoReelStopButtonDown(ButtonType.ReelStopCenter);
                            }
                            else if (Input.GetKeyDown(KeyCode.RightArrow))
                            {
                                DoReelStopButtonDown(ButtonType.ReelStopRight);
                            }
                        }
                        break;
                    case SlotCoreOneGame.Status.ButtonStep3ReleaseWait:
                        oneGame.oneGameWaitTimer += Time.deltaTime;

                        if (Input.GetKeyUp(KeyCode.LeftArrow))
                        {
                            DoReelStopButtonUp(ButtonType.ReelStopLeft);
                        }
                        else if (Input.GetKeyUp(KeyCode.DownArrow))
                        {
                            DoReelStopButtonUp(ButtonType.ReelStopCenter);

                        }
                        else if (Input.GetKeyUp(KeyCode.RightArrow))
                        {
                            DoReelStopButtonUp(ButtonType.ReelStopRight);

                        }

                        break;
                }
                break;

            case SceneStatus.Help:
                {

                }
                break;

            case SceneStatus.Config:
                {

                }
                break;
        }

        //Debug.Log($"SlotCore:Update {oneGame.status} {oneGame.isDownReelStopButtonLeft} {oneGame.isDownReelStopButtonCenter} {oneGame.isDownReelStopButtonRight} {oneGame.oneGameWaitTimer}");
    }

    public void DoBet()
    {
        if (oneGame.status != SlotCoreOneGame.Status.BetWait) return;
        _DoBet();
    }

    public void DoLeverOn()
    {
        if (oneGame.status != SlotCoreOneGame.Status.LeverOnWait) return;
        _DoLeverOn();
    }

    public void DoReelStart()
    {
        if (oneGame.status != SlotCoreOneGame.Status.ReelStartWait) return;
        _DoReelStart();
    }

    public void DoReelStopButtonUp(ButtonType buttonType)
    {
        switch (oneGame.status)
        {
            case SlotCoreOneGame.Status.ButtonStep3ReleaseWait:
                if ( oneGame.downButtonType == buttonType)
                {
                    _DoButtonStep3Release();
                }
                break;
        }
    }

    public void DoReelStopButtonDown(ButtonType buttonType)
    {
        switch (buttonType)
        {
            case ButtonType.ReelStopLeft:
                oneGame.isDownReelStopButtonLeft = true;
                oneGame.downButtonType = ButtonType.ReelStopLeft;
                break;
            case ButtonType.ReelStopCenter:
                oneGame.isDownReelStopButtonCenter = true;
                oneGame.downButtonType = ButtonType.ReelStopCenter;
                break;
            case ButtonType.ReelStopRight:
                oneGame.isDownReelStopButtonRight = true;
                oneGame.downButtonType = ButtonType.ReelStopRight;
                break;
        }

        switch (oneGame.status)
        {
            case SlotCoreOneGame.Status.ButtonStep1Wait:
                _DoButtonStep1(buttonType);
                break;
            case SlotCoreOneGame.Status.ButtonStep2Wait:
                _DoButtonStep2(buttonType);
                break;
            case SlotCoreOneGame.Status.ButtonStep3Wait:
                _DoButtonStep3(buttonType);
                break;
        }
    }

    private void _DoBet()
    {
        oneGame.status = SlotCoreOneGame.Status.LeverOnWait;
        bodyController.DoBet();
        //soundResource.PlayAudio("game_01_bet", new string[] { "SE" });
    }

    private void _DoLeverOn()
    {
        oneGame.status = SlotCoreOneGame.Status.ReelStartWait;
        bodyController.DoLeverOn();
        //soundResource.PlayAudio("game_02_lever", new string[] { "SE" });
    }
    private void _DoReelStart()
    {
        oneGame.oneGameWaitTimer = 0;
        oneGame.oneButtonWaitTimer = 0;
        oneGame.status = SlotCoreOneGame.Status.ButtonStep1Wait;

        oneGame.isDownReelStopButtonLeft   = false;
        oneGame.isDownReelStopButtonCenter = false;
        oneGame.isDownReelStopButtonRight  = false;

        LineScript.StartRollAll();
    }

    private void _DoButtonStep1( ButtonType buttonType )
    {
        oneGame.oneButtonWaitTimer = 0;
        oneGame.status = SlotCoreOneGame.Status.ButtonStep2Wait;

        LineScript.StopRollOne(buttonType);
    }

    private void _DoButtonStep2( ButtonType buttonType )
    {
        oneGame.oneButtonWaitTimer = 0;
        oneGame.status = SlotCoreOneGame.Status.ButtonStep3Wait;

        LineScript.StopRollOne(buttonType);
    }

    private void _DoButtonStep3( ButtonType buttonType )
    {
        oneGame.oneButtonWaitTimer = 0;
        oneGame.status = SlotCoreOneGame.Status.ButtonStep3ReleaseWait;

        LineScript.StopRollOne(buttonType);
    }
    private void _DoButtonStep3Release()
    {
        if (longGame.flagReplay)
        {
            oneGame.status = SlotCoreOneGame.Status.LeverOnWait;
            longGame.flagReplay = false;
        }
        else
        {
            oneGame.status = SlotCoreOneGame.Status.BetWait;
        }
    }

    public void ShowHelp(bool isShow)
    {

    }

    public void ShowConfig(bool isShow)
    {
    }

    public void SetConfigSoundVolumeBGM(float value)
    {
    }
    public void SetConfigSoundVolumeSE(float value)
    {
    }
    public void SetConfigReelSpeed(float value)
    {
    }
}
