using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageLot
{
    StageEffect stageEffect;
    public List<string> comboEffect = new List<string>();

    string role = "";
    string bonus = "";

    public StageLot(StageEffect stageEffect)
    {
        this.stageEffect = stageEffect;
    }

    public void LotBase(string role, string bonus, SlotCoreScript.GameStage gameStage, string bonusType)
    {
        this.role = role;
        this.bonus = bonus;

        // ボーナス中であるが、演出状態がボーナス出ない場合
        if (gameStage == SlotCoreScript.GameStage.Bonus && (stageEffect.status != "bonus_game"))
        {
            comboEffect.Clear();
            comboEffect.Add("bonus_game");
            stageEffect.status = "bonus_game";

            Debug.Log($"LotBase {bonusType}");

            switch (bonusType)
            {
                case "Reg":
                    stageEffect.SetDirection("bonus_game_reg");
                    break;
                case "Big-b7":
                    stageEffect.SetDirection("bonus_game_b7");
                    break;
                case "Big-r7":
                    stageEffect.SetDirection("bonus_game_r7");
                    break;
            }
            return;
        }

        // ボーナス終了
        //if (gameStage != SlotCoreScript.GameStage.Bonus && 
        //    ((stageEffect.status == "bonus_game_reg") || (stageEffect.status == "bonus_game_b7") || (stageEffect.status == "bonus_game_r7")) )
        if (gameStage != SlotCoreScript.GameStage.Bonus && stageEffect.status == "bonus_game" )
        {
            comboEffect.Clear();
            comboEffect.Add("bonus_end");
            stageEffect.status = "";
        }

        if (comboEffect.Count == 0)
        {
            switch (stageEffect.status)
            {
                default:
                    {
                        //comboEffect.Add("bonus_end");
                        //comboEffect.Add("normal_1");
                        //comboEffect.Add("battle_in_group_5");
                        //comboEffect.Add("battle_1");

                        if (bonus != "")
                        {
                            // ボーナス確定時はほぼ、連続演出を行う
                            var v = Lottery.LotteryBase.DefaultLottery(new int[] {
                                GetLotStringValue( 100,   10,  20,  30) , 
                                GetLotStringValue(  10,  100,  90,  90) 
                            });
                            if ( v==1 )
                            {
                                LotComboEffect();
                            }
                        }
                        else
                        {
                            switch (role)
                            {
                                case "bell":
                                    {
                                        var v = Lottery.LotteryBase.DefaultLottery(new int[] {
                                            GetLotStringValue( 100, 1, 1, 1) ,
                                            GetLotStringValue(   3, 1, 1, 1)
                                        });
                                        if (v == 1)
                                        {
                                            LotComboEffect();
                                        }
                                    }
                                    break;
                                case "suika":
                                    {
                                        var v = Lottery.LotteryBase.DefaultLottery(new int[] {
                                            GetLotStringValue( 60, 1, 1, 1) ,
                                            GetLotStringValue( 40, 1, 1, 1)
                                        });
                                        if (v == 1)
                                        {
                                            LotComboEffect();
                                        }
                                    }
                                    break;
                                case "chery":
                                    {
                                        var v = Lottery.LotteryBase.DefaultLottery(new int[] {
                                            GetLotStringValue( 30, 1, 1, 1) ,
                                            GetLotStringValue( 70, 1, 1, 1)
                                        });
                                        if (v == 1)
                                        {
                                            LotComboEffect();
                                        }
                                    }
                                    break;
                            }
                        }

                    }
                    break;
                case "bonus_fix_in":
                    {
                        comboEffect.Add("bonus_fix_in");
                        stageEffect.status = "bonus_fix";
                    }
                    break;
                case "bonus_fix":
                    {
                        comboEffect.Add("bonus_fix");

                        switch( bonus)
                        {
                            case "Big-r7":
                                stageEffect.status = "bonus_fix_big_r7_in";
                                break;
                            case "Big-b7":
                                stageEffect.status = "bonus_fix_big_b7_in";
                                break;
                            case "Reg":
                                stageEffect.status = "bonus_fix_reg_in";
                                break;
                        }
                    }
                    break;
                case "bonus_fix_big_r7_in":
                    {
                        comboEffect.Add("bonus_fix_big_r7_in");
                        stageEffect.status = "bonus_fix_big_r7";
                    }
                    break;
                case "bonus_fix_big_r7":
                    {
                        comboEffect.Add("bonus_fix_big_r7");
                    }
                    break;
                case "bonus_fix_big_b7_in":
                    {
                        comboEffect.Add("bonus_fix_big_b7_in");
                        stageEffect.status = "bonus_fix_big_b7";
                    }
                    break;
                case "bonus_fix_big_b7":
                    {
                        comboEffect.Add("bonus_fix_big_b7");
                    }
                    break;
                case "bonus_fix_reg_in":
                    {
                        comboEffect.Add("bonus_fix_reg_in");
                        stageEffect.status = "bonus_fix_reg";
                    }
                    break;
                case "bonus_fix_reg":
                    {
                        comboEffect.Add("bonus_fix_reg");
                    }
                    break;
                case "bonus_game":
                    {
                        switch (bonusType)
                        {
                            case "Reg":
                                comboEffect.Add("bonus_game_reg");
                                break;
                            case "Big-b7":
                                comboEffect.Add("bonus_game_b7");
                                break;
                            case "Big-r7":
                                comboEffect.Add("bonus_game_r7");
                                break;
                        }
                    }
                    break;
            }
        }

        if (comboEffect.Count > 0)
        {
            var tmp = comboEffect[0];
            comboEffect.RemoveAt(0);
            stageEffect.SetDirection(tmp);
            return;
        }

        //SetDirection("group_1_maku_4");
        //return;

        if (role == "rep")
        {
            var r = Random.Range(0, 6);
            switch (r)
            {
                default:
                    {
                        var v = Lottery.LotteryBase.DefaultLottery(new int[] {
                                            GetLotStringValue( 100, 1, 1, 1) , //
                                            GetLotStringValue(   7, 1, 1, 1) , // 幕ガセ 閉まらない
                                            GetLotStringValue(   3, 1, 1, 1) 　// 幕ガセ 閉まる
                                        });
                        switch (v)
                        {
                            default: stageEffect.SetDirection("normal_1"); break;
                            case 1: stageEffect.SetDirection("normal_maku_gase_1"); break;
                            case 2: stageEffect.SetDirection("normal_maku_gase_2"); break;
                        }
                    }
                    break;
                case 0: stageEffect.SetDirection("normal_image_makura_ao_3"); break;
                case 1: stageEffect.SetDirection("normal_image_makura_ao_2"); break;
                case 2: stageEffect.SetDirection("normal_image_hatena_3"); break;
            }
        }
        else if (role == "bell")
        {
            var r = Random.Range(0, 6);
            switch (r)
            {
                default:
                    {
                        var v = Lottery.LotteryBase.DefaultLottery(new int[] {
                                            GetLotStringValue( 100, 1, 1, 1) , //
                                            GetLotStringValue(  15, 1, 1, 1) , // 幕ガセ 閉まらない
                                            GetLotStringValue(  10, 1, 1, 1) 　// 幕ガセ 閉まる
                                        });
                        switch (v)
                        {
                            default: stageEffect.SetDirection("normal_1"); break;
                            case 1: stageEffect.SetDirection("normal_maku_gase_1"); break;
                            case 2: stageEffect.SetDirection("normal_maku_gase_2"); break;
                        }
                    }
                    break;
                case 0: stageEffect.SetDirection("normal_image_makura_ki_3"); break;
                case 1: stageEffect.SetDirection("normal_image_makura_ki_2"); break;
                case 2: stageEffect.SetDirection("normal_image_hatena_3"); break;
            }
        }
        else if (role == "suika")
        {
            var r = Random.Range(0, 4);
            switch (r)
            {
                default:
                    {
                        var v = Lottery.LotteryBase.DefaultLottery(new int[] {
                                            GetLotStringValue( 100, 1, 1, 1) , //
                                            GetLotStringValue(  10, 1, 1, 1) , // 幕ガセ 閉まらない
                                            GetLotStringValue(  18, 1, 1, 1) 　// 幕ガセ 閉まる
                                        });
                        switch (v)
                        {
                            default: stageEffect.SetDirection("normal_1"); break;
                            case 1: stageEffect.SetDirection("normal_maku_gase_1"); break;
                            case 2: stageEffect.SetDirection("normal_maku_gase_2"); break;
                        }
                    }
                    break;
                case 0: stageEffect.SetDirection("normal_image_makura_midori_3"); break;
                case 1: stageEffect.SetDirection("normal_image_makura_midori_2"); break;
            }
        }
        else if (role == "chery")
        {
            var r = Random.Range(0, 4);
            switch (r)
            {
                default:
                    {
                        var v = Lottery.LotteryBase.DefaultLottery(new int[] {
                                            GetLotStringValue(  50, 1, 1, 1) , //
                                            GetLotStringValue(  20, 1, 1, 1) , // 幕ガセ 閉まらない
                                            GetLotStringValue(  30, 1, 1, 1) 　// 幕ガセ 閉まる
                                        });
                        switch (v)
                        {
                            default: stageEffect.SetDirection("normal_1"); break;
                            case 1: stageEffect.SetDirection("normal_maku_gase_1"); break;
                            case 2: stageEffect.SetDirection("normal_maku_gase_2"); break;
                        }
                    }
                    break;
                case 0: stageEffect.SetDirection("normal_image_makura_pink_3"); break;
                case 1: stageEffect.SetDirection("normal_image_makura_pink_2"); break;
            }
        }
        else
        {
            var v = Lottery.LotteryBase.DefaultLottery(new int[] {
                                            GetLotStringValue( 100, 1, 1, 1) , //
                                            GetLotStringValue(  20, 1, 1, 1) , // ?
                                            GetLotStringValue(  20, 1, 1, 1) , // 幕ガセ 閉まらない
                                            GetLotStringValue(  10, 1, 1, 1) 　// 幕ガセ 閉まる
                                        });
            switch (v)
            {
                default: stageEffect.SetDirection("normal_1"); break;
                case 1: stageEffect.SetDirection("normal_image_hatena_3"); break;
                case 2: stageEffect.SetDirection("normal_maku_gase_1"); break;
                case 3: stageEffect.SetDirection("normal_maku_gase_2"); break;
            }
        }
    }

    public void LotComboEffect()
    {
        Debug.Log($"LotComboEffect {role} {bonus}");

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
                        GetLotStringValue( 100,  100, 100, 100) , // そのまま
                        GetLotStringValue(  30,   20,  20,  20) , // 幕が閉まる
                        GetLotStringValue(  10,   15,  15,  15) , // 幕が閉まる 第2停止ロゴ
                        GetLotStringValue(  10,   15,  15,  15)   // 幕が閉まる 第3停止ロゴ
                    });

            switch (v)
            {
                default:
                    comboEffect.Add("group_1");
                    break;
                case 1:
                    comboEffect.Add("group_1_maku_1");
                    break;
                case 2:
                    comboEffect.Add("group_1_maku_3");
                    break;
                case 3:
                    comboEffect.Add("group_1_maku_4");
                    break;
            }
        };

        System.Action AddComboEffect_group_2 = () =>
        {
            var v = Lottery.LotteryBase.DefaultLottery(new int[] {
                        GetLotStringValue( 100,  100, 100, 100) , // そのまま
                        GetLotStringValue(  30,   20,  20,  20) , // 幕が閉まる
                        GetLotStringValue(  10,   15,  15,  15) , // 幕が閉まる 第2停止ロゴ
                        GetLotStringValue(  10,   15,  15,  15)   // 幕が閉まる 第3停止ロゴ
                    });

            switch (v)
            {
                default:
                    comboEffect.Add("group_2");
                    break;
                case 1:
                    comboEffect.Add("group_2_maku_1");
                    break;
                case 2:
                    comboEffect.Add("group_2_maku_3");
                    break;
                case 3:
                    comboEffect.Add("group_2_maku_4");
                    break;
            }
        };

        System.Action AddComboEffect_group_3 = () =>
        {
            var v = Lottery.LotteryBase.DefaultLottery(new int[] {
                        GetLotStringValue( 100,  100, 100, 100) , // そのまま
                        GetLotStringValue(  30,   20,  20,  20) , // 幕が閉まる
                        GetLotStringValue(  10,   15,  15,  15) , // 幕が閉まる 第2停止ロゴ
                        GetLotStringValue(  10,   15,  15,  15)   // 幕が閉まる 第3停止ロゴ
                    });

            switch (v)
            {
                default:
                    comboEffect.Add("group_3");
                    break;
                case 1:
                    comboEffect.Add("group_3_maku_1");
                    break;
                case 2:
                    comboEffect.Add("group_3_maku_3");
                    break;
                case 3:
                    comboEffect.Add("group_3_maku_4");
                    break;
            }
        };

        System.Action AddComboEffect_group_4 = () =>
        {
            var v = Lottery.LotteryBase.DefaultLottery(new int[] {
                        GetLotStringValue( 100,  100, 100, 100) , // そのまま
                        GetLotStringValue(  30,   20,  20,  20) , // 幕が閉まる
                        GetLotStringValue(  10,   15,  15,  15) , // 幕が閉まる 第2停止ロゴ
                        GetLotStringValue(  10,   15,  15,  15)   // 幕が閉まる 第3停止ロゴ
                    });

            switch (v)
            {
                default:
                    comboEffect.Add("group_4");
                    break;
                case 1:
                    comboEffect.Add("group_4_maku_1");
                    break;
                case 2:
                    comboEffect.Add("group_4_maku_3");
                    break;
                case 3:
                    comboEffect.Add("group_4_maku_4");
                    break;
            }
        };

        System.Action AddComboEffect_group_5 = () =>
        {
            var v = Lottery.LotteryBase.DefaultLottery(new int[] {
                        GetLotStringValue( 100,  100, 100, 100) , // そのまま
                        GetLotStringValue(  30,   20,  20,  20) , // 幕が閉まる
                        GetLotStringValue(  10,   15,  15,  15) , // 幕が閉まる 第2停止ロゴ
                        GetLotStringValue(  10,   15,  15,  15)   // 幕が閉まる 第3停止ロゴ
                    });

            switch (v)
            {
                default:
                    comboEffect.Add("group_5");
                    break;
                case 1:
                    comboEffect.Add("group_5_maku_1");
                    break;
                case 2:
                    comboEffect.Add("group_5_maku_3");
                    break;
                case 3:
                    comboEffect.Add("group_5_maku_4");
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
                    comboEffect.Add("group_in");

                    var num = getGameCount_group_1();
                    for (var i = 0; i < num; i++) AddComboEffect_group_1();

                    comboEffect.Add("battle_in_group_1");
                    comboEffect.Add("battle_1");
                }
                break;
            case 1:
                {
                    comboEffect.Add("group_in");

                    var num = getGameCount_group_1();
                    for (var i = 0; i < num; i++) AddComboEffect_group_1();
                    comboEffect.Add("group_cutin_1");

                    num = getGameCount_group_2();
                    for (var i = 0; i < num; i++) AddComboEffect_group_2();

                    comboEffect.Add("battle_in_group_2");
                    comboEffect.Add("battle_1");
                }
                break;
            case 2:
                {
                    comboEffect.Add("group_in");

                    var num = getGameCount_group_1();
                    for (var i = 0; i < num; i++) AddComboEffect_group_1();
                    comboEffect.Add("group_cutin_1");

                    num = getGameCount_group_2();
                    for (var i = 0; i < num; i++) AddComboEffect_group_2();
                    comboEffect.Add("group_cutin_2");

                    num = getGameCount_group_3();
                    for (var i = 0; i < num; i++) AddComboEffect_group_3();

                    comboEffect.Add("battle_in_group_3");
                    comboEffect.Add("battle_1");
                }
                break;
            case 3:
                {
                    comboEffect.Add("group_in");

                    var num = getGameCount_group_1();
                    for (var i = 0; i < num; i++) AddComboEffect_group_1();
                    comboEffect.Add("group_cutin_1");

                    num = getGameCount_group_2();
                    for (var i = 0; i < num; i++) AddComboEffect_group_2();
                    comboEffect.Add("group_cutin_2");

                    num = getGameCount_group_3();
                    for (var i = 0; i < num; i++) AddComboEffect_group_3();
                    comboEffect.Add("group_cutin_3");

                    num = getGameCount_group_4();
                    for (var i = 0; i < num; i++) AddComboEffect_group_4();

                    comboEffect.Add("battle_in_group_4");
                    comboEffect.Add("battle_1");
                }
                break;
            case 4:
                {
                    comboEffect.Add("group_in");

                    var num = getGameCount_group_1();
                    for (var i = 0; i < num; i++) AddComboEffect_group_1();
                    comboEffect.Add("group_cutin_1");

                    num = getGameCount_group_2();
                    for (var i = 0; i < num; i++) AddComboEffect_group_2();
                    comboEffect.Add("group_cutin_2");

                    num = getGameCount_group_3();
                    for (var i = 0; i < num; i++) AddComboEffect_group_3();
                    comboEffect.Add("group_cutin_3");

                    num = getGameCount_group_4();
                    for (var i = 0; i < num; i++) AddComboEffect_group_4();
                    comboEffect.Add("group_cutin_4");

                    num = getGameCount_group_5();
                    for (var i = 0; i < num; i++) AddComboEffect_group_5();

                    comboEffect.Add("battle_in_group_5");
                    comboEffect.Add("battle_1");
                }
                break;
        }

    }
    public int GetLotStringValue(int normal, int r7, int b7, int reg)
    {
        var lotStrong = normal;
        switch (bonus)
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
}
