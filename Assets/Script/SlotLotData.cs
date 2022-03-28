using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 抽選にまつわるデータ
public class SlotLotData : MonoBehaviour
{
    [System.Serializable]
    public class BaseLot
    {
        public string Bonus;
        public string Role;
        public int LotValue; // 抽選の重み
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
    SlotCore slotCore;

    int baseLotsMax = 0;
    int bonusInLotsMax = 0;
    int bonusLotsMax = 0;

    private void Start()
    {
        foreach (var i in baseLots)
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

    public void DoBaseLot()
    {
        switch (slotCore.longGame.status)
        {
            case SlotCoreLongGame.Status.Normal:
                {
                    var r = Random.Range(0, baseLotsMax);
                    foreach (var i in baseLots)
                    {
                        if (r < i.LotValue)
                        {
                            slotCore.oneGame.flagBounus = i.Bonus;
                            slotCore.oneGame.flagRole = i.Role;
                            Debug.Log($"DoBaseLot a {slotCore.oneGame.flagRole} {slotCore.oneGame.flagBounus}");
                            return;
                        }

                        r -= i.LotValue;
                    }
                    slotCore.oneGame.flagBounus = "";
                    slotCore.oneGame.flagRole = "";
                }
                break;
            case SlotCoreLongGame.Status.BonusFix:
                {
                    var r = Random.Range(0, bonusInLotsMax);
                    foreach (var i in bonusInLots)
                    {
                        if (r < i.LotValue)
                        {
                            slotCore.oneGame.flagRole = i.Role;
                            Debug.Log($"DoBaseLot b {slotCore.oneGame.flagRole} {slotCore.oneGame.flagBounus}");
                            return;
                        }

                        r -= i.LotValue;
                    }
                    slotCore.oneGame.flagRole = "";
                }
                break;
            case SlotCoreLongGame.Status.BonusGame:
                {
                    var r = Random.Range(0, bonusLotsMax);
                    slotCore.oneGame.flagBounus = "";
                    foreach (var i in bonusLots)
                    {
                        if (r < i.LotValue)
                        {
                            slotCore.oneGame.flagRole = i.Role;
                            return;
                        }

                        r -= i.LotValue;
                    }
                    slotCore.oneGame.flagBounus = "";
                    slotCore.oneGame.flagRole = "";
                }
                break;
        }
        Debug.Log($"DoBaseLot c {slotCore.oneGame.flagRole} {slotCore.oneGame.flagBounus}");
    }

    public BonusStatus GetBonusStatus( string name )
    {
        foreach( var i in bonusType)
        {
            if(i.Name== name)
            {
                return i;
            }
        }
        return null;
    }

}
