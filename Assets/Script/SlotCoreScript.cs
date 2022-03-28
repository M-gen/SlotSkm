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


    Vector3 leberStartPostion;

    int baseLotsMax = 0;
    int bonusInLotsMax = 0;
    int bonusLotsMax = 0;

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

    private void Start()
    {
        foreach ( var i in baseLots)
        {
            baseLotsMax += i.LotValue;
        }

        foreach (var i in bonusInLots)
        {
            bonusInLotsMax += i.LotValue;
        }

        foreach (var i in bonusLots)
        {
            bonusLotsMax += i.LotValue;
        }

    }

    public void DoBaseLot( ref string bonus, ref string role )
    {
        switch (gameStage)
        {
            case GameStage.Normal:
                {
                    var r = Random.Range(0, baseLotsMax);
                    foreach (var i in baseLots)
                    {
                        if (r < i.LotValue)
                        {
                            bonus = i.Bonus;
                            role = i.Role;

                            //if(GameCount==1)
                            //{
                            //    //bonus = "Big-r7";
                            //    bonus = "Reg";
                            //}
                            //bonus = "Big-r7";
                            //role = "bell";

                            if (bonus != "")
                            {
                                if (gameStage == GameStage.Normal)
                                {
                                    gameStage = GameStage.BonusFix;
                                }
                            }

                            return;
                        }

                        r -= i.LotValue;
                    }
                    bonus = "";
                    role = "";
                }
                break;
            case GameStage.BonusFix:
                {
                    var r = Random.Range(0, bonusInLotsMax);
                    foreach (var i in bonusInLots)
                    {
                        if (r < i.LotValue)
                        {
                            role = i.Role;
                            return;
                        }

                        r -= i.LotValue;
                    }
                    role = "";
                }
                break;
            case GameStage.Bonus:
                {
                    var r = Random.Range(0, bonusLotsMax);
                    bonus = "";
                    foreach (var i in bonusLots)
                    {
                        if (r < i.LotValue)
                        {
                            role = i.Role;
                            Debug.Log($"i.Role {i.Role}");
                            return;
                        }

                        r -= i.LotValue;
                    }
                    Debug.Log($"i.Role X");
                    bonus = "";
                    role = "";
                }
                break;
        }
    }

}
