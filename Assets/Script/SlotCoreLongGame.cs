using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotCoreLongGame
{
    // 内部状態
    public enum Status
    {
        Normal,     // 通常
        BonusFix,   // ボーナス確定（内部的に、表示済みかかわらず）
        BonusGame,  // ボーナス中
    }
    public Status status = Status.BonusFix;

    // 表示状態
    public enum ViewStatus
    {
        Normal,                         // 非ボーナス、通常背景表示中
        NormalComboGame,                // 非ボーナス、連続演出中
        BonusFixShowIn,                 // ボーナス確定画面に突入
        BonusFixShow,                   // ボーナス確定画面、継続
        BonusFixShowAndBonusTypeShow,   // ボーナス確定画面、継続、図柄表示済み
    }
    public ViewStatus viewStatus = ViewStatus.Normal;

    public int hotLevel = 0;            // 熱さ
    public bool flagReplay = false;     // 1ゲームベット不要
    public int inCoin = 0;
    public int outCoin = 0;
    public int bonusOutCoin = 0;        // ボーナス中の獲得コイン
}
