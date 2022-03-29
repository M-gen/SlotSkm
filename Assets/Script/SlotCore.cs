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
        ReelStopL1,
        ReelStopL2,
        ReelStopL3,
    }

    public SlotCoreOneGame oneGame = new SlotCoreOneGame();
    public SlotCoreLongGame longGame = new SlotCoreLongGame();

    public enum SceneStatus
    {
        Normal, // ゲーム中
        Help,   // 説明画面表示中
        Config, // 設定画面表示中
    }
    SceneStatus sceneStatus = SceneStatus.Normal;

    [SerializeField]
    SlotLotData slotLotData;

    [SerializeField]
    LineScript lineScript;

    [SerializeField]
    UIController uiController;

    [SerializeField]
    SoundResource soundResource;

    [SerializeField]
    BodyController bodyController;

    [SerializeField]
    DirectingManager directingManager;

    [SerializeField]
    StageEffect stageEffect;

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
                            if ( (Input.GetKeyDown(KeyCode.LeftArrow)) && (!lineScript.zugaraStatusPackReelL1.IsFix))
                            {
                                DoReelStopButtonDown(ButtonType.ReelStopL1);
                            }
                            else if( (Input.GetKeyDown(KeyCode.DownArrow)) && (!lineScript.zugaraStatusPackReelL2.IsFix))
                            {
                                DoReelStopButtonDown(ButtonType.ReelStopL2);
                            }
                            else if((Input.GetKeyDown(KeyCode.RightArrow)) && (!lineScript.zugaraStatusPackReelL3.IsFix))
                            {
                                DoReelStopButtonDown(ButtonType.ReelStopL3);
                            }
                        }
                        break;
                    case SlotCoreOneGame.Status.ButtonStep2Wait:
                        oneGame.oneButtonWaitTimer += Time.deltaTime;
                        oneGame.oneGameWaitTimer += Time.deltaTime;
                        if (SlotCoreOneGame.oneButtonWaitTimerMax <= oneGame.oneButtonWaitTimer)
                        {
                            if ((Input.GetKeyDown(KeyCode.LeftArrow)) && (!lineScript.zugaraStatusPackReelL1.IsFix))
                            {
                                DoReelStopButtonDown(ButtonType.ReelStopL1);
                            }
                            else if ((Input.GetKeyDown(KeyCode.DownArrow)) && (!lineScript.zugaraStatusPackReelL2.IsFix))
                            {
                                DoReelStopButtonDown(ButtonType.ReelStopL2);
                            }
                            else if ((Input.GetKeyDown(KeyCode.RightArrow)) && (!lineScript.zugaraStatusPackReelL3.IsFix))
                            {
                                DoReelStopButtonDown(ButtonType.ReelStopL3);
                            }
                        }
                        break;
                    case SlotCoreOneGame.Status.ButtonStep3Wait:
                        oneGame.oneButtonWaitTimer += Time.deltaTime;
                        oneGame.oneGameWaitTimer += Time.deltaTime;
                        if (SlotCoreOneGame.oneButtonWaitTimerMax <= oneGame.oneButtonWaitTimer)
                        {
                            if ((Input.GetKeyDown(KeyCode.LeftArrow)) && (!lineScript.zugaraStatusPackReelL1.IsFix))
                            {
                                DoReelStopButtonDown(ButtonType.ReelStopL1);
                            }
                            else if ((Input.GetKeyDown(KeyCode.DownArrow)) && (!lineScript.zugaraStatusPackReelL2.IsFix))
                            {
                                DoReelStopButtonDown(ButtonType.ReelStopL2);
                            }
                            else if ((Input.GetKeyDown(KeyCode.RightArrow)) && (!lineScript.zugaraStatusPackReelL3.IsFix))
                            {
                                DoReelStopButtonDown(ButtonType.ReelStopL3);
                            }
                        }
                        break;
                    case SlotCoreOneGame.Status.ButtonStep3ReleaseWait:
                        oneGame.oneGameWaitTimer += Time.deltaTime;

                        if (Input.GetKeyUp(KeyCode.LeftArrow))
                        {
                            DoReelStopButtonUp(ButtonType.ReelStopL1);
                        }
                        else if (Input.GetKeyUp(KeyCode.DownArrow))
                        {
                            DoReelStopButtonUp(ButtonType.ReelStopL2);

                        }
                        else if (Input.GetKeyUp(KeyCode.RightArrow))
                        {
                            DoReelStopButtonUp(ButtonType.ReelStopL3);

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
            case ButtonType.ReelStopL1:
                oneGame.downButtonType = ButtonType.ReelStopL1;
                break;
            case ButtonType.ReelStopL2:
                oneGame.downButtonType = ButtonType.ReelStopL2;
                break;
            case ButtonType.ReelStopL3:
                oneGame.downButtonType = ButtonType.ReelStopL3;
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

        longGame.AddInCoin(3);
        uiController.UpdateGameStatusViewText();
    }

    private void _DoLeverOn()
    {
        oneGame.status = SlotCoreOneGame.Status.ReelStartWait;
        bodyController.DoLeverOn();
    }
    private void _DoReelStart()
    {
        oneGame.oneGameWaitTimer = 0;
        oneGame.oneButtonWaitTimer = 0;
        oneGame.status = SlotCoreOneGame.Status.ButtonStep1Wait;

        oneGame.flagRole = "";
        oneGame.hit = "";
        oneGame.downButtonType = ButtonType.None;

        longGame.gameCount++;

        Debug.Log($"_DoReelStart");
        slotLotData.DoBaseLot();

        if ( (longGame.status== SlotCoreLongGame.Status.Normal ) && (oneGame.flagBounus != "") )
        {
            longGame.status = SlotCoreLongGame.Status.BonusFix;
        }

        lineScript.StartRollAll();
        uiController.UpdateGameStatusViewText();

        bodyController.DoReelStart();

        // 演出遷移
        switch (longGame.status)
        {
            case SlotCoreLongGame.Status.Normal:
                switch (longGame.viewStatus)
                {
                    case SlotCoreLongGame.ViewStatus.Normal:
                        break;
                    case SlotCoreLongGame.ViewStatus.NormalComboGame:
                        break;
                    case SlotCoreLongGame.ViewStatus.BonusGameEnd:
                        longGame.viewStatus = SlotCoreLongGame.ViewStatus.Normal;
                        break;
                }
                break;
            case SlotCoreLongGame.Status.BonusFix:
                switch (longGame.viewStatus)
                {
                    case SlotCoreLongGame.ViewStatus.Normal:
                        break;
                    case SlotCoreLongGame.ViewStatus.NormalComboGame:
                        if ( directingManager.comboDirectionName.Count == 0 ) // 連続演出が終了したうえでボーナスが確定しているので、確定画面にする
                        {
                            longGame.viewStatus = SlotCoreLongGame.ViewStatus.BonusFixShowIn;
                        }
                        break;
                    case SlotCoreLongGame.ViewStatus.BonusFixShowIn:
                        longGame.viewStatus = SlotCoreLongGame.ViewStatus.BonusFixShow;
                        break;
                    case SlotCoreLongGame.ViewStatus.BonusFixShow:
                        longGame.viewStatus = SlotCoreLongGame.ViewStatus.BonusFixShowAndBonusTypeShow;
                        break;
                    case SlotCoreLongGame.ViewStatus.BonusFixShowAndBonusTypeShow:
                        break;
                    case SlotCoreLongGame.ViewStatus.BonusGameEnd:
                        longGame.viewStatus = SlotCoreLongGame.ViewStatus.Normal;
                        break;
                }

                break;
            case SlotCoreLongGame.Status.BonusGame:
                switch (longGame.viewStatus)
                {
                    case SlotCoreLongGame.ViewStatus.BonusGameIn:
                        longGame.viewStatus = SlotCoreLongGame.ViewStatus.BonusGame;
                        break;
                    case SlotCoreLongGame.ViewStatus.BonusGame:

                        Debug.Log($"SlotCoreLongGame.Status.BonusGame {longGame.bonusTypeName}");
                        if (slotLotData.GetBonusStatus(longGame.bonusTypeName).Coin <= longGame.bonusOutCoin)
                        {
                            longGame.status = SlotCoreLongGame.Status.Normal;
                            longGame.viewStatus = SlotCoreLongGame.ViewStatus.BonusGameEnd;
                        }
                        break;
                }
                break;
        }

        // 演出の抽選と実行
        directingManager.SettingDirection();

        stageEffect.LeberOn();
    }

    private void _DoButtonStep1( ButtonType buttonType )
    {
        oneGame.oneButtonWaitTimer = 0;
        oneGame.status = SlotCoreOneGame.Status.ButtonStep2Wait;

        bodyController.DoReelStopButton(buttonType);
        lineScript.StopRollOne(buttonType);
        stageEffect.Stop1();
    }

    private void _DoButtonStep2( ButtonType buttonType )
    {
        oneGame.oneButtonWaitTimer = 0;
        oneGame.status = SlotCoreOneGame.Status.ButtonStep3Wait;

        bodyController.DoReelStopButton(buttonType);
        lineScript.StopRollOne(buttonType);
        stageEffect.Stop2();
    }

    private void _DoButtonStep3( ButtonType buttonType )
    {
        oneGame.oneButtonWaitTimer = 0;
        oneGame.status = SlotCoreOneGame.Status.ButtonStep3ReleaseWait;

        bodyController.DoReelStopButton(buttonType);
        lineScript.StopRollOne(buttonType);
        stageEffect.Stop3();
    }
    private void _DoButtonStep3Release()
    {
        // 1ゲームの終了処理
        lineScript.FixOneGame();
        bodyController.FixOneGame();

        switch( oneGame.hit )
        {
            case "rep":
                longGame.flagReplay = true;
                break;
            case "bell":
                if ( longGame.status == SlotCoreLongGame.Status.BonusGame )
                {
                    longGame.AddOutCoin(15);
                    uiController.UpdateGameStatusViewText();
                }
                else
                {
                    longGame.AddOutCoin(10);
                    uiController.UpdateGameStatusViewText();
                }
                break;
            case "suika":
                longGame.AddOutCoin(6);
                uiController.UpdateGameStatusViewText();
                break;
            case "chery":
                longGame.AddOutCoin(3);
                uiController.UpdateGameStatusViewText();
                break;
            case "Big-r7":
                break;
            case "Big-b7":
                break;
            case "Reg":
                break;
        }

        // 次のゲームにまたがるもの
        if (longGame.flagReplay)
        {
            oneGame.status = SlotCoreOneGame.Status.LeverOnWait;
            longGame.flagReplay = false;
        }
        else
        {
            oneGame.status = SlotCoreOneGame.Status.BetWait;
        }

        // 演出遷移
        switch (longGame.status)
        {
            case SlotCoreLongGame.Status.BonusFix:
                if ( ( oneGame.hit=="Big-r7" ) || ( oneGame.hit == "Big-b7" ) || ( oneGame.hit == "Reg" ) )
                {
                    longGame.status = SlotCoreLongGame.Status.BonusGame;
                    longGame.viewStatus = SlotCoreLongGame.ViewStatus.BonusGameIn;
                    longGame.bonusTypeName = oneGame.hit;
                    longGame.bonusOutCoin = 0;
                    directingManager.BonusIn(oneGame.hit);
                }
                break;
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
