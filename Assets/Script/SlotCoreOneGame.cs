using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotCoreOneGame
{

    public string flagRole = "";    // 当選しているボーナス以外の役について
    public string flagBounus = "";  // 当選しているボーナスについて

    public enum Status
    {
        BetWait,                // ベット待ち
        LeverOnWait,            // レバーON待ち
        ReelStartWait,          // リール回転開始待ち
        ButtonStep1Wait,        // 第一停止待ち
        ButtonStep2Wait,        // 第二停止待ち
        ButtonStep3Wait,        // 第三停止待ち
        ButtonStep3ReleaseWait, // 第三停止ボタン離し待ち
    }
    public Status status = Status.BetWait;

    public string hit = ""; // 揃えた役について

    public int buttonCount
    {
        get
        {
            {
                switch (status)
                {
                    case Status.BetWait:
                        return 0;
                    case Status.LeverOnWait:
                        return 0;
                    case Status.ReelStartWait:
                        return 0;
                    case Status.ButtonStep1Wait:
                        return 0;
                    case Status.ButtonStep2Wait:
                        return 1;
                    case Status.ButtonStep3Wait:
                        return 2;
                    case Status.ButtonStep3ReleaseWait:
                        return 3;
                }
                return 0;
            }
        }
    }

    public const float oneButtonWaitTimerMax = 0.3f;
    public float oneButtonWaitTimer = 0;

    public const float oneGameWaitTimerMax = 4.1f;
    public float oneGameWaitTimer = oneGameWaitTimerMax;

    public SlotCore.ButtonType downButtonType = SlotCore.ButtonType.None;

}
