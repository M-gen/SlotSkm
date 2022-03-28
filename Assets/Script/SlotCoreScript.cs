using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotCoreScript : MonoBehaviour
{
    [System.Serializable]
    public class BaseLot
    {
        public string Bonus;
        public string Role;
        public int    LotValue; // 抽選の重み
    }


    [System.Serializable]
    public class BonusStatus
    {
        public string Name;
        public int Coin;
    }

    [SerializeField]
    BaseLot[] baseLots;

    [SerializeField]
    BaseLot[] bonusInLots; // ボーナスが確定しているときの抽選

    [SerializeField]
    BaseLot[] bonusLots;   // ボーナス中の抽選

    [SerializeField]
    BonusStatus[] bonusType;


    [SerializeField]
    GameObject Stage;


    public enum GameStage
    {
        Normal,  // 通常時
        BonusFix, // ボーナス確定中
        Bonus,   // ボーナス中
    }

    public GameStage gameStage = GameStage.Normal;
    public string bonusTypeName = "";
    public LineScript LineScript;

    [SerializeField]
    AudioManager audioManager;

    [SerializeField]
    StageEffect stageEffect;


}
