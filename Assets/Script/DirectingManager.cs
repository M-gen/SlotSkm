using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectingManager : MonoBehaviour
{
    [SerializeField]
    SlotCore slotCore;

    [SerializeField]
    StageEffect stageEffect;

    public List<string> comboDirectionName = new List<string>();
    public string directionName = "";

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void SettingDirection()
    {
        if ( (slotCore.longGame.viewStatus == SlotCoreLongGame.ViewStatus.NormalComboGame) && (comboDirectionName.Count==0))
        {
            slotCore.longGame.viewStatus = SlotCoreLongGame.ViewStatus.Normal;
        }

        switch (slotCore.longGame.viewStatus)
        {
            case SlotCoreLongGame.ViewStatus.Normal:
                // òAë±ââèoÇ™Ç–Ç¬ÇÊÇ§Ç»ÇÁÇªÇÍÇé¿çsÇµÅAÇªÇ§Ç≈Ç»Ç¢Ç»ÇÁí èÌÇÃââèoÇëIíËÇ∑ÇÈ
                if (slotCore.oneGame.flagBounus != "")
                {
                    // É{Å[ÉiÉXämíËéûÇÕÇŸÇ⁄ÅAòAë±ââèoÇçsÇ§
                    var v = Lottery.LotteryBase.DefaultLottery(new int[] {
                                GetLotStringValue( 1,   10,  20,  30) ,
                                GetLotStringValue( 1,  100,  90,  90)
                            });
                    if (v == 1) LotComboDirection();
                    else LotNormalDirection();
                }
                else
                {
                    switch( slotCore.oneGame.flagRole )
                    {
                        default:
                            {
                                LotNormalDirection();
                            }
                            break;
                        case "rep":
                            {
                                LotNormalDirection();
                            }
                            break;
                        case "bell":
                            {
                                var v = Lottery.LotteryBase.DefaultLottery(new int[] {
                                    GetLotStringValue( 100, 1, 1, 1) ,
                                    GetLotStringValue(   3, 1, 1, 1)
                                });
                                if (v == 1) LotComboDirection();
                                else LotNormalDirection();
                            }
                            break;
                        case "suika":
                            {
                                var v = Lottery.LotteryBase.DefaultLottery(new int[] {
                                    GetLotStringValue( 100, 1, 1, 1) ,
                                    GetLotStringValue(  30, 1, 1, 1)
                                });
                                if (v == 1) LotComboDirection();
                                else LotNormalDirection();
                            }
                            break;
                        case "chery":
                            {
                                var v = Lottery.LotteryBase.DefaultLottery(new int[] {
                                    GetLotStringValue( 100, 1, 1, 1) ,
                                    GetLotStringValue(  40, 1, 1, 1)
                                });
                                if (v == 1) LotComboDirection();
                                else LotNormalDirection();
                            }
                            break;
                    }
                }
                break;

            case SlotCoreLongGame.ViewStatus.NormalComboGame:
                directionName = comboDirectionName[0];
                comboDirectionName.RemoveAt(0);
                break;

            case SlotCoreLongGame.ViewStatus.BonusFixShowIn:
                directionName = "bonus_fix_in";
                break;

            case SlotCoreLongGame.ViewStatus.BonusFixShow:
                directionName = "bonus_fix";
                break;

            case SlotCoreLongGame.ViewStatus.BonusFixShowAndBonusTypeShow:
                switch (slotCore.oneGame.flagBounus)
                {
                    case "Big-r7":
                        switch(directionName)
                        {
                            default:
                                directionName = "bonus_fix_big_r7";
                                break;
                            case "bonus_fix":
                                directionName = "bonus_fix_big_r7_in";
                                break;
                        }
                        break;
                    case "Big-b7":
                        switch (directionName)
                        {
                            default:
                                directionName = "bonus_fix_big_b7";
                                break;
                            case "bonus_fix":
                                directionName = "bonus_fix_big_b7_in";
                                break;
                        }
                        break;
                    case "Reg":
                        switch (directionName)
                        {
                            default:
                                directionName = "bonus_fix_reg";
                                break;
                            case "bonus_fix":
                                directionName = "bonus_fix_reg_in";
                                break;
                        }
                        break;
                }
                break;

            case SlotCoreLongGame.ViewStatus.BonusGame:
                switch( slotCore.longGame.bonusTypeName )
                {
                    case "Big-r7":
                        directionName = "bonus_game_r7";
                        break;
                    case "Big-b7":
                        directionName = "bonus_game_b7";
                        break;
                    case "Reg":
                        directionName = "bonus_game_reg";
                        break;
                }
                break;

            case SlotCoreLongGame.ViewStatus.BonusGameEnd:
                stageEffect.bonusOutCoin = slotCore.longGame.bonusOutCoin;
                directionName = "bonus_end";
                break;
        }

        if (directionName == "") directionName = "xxxx";
        Debug.Log($"SettingDirection {directionName} {slotCore.longGame.status} {slotCore.longGame.viewStatus}");
        stageEffect.SetDirection(directionName);
    }

    public void LotNormalDirection()
    {
        switch ( slotCore.oneGame.flagRole)
        {
            default:
                switch (Lottery.LotteryBase.DefaultLottery(new int[] {
                                            GetLotStringValue( 100, 100, 100, 100) ,
                                            GetLotStringValue(   3,  10,  10,  10) ,
                                            GetLotStringValue(  30,  30,  30,  30)   // ÉyÉìÉ^Éuââèo
                                        }))
                {
                    default:
                        {
                            switch (Lottery.LotteryBase.DefaultLottery(new int[] {
                                            GetLotStringValue( 100, 1, 1, 1) , //
                                            GetLotStringValue(   7, 1, 1, 1) , // ñãÉKÉZ ï¬Ç‹ÇÁÇ»Ç¢
                                            GetLotStringValue(   3, 1, 1, 1) , // ñãÉKÉZ ï¬Ç‹ÇÈ
                                        }))
                            {
                                default: directionName = "normal_1"; break;
                                case 1: directionName = "normal_maku_gase_1"; break;
                                case 2: directionName = "normal_maku_gase_2"; break;
                            }
                        }
                        break;
                    case 1: directionName = "normal_image_hatena_3"; break;
                    case 2: directionName = "draw_0_2"; break;
                }
                break;
            case "rep":
                switch (Lottery.LotteryBase.DefaultLottery(new int[] {
                                            GetLotStringValue(  50, 100, 100, 100) ,
                                            GetLotStringValue( 100,  10,  10,  10) ,
                                            GetLotStringValue( 100,  10,  10,  10) ,
                                            GetLotStringValue( 100,  10,  10,  10) ,
                                            GetLotStringValue(  30,  30,  30,  30)   // ÉyÉìÉ^Éuââèo
                                        }))
                {
                    default:
                        {
                            switch (Lottery.LotteryBase.DefaultLottery(new int[] {
                                            GetLotStringValue( 100, 1, 1, 1) , //
                                            GetLotStringValue(   7, 1, 1, 1) , // ñãÉKÉZ ï¬Ç‹ÇÁÇ»Ç¢
                                            GetLotStringValue(   3, 1, 1, 1) Å@// ñãÉKÉZ ï¬Ç‹ÇÈ
                                        }))
                            {
                                default: directionName = "normal_1";           break;
                                case 1:  directionName = "normal_maku_gase_1"; break;
                                case 2:  directionName = "normal_maku_gase_2"; break;
                            }
                        }
                        break;
                    case 1: directionName = "normal_image_makura_ao_3"; break;
                    case 2: directionName = "normal_image_makura_ao_2"; break;
                    case 3: directionName = "normal_image_hatena_3";    break;
                    case 4: directionName = "draw_0_2"; break;
                }
                break;
            case "bell":
                switch (Lottery.LotteryBase.DefaultLottery(new int[] {
                                            GetLotStringValue(  50, 100, 100, 100) ,
                                            GetLotStringValue( 100,  10,  10,  10) ,
                                            GetLotStringValue( 100,  10,  10,  10) ,
                                            GetLotStringValue( 100,  10,  10,  10) ,
                                            GetLotStringValue(  30,  30,  30,  30)   // ÉyÉìÉ^Éuââèo
                                        }))
                {
                    default:
                        {
                            switch (Lottery.LotteryBase.DefaultLottery(new int[] {
                                            GetLotStringValue( 100, 1, 1, 1) , //
                                            GetLotStringValue(   7, 1, 1, 1) , // ñãÉKÉZ ï¬Ç‹ÇÁÇ»Ç¢
                                            GetLotStringValue(   3, 1, 1, 1) Å@// ñãÉKÉZ ï¬Ç‹ÇÈ
                                        }))
                            {
                                default: directionName = "normal_1"; break;
                                case 1: directionName = "normal_maku_gase_1"; break;
                                case 2: directionName = "normal_maku_gase_2"; break;
                            }
                        }
                        break;
                    case 1: directionName = "normal_image_makura_ki_3"; break;
                    case 2: directionName = "normal_image_makura_ki_2"; break;
                    case 3: directionName = "normal_image_hatena_3"; break;
                    case 4: directionName = "draw_0_2"; break;
                }
                break;
            case "suika":
                switch (Lottery.LotteryBase.DefaultLottery(new int[] {
                                            GetLotStringValue(  30, 100, 100, 100) ,
                                            GetLotStringValue( 100,  10,  10,  10) ,
                                            GetLotStringValue( 100,  10,  10,  10) ,
                                            GetLotStringValue(  40,  10,  10,  10) ,
                                            GetLotStringValue(  50,  50,  50,  50)   // ÉyÉìÉ^Éuââèo
                                        }))
                {
                    default:
                        {
                            switch (Lottery.LotteryBase.DefaultLottery(new int[] {
                                            GetLotStringValue( 100, 1, 1, 1) , //
                                            GetLotStringValue(   7, 1, 1, 1) , // ñãÉKÉZ ï¬Ç‹ÇÁÇ»Ç¢
                                            GetLotStringValue(   3, 1, 1, 1) Å@// ñãÉKÉZ ï¬Ç‹ÇÈ
                                        }))
                            {
                                default: directionName = "normal_1"; break;
                                case 1: directionName = "normal_maku_gase_1"; break;
                                case 2: directionName = "normal_maku_gase_2"; break;
                            }
                        }
                        break;
                    case 1: directionName = "normal_image_makura_midori_3"; break;
                    case 2: directionName = "normal_image_makura_midori_2"; break;
                    case 3: directionName = "normal_image_hatena_3"; break;
                    case 4: directionName = "draw_0_2"; break;
                }
                break;
            case "chery":
                switch (Lottery.LotteryBase.DefaultLottery(new int[] {
                                            GetLotStringValue(  20, 100, 100, 100) ,
                                            GetLotStringValue( 100,  10,  10,  10) ,
                                            GetLotStringValue( 100,  10,  10,  10) ,
                                            GetLotStringValue(  20,  10,  10,  10) ,
                                            GetLotStringValue(  60,  60,  60,  60)   // ÉyÉìÉ^Éuââèo
                                        }))
                {
                    default:
                        {
                            switch (Lottery.LotteryBase.DefaultLottery(new int[] {
                                            GetLotStringValue( 100, 1, 1, 1) , //
                                            GetLotStringValue(   7, 1, 1, 1) , // ñãÉKÉZ ï¬Ç‹ÇÁÇ»Ç¢
                                            GetLotStringValue(   3, 1, 1, 1) Å@// ñãÉKÉZ ï¬Ç‹ÇÈ
                                        }))
                            {
                                default: directionName = "normal_1"; break;
                                case 1: directionName = "normal_maku_gase_1"; break;
                                case 2: directionName = "normal_maku_gase_2"; break;
                            }
                        }
                        break;
                    case 1: directionName = "normal_image_makura_pink_3"; break;
                    case 2: directionName = "normal_image_makura_pink_2"; break;
                    case 3: directionName = "normal_image_hatena_3"; break;
                    case 4: directionName = "draw_0_2"; break;
                }
                break;
        }
    }

    public void LotComboDirection()
    {
        comboDirectionName.Clear();

        System.Func<int> getGameCount_group_1 = () =>
        {
            var gameCount = Lottery.LotteryBase.DefaultLottery(new int[] {
                        GetLotStringValue(   0,    0,    0,    0) , // 0
                        GetLotStringValue(   0,    0,    0,    0) ,
                        GetLotStringValue(  10,    0,    0,    0) ,
                        GetLotStringValue( 100,   10,   10,   10) , // 3
                        GetLotStringValue(  80,   50,   50,   50) ,
                        GetLotStringValue(  60,  100,  100,  100) ,
                        GetLotStringValue(  40,   80,   80,   80) ,
                        GetLotStringValue(  30,   60,   60,   60) ,
                        GetLotStringValue(  20,   40,   40,   40) ,
                        GetLotStringValue(  10,   20,   20,   20) ,
                        GetLotStringValue(   9,   10,   10,   10) , // 10
                        GetLotStringValue(   8,    9,    9,    9) ,
                        GetLotStringValue(   7,    8,    8,    8) ,
                        GetLotStringValue(   6,    7,    7,    7)
                    });
            return gameCount;
        };

        System.Func<int> getGameCount_group_2 = () =>
        {
            var gameCount = Lottery.LotteryBase.DefaultLottery(new int[] {
                        GetLotStringValue(   0,    0,    0,    0) , // 0
                        GetLotStringValue(   0,    0,    0,    0) ,
                        GetLotStringValue(  10,    0,    0,    0) ,
                        GetLotStringValue( 100,   10,   10,   10) , // 3
                        GetLotStringValue(  80,   50,   50,   50) ,
                        GetLotStringValue(  60,  100,  100,  100) ,
                        GetLotStringValue(  40,   80,   80,   80) ,
                        GetLotStringValue(  30,   60,   60,   60) ,
                        GetLotStringValue(  20,   40,   40,   40) ,
                        GetLotStringValue(  10,   20,   20,   20) ,
                        GetLotStringValue(   9,   10,   10,   10) , // 10
                        GetLotStringValue(   8,    9,    9,    9) ,
                        GetLotStringValue(   7,    8,    8,    8) ,
                        GetLotStringValue(   6,    7,    7,    7)
                    });
            return gameCount;
        };

        System.Func<int> getGameCount_group_3 = () =>
        {
            var gameCount = Lottery.LotteryBase.DefaultLottery(new int[] {
                        GetLotStringValue(  10,    0,    0,    0) ,
                        GetLotStringValue( 100,   10,   10,   10) ,
                        GetLotStringValue(  80,   50,   50,   50) ,
                        GetLotStringValue(  60,  100,  100,  100) , // 3
                        GetLotStringValue(  40,   80,   80,   80) ,
                        GetLotStringValue(  30,   60,   60,   60) ,
                        GetLotStringValue(  20,   40,   40,   40) ,
                        GetLotStringValue(  10,   20,   20,   20) ,
                        GetLotStringValue(   9,   10,   10,   10) ,
                        GetLotStringValue(   8,    9,    9,    9) ,
                        GetLotStringValue(   7,    8,    8,    8) , // 10
                        GetLotStringValue(   6,    7,    7,    7)
                    });
            return gameCount;
        };

        System.Func<int> getGameCount_group_4 = () =>
        {
            var gameCount = Lottery.LotteryBase.DefaultLottery(new int[] {
                        GetLotStringValue( 100,   10,   10,   10) ,
                        GetLotStringValue(  80,   50,   50,   50) ,
                        GetLotStringValue(  60,  100,  100,  100) ,
                        GetLotStringValue(  40,   80,   80,   80) ,
                        GetLotStringValue(  30,   60,   60,   60) ,
                        GetLotStringValue(  20,   40,   40,   40) ,
                        GetLotStringValue(  10,   20,   20,   20) ,
                        GetLotStringValue(   9,   10,   10,   10) ,
                        GetLotStringValue(   8,    9,    9,    9) ,
                        GetLotStringValue(   7,    8,    8,    8) ,
                        GetLotStringValue(   6,    7,    7,    7)
                    });
            return gameCount;
        };

        System.Func<int> getGameCount_group_5 = () =>
        {
            var gameCount = Lottery.LotteryBase.DefaultLottery(new int[] {
                        GetLotStringValue(   0,    0,    0,    0) ,
                        GetLotStringValue( 100,   10,   10,   10) ,
                        GetLotStringValue(  80,   50,   50,   50) ,
                        GetLotStringValue(  60,  100,  100,  100) ,
                        GetLotStringValue(  40,   80,   80,   80) ,
                        GetLotStringValue(  30,   60,   60,   60) ,
                        GetLotStringValue(  20,   40,   40,   40) ,
                        GetLotStringValue(  10,   20,   20,   20) ,
                        GetLotStringValue(   9,   10,   10,   10) ,
                        GetLotStringValue(   6,    7,    7,    7)
                    });
            return gameCount;
        };

        System.Action AddComboEffect_group_1 = () =>
        {
            var v = Lottery.LotteryBase.DefaultLottery(new int[] {
                        GetLotStringValue( 100,  100, 100, 100) , // ÇªÇÃÇ‹Ç‹
                        GetLotStringValue(  30,   20,  20,  20) , // ñãÇ™ï¬Ç‹ÇÈ
                        GetLotStringValue(  10,   15,  15,  15) , // ñãÇ™ï¬Ç‹ÇÈ ëÊ2í‚é~ÉçÉS
                        GetLotStringValue(  10,   15,  15,  15)   // ñãÇ™ï¬Ç‹ÇÈ ëÊ3í‚é~ÉçÉS
                    });

            switch (v)
            {
                default:
                    comboDirectionName.Add("group_1");
                    break;
                case 1:
                    comboDirectionName.Add("group_1_maku_1");
                    break;
                case 2:
                    comboDirectionName.Add("group_1_maku_3");
                    break;
                case 3:
                    comboDirectionName.Add("group_1_maku_4");
                    break;
            }
        };

        System.Action AddComboEffect_group_2 = () =>
        {
            var v = Lottery.LotteryBase.DefaultLottery(new int[] {
                        GetLotStringValue( 100,  100, 100, 100) , // ÇªÇÃÇ‹Ç‹
                        GetLotStringValue(  30,   20,  20,  20) , // ñãÇ™ï¬Ç‹ÇÈ
                        GetLotStringValue(  10,   15,  15,  15) , // ñãÇ™ï¬Ç‹ÇÈ ëÊ2í‚é~ÉçÉS
                        GetLotStringValue(  10,   15,  15,  15)   // ñãÇ™ï¬Ç‹ÇÈ ëÊ3í‚é~ÉçÉS
                    });

            switch (v)
            {
                default:
                    comboDirectionName.Add("group_2");
                    break;
                case 1:
                    comboDirectionName.Add("group_2_maku_1");
                    break;
                case 2:
                    comboDirectionName.Add("group_2_maku_3");
                    break;
                case 3:
                    comboDirectionName.Add("group_2_maku_4");
                    break;
            }
        };

        System.Action AddComboEffect_group_3 = () =>
        {
            var v = Lottery.LotteryBase.DefaultLottery(new int[] {
                        GetLotStringValue( 100,  100, 100, 100) , // ÇªÇÃÇ‹Ç‹
                        GetLotStringValue(  30,   20,  20,  20) , // ñãÇ™ï¬Ç‹ÇÈ
                        GetLotStringValue(  10,   15,  15,  15) , // ñãÇ™ï¬Ç‹ÇÈ ëÊ2í‚é~ÉçÉS
                        GetLotStringValue(  10,   15,  15,  15)   // ñãÇ™ï¬Ç‹ÇÈ ëÊ3í‚é~ÉçÉS
                    });

            switch (v)
            {
                default:
                    comboDirectionName.Add("group_3");
                    break;
                case 1:
                    comboDirectionName.Add("group_3_maku_1");
                    break;
                case 2:
                    comboDirectionName.Add("group_3_maku_3");
                    break;
                case 3:
                    comboDirectionName.Add("group_3_maku_4");
                    break;
            }
        };

        System.Action AddComboEffect_group_4 = () =>
        {
            var v = Lottery.LotteryBase.DefaultLottery(new int[] {
                        GetLotStringValue( 100,  100, 100, 100) , // ÇªÇÃÇ‹Ç‹
                        GetLotStringValue(  30,   20,  20,  20) , // ñãÇ™ï¬Ç‹ÇÈ
                        GetLotStringValue(  10,   15,  15,  15) , // ñãÇ™ï¬Ç‹ÇÈ ëÊ2í‚é~ÉçÉS
                        GetLotStringValue(  10,   15,  15,  15)   // ñãÇ™ï¬Ç‹ÇÈ ëÊ3í‚é~ÉçÉS
                    });

            switch (v)
            {
                default:
                    comboDirectionName.Add("group_4");
                    break;
                case 1:
                    comboDirectionName.Add("group_4_maku_1");
                    break;
                case 2:
                    comboDirectionName.Add("group_4_maku_3");
                    break;
                case 3:
                    comboDirectionName.Add("group_4_maku_4");
                    break;
            }
        };

        System.Action AddComboEffect_group_5 = () =>
        {
            var v = Lottery.LotteryBase.DefaultLottery(new int[] {
                        GetLotStringValue( 100,  100, 100, 100) , // ÇªÇÃÇ‹Ç‹
                        GetLotStringValue(  30,   20,  20,  20) , // ñãÇ™ï¬Ç‹ÇÈ
                        GetLotStringValue(  10,   15,  15,  15) , // ñãÇ™ï¬Ç‹ÇÈ ëÊ2í‚é~ÉçÉS
                        GetLotStringValue(  10,   15,  15,  15)   // ñãÇ™ï¬Ç‹ÇÈ ëÊ3í‚é~ÉçÉS
                    });

            switch (v)
            {
                default:
                    comboDirectionName.Add("group_5");
                    break;
                case 1:
                    comboDirectionName.Add("group_5_maku_1");
                    break;
                case 2:
                    comboDirectionName.Add("group_5_maku_3");
                    break;
                case 3:
                    comboDirectionName.Add("group_5_maku_4");
                    break;
            }
        };

        switch (Lottery.LotteryBase.DefaultLottery(new int[] {
            GetLotStringValue(100,   50,  70,  85) ,
            GetLotStringValue( 60,  500, 300, 150) ,
            GetLotStringValue( 35,  300, 200,  75) ,
            GetLotStringValue(  5,  200, 100,  50) ,
            GetLotStringValue(  1,  100,  50,  25)
        }))
        {
            case 0:
                {
                    comboDirectionName.Add("group_in");

                    var num = getGameCount_group_1();
                    for (var i = 0; i < num; i++) AddComboEffect_group_1();

                    comboDirectionName.Add("battle_in_group_1");
                    comboDirectionName.Add("battle_1");
                }
                break;
            case 1:
                {
                    comboDirectionName.Add("group_in");

                    var num = getGameCount_group_1();
                    for (var i = 0; i < num; i++) AddComboEffect_group_1();
                    comboDirectionName.Add("group_cutin_1");

                    num = getGameCount_group_2();
                    for (var i = 0; i < num; i++) AddComboEffect_group_2();

                    comboDirectionName.Add("battle_in_group_2");
                    comboDirectionName.Add("battle_1");
                }
                break;
            case 2:
                {
                    comboDirectionName.Add("group_in");

                    var num = getGameCount_group_1();
                    for (var i = 0; i < num; i++) AddComboEffect_group_1();
                    comboDirectionName.Add("group_cutin_1");

                    num = getGameCount_group_2();
                    for (var i = 0; i < num; i++) AddComboEffect_group_2();
                    comboDirectionName.Add("group_cutin_2");

                    num = getGameCount_group_3();
                    for (var i = 0; i < num; i++) AddComboEffect_group_3();

                    comboDirectionName.Add("battle_in_group_3");
                    comboDirectionName.Add("battle_1");
                }
                break;
            case 3:
                {
                    comboDirectionName.Add("group_in");

                    var num = getGameCount_group_1();
                    for (var i = 0; i < num; i++) AddComboEffect_group_1();
                    comboDirectionName.Add("group_cutin_1");

                    num = getGameCount_group_2();
                    for (var i = 0; i < num; i++) AddComboEffect_group_2();
                    comboDirectionName.Add("group_cutin_2");

                    num = getGameCount_group_3();
                    for (var i = 0; i < num; i++) AddComboEffect_group_3();
                    comboDirectionName.Add("group_cutin_3");

                    num = getGameCount_group_4();
                    for (var i = 0; i < num; i++) AddComboEffect_group_4();

                    comboDirectionName.Add("battle_in_group_4");
                    comboDirectionName.Add("battle_1");
                }
                break;
            case 4:
                {
                    comboDirectionName.Add("group_in");

                    var num = getGameCount_group_1();
                    for (var i = 0; i < num; i++) AddComboEffect_group_1();
                    comboDirectionName.Add("group_cutin_1");

                    num = getGameCount_group_2();
                    for (var i = 0; i < num; i++) AddComboEffect_group_2();
                    comboDirectionName.Add("group_cutin_2");

                    num = getGameCount_group_3();
                    for (var i = 0; i < num; i++) AddComboEffect_group_3();
                    comboDirectionName.Add("group_cutin_3");

                    num = getGameCount_group_4();
                    for (var i = 0; i < num; i++) AddComboEffect_group_4();
                    comboDirectionName.Add("group_cutin_4");

                    num = getGameCount_group_5();
                    for (var i = 0; i < num; i++) AddComboEffect_group_5();

                    comboDirectionName.Add("battle_in_group_5");
                    comboDirectionName.Add("battle_1");
                }
                break;
        }

        slotCore.longGame.viewStatus = SlotCoreLongGame.ViewStatus.NormalComboGame;
        directionName = comboDirectionName[0];
        comboDirectionName.RemoveAt(0);

        stageEffect.SetDirection(directionName);
    }

    public int GetLotStringValue(int normal, int r7, int b7, int reg)
    {
        var lotStrong = normal;
        switch ( slotCore.oneGame.flagBounus )
        {
            case "Big-r7":
                lotStrong = r7;
                break;
            case "Big-b7":
                lotStrong = b7;
                break;
            case "Reg":
                lotStrong = reg;
                break;
        }

        return lotStrong;
    }

    public void BonusIn( string bonusTypeName )
    {
        stageEffect.BonusIn(bonusTypeName);
    }
}
