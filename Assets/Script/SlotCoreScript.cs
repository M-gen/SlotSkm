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
    GameObject betButtonOn;

    [SerializeField]
    GameObject betButtonOff;

    [SerializeField]
    GameObject leber;

    [SerializeField]
    GameObject buttonLightL1;

    [SerializeField]
    GameObject buttonLightL2;

    [SerializeField]
    GameObject buttonLightL3;

    [SerializeField]
    GameObject uiText;

    [SerializeField]
    GameObject Stage;

    [System.Serializable]
    public class AudioClipItems
    {
        public string Key;
        public AudioClip Clip;
    }

    [SerializeField]
    AudioClipItems[] AudioClips;


    Vector3 leberStartPostion;

    int baseLotsMax = 0;
    int bonusInLotsMax = 0;
    int bonusLotsMax = 0;

    public int CoinIn = 0;
    public int CoinOut = 0;
    public int GameCount = 0;
    public bool IsReplay = false;
    public bool IsPlay = false;

    public int BonusCoinOut = 0;

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

    int stopCount = 0;

    private void Start()
    {

        //{
        //    audioManager = GameObject.Find("Audio Manager").GetComponent<AudioManager>();
        //}

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

        buttonLightL1.active = false;
        buttonLightL2.active = false;
        buttonLightL3.active = false;
        betButtonOn.active = false;
        leberStartPostion = leber.transform.position;
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
                            //bonus = "";
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

    public void LeverOn( string role, string bonus )
    {
        stopCount = 0;
        stageEffect.SetDirection(role, bonus, gameStage);
        stageEffect.LeberOn();
        StartCoroutine("_LeverOn");
    }
    private IEnumerator _LeverOn()
    {
        leber.transform.position += new Vector3(-0.01f, -0.12f);

        buttonLightL1.active = true;
        buttonLightL2.active = true;
        buttonLightL3.active = true;

        // 1秒待つ  
        yield return new WaitForSeconds(0.5f);

        leber.transform.position = leberStartPostion;
    }

    public void OnButtonL1()
    {
        _OnButton();
        buttonLightL1.active = false;
        PlayAudio("game_03_reel_stop", new string[] { "SE" });

    }
    public void OnButtonL2()
    {
        _OnButton();
        buttonLightL2.active = false;
        PlayAudio("game_03_reel_stop", new string[] { "SE" });
    }
    public void OnButtonL3()
    {
        _OnButton();
        buttonLightL3.active = false;
        PlayAudio("game_03_reel_stop", new string[] { "SE" });
    }

    private void _OnButton()
    {
        switch(stopCount)
        {
            case 0:
                stageEffect.Stop1();
                break;
            case 1:
                stageEffect.Stop2();
                break;
            case 2:
                stageEffect.Stop3();
                break;

        }
        stopCount++;
    }

    public void UpdateText()
    {
        //var text = $"--COIN--\nIN : {CoinIn}\nOUT : {CoinOut}\n\n---\nGame : {GameCount}\nMode : {gameStage}";
        var text = $"--COIN--\nIN : {CoinIn}\nOUT : {CoinOut}\n\n---\nGame : {GameCount}";
        if(gameStage== GameStage.Bonus)
        {
            text += "\nMode : Bonus";
        }
        else
        {
            text += "\nMode : Normal";
        }
        uiText.GetComponent<UnityEngine.UI.Text>().text = text;
    }

    public void SetCoinIn( int value )
    {
        CoinIn += value;
    }
    public void SetCoinOut(int value)
    {
        CoinOut += value;

        if( gameStage == GameStage.Bonus) // ボーナス中
        {
            BonusCoinOut += value;

            var status = GetBonusStatus();
            if ( status.Coin <= BonusCoinOut)
            {
                // ボーナス中終了
                BonusCoinOut = 0;
                gameStage = GameStage.Normal;
                audioManager.DeleteAudios("BGM");
            }
        }
    }

    public void BetOnToRelease()
    {
        StartCoroutine("_BetOnToRelease");
    }

    private IEnumerator _BetOnToRelease()
    {
        betButtonOff.SetActive(false);
        betButtonOn.SetActive(true);
        // 1秒待つ  
        yield return new WaitForSeconds(0.5f);

        betButtonOff.SetActive(true);
        betButtonOn.SetActive(false);
    }

    public void PlayAudio( string key, string[] tags, bool isLoop = false, float delay=0f)
    {
        foreach( var i in AudioClips)
        {
            if(key==i.Key)
            {
                audioManager.PlayAudio(i.Clip, 1f, tags, isLoop, delay);
            }
        }
    }

    public BonusStatus GetBonusStatus()
    {
        foreach( var i in bonusType)
        {
            if (bonusTypeName==i.Name)
            {
                return i;
            }
        }
        return null;
    }
}
