using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageEffect : MonoBehaviour
{
    [SerializeField]
    StageBackGround stageBackGround;

    [SerializeField]
    StageBackGroundGroup stageBackGroundGroup;

    [SerializeField]
    CharaSDSakamata charaSDSakamata;

    [SerializeField]
    Maku maku;

    [SerializeField]
    Cutin cutin;

    [SerializeField]
    Battle battle;

    [SerializeField]
    GameObject startScreen;

    public enum DirectionStep
    {
        LeberOn,
        Stop1,
        Stop2,
        Stop3,
    }

    public class DirectionOne
    {
        public DirectionStep directionStep;
        public System.Action Action;
    }

    public class Direction
    {
        public string Name;
        public List<DirectionOne> Dirs = new List<DirectionOne>();
    }

    List<Direction> directions = new List<Direction>();
    Direction targetDirection;

    [SerializeField]
    AudioManager audioManager;

    [System.Serializable]
    public class AudioClipItems
    {
        public string Key;
        public AudioClip Clip;
    }

    [SerializeField]
    AudioClipItems[] AudioClips;

    string role = "";
    string bonus = "";
    string bonusType = "";
    public string status = "";

    StageLot stageLot;
    public int bonusOutCoin = 0;

    void Start()
    {
        SetBackGround("default");

        stageLot = new StageLot(this);
        {
            var dir = new Direction() { Name = "normal_1" };
            dir.Dirs.Add(new DirectionOne()
            {
                directionStep = DirectionStep.LeberOn,
                Action = () =>
                {
                    SetBackGround("default");
                    maku.SetAnimation("");
                    maku.IsLogoShow(false);
                    cutin.SetAnimation("");
                    charaSDSakamata.SetAnimation("walk");
                    battle.SetAnimation("");
                }
            });

            directions.Add(dir);
        }

        {
            var dir = new Direction() { Name = "normal_maku_gase_1" };
            dir.Dirs.Add(new DirectionOne()
            {
                directionStep = DirectionStep.LeberOn,
                Action = () =>
                {
                    SetBackGround("default");
                    maku.SetAnimation("a1");
                    maku.IsLogoShow(false);
                    cutin.SetAnimation("");
                    charaSDSakamata.SetAnimation("walk");
                    battle.SetAnimation("");
                }
            });
            dir.Dirs.Add(new DirectionOne()
            {
                directionStep = DirectionStep.Stop1,
                Action = () =>
                {
                }
            });
            dir.Dirs.Add(new DirectionOne()
            {
                directionStep = DirectionStep.Stop3,
                Action = () =>
                {
                    maku.SetAnimation("");
                }
            });
            directions.Add(dir);
        }

        {
            var dir = new Direction() { Name = "normal_maku_gase_2" };
            dir.Dirs.Add(new DirectionOne()
            {
                directionStep = DirectionStep.LeberOn,
                Action = () =>
                {
                    SetBackGround("default");
                    maku.SetAnimation("close");
                    maku.IsLogoShow(false);
                    cutin.SetAnimation("");
                    charaSDSakamata.SetAnimation("walk");
                    battle.SetAnimation("");
                }
            });
            dir.Dirs.Add(new DirectionOne()
            {
                directionStep = DirectionStep.Stop1,
                Action = () =>
                {
                }
            });
            dir.Dirs.Add(new DirectionOne()
            {
                directionStep = DirectionStep.Stop3,
                Action = () =>
                {
                    maku.SetAnimation("open");
                }
            });
            directions.Add(dir);
        }

        {
            var dir = new Direction() { Name = "normal_image_makura_ao_3" };
            dir.Dirs.Add(new DirectionOne()
            {
                directionStep = DirectionStep.LeberOn,
                Action = () =>
                {
                    SetBackGround("default_stop");
                    maku.SetAnimation("");
                    maku.IsLogoShow(false);
                    cutin.SetAnimation("");
                    charaSDSakamata.SetAnimation("stop_!?");
                    battle.SetAnimation("");
                }
            });
            dir.Dirs.Add(new DirectionOne()
            {
                directionStep = DirectionStep.Stop1,
                Action = () =>
                {
                    SetBackGround("default_stop");
                    charaSDSakamata.SetAnimation("stop_moyamoya");
                }
            });
            dir.Dirs.Add(new DirectionOne()
            {
                directionStep = DirectionStep.Stop3,
                Action = () =>
                {
                    SetBackGround("default_stop");
                    charaSDSakamata.SetAnimation("stop_image_makura_ao");
                }
            });
            directions.Add(dir);
        }

        {
            var dir = new Direction() { Name = "normal_image_makura_ao_2" };
            dir.Dirs.Add(new DirectionOne()
            {
                directionStep = DirectionStep.LeberOn,
                Action = () =>
                {
                    SetBackGround("default_stop");
                    maku.SetAnimation("");
                    maku.IsLogoShow(false);
                    cutin.SetAnimation("");
                    charaSDSakamata.SetAnimation("stop_!?");
                    battle.SetAnimation("");
                }
            });
            dir.Dirs.Add(new DirectionOne()
            {
                directionStep = DirectionStep.Stop1,
                Action = () =>
                {
                    SetBackGround("default_stop");
                    charaSDSakamata.SetAnimation("stop_moyamoya");
                }
            });
            dir.Dirs.Add(new DirectionOne()
            {
                directionStep = DirectionStep.Stop2,
                Action = () =>
                {
                    SetBackGround("default_stop");
                    charaSDSakamata.SetAnimation("stop_image_makura_ao");
                }
            });
            directions.Add(dir);
        }

        {
            var dir = new Direction() { Name = "normal_image_makura_ki_3" };
            dir.Dirs.Add(new DirectionOne()
            {
                directionStep = DirectionStep.LeberOn,
                Action = () =>
                {
                    SetBackGround("default_stop");
                    maku.SetAnimation("");
                    maku.IsLogoShow(false);
                    cutin.SetAnimation("");
                    charaSDSakamata.SetAnimation("stop_!?");
                    battle.SetAnimation("");
                }
            });
            dir.Dirs.Add(new DirectionOne()
            {
                directionStep = DirectionStep.Stop1,
                Action = () =>
                {
                    SetBackGround("default_stop");
                    charaSDSakamata.SetAnimation("stop_moyamoya");
                }
            });
            dir.Dirs.Add(new DirectionOne()
            {
                directionStep = DirectionStep.Stop3,
                Action = () =>
                {
                    SetBackGround("default_stop");
                    charaSDSakamata.SetAnimation("stop_image_makura_ki");
                }
            });
            directions.Add(dir);
        }

        {
            var dir = new Direction() { Name = "normal_image_makura_ki_2" };
            dir.Dirs.Add(new DirectionOne()
            {
                directionStep = DirectionStep.LeberOn,
                Action = () =>
                {
                    SetBackGround("default_stop");
                    maku.SetAnimation("");
                    maku.IsLogoShow(false);
                    cutin.SetAnimation("");
                    charaSDSakamata.SetAnimation("stop_!?");
                    battle.SetAnimation("");
                }
            });
            dir.Dirs.Add(new DirectionOne()
            {
                directionStep = DirectionStep.Stop1,
                Action = () =>
                {
                    SetBackGround("default_stop");
                    charaSDSakamata.SetAnimation("stop_moyamoya");
                }
            });
            dir.Dirs.Add(new DirectionOne()
            {
                directionStep = DirectionStep.Stop2,
                Action = () =>
                {
                    SetBackGround("default_stop");
                    charaSDSakamata.SetAnimation("stop_image_makura_ki");
                }
            });
            directions.Add(dir);
        }

        {
            var dir = new Direction() { Name = "normal_image_makura_midori_3" };
            dir.Dirs.Add(new DirectionOne()
            {
                directionStep = DirectionStep.LeberOn,
                Action = () =>
                {
                    SetBackGround("default_stop");
                    maku.SetAnimation("");
                    maku.IsLogoShow(false);
                    cutin.SetAnimation("");
                    charaSDSakamata.SetAnimation("stop_!?");
                    battle.SetAnimation("");
                }
            });
            dir.Dirs.Add(new DirectionOne()
            {
                directionStep = DirectionStep.Stop1,
                Action = () =>
                {
                    SetBackGround("default_stop");
                    charaSDSakamata.SetAnimation("stop_moyamoya");
                }
            });
            dir.Dirs.Add(new DirectionOne()
            {
                directionStep = DirectionStep.Stop3,
                Action = () =>
                {
                    SetBackGround("default_stop");
                    charaSDSakamata.SetAnimation("stop_image_makura_midori");
                }
            });
            directions.Add(dir);
        }

        {
            var dir = new Direction() { Name = "normal_image_makura_midori_2" };
            dir.Dirs.Add(new DirectionOne()
            {
                directionStep = DirectionStep.LeberOn,
                Action = () =>
                {
                    SetBackGround("default_stop");
                    maku.SetAnimation("");
                    maku.IsLogoShow(false);
                    cutin.SetAnimation("");
                    charaSDSakamata.SetAnimation("stop_!?");
                    battle.SetAnimation("");
                }
            });
            dir.Dirs.Add(new DirectionOne()
            {
                directionStep = DirectionStep.Stop1,
                Action = () =>
                {
                    SetBackGround("default_stop");
                    charaSDSakamata.SetAnimation("stop_moyamoya");
                }
            });
            dir.Dirs.Add(new DirectionOne()
            {
                directionStep = DirectionStep.Stop2,
                Action = () =>
                {
                    SetBackGround("default_stop");
                    charaSDSakamata.SetAnimation("stop_image_makura_midori");
                }
            });
            directions.Add(dir);
        }

        {
            var dir = new Direction() { Name = "normal_image_makura_pink_3" };
            dir.Dirs.Add(new DirectionOne()
            {
                directionStep = DirectionStep.LeberOn,
                Action = () =>
                {
                    SetBackGround("default_stop");
                    maku.SetAnimation("");
                    maku.IsLogoShow(false);
                    cutin.SetAnimation("");
                    charaSDSakamata.SetAnimation("stop_!?");
                    battle.SetAnimation("");
                }
            });
            dir.Dirs.Add(new DirectionOne()
            {
                directionStep = DirectionStep.Stop1,
                Action = () =>
                {
                    SetBackGround("default_stop");
                    charaSDSakamata.SetAnimation("stop_moyamoya");
                }
            });
            dir.Dirs.Add(new DirectionOne()
            {
                directionStep = DirectionStep.Stop3,
                Action = () =>
                {
                    SetBackGround("default_stop");
                    charaSDSakamata.SetAnimation("stop_image_makura_pink");
                }
            });
            directions.Add(dir);
        }

        {
            var dir = new Direction() { Name = "normal_image_makura_pink_2" };
            dir.Dirs.Add(new DirectionOne()
            {
                directionStep = DirectionStep.LeberOn,
                Action = () =>
                {
                    SetBackGround("default_stop");
                    maku.SetAnimation("");
                    maku.IsLogoShow(false);
                    cutin.SetAnimation("");
                    charaSDSakamata.SetAnimation("stop_!?");
                    battle.SetAnimation("");
                }
            });
            dir.Dirs.Add(new DirectionOne()
            {
                directionStep = DirectionStep.Stop1,
                Action = () =>
                {
                    SetBackGround("default_stop");
                    charaSDSakamata.SetAnimation("stop_moyamoya");
                }
            });
            dir.Dirs.Add(new DirectionOne()
            {
                directionStep = DirectionStep.Stop2,
                Action = () =>
                {
                    SetBackGround("default_stop");
                    charaSDSakamata.SetAnimation("stop_image_makura_pink");
                }
            });
            directions.Add(dir);
        }

        {
            var dir = new Direction() { Name = "normal_image_hatena_3" };
            dir.Dirs.Add(new DirectionOne()
            {
                directionStep = DirectionStep.LeberOn,
                Action = () =>
                {
                    SetBackGround("default_stop");
                    maku.SetAnimation("");
                    maku.IsLogoShow(false);
                    cutin.SetAnimation("");
                    charaSDSakamata.SetAnimation("stop_!?");
                    battle.SetAnimation("");
                }
            });
            dir.Dirs.Add(new DirectionOne()
            {
                directionStep = DirectionStep.Stop1,
                Action = () =>
                {
                    SetBackGround("default_stop");
                    charaSDSakamata.SetAnimation("stop_moyamoya");
                }
            });
            dir.Dirs.Add(new DirectionOne()
            {
                directionStep = DirectionStep.Stop3,
                Action = () =>
                {
                    SetBackGround("default_stop");
                    charaSDSakamata.SetAnimation("stop_?");
                }
            });
            directions.Add(dir);
        }

        {
            var dir = new Direction() { Name = "normal_image_hatena_2" };
            dir.Dirs.Add(new DirectionOne()
            {
                directionStep = DirectionStep.LeberOn,
                Action = () =>
                {
                    SetBackGround("default_stop");
                    maku.SetAnimation("");
                    maku.IsLogoShow(false);
                    cutin.SetAnimation("");
                    charaSDSakamata.SetAnimation("stop_!?");
                    battle.SetAnimation("");
                }
            });
            dir.Dirs.Add(new DirectionOne()
            {
                directionStep = DirectionStep.Stop1,
                Action = () =>
                {
                    SetBackGround("default_stop");
                    charaSDSakamata.SetAnimation("stop_moyamoya");
                }
            });
            dir.Dirs.Add(new DirectionOne()
            {
                directionStep = DirectionStep.Stop2,
                Action = () =>
                {
                    SetBackGround("default_stop");
                    charaSDSakamata.SetAnimation("stop_?");
                }
            });
            directions.Add(dir);
        }

        {
            var dir = new Direction() { Name = "group_in" };
            dir.Dirs.Add(new DirectionOne()
            {
                directionStep = DirectionStep.LeberOn,
                Action = () =>
                {
                    SetBackGround("default");
                    maku.SetAnimation("close");
                    maku.IsLogoShow(false);
                    cutin.SetAnimation("");
                    battle.SetAnimation("");
                    DeleteAudios("BGM");
                }
            });
            dir.Dirs.Add(new DirectionOne()
            {
                directionStep = DirectionStep.Stop1,
                Action = () =>
                {
                }
            });
            dir.Dirs.Add(new DirectionOne()
            {
                directionStep = DirectionStep.Stop2,
                Action = () =>
                {
                }
            });
            dir.Dirs.Add(new DirectionOne()
            {
                directionStep = DirectionStep.Stop3,
                Action = () =>
                {
                    maku.SetAnimation("open");
                    SetBackGround("group_1");

                    cutin.SetAnimation("");
                    cutin.SetAnimation("group_in");
                    PlayAudio("group_shugou_chance_b", new string[] { "SE" });
                    PlayAudio("group_in", new string[] { "SE" });
                    PlayAudio("group_bgm", new string[] { "BGM" });
                }
            });
            directions.Add(dir);
        }

        {
            var dir = new Direction() { Name = "group_cutin_1" };
            dir.Dirs.Add(new DirectionOne()
            {
                directionStep = DirectionStep.LeberOn,
                Action = () =>
                {
                    SetBackGround("group_1");
                    maku.SetAnimation("");
                    maku.IsLogoShow(false);
                    cutin.SetAnimation("");
                    battle.SetAnimation("");
                    PlayAudio("group_bgm", new string[] { "BGM" });
                }
            });
            dir.Dirs.Add(new DirectionOne()
            {
                directionStep = DirectionStep.Stop1,
                Action = () =>
                {
                    switch (Lottery.LotteryBase.DefaultLottery(new int[] { 10, GetLotStringValue(1, 15, 10, 5) }))
                    {
                        case 1:
                            PlayAudio("group_shugou_chance", new string[] { "SE" });
                            break;
                    }
                }
            });
            dir.Dirs.Add(new DirectionOne()
            {
                directionStep = DirectionStep.Stop2,
                Action = () =>
                {
                    switch (Lottery.LotteryBase.DefaultLottery(new int[] { 10, GetLotStringValue(1, 15, 10, 5) }))
                    {
                        case 1:
                            PlayAudio("group_shugou_chance", new string[] { "SE" });
                            break;
                    }
                }
            });
            dir.Dirs.Add(new DirectionOne()
            {
                directionStep = DirectionStep.Stop3,
                Action = () =>
                {
                    SetBackGround("group_2");
                    cutin.SetAnimation("");
                    cutin.SetAnimation("group_1");
                    PlayAudio("group_shugou_chance_b", new string[] { "SE" });
                    PlayAudio("group_cutin_1", new string[] { "SE" });
                }
            });
            directions.Add(dir);
        }

        {
            var dir = new Direction() { Name = "group_cutin_2" };
            dir.Dirs.Add(new DirectionOne()
            {
                directionStep = DirectionStep.LeberOn,
                Action = () =>
                {
                    SetBackGround("group_2");
                    maku.SetAnimation("");
                    maku.IsLogoShow(false);
                    cutin.SetAnimation("");
                    battle.SetAnimation("");
                    PlayAudio("group_bgm", new string[] { "BGM" });
                }
            });
            dir.Dirs.Add(new DirectionOne()
            {
                directionStep = DirectionStep.Stop1,
                Action = () =>
                {
                    switch (Lottery.LotteryBase.DefaultLottery(new int[] { 10, GetLotStringValue(1, 15, 10, 5) }))
                    {
                        case 1:
                            PlayAudio("group_shugou_chance", new string[] { "SE" });
                            break;
                    }
                }
            });
            dir.Dirs.Add(new DirectionOne()
            {
                directionStep = DirectionStep.Stop2,
                Action = () =>
                {
                    switch (Lottery.LotteryBase.DefaultLottery(new int[] { 10, GetLotStringValue(1, 15, 10, 5) }))
                    {
                        case 1:
                            PlayAudio("group_shugou_chance", new string[] { "SE" });
                            break;
                    }
                }
            });
            dir.Dirs.Add(new DirectionOne()
            {
                directionStep = DirectionStep.Stop3,
                Action = () =>
                {
                    SetBackGround("group_3");
                    cutin.SetAnimation("");
                    cutin.SetAnimation("group_2");
                    PlayAudio("group_shugou_chance_b", new string[] { "SE" });
                    PlayAudio("group_cutin_2", new string[] { "SE" });
                }
            });
            directions.Add(dir);
        }

        {
            var dir = new Direction() { Name = "group_cutin_3" };
            dir.Dirs.Add(new DirectionOne()
            {
                directionStep = DirectionStep.LeberOn,
                Action = () =>
                {
                    SetBackGround("group_3");
                    maku.SetAnimation("");
                    maku.IsLogoShow(false);
                    cutin.SetAnimation("");
                    battle.SetAnimation("");
                    PlayAudio("group_bgm", new string[] { "BGM" });
                }
            });
            dir.Dirs.Add(new DirectionOne()
            {
                directionStep = DirectionStep.Stop1,
                Action = () =>
                {
                    switch (Lottery.LotteryBase.DefaultLottery( new int[] { 10, GetLotStringValue(1,15,10,5) }  ))
                    {
                        case 1:
                            PlayAudio("group_shugou_chance", new string[] { "SE" });
                            break;
                    }
                }
            });
            dir.Dirs.Add(new DirectionOne()
            {
                directionStep = DirectionStep.Stop2,
                Action = () =>
                {
                    switch (Lottery.LotteryBase.DefaultLottery(new int[] { 10, GetLotStringValue(1, 15, 10, 5) }))
                    {
                        case 1:
                            PlayAudio("group_shugou_chance", new string[] { "SE" });
                            break;
                    }
                }
            });
            dir.Dirs.Add(new DirectionOne()
            {
                directionStep = DirectionStep.Stop3,
                Action = () =>
                {
                    SetBackGround("group_4");
                    cutin.SetAnimation("");
                    cutin.SetAnimation("group_3");
                    PlayAudio("group_shugou_chance_b", new string[] { "SE" } );
                    PlayAudio("group_cutin_3", new string[] { "SE" });
                }
            });
            directions.Add(dir);
        }

        {
            var dir = new Direction() { Name = "group_cutin_4" };
            dir.Dirs.Add(new DirectionOne()
            {
                directionStep = DirectionStep.LeberOn,
                Action = () =>
                {
                    SetBackGround("group_4");
                    maku.SetAnimation("");
                    maku.IsLogoShow(false);
                    cutin.SetAnimation("");
                    battle.SetAnimation("");

                    PlayAudio("group_bgm", new string[] { "BGM" });
                }
            });
            dir.Dirs.Add(new DirectionOne()
            {
                directionStep = DirectionStep.Stop1,
                Action = () =>
                {
                    switch (Lottery.LotteryBase.DefaultLottery(new int[] { 10, GetLotStringValue(1, 15, 10, 5) }))
                    {
                        case 1:
                            PlayAudio("group_shugou_chance", new string[] { "SE" });
                            break;
                    }
                }
            });
            dir.Dirs.Add(new DirectionOne()
            {
                directionStep = DirectionStep.Stop2,
                Action = () =>
                {
                    switch (Lottery.LotteryBase.DefaultLottery(new int[] { 10, GetLotStringValue(1, 15, 10, 5) }))
                    {
                        case 1:
                            PlayAudio("group_shugou_chance", new string[] { "SE" });
                            break;
                    }
                }
            });
            dir.Dirs.Add(new DirectionOne()
            {
                directionStep = DirectionStep.Stop3,
                Action = () =>
                {
                    SetBackGround("group_5");
                    cutin.SetAnimation("");
                    cutin.SetAnimation("group_4");
                    PlayAudio("group_shugou_chance_b", new string[] { "SE" });
                    PlayAudio("group_cutin_4", new string[] { "SE" });
                }
            });
            directions.Add(dir);
        }

        {
            var dir = new Direction() { Name = "group_1" };
            dir.Dirs.Add(new DirectionOne()
            {
                directionStep = DirectionStep.LeberOn,
                Action = () =>
                {
                    SetBackGround("group_1");
                    maku.SetAnimation("");
                    maku.IsLogoShow(false);
                    cutin.SetAnimation("");
                    battle.SetAnimation("");
                    PlayAudio("group_bgm", new string[] { "BGM" });
                }
            });
            dir.Dirs.Add(new DirectionOne()
            {
                directionStep = DirectionStep.Stop1,
                Action = () =>
                {
                    switch (Lottery.LotteryBase.DefaultLottery(new int[] { 10, GetLotStringValue(1, 15, 10, 5) }))
                    {
                        case 1:
                            PlayAudio("group_shugou_chance", new string[] { "SE" });
                            break;
                    }
                }
            });
            dir.Dirs.Add(new DirectionOne()
            {
                directionStep = DirectionStep.Stop2,
                Action = () =>
                {
                    switch (Lottery.LotteryBase.DefaultLottery(new int[] { 10, GetLotStringValue(1, 15, 10, 5) }))
                    {
                        case 1:
                            PlayAudio("group_shugou_chance", new string[] { "SE" });
                            break;
                    }
                }
            });
            dir.Dirs.Add(new DirectionOne()
            {
                directionStep = DirectionStep.Stop3,
                Action = () =>
                {
                    switch (Lottery.LotteryBase.DefaultLottery(new int[] { 10, GetLotStringValue(1, 15, 10, 5) }))
                    {
                        case 1:
                            PlayAudio("group_shugou_chance", new string[] { "SE" });
                            break;
                    }
                }
            });
            directions.Add(dir);
        }

        {
            var dir = new Direction() { Name = "group_2" };
            dir.Dirs.Add(new DirectionOne()
            {
                directionStep = DirectionStep.LeberOn,
                Action = () =>
                {
                    SetBackGround("group_2");
                    maku.SetAnimation("");
                    maku.IsLogoShow(false);
                    cutin.SetAnimation("");
                    battle.SetAnimation("");
                    PlayAudio("group_bgm", new string[] { "BGM" });
                }
            });
            dir.Dirs.Add(new DirectionOne()
            {
                directionStep = DirectionStep.Stop1,
                Action = () =>
                {
                    switch (Lottery.LotteryBase.DefaultLottery(new int[] { 10, GetLotStringValue(1, 15, 10, 5) }))
                    {
                        case 1:
                            PlayAudio("group_shugou_chance", new string[] { "SE" });
                            break;
                    }
                }
            });
            dir.Dirs.Add(new DirectionOne()
            {
                directionStep = DirectionStep.Stop2,
                Action = () =>
                {
                    switch (Lottery.LotteryBase.DefaultLottery(new int[] { 10, GetLotStringValue(1, 15, 10, 5) }))
                    {
                        case 1:
                            PlayAudio("group_shugou_chance", new string[] { "SE" });
                            break;
                    }
                }
            });
            dir.Dirs.Add(new DirectionOne()
            {
                directionStep = DirectionStep.Stop3,
                Action = () =>
                {
                    switch (Lottery.LotteryBase.DefaultLottery(new int[] { 10, GetLotStringValue(1, 15, 10, 5) }))
                    {
                        case 1:
                            PlayAudio("group_shugou_chance", new string[] { "SE" });
                            break;
                    }
                }
            });
            directions.Add(dir);
        }

        {
            var dir = new Direction() { Name = "group_3" };
            dir.Dirs.Add(new DirectionOne()
            {
                directionStep = DirectionStep.LeberOn,
                Action = () =>
                {
                    SetBackGround("group_3");
                    maku.SetAnimation("");
                    maku.IsLogoShow(false);
                    cutin.SetAnimation("");
                    battle.SetAnimation("");
                    PlayAudio("group_bgm", new string[] { "BGM" });
                }
            });
            dir.Dirs.Add(new DirectionOne()
            {
                directionStep = DirectionStep.Stop1,
                Action = () =>
                {
                    switch (Lottery.LotteryBase.DefaultLottery(new int[] { 10, GetLotStringValue(1, 15, 10, 5) }))
                    {
                        case 1:
                            PlayAudio("group_shugou_chance", new string[] { "SE" });
                            break;
                    }
                }
            });
            dir.Dirs.Add(new DirectionOne()
            {
                directionStep = DirectionStep.Stop2,
                Action = () =>
                {
                    switch (Lottery.LotteryBase.DefaultLottery(new int[] { 10, GetLotStringValue(1, 15, 10, 5) }))
                    {
                        case 1:
                            PlayAudio("group_shugou_chance", new string[] { "SE" });
                            break;
                    }
                }
            });
            dir.Dirs.Add(new DirectionOne()
            {
                directionStep = DirectionStep.Stop3,
                Action = () =>
                {
                    switch (Lottery.LotteryBase.DefaultLottery(new int[] { 10, GetLotStringValue(1, 15, 10, 5) }))
                    {
                        case 1:
                            PlayAudio("group_shugou_chance", new string[] { "SE" });
                            break;
                    }
                }
            });
            directions.Add(dir);
        }

        {
            var dir = new Direction() { Name = "group_4" };
            dir.Dirs.Add(new DirectionOne()
            {
                directionStep = DirectionStep.LeberOn,
                Action = () =>
                {
                    SetBackGround("group_4");
                    maku.SetAnimation("");
                    maku.IsLogoShow(false);
                    cutin.SetAnimation("");
                    battle.SetAnimation("");
                    PlayAudio("group_bgm", new string[] { "BGM" });
                }
            });
            dir.Dirs.Add(new DirectionOne()
            {
                directionStep = DirectionStep.Stop1,
                Action = () =>
                {
                    switch (Lottery.LotteryBase.DefaultLottery(new int[] { 10, GetLotStringValue(1, 15, 10, 5) }))
                    {
                        case 1:
                            PlayAudio("group_shugou_chance", new string[] { "SE" });
                            break;
                    }
                }
            });
            dir.Dirs.Add(new DirectionOne()
            {
                directionStep = DirectionStep.Stop2,
                Action = () =>
                {
                    switch (Lottery.LotteryBase.DefaultLottery(new int[] { 10, GetLotStringValue(1, 15, 10, 5) }))
                    {
                        case 1:
                            PlayAudio("group_shugou_chance", new string[] { "SE" });
                            break;
                    }
                }
            });
            dir.Dirs.Add(new DirectionOne()
            {
                directionStep = DirectionStep.Stop3,
                Action = () =>
                {
                    switch (Lottery.LotteryBase.DefaultLottery(new int[] { 10, GetLotStringValue(1, 15, 10, 5) }))
                    {
                        case 1:
                            PlayAudio("group_shugou_chance", new string[] { "SE" });
                            break;
                    }
                }
            });
            directions.Add(dir);
        }

        {
            var dir = new Direction() { Name = "group_5" };
            dir.Dirs.Add(new DirectionOne()
            {
                directionStep = DirectionStep.LeberOn,
                Action = () =>
                {
                    SetBackGround("group_5");
                    maku.SetAnimation("");
                    maku.IsLogoShow(false);
                    cutin.SetAnimation("");
                    battle.SetAnimation("");
                    PlayAudio("group_bgm", new string[] { "BGM" });
                }
            });
            dir.Dirs.Add(new DirectionOne()
            {
                directionStep = DirectionStep.Stop1,
                Action = () =>
                {
                    switch (Lottery.LotteryBase.DefaultLottery(new int[] { 10, GetLotStringValue(1, 15, 10, 5) }))
                    {
                        case 1:
                            PlayAudio("group_shugou_chance", new string[] { "SE" });
                            break;
                    }
                }
            });
            dir.Dirs.Add(new DirectionOne()
            {
                directionStep = DirectionStep.Stop2,
                Action = () =>
                {
                    switch (Lottery.LotteryBase.DefaultLottery(new int[] { 10, GetLotStringValue(1, 15, 10, 5) }))
                    {
                        case 1:
                            PlayAudio("group_shugou_chance", new string[] { "SE" });
                            break;
                    }
                }
            });
            dir.Dirs.Add(new DirectionOne()
            {
                directionStep = DirectionStep.Stop3,
                Action = () =>
                {
                    switch (Lottery.LotteryBase.DefaultLottery(new int[] { 10, GetLotStringValue(1, 15, 10, 5) }))
                    {
                        case 1:
                            PlayAudio("group_shugou_chance", new string[] { "SE" });
                            break;
                    }
                }
            });
            directions.Add(dir);
        }

        {
            var dir = new Direction() { Name = "group_1_maku_1" };
            dir.Dirs.Add(new DirectionOne()
            {
                directionStep = DirectionStep.LeberOn,
                Action = () =>
                {
                    SetBackGround("group_1");
                    maku.SetAnimation("");
                    maku.SetAnimation("close");
                    maku.IsLogoShow(false);
                    cutin.SetAnimation("");
                    battle.SetAnimation("");
                    PlayAudio("group_shugou_chance", new string[] { "SE" });
                    PlayAudio("group_bgm", new string[] { "BGM" });
                }
            });

            dir.Dirs.Add(new DirectionOne()
            {
                directionStep = DirectionStep.Stop1,
                Action = () =>
                {
                }
            });
            dir.Dirs.Add(new DirectionOne()
            {
                directionStep = DirectionStep.Stop2,
                Action = () =>
                {
                }
            });
            dir.Dirs.Add(new DirectionOne()
            {
                directionStep = DirectionStep.Stop3,
                Action = () =>
                {
                }
            });
            directions.Add(dir);
        }

        {
            var dir = new Direction() { Name = "group_1_maku_2" };
            dir.Dirs.Add(new DirectionOne()
            {
                directionStep = DirectionStep.LeberOn,
                Action = () =>
                {
                    SetBackGround("group_1");
                    maku.SetAnimation("");
                    maku.SetAnimation("close");
                    maku.IsLogoShow(false);
                    cutin.SetAnimation("");
                    battle.SetAnimation("");
                    PlayAudio("group_shugou_chance", new string[] { "SE" });
                    PlayAudio("group_bgm", new string[] { "BGM" });
                }
            });

            dir.Dirs.Add(new DirectionOne()
            {
                directionStep = DirectionStep.Stop1,
                Action = () =>
                {
                    maku.IsLogoShow(true);
                    PlayAudio("group_logo", new string[] { "SE" });
                }
            });
            dir.Dirs.Add(new DirectionOne()
            {
                directionStep = DirectionStep.Stop2,
                Action = () =>
                {
                }
            });
            dir.Dirs.Add(new DirectionOne()
            {
                directionStep = DirectionStep.Stop3,
                Action = () =>
                {
                }
            });
            directions.Add(dir);
        }

        {
            var dir = new Direction() { Name = "group_1_maku_3" };
            dir.Dirs.Add(new DirectionOne()
            {
                directionStep = DirectionStep.LeberOn,
                Action = () =>
                {
                    SetBackGround("group_1");
                    maku.SetAnimation("");
                    maku.SetAnimation("close");
                    maku.IsLogoShow(false);
                    cutin.SetAnimation("");
                    battle.SetAnimation("");
                    PlayAudio("group_shugou_chance", new string[] { "SE" });
                    PlayAudio("group_bgm", new string[] { "BGM" });
                }
            });

            dir.Dirs.Add(new DirectionOne()
            {
                directionStep = DirectionStep.Stop1,
                Action = () =>
                {
                }
            });
            dir.Dirs.Add(new DirectionOne()
            {
                directionStep = DirectionStep.Stop2,
                Action = () =>
                {
                    maku.IsLogoShow(true);
                    PlayAudio("group_logo", new string[] { "SE" });
                }
            });
            dir.Dirs.Add(new DirectionOne()
            {
                directionStep = DirectionStep.Stop3,
                Action = () =>
                {
                }
            });
            directions.Add(dir);
        }

        {
            var dir = new Direction() { Name = "group_1_maku_4" };
            dir.Dirs.Add(new DirectionOne()
            {
                directionStep = DirectionStep.LeberOn,
                Action = () =>
                {
                    SetBackGround("group_1");
                    maku.SetAnimation("");
                    maku.SetAnimation("close");
                    maku.IsLogoShow(false);
                    cutin.SetAnimation("");
                    battle.SetAnimation("");
                    PlayAudio("group_shugou_chance", new string[] { "SE" });
                    PlayAudio("group_bgm", new string[] { "BGM" });
                }
            });

            dir.Dirs.Add(new DirectionOne()
            {
                directionStep = DirectionStep.Stop1,
                Action = () =>
                {
                }
            });
            dir.Dirs.Add(new DirectionOne()
            {
                directionStep = DirectionStep.Stop2,
                Action = () =>
                {
                }
            });
            dir.Dirs.Add(new DirectionOne()
            {
                directionStep = DirectionStep.Stop3,
                Action = () =>
                {
                    maku.IsLogoShow(true);
                    PlayAudio("group_logo", new string[] { "SE" });
                }
            });
            directions.Add(dir);
        }

        {
            var dir = new Direction() { Name = "group_2_maku_1" };
            dir.Dirs.Add(new DirectionOne()
            {
                directionStep = DirectionStep.LeberOn,
                Action = () =>
                {
                    SetBackGround("group_2");
                    maku.SetAnimation("");
                    maku.SetAnimation("close");
                    maku.IsLogoShow(false);
                    cutin.SetAnimation("");
                    battle.SetAnimation("");
                    PlayAudio("group_shugou_chance", new string[] { "SE" });
                    PlayAudio("group_bgm", new string[] { "BGM" });
                }
            });

            dir.Dirs.Add(new DirectionOne()
            {
                directionStep = DirectionStep.Stop1,
                Action = () =>
                {
                }
            });
            dir.Dirs.Add(new DirectionOne()
            {
                directionStep = DirectionStep.Stop2,
                Action = () =>
                {
                }
            });
            dir.Dirs.Add(new DirectionOne()
            {
                directionStep = DirectionStep.Stop3,
                Action = () =>
                {
                }
            });
            directions.Add(dir);
        }

        {
            var dir = new Direction() { Name = "group_2_maku_2" };
            dir.Dirs.Add(new DirectionOne()
            {
                directionStep = DirectionStep.LeberOn,
                Action = () =>
                {
                    SetBackGround("group_2");
                    maku.SetAnimation("");
                    maku.SetAnimation("close");
                    maku.IsLogoShow(false);
                    cutin.SetAnimation("");
                    battle.SetAnimation("");
                    PlayAudio("group_shugou_chance", new string[] { "SE" });
                    PlayAudio("group_bgm", new string[] { "BGM" });
                }
            });

            dir.Dirs.Add(new DirectionOne()
            {
                directionStep = DirectionStep.Stop1,
                Action = () =>
                {
                    maku.IsLogoShow(true);
                    PlayAudio("group_logo", new string[] { "SE" });
                }
            });
            dir.Dirs.Add(new DirectionOne()
            {
                directionStep = DirectionStep.Stop2,
                Action = () =>
                {
                }
            });
            dir.Dirs.Add(new DirectionOne()
            {
                directionStep = DirectionStep.Stop3,
                Action = () =>
                {
                }
            });
            directions.Add(dir);
        }

        {
            var dir = new Direction() { Name = "group_2_maku_3" };
            dir.Dirs.Add(new DirectionOne()
            {
                directionStep = DirectionStep.LeberOn,
                Action = () =>
                {
                    SetBackGround("group_2");
                    maku.SetAnimation("");
                    maku.SetAnimation("close");
                    maku.IsLogoShow(false);
                    cutin.SetAnimation("");
                    battle.SetAnimation("");
                    PlayAudio("group_shugou_chance", new string[] { "SE" });
                    PlayAudio("group_bgm", new string[] { "BGM" });
                }
            });

            dir.Dirs.Add(new DirectionOne()
            {
                directionStep = DirectionStep.Stop1,
                Action = () =>
                {
                }
            });
            dir.Dirs.Add(new DirectionOne()
            {
                directionStep = DirectionStep.Stop2,
                Action = () =>
                {
                    maku.IsLogoShow(true);
                    PlayAudio("group_logo", new string[] { "SE" });
                }
            });
            dir.Dirs.Add(new DirectionOne()
            {
                directionStep = DirectionStep.Stop3,
                Action = () =>
                {
                }
            });
            directions.Add(dir);
        }

        {
            var dir = new Direction() { Name = "group_2_maku_4" };
            dir.Dirs.Add(new DirectionOne()
            {
                directionStep = DirectionStep.LeberOn,
                Action = () =>
                {
                    SetBackGround("group_2");
                    maku.SetAnimation("");
                    maku.SetAnimation("close");
                    maku.IsLogoShow(false);
                    cutin.SetAnimation("");
                    battle.SetAnimation("");
                    PlayAudio("group_shugou_chance", new string[] { "SE" });
                    PlayAudio("group_bgm", new string[] { "BGM" });
                }
            });

            dir.Dirs.Add(new DirectionOne()
            {
                directionStep = DirectionStep.Stop1,
                Action = () =>
                {
                }
            });
            dir.Dirs.Add(new DirectionOne()
            {
                directionStep = DirectionStep.Stop2,
                Action = () =>
                {
                }
            });
            dir.Dirs.Add(new DirectionOne()
            {
                directionStep = DirectionStep.Stop3,
                Action = () =>
                {
                    maku.IsLogoShow(true);
                    PlayAudio("group_logo", new string[] { "SE" });
                }
            });
            directions.Add(dir);
        }


        {
            var dir = new Direction() { Name = "group_3_maku_1" };
            dir.Dirs.Add(new DirectionOne()
            {
                directionStep = DirectionStep.LeberOn,
                Action = () =>
                {
                    SetBackGround("group_3");
                    maku.SetAnimation("");
                    maku.SetAnimation("close");
                    maku.IsLogoShow(false);
                    cutin.SetAnimation("");
                    battle.SetAnimation("");
                    PlayAudio("group_shugou_chance", new string[] { "SE" });
                    PlayAudio("group_bgm", new string[] { "BGM" });
                }
            });

            dir.Dirs.Add(new DirectionOne()
            {
                directionStep = DirectionStep.Stop1,
                Action = () =>
                {
                }
            });
            dir.Dirs.Add(new DirectionOne()
            {
                directionStep = DirectionStep.Stop2,
                Action = () =>
                {
                }
            });
            dir.Dirs.Add(new DirectionOne()
            {
                directionStep = DirectionStep.Stop3,
                Action = () =>
                {
                }
            });
            directions.Add(dir);
        }

        {
            var dir = new Direction() { Name = "group_3_maku_2" };
            dir.Dirs.Add(new DirectionOne()
            {
                directionStep = DirectionStep.LeberOn,
                Action = () =>
                {
                    SetBackGround("group_3");
                    maku.SetAnimation("");
                    maku.SetAnimation("close");
                    maku.IsLogoShow(false);
                    cutin.SetAnimation("");
                    battle.SetAnimation("");
                    PlayAudio("group_shugou_chance", new string[] { "SE" });
                    PlayAudio("group_bgm", new string[] { "BGM" });
                }
            });

            dir.Dirs.Add(new DirectionOne()
            {
                directionStep = DirectionStep.Stop1,
                Action = () =>
                {
                    maku.IsLogoShow(true);
                    PlayAudio("group_logo", new string[] { "SE" });
                }
            });
            dir.Dirs.Add(new DirectionOne()
            {
                directionStep = DirectionStep.Stop2,
                Action = () =>
                {
                }
            });
            dir.Dirs.Add(new DirectionOne()
            {
                directionStep = DirectionStep.Stop3,
                Action = () =>
                {
                }
            });
            directions.Add(dir);
        }

        {
            var dir = new Direction() { Name = "group_3_maku_3" };
            dir.Dirs.Add(new DirectionOne()
            {
                directionStep = DirectionStep.LeberOn,
                Action = () =>
                {
                    SetBackGround("group_3");
                    maku.SetAnimation("");
                    maku.SetAnimation("close");
                    maku.IsLogoShow(false);
                    cutin.SetAnimation("");
                    battle.SetAnimation("");
                    PlayAudio("group_shugou_chance", new string[] { "SE" });
                    PlayAudio("group_bgm", new string[] { "BGM" });
                }
            });

            dir.Dirs.Add(new DirectionOne()
            {
                directionStep = DirectionStep.Stop1,
                Action = () =>
                {
                }
            });
            dir.Dirs.Add(new DirectionOne()
            {
                directionStep = DirectionStep.Stop2,
                Action = () =>
                {
                    maku.IsLogoShow(true);
                    PlayAudio("group_logo", new string[] { "SE" });
                }
            });
            dir.Dirs.Add(new DirectionOne()
            {
                directionStep = DirectionStep.Stop3,
                Action = () =>
                {
                }
            });
            directions.Add(dir);
        }

        {
            var dir = new Direction() { Name = "group_3_maku_4" };
            dir.Dirs.Add(new DirectionOne()
            {
                directionStep = DirectionStep.LeberOn,
                Action = () =>
                {
                    SetBackGround("group_3");
                    maku.SetAnimation("");
                    maku.SetAnimation("close");
                    maku.IsLogoShow(false);
                    cutin.SetAnimation("");
                    battle.SetAnimation("");
                    PlayAudio("group_shugou_chance", new string[] { "SE" });
                    PlayAudio("group_bgm", new string[] { "BGM" });
                }
            });

            dir.Dirs.Add(new DirectionOne()
            {
                directionStep = DirectionStep.Stop1,
                Action = () =>
                {
                }
            });
            dir.Dirs.Add(new DirectionOne()
            {
                directionStep = DirectionStep.Stop2,
                Action = () =>
                {
                }
            });
            dir.Dirs.Add(new DirectionOne()
            {
                directionStep = DirectionStep.Stop3,
                Action = () =>
                {
                    maku.IsLogoShow(true);
                    PlayAudio("group_logo", new string[] { "SE" });
                }
            });
            directions.Add(dir);
        }

        {
            var dir = new Direction() { Name = "group_4_maku_1" };
            dir.Dirs.Add(new DirectionOne()
            {
                directionStep = DirectionStep.LeberOn,
                Action = () =>
                {
                    SetBackGround("group_4");
                    maku.SetAnimation("");
                    maku.SetAnimation("close");
                    maku.IsLogoShow(false);
                    cutin.SetAnimation("");
                    battle.SetAnimation("");
                    PlayAudio("group_shugou_chance", new string[] { "SE" });
                    PlayAudio("group_bgm", new string[] { "BGM" });
                }
            });

            dir.Dirs.Add(new DirectionOne()
            {
                directionStep = DirectionStep.Stop1,
                Action = () =>
                {
                }
            });
            dir.Dirs.Add(new DirectionOne()
            {
                directionStep = DirectionStep.Stop2,
                Action = () =>
                {
                }
            });
            dir.Dirs.Add(new DirectionOne()
            {
                directionStep = DirectionStep.Stop3,
                Action = () =>
                {
                }
            });
            directions.Add(dir);
        }

        {
            var dir = new Direction() { Name = "group_4_maku_2" };
            dir.Dirs.Add(new DirectionOne()
            {
                directionStep = DirectionStep.LeberOn,
                Action = () =>
                {
                    SetBackGround("group_4");
                    maku.SetAnimation("");
                    maku.SetAnimation("close");
                    maku.IsLogoShow(false);
                    cutin.SetAnimation("");
                    battle.SetAnimation("");
                    PlayAudio("group_shugou_chance", new string[] { "SE" });
                    PlayAudio("group_bgm", new string[] { "BGM" });
                }
            });

            dir.Dirs.Add(new DirectionOne()
            {
                directionStep = DirectionStep.Stop1,
                Action = () =>
                {
                    maku.IsLogoShow(true);
                    PlayAudio("group_logo", new string[] { "SE" });
                }
            });
            dir.Dirs.Add(new DirectionOne()
            {
                directionStep = DirectionStep.Stop2,
                Action = () =>
                {
                }
            });
            dir.Dirs.Add(new DirectionOne()
            {
                directionStep = DirectionStep.Stop3,
                Action = () =>
                {
                }
            });
            directions.Add(dir);
        }

        {
            var dir = new Direction() { Name = "group_4_maku_3" };
            dir.Dirs.Add(new DirectionOne()
            {
                directionStep = DirectionStep.LeberOn,
                Action = () =>
                {
                    SetBackGround("group_4");
                    maku.SetAnimation("");
                    maku.SetAnimation("close");
                    maku.IsLogoShow(false);
                    cutin.SetAnimation("");
                    battle.SetAnimation("");
                    PlayAudio("group_shugou_chance", new string[] { "SE" });
                    PlayAudio("group_bgm", new string[] { "BGM" });
                }
            });

            dir.Dirs.Add(new DirectionOne()
            {
                directionStep = DirectionStep.Stop1,
                Action = () =>
                {
                }
            });
            dir.Dirs.Add(new DirectionOne()
            {
                directionStep = DirectionStep.Stop2,
                Action = () =>
                {
                    maku.IsLogoShow(true);
                    PlayAudio("group_logo", new string[] { "SE" });
                }
            });
            dir.Dirs.Add(new DirectionOne()
            {
                directionStep = DirectionStep.Stop3,
                Action = () =>
                {
                }
            });
            directions.Add(dir);
        }

        {
            var dir = new Direction() { Name = "group_4_maku_4" };
            dir.Dirs.Add(new DirectionOne()
            {
                directionStep = DirectionStep.LeberOn,
                Action = () =>
                {
                    SetBackGround("group_4");
                    maku.SetAnimation("");
                    maku.SetAnimation("close");
                    maku.IsLogoShow(false);
                    cutin.SetAnimation("");
                    battle.SetAnimation("");
                    PlayAudio("group_shugou_chance", new string[] { "SE" });
                    PlayAudio("group_bgm", new string[] { "BGM" });
                }
            });

            dir.Dirs.Add(new DirectionOne()
            {
                directionStep = DirectionStep.Stop1,
                Action = () =>
                {
                }
            });
            dir.Dirs.Add(new DirectionOne()
            {
                directionStep = DirectionStep.Stop2,
                Action = () =>
                {
                }
            });
            dir.Dirs.Add(new DirectionOne()
            {
                directionStep = DirectionStep.Stop3,
                Action = () =>
                {
                    maku.IsLogoShow(true);
                    PlayAudio("group_logo", new string[] { "SE" });
                }
            });
            directions.Add(dir);
        }


        {
            var dir = new Direction() { Name = "group_5_maku_1" };
            dir.Dirs.Add(new DirectionOne()
            {
                directionStep = DirectionStep.LeberOn,
                Action = () =>
                {
                    SetBackGround("group_5");
                    maku.SetAnimation("");
                    maku.SetAnimation("close");
                    maku.IsLogoShow(false);
                    cutin.SetAnimation("");
                    battle.SetAnimation("");
                    PlayAudio("group_shugou_chance", new string[] { "SE" });
                    PlayAudio("group_bgm", new string[] { "BGM" });
                }
            });

            dir.Dirs.Add(new DirectionOne()
            {
                directionStep = DirectionStep.Stop1,
                Action = () =>
                {
                }
            });
            dir.Dirs.Add(new DirectionOne()
            {
                directionStep = DirectionStep.Stop2,
                Action = () =>
                {
                }
            });
            dir.Dirs.Add(new DirectionOne()
            {
                directionStep = DirectionStep.Stop3,
                Action = () =>
                {
                }
            });
            directions.Add(dir);
        }

        {
            var dir = new Direction() { Name = "group_5_maku_2" };
            dir.Dirs.Add(new DirectionOne()
            {
                directionStep = DirectionStep.LeberOn,
                Action = () =>
                {
                    SetBackGround("group_5");
                    maku.SetAnimation("");
                    maku.SetAnimation("close");
                    maku.IsLogoShow(false);
                    cutin.SetAnimation("");
                    battle.SetAnimation("");
                    PlayAudio("group_shugou_chance", new string[] { "SE" });
                    PlayAudio("group_bgm", new string[] { "BGM" });
                }
            });

            dir.Dirs.Add(new DirectionOne()
            {
                directionStep = DirectionStep.Stop1,
                Action = () =>
                {
                    maku.IsLogoShow(true);
                    PlayAudio("group_logo", new string[] { "SE" });
                }
            });
            dir.Dirs.Add(new DirectionOne()
            {
                directionStep = DirectionStep.Stop2,
                Action = () =>
                {
                }
            });
            dir.Dirs.Add(new DirectionOne()
            {
                directionStep = DirectionStep.Stop3,
                Action = () =>
                {
                }
            });
            directions.Add(dir);
        }

        {
            var dir = new Direction() { Name = "group_5_maku_3" };
            dir.Dirs.Add(new DirectionOne()
            {
                directionStep = DirectionStep.LeberOn,
                Action = () =>
                {
                    SetBackGround("group_5");
                    maku.SetAnimation("");
                    maku.SetAnimation("close");
                    maku.IsLogoShow(false);
                    cutin.SetAnimation("");
                    battle.SetAnimation("");
                    PlayAudio("group_shugou_chance", new string[] { "SE" });
                    PlayAudio("group_bgm", new string[] { "BGM" });
                }
            });

            dir.Dirs.Add(new DirectionOne()
            {
                directionStep = DirectionStep.Stop1,
                Action = () =>
                {
                }
            });
            dir.Dirs.Add(new DirectionOne()
            {
                directionStep = DirectionStep.Stop2,
                Action = () =>
                {
                    maku.IsLogoShow(true);
                    PlayAudio("group_logo", new string[] { "SE" });
                }
            });
            dir.Dirs.Add(new DirectionOne()
            {
                directionStep = DirectionStep.Stop3,
                Action = () =>
                {
                }
            });
            directions.Add(dir);
        }

        {
            var dir = new Direction() { Name = "group_5_maku_4" };
            dir.Dirs.Add(new DirectionOne()
            {
                directionStep = DirectionStep.LeberOn,
                Action = () =>
                {
                    SetBackGround("group_5");
                    maku.SetAnimation("");
                    maku.SetAnimation("close");
                    maku.IsLogoShow(false);
                    cutin.SetAnimation("");
                    battle.SetAnimation("");
                    PlayAudio("group_shugou_chance", new string[] { "SE" });
                    PlayAudio("group_bgm", new string[] { "BGM" });
                }
            });

            dir.Dirs.Add(new DirectionOne()
            {
                directionStep = DirectionStep.Stop1,
                Action = () =>
                {
                }
            });
            dir.Dirs.Add(new DirectionOne()
            {
                directionStep = DirectionStep.Stop2,
                Action = () =>
                {
                }
            });
            dir.Dirs.Add(new DirectionOne()
            {
                directionStep = DirectionStep.Stop3,
                Action = () =>
                {
                    maku.IsLogoShow(true);
                    PlayAudio("group_logo", new string[] { "SE" });
                }
            });
            directions.Add(dir);
        }


        {
            var dir = new Direction() { Name = "battle_in_group_1" };
            dir.Dirs.Add(new DirectionOne()
            {
                directionStep = DirectionStep.LeberOn,
                Action = () =>
                {
                    SetBackGround("group_1");
                    maku.SetAnimation("");
                    maku.SetAnimation("close");
                    maku.IsLogoShow(false);
                    cutin.SetAnimation("");
                    battle.SetAnimation("");
                    PlayAudio("group_shugou_chance", new string[] { "SE" });
                    PlayAudio("group_bgm", new string[] { "BGM" });
                }
            });
            dir.Dirs.Add(new DirectionOne()
            {
                directionStep = DirectionStep.Stop3,
                Action = () =>
                {
                    SetBackGround("");
                    maku.SetAnimation("open");
                    PlayAudio("battle_in_voice", new string[] { "SE" }, delay: 0.75f);
                    PlayAudio("battle_bgm", new string[] { "BGM" });

                    var v = Lottery.LotteryBase.DefaultLottery(new int[] {
                        GetLotStringValue( 100,    4,    7,   10) , // 0
                        GetLotStringValue(  17,   50,   45,   40)
                    });

                    switch (v)
                    {
                        default:
                            battle.SetAnimation("battle_in");
                            break;
                        case 1:
                            battle.SetAnimation("battle_in_b");
                            break;
                    }
                }
            });
            directions.Add(dir);
        }

        {
            var dir = new Direction() { Name = "battle_in_group_2" };
            dir.Dirs.Add(new DirectionOne()
            {
                directionStep = DirectionStep.LeberOn,
                Action = () =>
                {
                    SetBackGround("group_2");
                    maku.SetAnimation("");
                    maku.SetAnimation("close");
                    maku.IsLogoShow(false);
                    cutin.SetAnimation("");
                    battle.SetAnimation("");
                    PlayAudio("group_shugou_chance", new string[] { "SE" });
                    PlayAudio("group_bgm", new string[] { "BGM" });
                }
            });
            dir.Dirs.Add(new DirectionOne()
            {
                directionStep = DirectionStep.Stop3,
                Action = () =>
                {
                    SetBackGround("");
                    maku.SetAnimation("open");
                    PlayAudio("battle_in_voice", new string[] { "SE" }, delay: 0.75f);
                    PlayAudio("battle_bgm", new string[] { "BGM" });

                    var v = Lottery.LotteryBase.DefaultLottery(new int[] {
                        GetLotStringValue( 100,    4,    7,   10) , // 0
                        GetLotStringValue(  17,   50,   45,   40)
                    });

                    switch (v)
                    {
                        default:
                            battle.SetAnimation("battle_in");
                            break;
                        case 1:
                            battle.SetAnimation("battle_in_b");
                            break;
                    }
                }
            });
            directions.Add(dir);
        }

        {
            var dir = new Direction() { Name = "battle_in_group_3" };
            dir.Dirs.Add(new DirectionOne()
            {
                directionStep = DirectionStep.LeberOn,
                Action = () =>
                {
                    SetBackGround("group_3");
                    maku.SetAnimation("");
                    maku.SetAnimation("close");
                    maku.IsLogoShow(false);
                    cutin.SetAnimation("");
                    battle.SetAnimation("");
                    PlayAudio("group_shugou_chance", new string[] { "SE" });
                    PlayAudio("group_bgm", new string[] { "BGM" });
                }
            });
            dir.Dirs.Add(new DirectionOne()
            {
                directionStep = DirectionStep.Stop3,
                Action = () =>
                {
                    SetBackGround("");
                    maku.SetAnimation("open");
                    PlayAudio("battle_in_voice", new string[] { "SE" }, delay: 0.75f);
                    PlayAudio("battle_bgm", new string[] { "BGM" });

                    var v = Lottery.LotteryBase.DefaultLottery(new int[] {
                        GetLotStringValue( 100,    4,    7,   10) , // 0
                        GetLotStringValue(  17,   50,   45,   40)
                    });

                    switch (v)
                    {
                        default:
                            battle.SetAnimation("battle_in");
                            break;
                        case 1:
                            battle.SetAnimation("battle_in_b");
                            break;
                    }
                }
            });
            directions.Add(dir);
        }
        {
            var dir = new Direction() { Name = "battle_in_group_4" };
            dir.Dirs.Add(new DirectionOne()
            {
                directionStep = DirectionStep.LeberOn,
                Action = () =>
                {
                    SetBackGround("group_4");
                    maku.SetAnimation("");
                    maku.SetAnimation("close");
                    maku.IsLogoShow(false);
                    cutin.SetAnimation("");
                    battle.SetAnimation("");
                    PlayAudio("group_shugou_chance", new string[] { "SE" });
                    PlayAudio("group_bgm", new string[] { "BGM" });
                }
            });
            dir.Dirs.Add(new DirectionOne()
            {
                directionStep = DirectionStep.Stop3,
                Action = () =>
                {
                    SetBackGround("");
                    maku.SetAnimation("open");
                    PlayAudio("battle_in_voice", new string[] { "SE" }, delay: 0.75f);
                    PlayAudio("battle_bgm", new string[] { "BGM" });

                    var v = Lottery.LotteryBase.DefaultLottery(new int[] {
                        GetLotStringValue( 100,    4,    7,   10) , // 0
                        GetLotStringValue(  17,   50,   45,   40)
                    });

                    switch (v)
                    {
                        default:
                            battle.SetAnimation("battle_in");
                            break;
                        case 1:
                            battle.SetAnimation("battle_in_b");
                            break;
                    }
                }
            });
            directions.Add(dir);
        }

        {
            var dir = new Direction() { Name = "battle_in_group_5" };
            dir.Dirs.Add(new DirectionOne()
            {
                directionStep = DirectionStep.LeberOn,
                Action = () =>
                {
                    SetBackGround("group_5");
                    maku.SetAnimation("");
                    maku.SetAnimation("close");
                    maku.IsLogoShow(false);
                    cutin.SetAnimation("");
                    battle.SetAnimation("");
                    PlayAudio("group_shugou_chance", new string[] { "SE" });
                    PlayAudio("group_bgm", new string[] { "BGM" });
                }
            });
            dir.Dirs.Add(new DirectionOne()
            {
                directionStep = DirectionStep.Stop3,
                Action = () =>
                {
                    SetBackGround("");
                    maku.SetAnimation("open");
                    PlayAudio("battle_in_voice", new string[] { "SE" }, delay: 0.75f);

                    var v = Lottery.LotteryBase.DefaultLottery(new int[] {
                        GetLotStringValue( 100,    4,    7,   10) , // 0
                        GetLotStringValue(  17,   50,   45,   40)
                    });

                    switch(v)
                    {
                        default:
                            battle.SetAnimation("battle_in");
                            break;
                        case 1:
                            battle.SetAnimation("battle_in_b");
                            break;
                    }
                    PlayAudio("battle_bgm", new string[] { "BGM" });
                }
            });
            directions.Add(dir);
        }

        {
            var dir = new Direction() { Name = "battle_1" };
            dir.Dirs.Add(new DirectionOne()
            {
                directionStep = DirectionStep.LeberOn,
                Action = () =>
                {
                    SetBackGround("");
                    maku.SetAnimation("");
                    maku.IsLogoShow(false);
                    cutin.SetAnimation("");
                    battle.SetAnimation("battle_01");
                    PlayAudio("battle_bgm", new string[] { "BGM" });


                    var v = Lottery.LotteryBase.DefaultLottery(new int[] {
                        GetLotStringValue( 100,   10,   20,   30) , // 0
                        GetLotStringValue(  20,   50,   40,   30) ,
                        GetLotStringValue(   5,   40,   30,   20) ,
                    });

                    switch(v)
                    {
                        case 0: battle.SetBackground("battle_bg_01"); break;
                        case 1: battle.SetBackground("battle_bg_02"); break;
                        case 2: battle.SetBackground("battle_bg_03"); break;
                    }
                    
                }
            });

            dir.Dirs.Add(new DirectionOne()
            {
                directionStep = DirectionStep.Stop1,
                Action = () =>
                {
                    battle.SetAnimation("battle_02");
                    PlayAudio("battle_02", new string[] { "SE" });
                }
            });
            dir.Dirs.Add(new DirectionOne()
            {
                directionStep = DirectionStep.Stop2,
                Action = () =>
                {
                    battle.SetAnimation("battle_02_b");
                    PlayAudio("battle_02_b", new string[] { "SE" });
                }
            });
            dir.Dirs.Add(new DirectionOne()
            {
                directionStep = DirectionStep.Stop3,
                Action = () =>
                {
                    DeleteAudios("BGM");

                    if (bonus == "")
                    {
                        battle.SetAnimation("battle_03");
                        battle.SetBackground("");
                        PlayAudio("battle_lose", new string[] { "SE" });
                    }
                    else
                    {
                        battle.SetAnimation("battle_04");
                        battle.SetBackground("");
                        PlayAudio("battle_win", new string[] { "SE" });

                        status = "bonus_fix_in";
                    }
                }
            });
            directions.Add(dir);
        }

        {
            var dir = new Direction() { Name = "bonus_fix_in" };
            dir.Dirs.Add(new DirectionOne()
            {
                directionStep = DirectionStep.LeberOn,
                Action = () =>
                {
                    SetBackGround("");
                    maku.SetAnimation("");
                    maku.IsLogoShow(false);
                    cutin.SetAnimation("");
                    battle.SetAnimation("bonus_fix");
                    PlayAudio("bonus_fix", new string[] { "SE" }, delay: 1.5f);
                    PlayAudio("bonus_fix_in_single", new string[] { "SE" });
                    DeleteAudios("BGM");


                }
            });

            dir.Dirs.Add(new DirectionOne()
            {
                directionStep = DirectionStep.Stop1,
                Action = () =>
                {
                }
            });
            dir.Dirs.Add(new DirectionOne()
            {
                directionStep = DirectionStep.Stop2,
                Action = () =>
                {
                }
            });
            dir.Dirs.Add(new DirectionOne()
            {
                directionStep = DirectionStep.Stop3,
                Action = () =>
                {
                }
            });
            directions.Add(dir);
        }

        {
            var dir = new Direction() { Name = "bonus_fix" };
            dir.Dirs.Add(new DirectionOne()
            {
                directionStep = DirectionStep.LeberOn,
                Action = () =>
                {
                    SetBackGround("");
                    maku.SetAnimation("");
                    maku.IsLogoShow(false);
                    cutin.SetAnimation("");
                    battle.SetAnimation("bonus_fix");
                    DeleteAudios("BGM");


                }
            });
            directions.Add(dir);
        }

        {
            var dir = new Direction() { Name = "bonus_fix_big_r7_in" };
            dir.Dirs.Add(new DirectionOne()
            {
                directionStep = DirectionStep.LeberOn,
                Action = () =>
                {
                    SetBackGround("");
                    maku.SetAnimation("");
                    maku.IsLogoShow(false);
                    cutin.SetAnimation("");
                    battle.SetAnimation("bonus_fix_big_r7");
                    DeleteAudios("BGM");

                    PlayAudio("bonus_fix_show", new string[] { "SE" });

                }
            });
            directions.Add(dir);
        }

        {
            var dir = new Direction() { Name = "bonus_fix_big_r7" };
            dir.Dirs.Add(new DirectionOne()
            {
                directionStep = DirectionStep.LeberOn,
                Action = () =>
                {
                    SetBackGround("");
                    maku.SetAnimation("");
                    maku.IsLogoShow(false);
                    cutin.SetAnimation("");
                    battle.SetAnimation("bonus_fix_big_r7");
                    DeleteAudios("BGM");
                }
            });
            directions.Add(dir);
        }

        {
            var dir = new Direction() { Name = "bonus_fix_big_b7_in" };
            dir.Dirs.Add(new DirectionOne()
            {
                directionStep = DirectionStep.LeberOn,
                Action = () =>
                {
                    SetBackGround("");
                    maku.SetAnimation("");
                    maku.IsLogoShow(false);
                    cutin.SetAnimation("");
                    battle.SetAnimation("bonus_fix_big_b7");
                    DeleteAudios("BGM");

                    PlayAudio("bonus_fix_show", new string[] { "SE" });

                }
            });
            directions.Add(dir);
        }

        {
            var dir = new Direction() { Name = "bonus_fix_big_b7" };
            dir.Dirs.Add(new DirectionOne()
            {
                directionStep = DirectionStep.LeberOn,
                Action = () =>
                {
                    SetBackGround("");
                    maku.SetAnimation("");
                    maku.IsLogoShow(false);
                    cutin.SetAnimation("");
                    battle.SetAnimation("bonus_fix_big_b7");
                    DeleteAudios("BGM");
                }
            });
            directions.Add(dir);
        }
        {
            var dir = new Direction() { Name = "bonus_fix_reg_in" };
            dir.Dirs.Add(new DirectionOne()
            {
                directionStep = DirectionStep.LeberOn,
                Action = () =>
                {
                    SetBackGround("");
                    maku.SetAnimation("");
                    maku.IsLogoShow(false);
                    cutin.SetAnimation("");
                    battle.SetAnimation("bonus_fix_reg");
                    DeleteAudios("BGM");

                    PlayAudio("bonus_fix_show", new string[] { "SE" });

                }
            });
            directions.Add(dir);
        }

        {
            var dir = new Direction() { Name = "bonus_fix_reg" };
            dir.Dirs.Add(new DirectionOne()
            {
                directionStep = DirectionStep.LeberOn,
                Action = () =>
                {
                    SetBackGround("");
                    maku.SetAnimation("");
                    maku.IsLogoShow(false);
                    cutin.SetAnimation("");
                    battle.SetAnimation("bonus_fix_reg");
                    DeleteAudios("BGM");
                }
            });
            directions.Add(dir);
        }

        {
            var dir = new Direction() { Name = "bonus_game_reg" };
            dir.Dirs.Add(new DirectionOne()
            {
                directionStep = DirectionStep.LeberOn,
                Action = () =>
                {
                    SetBackGround("");
                    maku.SetAnimation("");
                    maku.IsLogoShow(false);
                    cutin.SetAnimation("");
                    battle.SetAnimation("bonus_game");
                    PlayAudio("bonus_reg", new string[] { "BGM" }, true);
                }
            });
            directions.Add(dir);
        }

        {
            var dir = new Direction() { Name = "bonus_game_b7" };
            dir.Dirs.Add(new DirectionOne()
            {
                directionStep = DirectionStep.LeberOn,
                Action = () =>
                {
                    SetBackGround("");
                    maku.SetAnimation("");
                    maku.IsLogoShow(false);
                    cutin.SetAnimation("");
                    battle.SetAnimation("bonus_game");
                    PlayAudio("bonus_b7", new string[] { "BGM" }, true);
                }
            });
            directions.Add(dir);
        }

        {
            var dir = new Direction() { Name = "bonus_game_r7" };
            dir.Dirs.Add(new DirectionOne()
            {
                directionStep = DirectionStep.LeberOn,
                Action = () =>
                {
                    SetBackGround("");
                    maku.SetAnimation("");
                    maku.IsLogoShow(false);
                    cutin.SetAnimation("");
                    battle.SetAnimation("bonus_game");
                    PlayAudio("bonus_r7", new string[] { "BGM" }, true);
                }
            });
            directions.Add(dir);
        }

        {
            var dir = new Direction() { Name = "bonus_end" };
            dir.Dirs.Add(new DirectionOne()
            {
                directionStep = DirectionStep.LeberOn,
                Action = () =>
                {
                    SetBackGround("");
                    maku.SetAnimation("");
                    maku.IsLogoShow(false);
                    cutin.SetAnimation("");
                    battle.SetAnimation("bonus_end");
                    battle.SetBonusGetCoinCounter(bonusOutCoin);
                    battle.IsShowBonusGetCoinCounter(true);

                    PlayAudio("bonus_end_voice", new string[] { "SE" }, delay:1.5f);
                    PlayAudio("bonus_end", new string[] { "BGM" });
                }
            });
            dir.Dirs.Add(new DirectionOne()
            {
                directionStep = DirectionStep.Stop3,
                Action = () =>
                {
                    DeleteAudios("BGM");
                }
            });
            directions.Add(dir);
        }

        //SetBackGround("default");//
        SetBackGround("group_5");

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetDirection( string name)
    {
        foreach( var i in directions)
        {
            if ( i.Name==name )
            {
                targetDirection = i;
            }
        }
    }

    public void LeberOn()
    {
        foreach( var i in targetDirection.Dirs )
        {
            if( i.directionStep == DirectionStep.LeberOn)
            {
                i.Action();
            }
        }
    }

    public void Stop1()
    {
        foreach (var i in targetDirection.Dirs)
        {
            if (i.directionStep == DirectionStep.Stop1)
            {
                i.Action();
            }
        }
    }

    public void Stop2()
    {
        foreach (var i in targetDirection.Dirs)
        {
            if (i.directionStep == DirectionStep.Stop2)
            {
                i.Action();
            }
        }
    }

    public void Stop3()
    {
        foreach (var i in targetDirection.Dirs)
        {
            if (i.directionStep == DirectionStep.Stop3)
            {
                i.Action();
            }
        }
    }

    // フラグを見て判断する
    public void SetDirection(string role, string bonus, SlotCoreScript.GameStage gameStage, string bonusType)
    {
        this.role = role;
        this.bonus = bonus;

        startScreen.SetActive(false);

        stageLot.LotBase(role, bonus, gameStage, bonusType);

        // ボーナス確定後、ボーナスをそろえられるときの音声を出す処理
        if ((bonus != "") && (role == "") && 
            ( (status== "bonus_fix_big_b7") || (status == "bonus_fix_big_r7") || (status == "bonus_fix_reg")) ) { 
            PlayAudio("bonus_fix_aim", new string[] { "SE" }, volume: 0.3f);
        }
    }

    private void SetBackGround( string key )
    {
        switch(key)
        {
            case "default":
                stageBackGround.gameObject.SetActive(true);
                stageBackGround.IsMove = true;
                stageBackGroundGroup.gameObject.SetActive(false);
                break;

            case "default_stop":
                stageBackGround.gameObject.SetActive(true);
                stageBackGround.IsMove = false;
                stageBackGroundGroup.gameObject.SetActive(false);
                break;

            case "group_1":
                stageBackGround.gameObject.SetActive(false);
                stageBackGround.IsMove = false;
                stageBackGroundGroup.gameObject.SetActive(true);
                stageBackGroundGroup.SetAnimation("a1");
                break;

            case "group_2":
                stageBackGround.gameObject.SetActive(false);
                stageBackGround.IsMove = false;
                stageBackGroundGroup.gameObject.SetActive(true);
                stageBackGroundGroup.SetAnimation("a2");
                break;

            case "group_3":
                stageBackGround.gameObject.SetActive(false);
                stageBackGround.IsMove = false;
                stageBackGroundGroup.gameObject.SetActive(true);
                stageBackGroundGroup.SetAnimation("a3");
                break;

            case "group_4":
                stageBackGround.gameObject.SetActive(false);
                stageBackGround.IsMove = false;
                stageBackGroundGroup.gameObject.SetActive(true);
                stageBackGroundGroup.SetAnimation("a4");
                break;

            case "group_5":
                stageBackGround.gameObject.SetActive(false);
                stageBackGround.IsMove = false;
                stageBackGroundGroup.gameObject.SetActive(true);
                stageBackGroundGroup.SetAnimation("a5");
                break;

            case "":
                stageBackGround.gameObject.SetActive(false);
                stageBackGround.IsMove = false;
                stageBackGroundGroup.gameObject.SetActive(false);
                stageBackGroundGroup.SetAnimation("");
                break;
        }
    }
    public void PlayAudio(string key, string[] tags, bool isLoop = false, float delay = 0f, float volume = 1.0f)
    {
        foreach (var i in AudioClips)
        {
            if (key == i.Key)
            {
                audioManager.PlayAudio(i.Clip, volume, tags, isLoop, delay);
            }
        }
    }
    public void DeleteAudios(string tag)
    {
        audioManager.DeleteAudios(tag);
    }

    public int GetLotStringValue(int normal, int r7, int b7, int reg)
    {
        return stageLot.GetLotStringValue( normal, r7, b7, reg);
    }

    public void BonusIn(string bonusTypeName)
    {
        SetBackGround("");
        maku.SetAnimation("");
        maku.IsLogoShow(false);
        cutin.SetAnimation("");
        charaSDSakamata.SetAnimation("");
        PlayAudio("bonus_in_bgm", new string[] { "BGM" });

        switch (bonusTypeName)
        {
            case "Reg":
                battle.SetAnimation("bonus_in_reg");
                PlayAudio("bonus_in_reg", new string[] { "SE" }, delay: 1.5f, volume: 1.0f);
                break;
            case "Big-b7":
                battle.SetAnimation("bonus_in_b7");
                PlayAudio("bonus_in_b7", new string[] { "SE" }, delay: 1.5f, volume: 1.0f);
                break;
            case "Big-r7":
                battle.SetAnimation("bonus_in_r7");
                PlayAudio("bonus_in_r7", new string[] { "SE" }, delay: 1.5f, volume: 1.0f);
                break;
        }

    }

}
