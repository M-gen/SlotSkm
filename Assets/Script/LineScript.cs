using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineScript : MonoBehaviour
{
    [System.Serializable]
    public class Zugara {
        public string Key;
        public Sprite Sprite;
    }

    [SerializeField]
    SlotCoreScript SlotCoreScript;

    [SerializeField]
    List<Zugara> Zugaras = new List<Zugara>();

    [SerializeField]
    GameObject ZugaraPrefab;

    [SerializeField]
    Lamp LampL;

    [SerializeField]
    Lamp LampR;

    public class ZugaraStatus
    {
        public int Index;
        public int Line;
        public string Key;
        public GameObject GameObject;
    }

    public class ZugaraStatusPack
    {
        public List<ZugaraStatus> Reel = new List<ZugaraStatus>();
        public int index = 0;

        public float ReelTimer = 0f;
        public float ReelTimerMax = 0.0333f * 1; // 0.0333f 1分間80回転が上限
        public float ReelTimerMaxBase = 0.0333f;
        public float ReelTimerMaxOffset = 1.0f;

        public bool IsFix = false;
        public string[] FixZugara = new string[] { "", "", "" };

        public float Speed = 15.0f;
        public float OffsetX = 0f;
        public float OffsetY = 0f;

        public int srideIndex = 1;

        float FixTimer = 0f;
        float FixTimerMax = 0.19f; // 規定があるらしい0.19秒以内に停止しないといけない

        float scaleSize = 0.53f;
        float marginY = 1.80f * 0.45f;
        float marginYMax = 0;
        float returnYLine = 8f;
        LineScript lineScript;

        public void Init( float offsetX, float offsetY, string[] reelZugaraSource, GameObject zugaraPrefab, Transform parent, LineScript lineScript)
        {
            this.lineScript = lineScript;
            this.OffsetX = offsetX;
            this.OffsetY = offsetY;

            marginYMax = marginY * reelZugaraSource.Length;
            var y = 0.0f - index * marginY;
            if (y < -returnYLine)
            {
                y += marginYMax;
            }

            foreach (var i in reelZugaraSource)
            {
                var o = Instantiate(zugaraPrefab, parent);
                var zugara = lineScript.GetZugaraByKey(i);
                o.GetComponent<SpriteRenderer>().sprite = zugara.Sprite;
                o.transform.position = new Vector3(OffsetX, y+ OffsetY, 0);
                o.transform.localScale = new Vector3(scaleSize, scaleSize, 1);
                
                y -= marginY;

                if (y < -returnYLine)
                {
                    y += marginYMax;
                }

                var zs = new ZugaraStatus() { Index = Reel.Count, GameObject = o, Key = i, Line = 0 };
                Reel.Add(zs);
            }
        }

        public void Update()
        {
            if (!IsFix)
            {
                // ひとまず、固定場所で流れるのはできたのであとは、ここに補正で滑らかに動くように見せかける
                ReelTimer += Time.deltaTime;
                while (ReelTimerMax <= ReelTimer) // 何重にもしないとダメ
                {
                    ReelTimer -= ReelTimerMax;
                    index = (index + 1) % Reel.Count;
                }

                // 滑らかに見せかける仕組み
                // 一つ手前で動かす
                var mix = ReelTimer / ReelTimerMax;
                var offsetY2 = marginY * mix;


                var y = 0.0f - index * marginY - offsetY2;
                if (y < -returnYLine)
                {
                    y += marginYMax;
                }

                foreach (var i in Reel)
                {
                    i.GameObject.transform.position = new Vector3(OffsetX, y + OffsetY, 0);
                    y -= marginY;

                    if (y < -returnYLine)
                    {
                        y += marginYMax;
                    }
                }
            }
            else
            {
                FixTimer += Time.deltaTime;
                if (FixTimerMax <= FixTimer)
                {
                    FixTimer = FixTimerMax;
                }

                var mix = Mathf.Sin( (FixTimer / FixTimerMax)  * Mathf.PI / 2f);
                var offsetY2 = marginY * mix * (1+ srideIndex);

                var y = 0.0f - index * marginY - offsetY2 + marginY * (1 + srideIndex);
                if (y < -returnYLine)
                {
                    y += marginYMax;
                }

                foreach (var i in Reel)
                {
                    i.GameObject.transform.position = new Vector3(OffsetX, y + OffsetY, 0);
                    y -= marginY;

                    if (y < -returnYLine)
                    {
                        y += marginYMax;
                    }
                }

            }
        }

        public void Fix( int srideIndex = 0)
        {
            if (!IsFix)
            {
                IsFix = true;

                // すべる量を確定させる
                this.srideIndex = srideIndex;

                //
                FixTimer = ( ReelTimer / ReelTimerMax ) * ( 1f / (float)(srideIndex+1));
                ReelTimer = 0;
                index = (index + 1 + srideIndex) % Reel.Count;

                // 確定した図柄の検出
                var i2 = 0;
                var z1 = "";
                var z2 = "";
                var z3 = "";
                foreach (var i in Reel)
                {
                    var j = (i2 + index) % Reel.Count;
                    if (j == 19) z1 = i.Key;
                    if (j == 0) z2 = i.Key;
                    if (j == 1) z3 = i.Key;
                    i2++;
                }

                FixZugara[0] = z1;
                FixZugara[1] = z2;
                FixZugara[2] = z3;
                Debug.Log( $"Fix {z1} {z2} {z3}" );
            }
        }

        public string[] GetFixZugara( int slide )
        {
            var tmp = new string[] { "", "", "" };
            var tmp2 = (index + 1 + slide) % Reel.Count;

            var i2 = 0;
            foreach (var i in Reel)
            {
                var j = (i2 + tmp2) % Reel.Count;
                if (j == 19) tmp[0] = i.Key;
                if (j == 0) tmp[1] = i.Key;
                if (j == 1) tmp[2] = i.Key;
                i2++;
            }

            return tmp;
        }

        public void Play()
        {
            if (IsFix)
            {
                FixZugara[0] = "";
                FixZugara[1] = "";
                FixZugara[2] = "";
                IsFix = false;
            }
        }

        public bool IsReelStop()
        {
            if(IsFix)
            {
                if (FixTimer == FixTimerMax) return true;
            }

            return false;
        }
    }

    ZugaraStatusPack ZugaraStatusPackReel1 = new ZugaraStatusPack();
    ZugaraStatusPack ZugaraStatusPackReel2 = new ZugaraStatusPack();
    ZugaraStatusPack ZugaraStatusPackReel3 = new ZugaraStatusPack();

    [SerializeField]
    string[] Reel1ZugaraSource;

    [SerializeField]
    string[] Reel2ZugaraSource;

    [SerializeField]
    string[] Reel3ZugaraSource;

    enum SlotStep
    {
        NowGame,
        EndGameUpWait,
        EndGame,
    }
    SlotStep slotStep = SlotStep.EndGame;

    string bonus;
    string role;
    int buttonCount = 0;

    const float oneButtonWaitTimerMax = 0.3f;
    const float oneGameWaitTimerMax = 4.1f;
    float oneButtonWaitTimer = 0;
    float oneGameWaitTimer = oneGameWaitTimerMax;
    bool preLeberOn = false;

    // Start is called before the first frame update
    void Start()
    {
        ZugaraStatusPackReel1.Init(-2.15f, -0.41f, Reel1ZugaraSource, ZugaraPrefab, transform, this);
        ZugaraStatusPackReel2.Init(    0f, -0.41f, Reel2ZugaraSource, ZugaraPrefab, transform, this);
        ZugaraStatusPackReel3.Init( 2.15f, -0.41f, Reel3ZugaraSource, ZugaraPrefab, transform, this);

    }

    private Zugara GetZugaraByKey(string key)
    {
        foreach (var i in Zugaras)
        {
            if (i.Key == key) return i;
        }
        return null;
    }

    void Update()
    {
        oneButtonWaitTimer += Time.deltaTime;
        oneGameWaitTimer += Time.deltaTime;

        switch (slotStep)
        {
            case SlotStep.NowGame:

                if (oneButtonWaitTimerMax <= oneButtonWaitTimer)
                {
                    if (Input.GetKeyDown(KeyCode.LeftArrow))
                    {
                        oneButtonWaitTimer = 0;
                        if (!ZugaraStatusPackReel1.IsFix)
                        {
                            var srideIndex = 0;
                            var key = "";
                            switch (role)
                            {
                                default:
                                    switch (bonus)
                                    {
                                        case "Big-r7":
                                            Debug.Log($"Big-r7");
                                            key = "r7";
                                            if (buttonCount == 0)
                                            {
                                                srideIndex = FixSride_O_O_O(key, ZugaraStatusPackReel1);
                                                if (srideIndex == -1) srideIndex = 0;
                                            }
                                            else if (buttonCount == 1)
                                            {
                                                if (ZugaraStatusPackReel2.IsFix)
                                                {
                                                    if (ZugaraStatusPackReel2.FixZugara[0] == key)
                                                    {
                                                        // key key ???
                                                        // --- --- ???
                                                        // --- --- ???
                                                        srideIndex = FixSride_O_X_X(key, ZugaraStatusPackReel1);
                                                        if (srideIndex == -1) srideIndex = 0;
                                                    }
                                                    else if (ZugaraStatusPackReel2.FixZugara[1] == key)
                                                    {
                                                        // key --- ???
                                                        // key key ???
                                                        // key --- ???
                                                        srideIndex = FixSride_O_O_O(key, ZugaraStatusPackReel1);
                                                        if (srideIndex == -1) srideIndex = 0;
                                                    }
                                                    else if (ZugaraStatusPackReel2.FixZugara[2] == key)
                                                    {
                                                        // --- --- ???
                                                        // --- --- ???
                                                        // key key ???
                                                        srideIndex = FixSride_X_X_O(key, ZugaraStatusPackReel1);
                                                        if (srideIndex == -1) srideIndex = 0;
                                                    }
                                                }
                                                else
                                                {
                                                    if (ZugaraStatusPackReel3.FixZugara[0] == key)
                                                    {
                                                        // key ??? key
                                                        // --- ??? ---
                                                        // key ??? ---
                                                        srideIndex = FixSride_O_X_O(key, ZugaraStatusPackReel1);
                                                        if (srideIndex == -1) srideIndex = 0;
                                                    }
                                                    else if (ZugaraStatusPackReel3.FixZugara[1] == key)
                                                    {
                                                        // --- ??? ---
                                                        // key ??? key
                                                        // --- ??? ---
                                                        srideIndex = FixSride_X_O_X(key, ZugaraStatusPackReel1);
                                                        if (srideIndex == -1) srideIndex = 0;
                                                    }
                                                    else if (ZugaraStatusPackReel3.FixZugara[2] == key)
                                                    {
                                                        // key ??? ---
                                                        // --- ??? ---
                                                        // key ??? key
                                                        srideIndex = FixSride_O_X_O(key, ZugaraStatusPackReel1);
                                                        if (srideIndex == -1) srideIndex = 0;
                                                    }
                                                }
                                            }
                                            else if (buttonCount == 2)
                                            {
                                                if ((ZugaraStatusPackReel2.FixZugara[0] == key) && (ZugaraStatusPackReel3.FixZugara[0] == key))
                                                {
                                                    // key key key
                                                    // --- --- ---
                                                    // --- --- ---
                                                    srideIndex = FixSride_O_X_X(key, ZugaraStatusPackReel1);
                                                    if (srideIndex == -1) srideIndex = 0;
                                                }
                                                else if ((ZugaraStatusPackReel2.FixZugara[1] == key) && (ZugaraStatusPackReel3.FixZugara[2] == key))
                                                {
                                                    // key --- ---
                                                    // --- key ---
                                                    // --- --- key
                                                    srideIndex = FixSride_O_X_X(key, ZugaraStatusPackReel1);
                                                    if (srideIndex == -1) srideIndex = 0;
                                                }
                                                else if ((ZugaraStatusPackReel2.FixZugara[1] == key) && (ZugaraStatusPackReel3.FixZugara[1] == key))
                                                {
                                                    // --- --- ---
                                                    // key key key
                                                    // --- --- ---
                                                    srideIndex = FixSride_X_O_X(key, ZugaraStatusPackReel1);
                                                    if (srideIndex == -1) srideIndex = 0;
                                                }
                                                else if ((ZugaraStatusPackReel2.FixZugara[1] == key) && (ZugaraStatusPackReel3.FixZugara[0] == key))
                                                {
                                                    // --- --- key
                                                    // --- key ---
                                                    // key --- ---
                                                    srideIndex = FixSride_X_X_O(key, ZugaraStatusPackReel1);
                                                    if (srideIndex == -1) srideIndex = 0;
                                                }
                                                else if ((ZugaraStatusPackReel2.FixZugara[2] == key) && (ZugaraStatusPackReel3.FixZugara[2] == key))
                                                {
                                                    // --- --- ---
                                                    // --- --- ---
                                                    // key key key
                                                    srideIndex = FixSride_X_X_O(key, ZugaraStatusPackReel1);
                                                    if (srideIndex == -1) srideIndex = 0;
                                                }
                                            }
                                            break;
                                        case "Big-b7":
                                            key = "b7";
                                            if (buttonCount == 0)
                                            {
                                                srideIndex = FixSride_O_O_O(key, ZugaraStatusPackReel1);
                                                if (srideIndex == -1) srideIndex = 0;
                                            }
                                            else if (buttonCount == 1)
                                            {
                                                if (ZugaraStatusPackReel2.IsFix)
                                                {
                                                    if (ZugaraStatusPackReel2.FixZugara[0] == key)
                                                    {
                                                        // key key ???
                                                        // --- --- ???
                                                        // --- --- ???
                                                        srideIndex = FixSride_O_X_X(key, ZugaraStatusPackReel1);
                                                        if (srideIndex == -1) srideIndex = 0;
                                                    }
                                                    else if (ZugaraStatusPackReel2.FixZugara[1] == key)
                                                    {
                                                        // key --- ???
                                                        // key key ???
                                                        // key --- ???
                                                        srideIndex = FixSride_O_O_O(key, ZugaraStatusPackReel1);
                                                        if (srideIndex == -1) srideIndex = 0;
                                                    }
                                                    else if (ZugaraStatusPackReel2.FixZugara[2] == key)
                                                    {
                                                        // --- --- ???
                                                        // --- --- ???
                                                        // key key ???
                                                        srideIndex = FixSride_X_X_O(key, ZugaraStatusPackReel1);
                                                        if (srideIndex == -1) srideIndex = 0;
                                                    }
                                                }
                                                else
                                                {
                                                    if (ZugaraStatusPackReel3.FixZugara[0] == key)
                                                    {
                                                        // key ??? key
                                                        // --- ??? ---
                                                        // key ??? ---
                                                        srideIndex = FixSride_O_X_O(key, ZugaraStatusPackReel1);
                                                        if (srideIndex == -1) srideIndex = 0;
                                                    }
                                                    else if (ZugaraStatusPackReel3.FixZugara[1] == key)
                                                    {
                                                        // --- ??? ---
                                                        // key ??? key
                                                        // --- ??? ---
                                                        srideIndex = FixSride_X_O_X(key, ZugaraStatusPackReel1);
                                                        if (srideIndex == -1) srideIndex = 0;
                                                    }
                                                    else if (ZugaraStatusPackReel3.FixZugara[2] == key)
                                                    {
                                                        // key ??? ---
                                                        // --- ??? ---
                                                        // key ??? key
                                                        srideIndex = FixSride_O_X_O(key, ZugaraStatusPackReel1);
                                                        if (srideIndex == -1) srideIndex = 0;
                                                    }
                                                }
                                            }
                                            else if (buttonCount == 2)
                                            {
                                                if ((ZugaraStatusPackReel2.FixZugara[0] == key) && (ZugaraStatusPackReel3.FixZugara[0] == key))
                                                {
                                                    // key key key
                                                    // --- --- ---
                                                    // --- --- ---
                                                    srideIndex = FixSride_O_X_X(key, ZugaraStatusPackReel1);
                                                    if (srideIndex == -1) srideIndex = 0;
                                                }
                                                else if ((ZugaraStatusPackReel2.FixZugara[1] == key) && (ZugaraStatusPackReel3.FixZugara[2] == key))
                                                {
                                                    // key --- ---
                                                    // --- key ---
                                                    // --- --- key
                                                    srideIndex = FixSride_O_X_X(key, ZugaraStatusPackReel1);
                                                    if (srideIndex == -1) srideIndex = 0;
                                                }
                                                else if ((ZugaraStatusPackReel2.FixZugara[1] == key) && (ZugaraStatusPackReel3.FixZugara[1] == key))
                                                {
                                                    // --- --- ---
                                                    // key key key
                                                    // --- --- ---
                                                    srideIndex = FixSride_X_O_X(key, ZugaraStatusPackReel1);
                                                    if (srideIndex == -1) srideIndex = 0;
                                                }
                                                else if ((ZugaraStatusPackReel2.FixZugara[1] == key) && (ZugaraStatusPackReel3.FixZugara[0] == key))
                                                {
                                                    // --- --- key
                                                    // --- key ---
                                                    // key --- ---
                                                    srideIndex = FixSride_X_X_O(key, ZugaraStatusPackReel1);
                                                    if (srideIndex == -1) srideIndex = 0;
                                                }
                                                else if ((ZugaraStatusPackReel2.FixZugara[2] == key) && (ZugaraStatusPackReel3.FixZugara[2] == key))
                                                {
                                                    // --- --- ---
                                                    // --- --- ---
                                                    // key key key
                                                    srideIndex = FixSride_X_X_O(key, ZugaraStatusPackReel1);
                                                    if (srideIndex == -1) srideIndex = 0;
                                                }
                                            }
                                            break;
                                        case "Reg":
                                            key = "r7";
                                            if (buttonCount == 0)
                                            {
                                                srideIndex = FixSride_O_O_O(key, ZugaraStatusPackReel1);
                                                if (srideIndex == -1) srideIndex = 0;
                                            }
                                            else if (buttonCount == 1)
                                            {
                                                if (ZugaraStatusPackReel2.IsFix)
                                                {
                                                    if (ZugaraStatusPackReel2.FixZugara[0] == key)
                                                    {
                                                        // key key ???
                                                        // --- --- ???
                                                        // --- --- ???
                                                        srideIndex = FixSride_O_X_X(key, ZugaraStatusPackReel1);
                                                        if (srideIndex == -1) srideIndex = 0;
                                                    }
                                                    else if (ZugaraStatusPackReel2.FixZugara[1] == key)
                                                    {
                                                        // key --- ???
                                                        // key key ???
                                                        // key --- ???
                                                        srideIndex = FixSride_O_O_O(key, ZugaraStatusPackReel1);
                                                        if (srideIndex == -1) srideIndex = 0;
                                                    }
                                                    else if (ZugaraStatusPackReel2.FixZugara[2] == key)
                                                    {
                                                        // --- --- ???
                                                        // --- --- ???
                                                        // key key ???
                                                        srideIndex = FixSride_X_X_O(key, ZugaraStatusPackReel1);
                                                        if (srideIndex == -1) srideIndex = 0;
                                                    }
                                                }
                                                else
                                                {
                                                    if (ZugaraStatusPackReel3.FixZugara[0] == "bar")
                                                    {
                                                        // key ??? key
                                                        // --- ??? ---
                                                        // key ??? ---
                                                        srideIndex = FixSride_O_X_O(key, ZugaraStatusPackReel1);
                                                        if (srideIndex == -1) srideIndex = 0;
                                                    }
                                                    else if (ZugaraStatusPackReel3.FixZugara[1] == "bar")
                                                    {
                                                        // --- ??? ---
                                                        // key ??? key
                                                        // --- ??? ---
                                                        srideIndex = FixSride_X_O_X(key, ZugaraStatusPackReel1);
                                                        if (srideIndex == -1) srideIndex = 0;
                                                    }
                                                    else if (ZugaraStatusPackReel3.FixZugara[2] == "bar")
                                                    {
                                                        // key ??? ---
                                                        // --- ??? ---
                                                        // key ??? key
                                                        srideIndex = FixSride_O_X_O(key, ZugaraStatusPackReel1);
                                                        if (srideIndex == -1) srideIndex = 0;
                                                    }
                                                }
                                            }
                                            else if (buttonCount == 2)
                                            {
                                                if ((ZugaraStatusPackReel2.FixZugara[0] == key) && (ZugaraStatusPackReel3.FixZugara[0] == "bar"))
                                                {
                                                    // key key key
                                                    // --- --- ---
                                                    // --- --- ---
                                                    srideIndex = FixSride_O_X_X(key, ZugaraStatusPackReel1);
                                                    if (srideIndex == -1) srideIndex = 0;
                                                }
                                                else if ((ZugaraStatusPackReel2.FixZugara[1] == key) && (ZugaraStatusPackReel3.FixZugara[2] == "bar"))
                                                {
                                                    // key --- ---
                                                    // --- key ---
                                                    // --- --- key
                                                    srideIndex = FixSride_O_X_X(key, ZugaraStatusPackReel1);
                                                    if (srideIndex == -1) srideIndex = 0;
                                                }
                                                else if ((ZugaraStatusPackReel2.FixZugara[1] == key) && (ZugaraStatusPackReel3.FixZugara[1] == "bar"))
                                                {
                                                    // --- --- ---
                                                    // key key key
                                                    // --- --- ---
                                                    srideIndex = FixSride_X_O_X(key, ZugaraStatusPackReel1);
                                                    if (srideIndex == -1) srideIndex = 0;
                                                }
                                                else if ((ZugaraStatusPackReel2.FixZugara[1] == key) && (ZugaraStatusPackReel3.FixZugara[0] == "bar"))
                                                {
                                                    // --- --- key
                                                    // --- key ---
                                                    // key --- ---
                                                    srideIndex = FixSride_X_X_O(key, ZugaraStatusPackReel1);
                                                    if (srideIndex == -1) srideIndex = 0;
                                                }
                                                else if ((ZugaraStatusPackReel2.FixZugara[2] == key) && (ZugaraStatusPackReel3.FixZugara[2] == "bar"))
                                                {
                                                    // --- --- ---
                                                    // --- --- ---
                                                    // key key key
                                                    srideIndex = FixSride_X_X_O(key, ZugaraStatusPackReel1);
                                                    if (srideIndex == -1) srideIndex = 0;
                                                }
                                            }
                                            break;
                                    }
                                    break;
                                case "rep":
                                    key = "rep";
                                    if (buttonCount == 0)
                                    {
                                        srideIndex = FixSride_O_O_O(key, ZugaraStatusPackReel1);
                                        if (srideIndex == -1) srideIndex = 0;
                                    }
                                    else if (buttonCount == 1)
                                    {
                                        if (ZugaraStatusPackReel2.IsFix)
                                        {
                                            if (ZugaraStatusPackReel2.FixZugara[0] == key)
                                            {
                                                // key key ???
                                                // --- --- ???
                                                // --- --- ???
                                                srideIndex = FixSride_O_X_X(key, ZugaraStatusPackReel1);
                                                if (srideIndex == -1) srideIndex = 0;
                                            }
                                            else if (ZugaraStatusPackReel2.FixZugara[1] == key)
                                            {
                                                // key --- ???
                                                // key key ???
                                                // key --- ???
                                                srideIndex = FixSride_O_O_O(key, ZugaraStatusPackReel1);
                                                if (srideIndex == -1) srideIndex = 0;
                                            }
                                            else if (ZugaraStatusPackReel2.FixZugara[2] == key)
                                            {
                                                // --- --- ???
                                                // --- --- ???
                                                // key key ???
                                                srideIndex = FixSride_X_X_O(key, ZugaraStatusPackReel1);
                                                if (srideIndex == -1) srideIndex = 0;
                                            }
                                        }
                                        else
                                        {
                                            if (ZugaraStatusPackReel3.FixZugara[0] == key)
                                            {
                                                // key ??? key
                                                // --- ??? ---
                                                // key ??? ---
                                                srideIndex = FixSride_O_X_O(key, ZugaraStatusPackReel1);
                                                if (srideIndex == -1) srideIndex = 0;
                                            }
                                            else if (ZugaraStatusPackReel3.FixZugara[1] == key)
                                            {
                                                // --- ??? ---
                                                // key ??? key
                                                // --- ??? ---
                                                srideIndex = FixSride_X_O_X(key, ZugaraStatusPackReel1);
                                                if (srideIndex == -1) srideIndex = 0;
                                            }
                                            else if (ZugaraStatusPackReel3.FixZugara[2] == key)
                                            {
                                                // key ??? ---
                                                // --- ??? ---
                                                // key ??? key
                                                srideIndex = FixSride_O_X_O(key, ZugaraStatusPackReel1);
                                                if (srideIndex == -1) srideIndex = 0;
                                            }
                                        }
                                    }
                                    else if (buttonCount == 2)
                                    {
                                        if ((ZugaraStatusPackReel2.FixZugara[0] == key) && (ZugaraStatusPackReel3.FixZugara[0] == key))
                                        {
                                            // key key key
                                            // --- --- ---
                                            // --- --- ---
                                            srideIndex = FixSride_O_X_X(key, ZugaraStatusPackReel1);
                                            if (srideIndex == -1) srideIndex = 0;
                                        }
                                        else if ((ZugaraStatusPackReel2.FixZugara[1] == key) && (ZugaraStatusPackReel3.FixZugara[2] == key))
                                        {
                                            // key --- ---
                                            // --- key ---
                                            // --- --- key
                                            srideIndex = FixSride_O_X_X(key, ZugaraStatusPackReel1);
                                            if (srideIndex == -1) srideIndex = 0;
                                        }
                                        else if ((ZugaraStatusPackReel2.FixZugara[1] == key) && (ZugaraStatusPackReel3.FixZugara[1] == key))
                                        {
                                            // --- --- ---
                                            // key key key
                                            // --- --- ---
                                            srideIndex = FixSride_X_O_X(key, ZugaraStatusPackReel1);
                                            if (srideIndex == -1) srideIndex = 0;
                                        }
                                        else if ((ZugaraStatusPackReel2.FixZugara[1] == key) && (ZugaraStatusPackReel3.FixZugara[0] == key))
                                        {
                                            // --- --- key
                                            // --- key ---
                                            // key --- ---
                                            srideIndex = FixSride_X_X_O(key, ZugaraStatusPackReel1);
                                            if (srideIndex == -1) srideIndex = 0;
                                        }
                                        else if ((ZugaraStatusPackReel2.FixZugara[2] == key) && (ZugaraStatusPackReel3.FixZugara[2] == key))
                                        {
                                            // --- --- ---
                                            // --- --- ---
                                            // key key key
                                            srideIndex = FixSride_X_X_O(key, ZugaraStatusPackReel1);
                                            if (srideIndex == -1) srideIndex = 0;
                                        }
                                    }
                                    break;
                                case "bell":
                                    key = "bell";
                                    if (buttonCount == 0)
                                    {
                                        srideIndex = FixSride_O_O_O(key, ZugaraStatusPackReel1);
                                        if (srideIndex == -1) srideIndex = 0;
                                    }
                                    else if (buttonCount == 1)
                                    {
                                        if (ZugaraStatusPackReel2.IsFix)
                                        {
                                            if (ZugaraStatusPackReel2.FixZugara[0] == key)
                                            {
                                                // key key ???
                                                // --- --- ???
                                                // --- --- ???
                                                srideIndex = FixSride_O_X_X(key, ZugaraStatusPackReel1);
                                                if (srideIndex == -1) srideIndex = 0;
                                            }
                                            else if (ZugaraStatusPackReel2.FixZugara[1] == key)
                                            {
                                                // key --- ???
                                                // key key ???
                                                // key --- ???
                                                srideIndex = FixSride_O_O_O(key, ZugaraStatusPackReel1);
                                                if (srideIndex == -1) srideIndex = 0;
                                            }
                                            else if (ZugaraStatusPackReel2.FixZugara[2] == key)
                                            {
                                                // --- --- ???
                                                // --- --- ???
                                                // key key ???
                                                srideIndex = FixSride_X_X_O(key, ZugaraStatusPackReel1);
                                                if (srideIndex == -1) srideIndex = 0;
                                            }
                                        }
                                        else
                                        {
                                            if (ZugaraStatusPackReel3.FixZugara[0] == key)
                                            {
                                                // key ??? key
                                                // --- ??? ---
                                                // key ??? ---
                                                srideIndex = FixSride_O_X_O(key, ZugaraStatusPackReel1);
                                                if (srideIndex == -1) srideIndex = 0;
                                            }
                                            else if (ZugaraStatusPackReel3.FixZugara[1] == key)
                                            {
                                                // --- ??? ---
                                                // key ??? key
                                                // --- ??? ---
                                                srideIndex = FixSride_X_O_X(key, ZugaraStatusPackReel1);
                                                if (srideIndex == -1) srideIndex = 0;
                                            }
                                            else if (ZugaraStatusPackReel3.FixZugara[2] == key)
                                            {
                                                // key ??? ---
                                                // --- ??? ---
                                                // key ??? key
                                                srideIndex = FixSride_O_X_O(key, ZugaraStatusPackReel1);
                                                if (srideIndex == -1) srideIndex = 0;
                                            }

                                        }
                                    }
                                    else if (buttonCount == 2)
                                    {
                                        if ((ZugaraStatusPackReel2.FixZugara[0] == key) && (ZugaraStatusPackReel3.FixZugara[0] == key))
                                        {
                                            // key key key
                                            // --- --- ---
                                            // --- --- ---
                                            srideIndex = FixSride_O_X_X(key, ZugaraStatusPackReel1);
                                            if (srideIndex == -1) srideIndex = 0;
                                        }
                                        else if ((ZugaraStatusPackReel2.FixZugara[1] == key) && (ZugaraStatusPackReel3.FixZugara[2] == key))
                                        {
                                            // key --- ---
                                            // --- key ---
                                            // --- --- key
                                            srideIndex = FixSride_O_X_X(key, ZugaraStatusPackReel1);
                                            if (srideIndex == -1) srideIndex = 0;
                                        }
                                        else if ((ZugaraStatusPackReel2.FixZugara[1] == key) && (ZugaraStatusPackReel3.FixZugara[1] == key))
                                        {
                                            // --- --- ---
                                            // key key key
                                            // --- --- ---
                                            srideIndex = FixSride_X_O_X(key, ZugaraStatusPackReel1);
                                            if (srideIndex == -1) srideIndex = 0;
                                        }
                                        else if ((ZugaraStatusPackReel2.FixZugara[1] == key) && (ZugaraStatusPackReel3.FixZugara[0] == key))
                                        {
                                            // --- --- key
                                            // --- key ---
                                            // key --- ---
                                            srideIndex = FixSride_X_X_O(key, ZugaraStatusPackReel1);
                                            if (srideIndex == -1) srideIndex = 0;
                                        }
                                        else if ((ZugaraStatusPackReel2.FixZugara[2] == key) && (ZugaraStatusPackReel3.FixZugara[2] == key))
                                        {
                                            // --- --- ---
                                            // --- --- ---
                                            // key key key
                                            srideIndex = FixSride_X_X_O(key, ZugaraStatusPackReel1);
                                            if (srideIndex == -1) srideIndex = 0;
                                        }

                                    }
                                    break;
                                case "suika":
                                    key = "suika";
                                    if (buttonCount == 0)
                                    {
                                        srideIndex = FixSride_O_O_O(key, ZugaraStatusPackReel1);
                                        if (srideIndex == -1) srideIndex = 0;
                                    }
                                    else if (buttonCount == 1)
                                    {
                                        if (ZugaraStatusPackReel2.IsFix)
                                        {
                                            if (ZugaraStatusPackReel2.FixZugara[0] == key)
                                            {
                                                // key key ???
                                                // --- --- ???
                                                // --- --- ???
                                                srideIndex = FixSride_O_X_X(key, ZugaraStatusPackReel1);
                                                if (srideIndex == -1) srideIndex = 0;
                                            }
                                            else if (ZugaraStatusPackReel2.FixZugara[1] == key)
                                            {
                                                // key --- ???
                                                // key key ???
                                                // key --- ???
                                                srideIndex = FixSride_O_O_O(key, ZugaraStatusPackReel1);
                                                if (srideIndex == -1) srideIndex = 0;
                                            }
                                            else if (ZugaraStatusPackReel2.FixZugara[2] == key)
                                            {
                                                // --- --- ???
                                                // --- --- ???
                                                // key key ???
                                                srideIndex = FixSride_X_X_O(key, ZugaraStatusPackReel1);
                                                if (srideIndex == -1) srideIndex = 0;
                                            }
                                        }
                                        else
                                        {
                                            if (ZugaraStatusPackReel3.FixZugara[0] == key)
                                            {
                                                // key ??? key
                                                // --- ??? ---
                                                // key ??? ---
                                                srideIndex = FixSride_O_X_O(key, ZugaraStatusPackReel1);
                                                if (srideIndex == -1) srideIndex = 0;
                                            }
                                            else if (ZugaraStatusPackReel3.FixZugara[1] == key)
                                            {
                                                // --- ??? ---
                                                // key ??? key
                                                // --- ??? ---
                                                srideIndex = FixSride_X_O_X(key, ZugaraStatusPackReel1);
                                                if (srideIndex == -1) srideIndex = 0;
                                            }
                                            else if (ZugaraStatusPackReel3.FixZugara[2] == key)
                                            {
                                                // key ??? ---
                                                // --- ??? ---
                                                // key ??? key
                                                srideIndex = FixSride_O_X_O(key, ZugaraStatusPackReel1);
                                                if (srideIndex == -1) srideIndex = 0;
                                            }

                                        }
                                    }
                                    else if (buttonCount == 2)
                                    {
                                        if ((ZugaraStatusPackReel2.FixZugara[0] == key) && (ZugaraStatusPackReel3.FixZugara[0] == key))
                                        {
                                            // key key key
                                            // --- --- ---
                                            // --- --- ---
                                            srideIndex = FixSride_O_X_X(key, ZugaraStatusPackReel1);
                                            if (srideIndex == -1) srideIndex = 0;
                                        }
                                        else if ((ZugaraStatusPackReel2.FixZugara[1] == key) && (ZugaraStatusPackReel3.FixZugara[2] == key))
                                        {
                                            // key --- ---
                                            // --- key ---
                                            // --- --- key
                                            srideIndex = FixSride_O_X_X(key, ZugaraStatusPackReel1);
                                            if (srideIndex == -1) srideIndex = 0;
                                        }
                                        else if ((ZugaraStatusPackReel2.FixZugara[1] == key) && (ZugaraStatusPackReel3.FixZugara[1] == key))
                                        {
                                            // --- --- ---
                                            // key key key
                                            // --- --- ---
                                            srideIndex = FixSride_X_O_X(key, ZugaraStatusPackReel1);
                                            if (srideIndex == -1) srideIndex = 0;
                                        }
                                        else if ((ZugaraStatusPackReel2.FixZugara[1] == key) && (ZugaraStatusPackReel3.FixZugara[0] == key))
                                        {
                                            // --- --- key
                                            // --- key ---
                                            // key --- ---
                                            srideIndex = FixSride_X_X_O(key, ZugaraStatusPackReel1);
                                            if (srideIndex == -1) srideIndex = 0;
                                        }
                                        else if ((ZugaraStatusPackReel2.FixZugara[2] == key) && (ZugaraStatusPackReel3.FixZugara[2] == key))
                                        {
                                            // --- --- ---
                                            // --- --- ---
                                            // key key key
                                            srideIndex = FixSride_X_X_O(key, ZugaraStatusPackReel1);
                                            if (srideIndex == -1) srideIndex = 0;
                                        }

                                    }
                                    break;
                            }

                            if (CheckBadRoleHit(0, srideIndex, ZugaraStatusPackReel1))
                            {
                                srideIndex = GetSrideBadRoleHit(0, ZugaraStatusPackReel1);
                                Debug.Log($"GetSrideBadRoleHit L1 {srideIndex} ");
                            }

                            buttonCount++;
                            ZugaraStatusPackReel1.Fix(srideIndex);
                            SlotCoreScript.OnButtonL1();
                        }
                    }
                    ZugaraStatusPackReel1.Update();

                    if (Input.GetKeyDown(KeyCode.DownArrow))
                    {
                        oneButtonWaitTimer = 0;
                        if (!ZugaraStatusPackReel2.IsFix)
                        {
                            var srideIndex = 0;
                            var key = "";
                            switch (role)
                            {
                                default:
                                    switch (bonus)
                                    {
                                        case "Big-r7":
                                            key = "r7";
                                            if (buttonCount == 0)
                                            {
                                                srideIndex = FixSride_O_O_O(key, ZugaraStatusPackReel2);
                                                if (srideIndex == -1) srideIndex = 0;
                                            }
                                            else if (buttonCount == 1)
                                            {
                                                if (ZugaraStatusPackReel1.IsFix)
                                                {
                                                    if (ZugaraStatusPackReel1.FixZugara[0] == key)
                                                    {
                                                        // key key ???
                                                        // --- key ???
                                                        // --- --- ???
                                                        srideIndex = FixSride_O_O_X(key, ZugaraStatusPackReel2);
                                                        if (srideIndex == -1) srideIndex = 0;

                                                    }
                                                    else if (ZugaraStatusPackReel1.FixZugara[1] == key)
                                                    {
                                                        // --- --- ???
                                                        // key key ???
                                                        // --- --- ???
                                                        srideIndex = FixSride_X_O_X(key, ZugaraStatusPackReel2);
                                                        if (srideIndex == -1) srideIndex = 0;
                                                    }
                                                    else if (ZugaraStatusPackReel1.FixZugara[2] == key)
                                                    {
                                                        // --- --- ???
                                                        // --- key ???
                                                        // key key ???
                                                        srideIndex = FixSride_X_O_O(key, ZugaraStatusPackReel2);
                                                        if (srideIndex == -1) srideIndex = 0;
                                                    }
                                                }
                                                else
                                                {
                                                    if (ZugaraStatusPackReel3.FixZugara[0] == key)
                                                    {
                                                        // ??? key key
                                                        // ??? key ---
                                                        // ??? --- ---
                                                        srideIndex = FixSride_O_O_X(key, ZugaraStatusPackReel2);
                                                        if (srideIndex == -1) srideIndex = 0;

                                                    }
                                                    else if (ZugaraStatusPackReel3.FixZugara[1] == key)
                                                    {
                                                        // ??? --- ---
                                                        // ??? key key
                                                        // ??? --- ---
                                                        srideIndex = FixSride_X_O_X(key, ZugaraStatusPackReel2);
                                                        if (srideIndex == -1) srideIndex = 0;
                                                    }
                                                    else if (ZugaraStatusPackReel3.FixZugara[2] == key)
                                                    {
                                                        // ??? --- ---
                                                        // ??? key ---
                                                        // ??? key key
                                                        srideIndex = FixSride_X_O_O(key, ZugaraStatusPackReel2);
                                                        if (srideIndex == -1) srideIndex = 0;
                                                    }
                                                }
                                            }
                                            else if (buttonCount == 2)
                                            {
                                                if ((ZugaraStatusPackReel1.FixZugara[0] == key) && (ZugaraStatusPackReel3.FixZugara[0] == key))
                                                {
                                                    // key key key
                                                    // --- --- ---
                                                    // --- --- ---
                                                    srideIndex = FixSride_O_X_X(key, ZugaraStatusPackReel2);
                                                    if (srideIndex == -1) srideIndex = 0;
                                                }
                                                else if ((ZugaraStatusPackReel1.FixZugara[0] == key) && (ZugaraStatusPackReel3.FixZugara[2] == key))
                                                {
                                                    // key --- ---
                                                    // --- key ---
                                                    // --- --- key
                                                    srideIndex = FixSride_X_O_X(key, ZugaraStatusPackReel2);
                                                    if (srideIndex == -1) srideIndex = 0;
                                                }
                                                else if ((ZugaraStatusPackReel1.FixZugara[1] == key) && (ZugaraStatusPackReel3.FixZugara[1] == key))
                                                {
                                                    // --- --- ---
                                                    // key key key
                                                    // --- --- ---
                                                    srideIndex = FixSride_X_O_X(key, ZugaraStatusPackReel2);
                                                    if (srideIndex == -1) srideIndex = 0;
                                                }
                                                else if ((ZugaraStatusPackReel1.FixZugara[2] == key) && (ZugaraStatusPackReel3.FixZugara[0] == key))
                                                {
                                                    // --- --- key
                                                    // --- key ---
                                                    // key --- ---
                                                    srideIndex = FixSride_X_O_X(key, ZugaraStatusPackReel2);
                                                    if (srideIndex == -1) srideIndex = 0;
                                                }
                                                else if ((ZugaraStatusPackReel1.FixZugara[2] == key) && (ZugaraStatusPackReel3.FixZugara[2] == key))
                                                {
                                                    // --- --- ---
                                                    // --- --- ---
                                                    // key key key
                                                    srideIndex = FixSride_X_X_O(key, ZugaraStatusPackReel2);
                                                    if (srideIndex == -1) srideIndex = 0;
                                                }

                                            }
                                            break;
                                        case "Big-b7":
                                            key = "b7";
                                            if (buttonCount == 0)
                                            {
                                                srideIndex = FixSride_O_O_O(key, ZugaraStatusPackReel2);
                                                if (srideIndex == -1) srideIndex = 0;
                                            }
                                            else if (buttonCount == 1)
                                            {
                                                if (ZugaraStatusPackReel1.IsFix)
                                                {
                                                    if (ZugaraStatusPackReel1.FixZugara[0] == key)
                                                    {
                                                        // key key ???
                                                        // --- key ???
                                                        // --- --- ???
                                                        srideIndex = FixSride_O_O_X(key, ZugaraStatusPackReel2);
                                                        if (srideIndex == -1) srideIndex = 0;

                                                    }
                                                    else if (ZugaraStatusPackReel1.FixZugara[1] == key)
                                                    {
                                                        // --- --- ???
                                                        // key key ???
                                                        // --- --- ???
                                                        srideIndex = FixSride_X_O_X(key, ZugaraStatusPackReel2);
                                                        if (srideIndex == -1) srideIndex = 0;
                                                    }
                                                    else if (ZugaraStatusPackReel1.FixZugara[2] == key)
                                                    {
                                                        // --- --- ???
                                                        // --- key ???
                                                        // key key ???
                                                        srideIndex = FixSride_X_O_O(key, ZugaraStatusPackReel2);
                                                        if (srideIndex == -1) srideIndex = 0;
                                                    }
                                                }
                                                else
                                                {
                                                    if (ZugaraStatusPackReel3.FixZugara[0] == key)
                                                    {
                                                        // ??? key key
                                                        // ??? key ---
                                                        // ??? --- ---
                                                        srideIndex = FixSride_O_O_X(key, ZugaraStatusPackReel2);
                                                        if (srideIndex == -1) srideIndex = 0;

                                                    }
                                                    else if (ZugaraStatusPackReel3.FixZugara[1] == key)
                                                    {
                                                        // ??? --- ---
                                                        // ??? key key
                                                        // ??? --- ---
                                                        srideIndex = FixSride_X_O_X(key, ZugaraStatusPackReel2);
                                                        if (srideIndex == -1) srideIndex = 0;
                                                    }
                                                    else if (ZugaraStatusPackReel3.FixZugara[2] == key)
                                                    {
                                                        // ??? --- ---
                                                        // ??? key ---
                                                        // ??? key key
                                                        srideIndex = FixSride_X_O_O(key, ZugaraStatusPackReel2);
                                                        if (srideIndex == -1) srideIndex = 0;
                                                    }
                                                }
                                            }
                                            else if (buttonCount == 2)
                                            {
                                                if ((ZugaraStatusPackReel1.FixZugara[0] == key) && (ZugaraStatusPackReel3.FixZugara[0] == key))
                                                {
                                                    // key key key
                                                    // --- --- ---
                                                    // --- --- ---
                                                    srideIndex = FixSride_O_X_X(key, ZugaraStatusPackReel2);
                                                    if (srideIndex == -1) srideIndex = 0;
                                                }
                                                else if ((ZugaraStatusPackReel1.FixZugara[0] == key) && (ZugaraStatusPackReel3.FixZugara[2] == key))
                                                {
                                                    // key --- ---
                                                    // --- key ---
                                                    // --- --- key
                                                    srideIndex = FixSride_X_O_X(key, ZugaraStatusPackReel2);
                                                    if (srideIndex == -1) srideIndex = 0;
                                                }
                                                else if ((ZugaraStatusPackReel1.FixZugara[1] == key) && (ZugaraStatusPackReel3.FixZugara[1] == key))
                                                {
                                                    // --- --- ---
                                                    // key key key
                                                    // --- --- ---
                                                    srideIndex = FixSride_X_O_X(key, ZugaraStatusPackReel2);
                                                    if (srideIndex == -1) srideIndex = 0;
                                                }
                                                else if ((ZugaraStatusPackReel1.FixZugara[2] == key) && (ZugaraStatusPackReel3.FixZugara[0] == key))
                                                {
                                                    // --- --- key
                                                    // --- key ---
                                                    // key --- ---
                                                    srideIndex = FixSride_X_O_X(key, ZugaraStatusPackReel2);
                                                    if (srideIndex == -1) srideIndex = 0;
                                                }
                                                else if ((ZugaraStatusPackReel1.FixZugara[2] == key) && (ZugaraStatusPackReel3.FixZugara[2] == key))
                                                {
                                                    // --- --- ---
                                                    // --- --- ---
                                                    // key key key
                                                    srideIndex = FixSride_X_X_O(key, ZugaraStatusPackReel2);
                                                    if (srideIndex == -1) srideIndex = 0;
                                                }

                                            }
                                            break;
                                        case "Reg":
                                            key = "r7";
                                            if (buttonCount == 0)
                                            {
                                                srideIndex = FixSride_O_O_O(key, ZugaraStatusPackReel2);
                                                if (srideIndex == -1) srideIndex = 0;
                                            }
                                            else if (buttonCount == 1)
                                            {
                                                if (ZugaraStatusPackReel1.IsFix)
                                                {
                                                    if (ZugaraStatusPackReel1.FixZugara[0] == key)
                                                    {
                                                        // key key ???
                                                        // --- key ???
                                                        // --- --- ???
                                                        srideIndex = FixSride_O_O_X(key, ZugaraStatusPackReel2);
                                                        if (srideIndex == -1) srideIndex = 0;

                                                    }
                                                    else if (ZugaraStatusPackReel1.FixZugara[1] == key)
                                                    {
                                                        // --- --- ???
                                                        // key key ???
                                                        // --- --- ???
                                                        srideIndex = FixSride_X_O_X(key, ZugaraStatusPackReel2);
                                                        if (srideIndex == -1) srideIndex = 0;
                                                    }
                                                    else if (ZugaraStatusPackReel1.FixZugara[2] == key)
                                                    {
                                                        // --- --- ???
                                                        // --- key ???
                                                        // key key ???
                                                        srideIndex = FixSride_X_O_O(key, ZugaraStatusPackReel2);
                                                        if (srideIndex == -1) srideIndex = 0;
                                                    }
                                                }
                                                else
                                                {
                                                    if (ZugaraStatusPackReel3.FixZugara[0] == "bar")
                                                    {
                                                        // ??? key key
                                                        // ??? key ---
                                                        // ??? --- ---
                                                        srideIndex = FixSride_O_O_X(key, ZugaraStatusPackReel2);
                                                        if (srideIndex == -1) srideIndex = 0;

                                                    }
                                                    else if (ZugaraStatusPackReel3.FixZugara[1] == "bar")
                                                    {
                                                        // ??? --- ---
                                                        // ??? key key
                                                        // ??? --- ---
                                                        srideIndex = FixSride_X_O_X(key, ZugaraStatusPackReel2);
                                                        if (srideIndex == -1) srideIndex = 0;
                                                    }
                                                    else if (ZugaraStatusPackReel3.FixZugara[2] == "bar")
                                                    {
                                                        // ??? --- ---
                                                        // ??? key ---
                                                        // ??? key key
                                                        srideIndex = FixSride_X_O_O(key, ZugaraStatusPackReel2);
                                                        if (srideIndex == -1) srideIndex = 0;
                                                    }
                                                }
                                            }
                                            else if (buttonCount == 2)
                                            {
                                                if ((ZugaraStatusPackReel1.FixZugara[0] == key) && (ZugaraStatusPackReel3.FixZugara[0] == "bar"))
                                                {
                                                    // key key key
                                                    // --- --- ---
                                                    // --- --- ---
                                                    srideIndex = FixSride_O_X_X(key, ZugaraStatusPackReel2);
                                                    if (srideIndex == -1) srideIndex = 0;
                                                }
                                                else if ((ZugaraStatusPackReel1.FixZugara[0] == key) && (ZugaraStatusPackReel3.FixZugara[2] == "bar"))
                                                {
                                                    // key --- ---
                                                    // --- key ---
                                                    // --- --- key
                                                    srideIndex = FixSride_X_O_X(key, ZugaraStatusPackReel2);
                                                    if (srideIndex == -1) srideIndex = 0;
                                                }
                                                else if ((ZugaraStatusPackReel1.FixZugara[1] == key) && (ZugaraStatusPackReel3.FixZugara[1] == "bar"))
                                                {
                                                    // --- --- ---
                                                    // key key key
                                                    // --- --- ---
                                                    srideIndex = FixSride_X_O_X(key, ZugaraStatusPackReel2);
                                                    if (srideIndex == -1) srideIndex = 0;
                                                }
                                                else if ((ZugaraStatusPackReel1.FixZugara[2] == key) && (ZugaraStatusPackReel3.FixZugara[0] == "bar"))
                                                {
                                                    // --- --- key
                                                    // --- key ---
                                                    // key --- ---
                                                    srideIndex = FixSride_X_O_X(key, ZugaraStatusPackReel2);
                                                    if (srideIndex == -1) srideIndex = 0;
                                                }
                                                else if ((ZugaraStatusPackReel1.FixZugara[2] == key) && (ZugaraStatusPackReel3.FixZugara[2] == "bar"))
                                                {
                                                    // --- --- ---
                                                    // --- --- ---
                                                    // key key key
                                                    srideIndex = FixSride_X_X_O(key, ZugaraStatusPackReel2);
                                                    if (srideIndex == -1) srideIndex = 0;
                                                }

                                            }
                                            break;
                                    }
                                    break;
                                case "rep":
                                    key = "rep";
                                    if (buttonCount == 0)
                                    {
                                        srideIndex = FixSride_O_O_O(key, ZugaraStatusPackReel2);
                                        if (srideIndex == -1) srideIndex = 0;
                                    }
                                    else if (buttonCount == 1)
                                    {
                                        if (ZugaraStatusPackReel1.IsFix)
                                        {
                                            if (ZugaraStatusPackReel1.FixZugara[0] == key)
                                            {
                                                // key key ???
                                                // --- key ???
                                                // --- --- ???
                                                srideIndex = FixSride_O_O_X(key, ZugaraStatusPackReel2);
                                                if (srideIndex == -1) srideIndex = 0;

                                            }
                                            else if (ZugaraStatusPackReel1.FixZugara[1] == key)
                                            {
                                                // --- --- ???
                                                // key key ???
                                                // --- --- ???
                                                srideIndex = FixSride_X_O_X(key, ZugaraStatusPackReel2);
                                                if (srideIndex == -1) srideIndex = 0;
                                            }
                                            else if (ZugaraStatusPackReel1.FixZugara[2] == key)
                                            {
                                                // --- --- ???
                                                // --- key ???
                                                // key key ???
                                                srideIndex = FixSride_X_O_O(key, ZugaraStatusPackReel2);
                                                if (srideIndex == -1) srideIndex = 0;
                                            }
                                        }
                                        else
                                        {
                                            if (ZugaraStatusPackReel3.FixZugara[0] == key)
                                            {
                                                // ??? key key
                                                // ??? key ---
                                                // ??? --- ---
                                                srideIndex = FixSride_O_O_X(key, ZugaraStatusPackReel2);
                                                if (srideIndex == -1) srideIndex = 0;

                                            }
                                            else if (ZugaraStatusPackReel3.FixZugara[1] == key)
                                            {
                                                // ??? --- ---
                                                // ??? key key
                                                // ??? --- ---
                                                srideIndex = FixSride_X_O_X(key, ZugaraStatusPackReel2);
                                                if (srideIndex == -1) srideIndex = 0;
                                            }
                                            else if (ZugaraStatusPackReel3.FixZugara[2] == key)
                                            {
                                                // ??? --- ---
                                                // ??? key ---
                                                // ??? key key
                                                srideIndex = FixSride_X_O_O(key, ZugaraStatusPackReel2);
                                                if (srideIndex == -1) srideIndex = 0;
                                            }
                                        }
                                    }
                                    else if (buttonCount == 2)
                                    {
                                        if ((ZugaraStatusPackReel1.FixZugara[0] == key) && (ZugaraStatusPackReel3.FixZugara[0] == key))
                                        {
                                            // key key key
                                            // --- --- ---
                                            // --- --- ---
                                            srideIndex = FixSride_O_X_X(key, ZugaraStatusPackReel2);
                                            if (srideIndex == -1) srideIndex = 0;
                                        }
                                        else if ((ZugaraStatusPackReel1.FixZugara[0] == key) && (ZugaraStatusPackReel3.FixZugara[2] == key))
                                        {
                                            // key --- ---
                                            // --- key ---
                                            // --- --- key
                                            srideIndex = FixSride_X_O_X(key, ZugaraStatusPackReel2);
                                            if (srideIndex == -1) srideIndex = 0;
                                        }
                                        else if ((ZugaraStatusPackReel1.FixZugara[1] == key) && (ZugaraStatusPackReel3.FixZugara[1] == key))
                                        {
                                            // --- --- ---
                                            // key key key
                                            // --- --- ---
                                            srideIndex = FixSride_X_O_X(key, ZugaraStatusPackReel2);
                                            if (srideIndex == -1) srideIndex = 0;
                                        }
                                        else if ((ZugaraStatusPackReel1.FixZugara[2] == key) && (ZugaraStatusPackReel3.FixZugara[0] == key))
                                        {
                                            // --- --- key
                                            // --- key ---
                                            // key --- ---
                                            srideIndex = FixSride_X_O_X(key, ZugaraStatusPackReel2);
                                            if (srideIndex == -1) srideIndex = 0;
                                        }
                                        else if ((ZugaraStatusPackReel1.FixZugara[2] == key) && (ZugaraStatusPackReel3.FixZugara[2] == key))
                                        {
                                            // --- --- ---
                                            // --- --- ---
                                            // key key key
                                            srideIndex = FixSride_X_X_O(key, ZugaraStatusPackReel2);
                                            if (srideIndex == -1) srideIndex = 0;
                                        }

                                    }
                                    break;
                                case "bell":
                                    key = "bell";
                                    if (buttonCount == 0)
                                    {
                                        srideIndex = FixSride_O_O_O(key, ZugaraStatusPackReel2);
                                        if (srideIndex == -1) srideIndex = 0;
                                    }
                                    else if (buttonCount == 1)
                                    {
                                        if (ZugaraStatusPackReel1.IsFix)
                                        {
                                            if (ZugaraStatusPackReel1.FixZugara[0] == key)
                                            {
                                                // key key ???
                                                // --- key ???
                                                // --- --- ???
                                                srideIndex = FixSride_O_O_X(key, ZugaraStatusPackReel2);
                                                if (srideIndex == -1) srideIndex = 0;

                                            }
                                            else if (ZugaraStatusPackReel1.FixZugara[1] == key)
                                            {
                                                // --- --- ???
                                                // key key ???
                                                // --- --- ???
                                                srideIndex = FixSride_X_O_X(key, ZugaraStatusPackReel2);
                                                if (srideIndex == -1) srideIndex = 0;
                                            }
                                            else if (ZugaraStatusPackReel1.FixZugara[2] == key)
                                            {
                                                // --- --- ???
                                                // --- key ???
                                                // key key ???
                                                srideIndex = FixSride_X_O_O(key, ZugaraStatusPackReel2);
                                                if (srideIndex == -1) srideIndex = 0;
                                            }
                                        }
                                        else
                                        {
                                            if (ZugaraStatusPackReel3.FixZugara[0] == key)
                                            {
                                                // ??? key key
                                                // ??? key ---
                                                // ??? --- ---
                                                srideIndex = FixSride_O_O_X(key, ZugaraStatusPackReel2);
                                                if (srideIndex == -1) srideIndex = 0;

                                            }
                                            else if (ZugaraStatusPackReel3.FixZugara[1] == key)
                                            {
                                                // ??? --- ---
                                                // ??? key key
                                                // ??? --- ---
                                                srideIndex = FixSride_X_O_X(key, ZugaraStatusPackReel2);
                                                if (srideIndex == -1) srideIndex = 0;
                                            }
                                            else if (ZugaraStatusPackReel3.FixZugara[2] == key)
                                            {
                                                // ??? --- ---
                                                // ??? key ---
                                                // ??? key key
                                                srideIndex = FixSride_X_O_O(key, ZugaraStatusPackReel2);
                                                if (srideIndex == -1) srideIndex = 0;
                                            }
                                        }
                                    }
                                    else if (buttonCount == 2)
                                    {
                                        if ((ZugaraStatusPackReel1.FixZugara[0] == key) && (ZugaraStatusPackReel3.FixZugara[0] == key))
                                        {
                                            // key key key
                                            // --- --- ---
                                            // --- --- ---
                                            srideIndex = FixSride_O_X_X(key, ZugaraStatusPackReel2);
                                            if (srideIndex == -1) srideIndex = 0;
                                        }
                                        else if ((ZugaraStatusPackReel1.FixZugara[0] == key) && (ZugaraStatusPackReel3.FixZugara[2] == key))
                                        {
                                            // key --- ---
                                            // --- key ---
                                            // --- --- key
                                            srideIndex = FixSride_X_O_X(key, ZugaraStatusPackReel2);
                                            if (srideIndex == -1) srideIndex = 0;
                                        }
                                        else if ((ZugaraStatusPackReel1.FixZugara[1] == key) && (ZugaraStatusPackReel3.FixZugara[1] == key))
                                        {
                                            // --- --- ---
                                            // key key key
                                            // --- --- ---
                                            srideIndex = FixSride_X_O_X(key, ZugaraStatusPackReel2);
                                            if (srideIndex == -1) srideIndex = 0;
                                        }
                                        else if ((ZugaraStatusPackReel1.FixZugara[2] == key) && (ZugaraStatusPackReel3.FixZugara[0] == key))
                                        {
                                            // --- --- key
                                            // --- key ---
                                            // key --- ---
                                            srideIndex = FixSride_X_O_X(key, ZugaraStatusPackReel2);
                                            if (srideIndex == -1) srideIndex = 0;
                                        }
                                        else if ((ZugaraStatusPackReel1.FixZugara[2] == key) && (ZugaraStatusPackReel3.FixZugara[2] == key))
                                        {
                                            // --- --- ---
                                            // --- --- ---
                                            // key key key
                                            srideIndex = FixSride_X_X_O(key, ZugaraStatusPackReel2);
                                            if (srideIndex == -1) srideIndex = 0;
                                        }

                                    }
                                    break;
                                case "suika":
                                    key = "suika";
                                    if (buttonCount == 0)
                                    {
                                        srideIndex = FixSride_O_O_O(key, ZugaraStatusPackReel2);
                                        if (srideIndex == -1) srideIndex = 0;
                                    }
                                    else if (buttonCount == 1)
                                    {
                                        if (ZugaraStatusPackReel1.IsFix)
                                        {
                                            if (ZugaraStatusPackReel1.FixZugara[0] == key)
                                            {
                                                // key key ???
                                                // --- key ???
                                                // --- --- ???
                                                srideIndex = FixSride_O_O_X(key, ZugaraStatusPackReel2);
                                                if (srideIndex == -1) srideIndex = 0;

                                            }
                                            else if (ZugaraStatusPackReel1.FixZugara[1] == key)
                                            {
                                                // --- --- ???
                                                // key key ???
                                                // --- --- ???
                                                srideIndex = FixSride_X_O_X(key, ZugaraStatusPackReel2);
                                                if (srideIndex == -1) srideIndex = 0;
                                            }
                                            else if (ZugaraStatusPackReel1.FixZugara[2] == key)
                                            {
                                                // --- --- ???
                                                // --- key ???
                                                // key key ???
                                                srideIndex = FixSride_X_O_O(key, ZugaraStatusPackReel2);
                                                if (srideIndex == -1) srideIndex = 0;
                                            }
                                        }
                                        else
                                        {
                                            if (ZugaraStatusPackReel3.FixZugara[0] == key)
                                            {
                                                // ??? key key
                                                // ??? key ---
                                                // ??? --- ---
                                                srideIndex = FixSride_O_O_X(key, ZugaraStatusPackReel2);
                                                if (srideIndex == -1) srideIndex = 0;

                                            }
                                            else if (ZugaraStatusPackReel3.FixZugara[1] == key)
                                            {
                                                // ??? --- ---
                                                // ??? key key
                                                // ??? --- ---
                                                srideIndex = FixSride_X_O_X(key, ZugaraStatusPackReel2);
                                                if (srideIndex == -1) srideIndex = 0;
                                            }
                                            else if (ZugaraStatusPackReel3.FixZugara[2] == key)
                                            {
                                                // ??? --- ---
                                                // ??? key ---
                                                // ??? key key
                                                srideIndex = FixSride_X_O_O(key, ZugaraStatusPackReel2);
                                                if (srideIndex == -1) srideIndex = 0;
                                            }
                                        }
                                    }
                                    else if (buttonCount == 2)
                                    {
                                        if ((ZugaraStatusPackReel1.FixZugara[0] == key) && (ZugaraStatusPackReel3.FixZugara[0] == key))
                                        {
                                            // key key key
                                            // --- --- ---
                                            // --- --- ---
                                            srideIndex = FixSride_O_X_X(key, ZugaraStatusPackReel2);
                                            if (srideIndex == -1) srideIndex = 0;
                                        }
                                        else if ((ZugaraStatusPackReel1.FixZugara[0] == key) && (ZugaraStatusPackReel3.FixZugara[2] == key))
                                        {
                                            // key --- ---
                                            // --- key ---
                                            // --- --- key
                                            srideIndex = FixSride_X_O_X(key, ZugaraStatusPackReel2);
                                            if (srideIndex == -1) srideIndex = 0;
                                        }
                                        else if ((ZugaraStatusPackReel1.FixZugara[1] == key) && (ZugaraStatusPackReel3.FixZugara[1] == key))
                                        {
                                            // --- --- ---
                                            // key key key
                                            // --- --- ---
                                            srideIndex = FixSride_X_O_X(key, ZugaraStatusPackReel2);
                                            if (srideIndex == -1) srideIndex = 0;
                                        }
                                        else if ((ZugaraStatusPackReel1.FixZugara[2] == key) && (ZugaraStatusPackReel3.FixZugara[0] == key))
                                        {
                                            // --- --- key
                                            // --- key ---
                                            // key --- ---
                                            srideIndex = FixSride_X_O_X(key, ZugaraStatusPackReel2);
                                            if (srideIndex == -1) srideIndex = 0;
                                        }
                                        else if ((ZugaraStatusPackReel1.FixZugara[2] == key) && (ZugaraStatusPackReel3.FixZugara[2] == key))
                                        {
                                            // --- --- ---
                                            // --- --- ---
                                            // key key key
                                            srideIndex = FixSride_X_X_O(key, ZugaraStatusPackReel2);
                                            if (srideIndex == -1) srideIndex = 0;
                                        }

                                    }
                                    break;
                            }

                            if (CheckBadRoleHit(1, srideIndex, ZugaraStatusPackReel2))
                            {
                                srideIndex = GetSrideBadRoleHit(1, ZugaraStatusPackReel2);
                                Debug.Log($"GetSrideBadRoleHit L2 {srideIndex} ");
                            }

                            buttonCount++;
                            ZugaraStatusPackReel2.Fix(srideIndex);
                            SlotCoreScript.OnButtonL2();
                        }
                    }
                    ZugaraStatusPackReel2.Update();

                    if (Input.GetKeyDown(KeyCode.RightArrow))
                    {
                        oneButtonWaitTimer = 0;
                        if (!ZugaraStatusPackReel3.IsFix)
                        {
                            var srideIndex = 0;
                            var key = "";
                            switch (role)
                            {
                                default:
                                    switch (bonus)
                                    {
                                        case "Big-r7":
                                            key = "r7";
                                            if (buttonCount == 0)
                                            {
                                                srideIndex = FixSride_O_O_O(key, ZugaraStatusPackReel3);
                                                if (srideIndex == -1) srideIndex = 0;
                                            }
                                            else if (buttonCount == 1)
                                            {
                                                if (ZugaraStatusPackReel2.IsFix)
                                                {
                                                    if (ZugaraStatusPackReel2.FixZugara[0] == key)
                                                    {
                                                        // ??? key key
                                                        // ??? --- ---
                                                        // ??? --- ---
                                                        srideIndex = FixSride_O_X_X(key, ZugaraStatusPackReel3);
                                                        if (srideIndex == -1) srideIndex = 0;
                                                    }
                                                    else if (ZugaraStatusPackReel2.FixZugara[1] == key)
                                                    {
                                                        // ??? --- key
                                                        // ??? key key
                                                        // ??? --- key
                                                        srideIndex = FixSride_O_O_O(key, ZugaraStatusPackReel3);
                                                        if (srideIndex == -1) srideIndex = 0;
                                                    }
                                                    else if (ZugaraStatusPackReel2.FixZugara[2] == key)
                                                    {
                                                        // ??? --- ---
                                                        // ??? --- ---
                                                        // ??? key key
                                                        srideIndex = FixSride_X_X_O(key, ZugaraStatusPackReel3);
                                                        if (srideIndex == -1) srideIndex = 0;
                                                    }
                                                }
                                                else
                                                {
                                                    if (ZugaraStatusPackReel1.FixZugara[0] == key)
                                                    {
                                                        // key ??? key
                                                        // --- ??? ---
                                                        // --- ??? key
                                                        srideIndex = FixSride_O_X_O(key, ZugaraStatusPackReel3);
                                                        if (srideIndex == -1) srideIndex = 0;
                                                    }
                                                    else if (ZugaraStatusPackReel1.FixZugara[1] == key)
                                                    {
                                                        // --- ??? ---
                                                        // key ??? key
                                                        // --- ??? ---
                                                        srideIndex = FixSride_X_O_X(key, ZugaraStatusPackReel3);
                                                        if (srideIndex == -1) srideIndex = 0;
                                                    }
                                                    else if (ZugaraStatusPackReel2.FixZugara[2] == key)
                                                    {
                                                        // --- ??? key
                                                        // --- ??? ---
                                                        // key ??? key
                                                        srideIndex = FixSride_O_X_O(key, ZugaraStatusPackReel3);
                                                        if (srideIndex == -1) srideIndex = 0;
                                                    }

                                                }
                                            }
                                            else if (buttonCount == 2)
                                            {
                                                if ((ZugaraStatusPackReel1.FixZugara[0] == key) && (ZugaraStatusPackReel2.FixZugara[0] == key))
                                                {
                                                    // key key key
                                                    // --- --- ---
                                                    // --- --- ---
                                                    srideIndex = FixSride_O_X_X(key, ZugaraStatusPackReel3);
                                                    if (srideIndex == -1) srideIndex = 0;
                                                }
                                                else if ((ZugaraStatusPackReel1.FixZugara[0] == key) && (ZugaraStatusPackReel2.FixZugara[1] == key))
                                                {
                                                    // key --- ---
                                                    // --- key ---
                                                    // --- --- key
                                                    srideIndex = FixSride_X_X_O(key, ZugaraStatusPackReel3); // todo : 不具合ありそう、赤7がそろわないことがあった、はじいてるかも
                                                    if (srideIndex == -1) srideIndex = 0;
                                                }
                                                else if ((ZugaraStatusPackReel1.FixZugara[1] == key) && (ZugaraStatusPackReel2.FixZugara[1] == key))
                                                {
                                                    // --- --- ---
                                                    // key key key
                                                    // --- --- ---
                                                    srideIndex = FixSride_X_O_X(key, ZugaraStatusPackReel3);
                                                    if (srideIndex == -1) srideIndex = 0;
                                                }
                                                else if ((ZugaraStatusPackReel1.FixZugara[2] == key) && (ZugaraStatusPackReel2.FixZugara[1] == key))
                                                {
                                                    // --- --- key
                                                    // --- key ---
                                                    // key --- ---
                                                    srideIndex = FixSride_O_X_X(key, ZugaraStatusPackReel3);
                                                    if (srideIndex == -1) srideIndex = 0;
                                                }
                                                else if ((ZugaraStatusPackReel1.FixZugara[2] == key) && (ZugaraStatusPackReel2.FixZugara[2] == key))
                                                {
                                                    // --- --- ---
                                                    // --- --- ---
                                                    // key key key
                                                    srideIndex = FixSride_X_X_O(key, ZugaraStatusPackReel3);
                                                    if (srideIndex == -1) srideIndex = 0;
                                                }
                                            }
                                            break;
                                        case "Big-b7":
                                            key = "b7";
                                            if (buttonCount == 0)
                                            {
                                                srideIndex = FixSride_O_O_O(key, ZugaraStatusPackReel3);
                                                if (srideIndex == -1) srideIndex = 0;
                                            }
                                            else if (buttonCount == 1)
                                            {
                                                if (ZugaraStatusPackReel2.IsFix)
                                                {
                                                    if (ZugaraStatusPackReel2.FixZugara[0] == key)
                                                    {
                                                        // ??? key key
                                                        // ??? --- ---
                                                        // ??? --- ---
                                                        srideIndex = FixSride_O_X_X(key, ZugaraStatusPackReel3);
                                                        if (srideIndex == -1) srideIndex = 0;
                                                    }
                                                    else if (ZugaraStatusPackReel2.FixZugara[1] == key)
                                                    {
                                                        // ??? --- key
                                                        // ??? key key
                                                        // ??? --- key
                                                        srideIndex = FixSride_O_O_O(key, ZugaraStatusPackReel3);
                                                        if (srideIndex == -1) srideIndex = 0;
                                                    }
                                                    else if (ZugaraStatusPackReel2.FixZugara[2] == key)
                                                    {
                                                        // ??? --- ---
                                                        // ??? --- ---
                                                        // ??? key key
                                                        srideIndex = FixSride_X_X_O(key, ZugaraStatusPackReel3);
                                                        if (srideIndex == -1) srideIndex = 0;
                                                    }
                                                }
                                                else
                                                {
                                                    if (ZugaraStatusPackReel1.FixZugara[0] == key)
                                                    {
                                                        // key ??? key
                                                        // --- ??? ---
                                                        // --- ??? key
                                                        srideIndex = FixSride_O_X_O(key, ZugaraStatusPackReel3);
                                                        if (srideIndex == -1) srideIndex = 0;
                                                    }
                                                    else if (ZugaraStatusPackReel1.FixZugara[1] == key)
                                                    {
                                                        // --- ??? ---
                                                        // key ??? key
                                                        // --- ??? ---
                                                        srideIndex = FixSride_X_O_X(key, ZugaraStatusPackReel3);
                                                        if (srideIndex == -1) srideIndex = 0;
                                                    }
                                                    else if (ZugaraStatusPackReel2.FixZugara[2] == key)
                                                    {
                                                        // --- ??? key
                                                        // --- ??? ---
                                                        // key ??? key
                                                        srideIndex = FixSride_O_X_O(key, ZugaraStatusPackReel3);
                                                        if (srideIndex == -1) srideIndex = 0;
                                                    }

                                                }
                                            }
                                            else if (buttonCount == 2)
                                            {
                                                if ((ZugaraStatusPackReel1.FixZugara[0] == key) && (ZugaraStatusPackReel2.FixZugara[0] == key))
                                                {
                                                    // key key key
                                                    // --- --- ---
                                                    // --- --- ---
                                                    srideIndex = FixSride_O_X_X(key, ZugaraStatusPackReel3);
                                                    if (srideIndex == -1) srideIndex = 0;
                                                }
                                                else if ((ZugaraStatusPackReel1.FixZugara[0] == key) && (ZugaraStatusPackReel2.FixZugara[1] == key))
                                                {
                                                    // key --- ---
                                                    // --- key ---
                                                    // --- --- key
                                                    srideIndex = FixSride_X_X_O(key, ZugaraStatusPackReel3);
                                                    if (srideIndex == -1) srideIndex = 0;
                                                }
                                                else if ((ZugaraStatusPackReel1.FixZugara[1] == key) && (ZugaraStatusPackReel2.FixZugara[1] == key))
                                                {
                                                    // --- --- ---
                                                    // key key key
                                                    // --- --- ---
                                                    srideIndex = FixSride_X_O_X(key, ZugaraStatusPackReel3);
                                                    if (srideIndex == -1) srideIndex = 0;
                                                }
                                                else if ((ZugaraStatusPackReel1.FixZugara[2] == key) && (ZugaraStatusPackReel2.FixZugara[1] == key))
                                                {
                                                    // --- --- key
                                                    // --- key ---
                                                    // key --- ---
                                                    srideIndex = FixSride_O_X_X(key, ZugaraStatusPackReel3);
                                                    if (srideIndex == -1) srideIndex = 0;
                                                }
                                                else if ((ZugaraStatusPackReel1.FixZugara[2] == key) && (ZugaraStatusPackReel2.FixZugara[2] == key))
                                                {
                                                    // --- --- ---
                                                    // --- --- ---
                                                    // key key key
                                                    srideIndex = FixSride_X_X_O(key, ZugaraStatusPackReel3);
                                                    if (srideIndex == -1) srideIndex = 0;
                                                }
                                            }
                                            break;
                                        case "Reg":
                                            key = "bar";
                                            if (buttonCount == 0)
                                            {
                                                srideIndex = FixSride_O_O_O(key, ZugaraStatusPackReel3);
                                                if (srideIndex == -1) srideIndex = 0;
                                            }
                                            else if (buttonCount == 1)
                                            {
                                                if (ZugaraStatusPackReel2.IsFix)
                                                {
                                                    if (ZugaraStatusPackReel2.FixZugara[0] == "r7")
                                                    {
                                                        // ??? key key
                                                        // ??? --- ---
                                                        // ??? --- ---
                                                        srideIndex = FixSride_O_X_X(key, ZugaraStatusPackReel3);
                                                        if (srideIndex == -1) srideIndex = 0;
                                                    }
                                                    else if (ZugaraStatusPackReel2.FixZugara[1] == "r7")
                                                    {
                                                        // ??? --- key
                                                        // ??? key key
                                                        // ??? --- key
                                                        srideIndex = FixSride_O_O_O(key, ZugaraStatusPackReel3);
                                                        if (srideIndex == -1) srideIndex = 0;
                                                    }
                                                    else if (ZugaraStatusPackReel2.FixZugara[2] == "r7")
                                                    {
                                                        // ??? --- ---
                                                        // ??? --- ---
                                                        // ??? key key
                                                        srideIndex = FixSride_X_X_O(key, ZugaraStatusPackReel3);
                                                        if (srideIndex == -1) srideIndex = 0;
                                                    }
                                                }
                                                else
                                                {
                                                    if (ZugaraStatusPackReel1.FixZugara[0] == "r7")
                                                    {
                                                        // key ??? key
                                                        // --- ??? ---
                                                        // --- ??? key
                                                        srideIndex = FixSride_O_X_O(key, ZugaraStatusPackReel3);
                                                        if (srideIndex == -1) srideIndex = 0;
                                                    }
                                                    else if (ZugaraStatusPackReel1.FixZugara[1] == "r7")
                                                    {
                                                        // --- ??? ---
                                                        // key ??? key
                                                        // --- ??? ---
                                                        srideIndex = FixSride_X_O_X(key, ZugaraStatusPackReel3);
                                                        if (srideIndex == -1) srideIndex = 0;
                                                    }
                                                    else if (ZugaraStatusPackReel2.FixZugara[2] == "r7")
                                                    {
                                                        // --- ??? key
                                                        // --- ??? ---
                                                        // key ??? key
                                                        srideIndex = FixSride_O_X_O(key, ZugaraStatusPackReel3);
                                                        if (srideIndex == -1) srideIndex = 0;
                                                    }

                                                }
                                            }
                                            else if (buttonCount == 2)
                                            {
                                                if ((ZugaraStatusPackReel1.FixZugara[0] == "r7") && (ZugaraStatusPackReel2.FixZugara[0] == "r7"))
                                                {
                                                    // key key key
                                                    // --- --- ---
                                                    // --- --- ---
                                                    srideIndex = FixSride_O_X_X(key, ZugaraStatusPackReel3);
                                                    if (srideIndex == -1) srideIndex = 0;
                                                }
                                                else if ((ZugaraStatusPackReel1.FixZugara[0] == "r7") && (ZugaraStatusPackReel2.FixZugara[1] == "r7"))
                                                {
                                                    // key --- ---
                                                    // --- key ---
                                                    // --- --- key
                                                    srideIndex = FixSride_X_X_O(key, ZugaraStatusPackReel3);
                                                    if (srideIndex == -1) srideIndex = 0;
                                                }
                                                else if ((ZugaraStatusPackReel1.FixZugara[1] == "r7") && (ZugaraStatusPackReel2.FixZugara[1] == "r7"))
                                                {
                                                    // --- --- ---
                                                    // key key key
                                                    // --- --- ---
                                                    srideIndex = FixSride_X_O_X(key, ZugaraStatusPackReel3);
                                                    if (srideIndex == -1) srideIndex = 0;
                                                }
                                                else if ((ZugaraStatusPackReel1.FixZugara[2] == "r7") && (ZugaraStatusPackReel2.FixZugara[1] == "r7"))
                                                {
                                                    // --- --- key
                                                    // --- key ---
                                                    // key --- ---
                                                    srideIndex = FixSride_O_X_X(key, ZugaraStatusPackReel3);
                                                    if (srideIndex == -1) srideIndex = 0;
                                                }
                                                else if ((ZugaraStatusPackReel1.FixZugara[2] == "r7") && (ZugaraStatusPackReel2.FixZugara[2] == "r7"))
                                                {
                                                    // --- --- ---
                                                    // --- --- ---
                                                    // key key key
                                                    srideIndex = FixSride_X_X_O(key, ZugaraStatusPackReel3);
                                                    if (srideIndex == -1) srideIndex = 0;
                                                }
                                            }
                                            break;
                                    }
                                    break;
                                case "rep":
                                    key = "rep";
                                    if (buttonCount == 0)
                                    {
                                        srideIndex = FixSride_O_O_O(key, ZugaraStatusPackReel3);
                                        if (srideIndex == -1) srideIndex = 0;
                                    }
                                    else if (buttonCount == 1)
                                    {
                                        if (ZugaraStatusPackReel2.IsFix)
                                        {
                                            if (ZugaraStatusPackReel2.FixZugara[0] == key)
                                            {
                                                // ??? key key
                                                // ??? --- ---
                                                // ??? --- ---
                                                srideIndex = FixSride_O_X_X(key, ZugaraStatusPackReel3);
                                                if (srideIndex == -1) srideIndex = 0;
                                            }
                                            else if (ZugaraStatusPackReel2.FixZugara[1] == key)
                                            {
                                                // ??? --- key
                                                // ??? key key
                                                // ??? --- key
                                                srideIndex = FixSride_O_O_O(key, ZugaraStatusPackReel3);
                                                if (srideIndex == -1) srideIndex = 0;
                                            }
                                            else if (ZugaraStatusPackReel2.FixZugara[2] == key)
                                            {
                                                // ??? --- ---
                                                // ??? --- ---
                                                // ??? key key
                                                srideIndex = FixSride_X_X_O(key, ZugaraStatusPackReel3);
                                                if (srideIndex == -1) srideIndex = 0;
                                            }
                                        }
                                        else
                                        {
                                            if (ZugaraStatusPackReel1.FixZugara[0] == key)
                                            {
                                                // key ??? key
                                                // --- ??? ---
                                                // --- ??? key
                                                srideIndex = FixSride_O_X_O(key, ZugaraStatusPackReel3);
                                                if (srideIndex == -1) srideIndex = 0;
                                            }
                                            else if (ZugaraStatusPackReel1.FixZugara[1] == key)
                                            {
                                                // --- ??? ---
                                                // key ??? key
                                                // --- ??? ---
                                                srideIndex = FixSride_X_O_X(key, ZugaraStatusPackReel3);
                                                if (srideIndex == -1) srideIndex = 0;
                                            }
                                            else if (ZugaraStatusPackReel2.FixZugara[2] == key)
                                            {
                                                // --- ??? key
                                                // --- ??? ---
                                                // key ??? key
                                                srideIndex = FixSride_O_X_O(key, ZugaraStatusPackReel3);
                                                if (srideIndex == -1) srideIndex = 0;
                                            }

                                        }
                                    }
                                    else if (buttonCount == 2)
                                    {
                                        if ((ZugaraStatusPackReel1.FixZugara[0] == key) && (ZugaraStatusPackReel2.FixZugara[0] == key))
                                        {
                                            // key key key
                                            // --- --- ---
                                            // --- --- ---
                                            srideIndex = FixSride_O_X_X(key, ZugaraStatusPackReel3);
                                            if (srideIndex == -1) srideIndex = 0;
                                        }
                                        else if ((ZugaraStatusPackReel1.FixZugara[0] == key) && (ZugaraStatusPackReel2.FixZugara[1] == key))
                                        {
                                            // key --- ---
                                            // --- key ---
                                            // --- --- key
                                            srideIndex = FixSride_X_X_O(key, ZugaraStatusPackReel3);
                                            if (srideIndex == -1) srideIndex = 0;
                                        }
                                        else if ((ZugaraStatusPackReel1.FixZugara[1] == key) && (ZugaraStatusPackReel2.FixZugara[1] == key))
                                        {
                                            // --- --- ---
                                            // key key key
                                            // --- --- ---
                                            srideIndex = FixSride_X_O_X(key, ZugaraStatusPackReel3);
                                            if (srideIndex == -1) srideIndex = 0;
                                        }
                                        else if ((ZugaraStatusPackReel1.FixZugara[2] == key) && (ZugaraStatusPackReel2.FixZugara[1] == key))
                                        {
                                            // --- --- key
                                            // --- key ---
                                            // key --- ---
                                            srideIndex = FixSride_O_X_X(key, ZugaraStatusPackReel3);
                                            if (srideIndex == -1) srideIndex = 0;
                                        }
                                        else if ((ZugaraStatusPackReel1.FixZugara[2] == key) && (ZugaraStatusPackReel2.FixZugara[2] == key))
                                        {
                                            // --- --- ---
                                            // --- --- ---
                                            // key key key
                                            srideIndex = FixSride_X_X_O(key, ZugaraStatusPackReel3);
                                            if (srideIndex == -1) srideIndex = 0;
                                        }
                                    }
                                    break;
                                case "bell":
                                    key = "bell";
                                    if (buttonCount == 0)
                                    {
                                        srideIndex = FixSride_O_O_O(key, ZugaraStatusPackReel3);
                                        if (srideIndex == -1) srideIndex = 0;
                                    }
                                    else if (buttonCount == 1)
                                    {
                                        if (ZugaraStatusPackReel2.IsFix)
                                        {
                                            if (ZugaraStatusPackReel2.FixZugara[0] == key)
                                            {
                                                // ??? key key
                                                // ??? --- ---
                                                // ??? --- ---
                                                srideIndex = FixSride_O_X_X(key, ZugaraStatusPackReel3);
                                                if (srideIndex == -1) srideIndex = 0;
                                            }
                                            else if (ZugaraStatusPackReel2.FixZugara[1] == key)
                                            {
                                                // ??? --- key
                                                // ??? key key
                                                // ??? --- key
                                                srideIndex = FixSride_O_O_O(key, ZugaraStatusPackReel3);
                                                if (srideIndex == -1) srideIndex = 0;
                                            }
                                            else if (ZugaraStatusPackReel2.FixZugara[2] == key)
                                            {
                                                // ??? --- ---
                                                // ??? --- ---
                                                // ??? key key
                                                srideIndex = FixSride_X_X_O(key, ZugaraStatusPackReel3);
                                                if (srideIndex == -1) srideIndex = 0;
                                            }
                                        }
                                        else
                                        {
                                            if (ZugaraStatusPackReel1.FixZugara[0] == key)
                                            {
                                                // key ??? key
                                                // --- ??? ---
                                                // --- ??? key
                                                srideIndex = FixSride_O_X_O(key, ZugaraStatusPackReel3);
                                                if (srideIndex == -1) srideIndex = 0;
                                            }
                                            else if (ZugaraStatusPackReel1.FixZugara[1] == key)
                                            {
                                                // --- ??? ---
                                                // key ??? key
                                                // --- ??? ---
                                                srideIndex = FixSride_X_O_X(key, ZugaraStatusPackReel3);
                                                if (srideIndex == -1) srideIndex = 0;
                                            }
                                            else if (ZugaraStatusPackReel2.FixZugara[2] == key)
                                            {
                                                // --- ??? key
                                                // --- ??? ---
                                                // key ??? key
                                                srideIndex = FixSride_O_X_O(key, ZugaraStatusPackReel3);
                                                if (srideIndex == -1) srideIndex = 0;
                                            }

                                        }
                                    }
                                    else if (buttonCount == 2)
                                    {
                                        if ((ZugaraStatusPackReel1.FixZugara[0] == key) && (ZugaraStatusPackReel2.FixZugara[0] == key))
                                        {
                                            // key key key
                                            // --- --- ---
                                            // --- --- ---
                                            srideIndex = FixSride_O_X_X(key, ZugaraStatusPackReel3);
                                            if (srideIndex == -1) srideIndex = 0;
                                        }
                                        else if ((ZugaraStatusPackReel1.FixZugara[0] == key) && (ZugaraStatusPackReel2.FixZugara[1] == key))
                                        {
                                            // key --- ---
                                            // --- key ---
                                            // --- --- key
                                            srideIndex = FixSride_X_X_O(key, ZugaraStatusPackReel3);
                                            if (srideIndex == -1) srideIndex = 0;
                                        }
                                        else if ((ZugaraStatusPackReel1.FixZugara[1] == key) && (ZugaraStatusPackReel2.FixZugara[1] == key))
                                        {
                                            // --- --- ---
                                            // key key key
                                            // --- --- ---
                                            srideIndex = FixSride_X_O_X(key, ZugaraStatusPackReel3);
                                            if (srideIndex == -1) srideIndex = 0;
                                        }
                                        else if ((ZugaraStatusPackReel1.FixZugara[2] == key) && (ZugaraStatusPackReel2.FixZugara[1] == key))
                                        {
                                            // --- --- key
                                            // --- key ---
                                            // key --- ---
                                            srideIndex = FixSride_O_X_X(key, ZugaraStatusPackReel3);
                                            if (srideIndex == -1) srideIndex = 0;
                                        }
                                        else if ((ZugaraStatusPackReel1.FixZugara[2] == key) && (ZugaraStatusPackReel2.FixZugara[2] == key))
                                        {
                                            // --- --- ---
                                            // --- --- ---
                                            // key key key
                                            srideIndex = FixSride_X_X_O(key, ZugaraStatusPackReel3);
                                            if (srideIndex == -1) srideIndex = 0;
                                        }
                                    }
                                    break;
                                case "suika":
                                    key = "suika";
                                    if (buttonCount == 0)
                                    {
                                        srideIndex = FixSride_O_O_O(key, ZugaraStatusPackReel3);
                                        if (srideIndex == -1) srideIndex = 0;
                                    }
                                    else if (buttonCount == 1)
                                    {
                                        if (ZugaraStatusPackReel2.IsFix)
                                        {
                                            if (ZugaraStatusPackReel2.FixZugara[0] == key)
                                            {
                                                // ??? key key
                                                // ??? --- ---
                                                // ??? --- ---
                                                srideIndex = FixSride_O_X_X(key, ZugaraStatusPackReel3);
                                                if (srideIndex == -1) srideIndex = 0;
                                            }
                                            else if (ZugaraStatusPackReel2.FixZugara[1] == key)
                                            {
                                                // ??? --- key
                                                // ??? key key
                                                // ??? --- key
                                                srideIndex = FixSride_O_O_O(key, ZugaraStatusPackReel3);
                                                if (srideIndex == -1) srideIndex = 0;
                                            }
                                            else if (ZugaraStatusPackReel2.FixZugara[2] == key)
                                            {
                                                // ??? --- ---
                                                // ??? --- ---
                                                // ??? key key
                                                srideIndex = FixSride_X_X_O(key, ZugaraStatusPackReel3);
                                                if (srideIndex == -1) srideIndex = 0;
                                            }
                                        }
                                        else
                                        {
                                            if (ZugaraStatusPackReel1.FixZugara[0] == key)
                                            {
                                                // key ??? key
                                                // --- ??? ---
                                                // --- ??? key
                                                srideIndex = FixSride_O_X_O(key, ZugaraStatusPackReel3);
                                                if (srideIndex == -1) srideIndex = 0;
                                            }
                                            else if (ZugaraStatusPackReel1.FixZugara[1] == key)
                                            {
                                                // --- ??? ---
                                                // key ??? key
                                                // --- ??? ---
                                                srideIndex = FixSride_X_O_X(key, ZugaraStatusPackReel3);
                                                if (srideIndex == -1) srideIndex = 0;
                                            }
                                            else if (ZugaraStatusPackReel2.FixZugara[2] == key)
                                            {
                                                // --- ??? key
                                                // --- ??? ---
                                                // key ??? key
                                                srideIndex = FixSride_O_X_O(key, ZugaraStatusPackReel3);
                                                if (srideIndex == -1) srideIndex = 0;
                                            }

                                        }
                                    }
                                    else if (buttonCount == 2)
                                    {
                                        if ((ZugaraStatusPackReel1.FixZugara[0] == key) && (ZugaraStatusPackReel2.FixZugara[0] == key))
                                        {
                                            // key key key
                                            // --- --- ---
                                            // --- --- ---
                                            srideIndex = FixSride_O_X_X(key, ZugaraStatusPackReel3);
                                            if (srideIndex == -1) srideIndex = 0;
                                        }
                                        else if ((ZugaraStatusPackReel1.FixZugara[0] == key) && (ZugaraStatusPackReel2.FixZugara[1] == key))
                                        {
                                            // key --- ---
                                            // --- key ---
                                            // --- --- key
                                            srideIndex = FixSride_X_X_O(key, ZugaraStatusPackReel3);
                                            if (srideIndex == -1) srideIndex = 0;
                                        }
                                        else if ((ZugaraStatusPackReel1.FixZugara[1] == key) && (ZugaraStatusPackReel2.FixZugara[1] == key))
                                        {
                                            // --- --- ---
                                            // key key key
                                            // --- --- ---
                                            srideIndex = FixSride_X_O_X(key, ZugaraStatusPackReel3);
                                            if (srideIndex == -1) srideIndex = 0;
                                        }
                                        else if ((ZugaraStatusPackReel1.FixZugara[2] == key) && (ZugaraStatusPackReel2.FixZugara[1] == key))
                                        {
                                            // --- --- key
                                            // --- key ---
                                            // key --- ---
                                            srideIndex = FixSride_O_X_X(key, ZugaraStatusPackReel3);
                                            if (srideIndex == -1) srideIndex = 0;
                                        }
                                        else if ((ZugaraStatusPackReel1.FixZugara[2] == key) && (ZugaraStatusPackReel2.FixZugara[2] == key))
                                        {
                                            // --- --- ---
                                            // --- --- ---
                                            // key key key
                                            srideIndex = FixSride_X_X_O(key, ZugaraStatusPackReel3);
                                            if (srideIndex == -1) srideIndex = 0;
                                        }
                                    }
                                    break;
                            }

                            if (CheckBadRoleHit(2, srideIndex, ZugaraStatusPackReel3))
                            {
                                srideIndex = GetSrideBadRoleHit(2, ZugaraStatusPackReel3);
                                Debug.Log($"GetSrideBadRoleHit L3 {srideIndex} ");
                            }

                            buttonCount++;
                            ZugaraStatusPackReel3.Fix(srideIndex);
                            SlotCoreScript.OnButtonL3();
                        }
                    }
                    ZugaraStatusPackReel3.Update();

                    if ((ZugaraStatusPackReel1.IsReelStop()) && (ZugaraStatusPackReel2.IsReelStop()) && (ZugaraStatusPackReel3.IsReelStop()))
                    {
                        slotStep = SlotStep.EndGameUpWait;
                    }
                }
                else
                {
                    ZugaraStatusPackReel1.Update();
                    ZugaraStatusPackReel2.Update();
                    ZugaraStatusPackReel3.Update();
                }

                break;

            case SlotStep.EndGame:

                if (Input.GetKeyUp(KeyCode.Backspace))
                {
                    if(!SlotCoreScript.IsPlay && !SlotCoreScript.IsReplay )
                    {
                        SlotCoreScript.SetCoinIn(3);
                        SlotCoreScript.BetOnToRelease();
                        SlotCoreScript.UpdateText();
                        SlotCoreScript.IsPlay = true;
                        SlotCoreScript.PlayAudio("game_01_bet", new string[] { "SE" });

                        LampL.SetAnimation("def");
                        LampR.SetAnimation("def");
                    }
                }
                else if ( (Input.GetKeyUp(KeyCode.Return)) || preLeberOn )
                {
                    if (oneGameWaitTimerMax <= oneGameWaitTimer)
                    {
                        if (SlotCoreScript.IsPlay || SlotCoreScript.IsReplay)
                        {
                            SlotCoreScript.IsPlay = false;
                            SlotCoreScript.IsReplay = false;

                            ZugaraStatusPackReel1.Play();
                            ZugaraStatusPackReel2.Play();
                            ZugaraStatusPackReel3.Play();

                            SlotCoreScript.DoBaseLot(ref bonus, ref role);
                            Debug.Log($"DoBaseLot {bonus}, {role}");

                            buttonCount = 0;
                            slotStep = SlotStep.NowGame;
                            SlotCoreScript.LeverOn(role, bonus);
                            SlotCoreScript.PlayAudio("game_02_start_a", new string[] { "SE" });

                            SlotCoreScript.GameCount++;
                            SlotCoreScript.UpdateText();

                            LampL.SetAnimation("def");
                            LampR.SetAnimation("def");

                            oneButtonWaitTimer = 0;
                            oneGameWaitTimer = 0;
                            preLeberOn = false;
                        }
                    }
                    else
                    {
                        preLeberOn = true;
                    }
                }
                break;
        }

        switch(slotStep)
        {
            case SlotStep.EndGameUpWait:

                if (Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.DownArrow) || Input.GetKeyUp(KeyCode.RightArrow))
                {
                    NextStepEndGame();
                }
                else
                {
                    if ( !Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.DownArrow) && !Input.GetKey(KeyCode.RightArrow) ) {
                        NextStepEndGame();
                    }
                }
                break;
        }

    }

    public void NextStepEndGame()
    {
        slotStep = SlotStep.EndGame;

        if (CheckHitLine("rep", "rep", "rep"))
        {
            SlotCoreScript.IsReplay = true;
            SlotCoreScript.PlayAudio("game_sorou_koyaku_rep", new string[] { "SE" });
            LampL.SetAnimation("ao");
            LampR.SetAnimation("ao");
        }
        else if (CheckHitLine("bell", "bell", "bell"))
        {
            if (SlotCoreScript.gameStage == SlotCoreScript.GameStage.Normal)
            {
                SlotCoreScript.SetCoinOut(10);
            }
            else
            {
                SlotCoreScript.SetCoinOut(15);
            }
            SlotCoreScript.UpdateText();
            SlotCoreScript.PlayAudio("game_sorou_koyaku_bell", new string[] { "SE" });
            LampL.SetAnimation("ki");
            LampR.SetAnimation("ki");
        }
        else if (CheckHitLine("suika", "suika", "suika"))
        {
            SlotCoreScript.SetCoinOut(6);
            SlotCoreScript.UpdateText();
            SlotCoreScript.PlayAudio("game_sorou_koyaku_suika", new string[] { "SE" });
            LampL.SetAnimation("midori");
            LampR.SetAnimation("midori");
        }
        else if (CheckHitLine("chery", "", ""))
        {
            SlotCoreScript.SetCoinOut(3);
            SlotCoreScript.UpdateText();
            SlotCoreScript.PlayAudio("game_sorou_koyaku_chery", new string[] { "SE" });
            LampL.SetAnimation("aka");
            LampR.SetAnimation("aka");
        }
        else if (CheckHitLine("r7", "r7", "r7"))
        {
            SlotCoreScript.gameStage = SlotCoreScript.GameStage.Bonus;
            SlotCoreScript.bonusTypeName = "Big-r7";
            SlotCoreScript.PlayAudio("bonus_big_a", new string[] { "BGM" }, true);
            SlotCoreScript.PlayAudio("game_sorou_bonus", new string[] { "SE" });
            SlotCoreScript.UpdateText();
            LampL.SetAnimation("shiro");
            LampR.SetAnimation("shiro");
        }
        else if (CheckHitLine("b7", "b7", "b7"))
        {
            SlotCoreScript.gameStage = SlotCoreScript.GameStage.Bonus;
            SlotCoreScript.bonusTypeName = "Big-b7";
            SlotCoreScript.PlayAudio("bonus_big_b", new string[] { "BGM" }, true);
            SlotCoreScript.PlayAudio("game_sorou_bonus", new string[] { "SE" });
            SlotCoreScript.UpdateText();
            LampL.SetAnimation("shiro");
            LampR.SetAnimation("shiro");
        }
        else if (CheckHitLine("r7", "r7", "bar"))
        {
            SlotCoreScript.gameStage = SlotCoreScript.GameStage.Bonus;
            SlotCoreScript.bonusTypeName = "Reg";
            SlotCoreScript.PlayAudio("bonus_reg", new string[] { "BGM" }, true);
            SlotCoreScript.PlayAudio("game_sorou_bonus", new string[] { "SE" });
            SlotCoreScript.UpdateText();
            LampL.SetAnimation("shiro");
            LampR.SetAnimation("shiro");
        }
    }

    public bool CheckHitLine( string key1, string key2, string key3)
    {
        if (key1=="chery")
        {
            if ((ZugaraStatusPackReel1.FixZugara[0] == key1) || (ZugaraStatusPackReel1.FixZugara[1] == key1) || (ZugaraStatusPackReel1.FixZugara[2] == key1))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        if ((ZugaraStatusPackReel1.FixZugara[0] == key1) && (ZugaraStatusPackReel2.FixZugara[0] == key2) && (ZugaraStatusPackReel3.FixZugara[0] == key3))
        {
            // key key key
            // --- --- ---
            // --- --- ---
            return true;
        }
        else if ((ZugaraStatusPackReel1.FixZugara[0] == key1) && (ZugaraStatusPackReel2.FixZugara[1] == key2) && (ZugaraStatusPackReel3.FixZugara[2] == key3))
        {
            // key --- ---
            // --- key ---
            // --- --- key
            return true;
        }
        else if ((ZugaraStatusPackReel1.FixZugara[1] == key1) && (ZugaraStatusPackReel2.FixZugara[1] == key2) && (ZugaraStatusPackReel3.FixZugara[1] == key3))
        {
            // --- --- ---
            // key key key
            // --- --- ---
            return true;
        }
        else if ((ZugaraStatusPackReel1.FixZugara[2] == key1) && (ZugaraStatusPackReel2.FixZugara[1] == key2) && (ZugaraStatusPackReel3.FixZugara[0] == key3))
        {
            // --- --- key
            // --- key ---
            // key --- ---
            return true;
        }
        else if ((ZugaraStatusPackReel1.FixZugara[2] == key1) && (ZugaraStatusPackReel2.FixZugara[2] == key2) && (ZugaraStatusPackReel3.FixZugara[2] == key3))
        {
            // --- --- ---
            // --- --- ---
            // key key key
            return true;
        }
        return false;
    }

    // 抽選されていないものがヒットしてしまうかどうか
    private bool CheckBadRoleHit( int line, int sride, ZugaraStatusPack zugaraStatusPack)
    {
        if ( line==0)
        {
            var tmp = zugaraStatusPack.GetFixZugara(sride);
            if (role != "chery")
            {
                if ((tmp[0] == "chery") || (tmp[1] == "chery") || (tmp[2] == "chery"))
                {
                    return true;
                }
            }

            if ( buttonCount==2 )
            {
                System.Func<string, bool> func1 = (key) =>
                 {
                     if ((tmp[0] == key) && (ZugaraStatusPackReel2.FixZugara[0] == key) && (ZugaraStatusPackReel3.FixZugara[0] == key))
                     {
                        // key key key
                        // --- --- ---
                        // --- --- ---
                        return true;
                     }
                     else if ((tmp[0] == key) && (ZugaraStatusPackReel2.FixZugara[1] == key) && (ZugaraStatusPackReel3.FixZugara[2] == key))
                     {
                        // key --- ---
                        // --- key ---
                        // --- --- key
                        return true;
                     }
                     else if ((tmp[1] == key) && (ZugaraStatusPackReel2.FixZugara[1] == key) && (ZugaraStatusPackReel3.FixZugara[1] == key))
                     {
                        // --- --- ---
                        // key key key
                        // --- --- ---
                        return true;
                     }
                     else if ((tmp[2] == key) && (ZugaraStatusPackReel2.FixZugara[1] == key) && (ZugaraStatusPackReel3.FixZugara[0] == key))
                     {
                        // --- --- key
                        // --- key ---
                        // key --- ---
                        return true;
                     }
                     else if ((tmp[2] == key) && (ZugaraStatusPackReel2.FixZugara[2] == key) && (ZugaraStatusPackReel3.FixZugara[2] == key))
                     {
                        // --- --- ---
                        // --- --- ---
                        // key key key
                        return true;
                     }
                     return false;
                 };

                var key = "";

                key = "rep";
                if (key != role)
                {
                    if (func1(key)) return true;
                }

                key = "bell";
                if (key != role)
                {
                    if (func1(key)) return true;
                }

                key = "suika";
                if (key != role)
                {
                    if (func1(key)) return true;
                }

                if ("Big-r7" != bonus)
                {
                    if (func1("r7")) return true;
                }

                if ("Big-b7" != bonus)
                {
                    if (func1("b7")) return true;
                }

                key = "Reg";
                if (key != bonus)
                {
                    if ((tmp[0] == "r7") && (ZugaraStatusPackReel2.FixZugara[0] == "r7") && (ZugaraStatusPackReel3.FixZugara[0] == "bar"))
                    {
                        // key key key
                        // --- --- ---
                        // --- --- ---
                        return true;
                    }
                    else if ((tmp[0] == "r7") && (ZugaraStatusPackReel2.FixZugara[1] == "r7") && (ZugaraStatusPackReel3.FixZugara[2] == "bar"))
                    {
                        // key --- ---
                        // --- key ---
                        // --- --- key
                        return true;
                    }
                    else if ((tmp[1] == "r7") && (ZugaraStatusPackReel2.FixZugara[1] == "r7") && (ZugaraStatusPackReel3.FixZugara[1] == "bar"))
                    {
                        // --- --- ---
                        // key key key
                        // --- --- ---
                        return true;
                    }
                    else if ((tmp[2] == "r7") && (ZugaraStatusPackReel2.FixZugara[1] == "r7") && (ZugaraStatusPackReel3.FixZugara[0] == "bar"))
                    {
                        // --- --- key
                        // --- key ---
                        // key --- ---
                        return true;
                    }
                    else if ((tmp[2] == "r7") && (ZugaraStatusPackReel2.FixZugara[2] == "r7") && (ZugaraStatusPackReel3.FixZugara[2] == "bar"))
                    {
                        // --- --- ---
                        // --- --- ---
                        // key key key
                        return true;
                    }
                    return false;
                }
            }
        }
        else if (line == 1)
        {
            var tmp = zugaraStatusPack.GetFixZugara(sride);

            if (buttonCount == 2)
            {
                System.Func<string, bool> func2 = (key) =>
                {
                    if ((tmp[0] == key) && (ZugaraStatusPackReel1.FixZugara[0] == key) && (ZugaraStatusPackReel3.FixZugara[0] == key))
                    {
                        // key key key
                        // --- --- ---
                        // --- --- ---
                        return true;
                    }
                    else if ((tmp[1] == key) && (ZugaraStatusPackReel1.FixZugara[0] == key) && (ZugaraStatusPackReel3.FixZugara[2] == key))
                    {
                        // key --- ---
                        // --- key ---
                        // --- --- key
                        return true;
                    }
                    else if ((tmp[1] == key) && (ZugaraStatusPackReel1.FixZugara[1] == key) && (ZugaraStatusPackReel3.FixZugara[1] == key))
                    {
                        // --- --- ---
                        // key key key
                        // --- --- ---
                        return true;
                    }
                    else if ((tmp[1] == key) && (ZugaraStatusPackReel1.FixZugara[2] == key) && (ZugaraStatusPackReel3.FixZugara[0] == key))
                    {
                        // --- --- key
                        // --- key ---
                        // key --- ---
                        return true;
                    }
                    else if ((tmp[2] == key) && (ZugaraStatusPackReel1.FixZugara[2] == key) && (ZugaraStatusPackReel3.FixZugara[2] == key))
                    {
                        // --- --- ---
                        // --- --- ---
                        // key key key
                        return true;
                    }
                    return false;
                };

                var key = "";

                key = "rep";
                if (key != role)
                {
                    if (func2(key)) return true;
                }

                key = "bell";
                if (key != role)
                {
                    if (func2(key)) return true;
                }

                key = "suika";
                if (key != role)
                {
                    if (func2(key)) return true;
                }

                if ("Big-r7" != bonus)
                {
                    if (func2("r7")) return true;
                }

                if ("Big-b7" != bonus)
                {
                    if (func2("b7")) return true;
                }

                if ("Reg" != bonus) {
                    if ((tmp[0] == "r7") && (ZugaraStatusPackReel1.FixZugara[0] == "r7") && (ZugaraStatusPackReel3.FixZugara[0] == "bar"))
                    {
                        // key key key
                        // --- --- ---
                        // --- --- ---
                        return true;
                    }
                    else if ((tmp[1] == "r7") && (ZugaraStatusPackReel1.FixZugara[0] == "r7") && (ZugaraStatusPackReel3.FixZugara[2] == "bar"))
                    {
                        // key --- ---
                        // --- key ---
                        // --- --- key
                        return true;
                    }
                    else if ((tmp[1] == "r7") && (ZugaraStatusPackReel1.FixZugara[1] == "r7") && (ZugaraStatusPackReel3.FixZugara[1] == "bar"))
                    {
                        // --- --- ---
                        // key key key
                        // --- --- ---
                        return true;
                    }
                    else if ((tmp[1] == "r7") && (ZugaraStatusPackReel1.FixZugara[2] == "r7") && (ZugaraStatusPackReel3.FixZugara[0] == "bar"))
                    {
                        // --- --- key
                        // --- key ---
                        // key --- ---
                        return true;
                    }
                    else if ((tmp[2] == "r7") && (ZugaraStatusPackReel1.FixZugara[2] == "r7") && (ZugaraStatusPackReel3.FixZugara[2] == "bar"))
                    {
                        // --- --- ---
                        // --- --- ---
                        // key key key
                        return true;
                    }
                    return false;
                }
            }
        }
        else if (line == 2)
        {
            var tmp = zugaraStatusPack.GetFixZugara(sride);

            if (buttonCount == 2)
            {
                System.Func<string, bool> func3 = (key) =>
                {
                    if ((tmp[0] == key) && (ZugaraStatusPackReel1.FixZugara[0] == key) && (ZugaraStatusPackReel2.FixZugara[0] == key))
                    {
                        // key key key
                        // --- --- ---
                        // --- --- ---
                        return true;
                    }
                    else if ((tmp[2] == key) && (ZugaraStatusPackReel1.FixZugara[0] == key) && (ZugaraStatusPackReel2.FixZugara[1] == key))
                    {
                        // key --- ---
                        // --- key ---
                        // --- --- key
                        return true;
                    }
                    else if ((tmp[1] == key) && (ZugaraStatusPackReel1.FixZugara[1] == key) && (ZugaraStatusPackReel2.FixZugara[1] == key))
                    {
                        // --- --- ---
                        // key key key
                        // --- --- ---
                        return true;
                    }
                    else if ((tmp[0] == key) && (ZugaraStatusPackReel1.FixZugara[2] == key) && (ZugaraStatusPackReel2.FixZugara[1] == key))
                    {
                        // --- --- key
                        // --- key ---
                        // key --- ---
                        return true;
                    }
                    else if ((tmp[2] == key) && (ZugaraStatusPackReel1.FixZugara[2] == key) && (ZugaraStatusPackReel2.FixZugara[2] == key))
                    {
                        // --- --- ---
                        // --- --- ---
                        // key key key
                        return true;
                    }
                    return false;
                };

                var key = "";

                key = "rep";
                if (key != role)
                {
                    if (func3(key)) return true;
                }

                key = "bell";
                if (key != role)
                {
                    if (func3(key)) return true;
                }

                key = "suika";
                if (key != role)
                {
                    if (func3(key)) return true;
                }

                if ("Big-r7" != bonus)
                {
                    if (func3("r7")) return true;
                }

                if ("Big-b7" != bonus)
                {
                    if (func3("b7")) return true;
                }

                if ("Reg" != bonus)
                {
                    if ((tmp[0] == "r7") && (ZugaraStatusPackReel1.FixZugara[0] == "r7") && (ZugaraStatusPackReel2.FixZugara[0] == "bar"))
                    {
                        // key key key
                        // --- --- ---
                        // --- --- ---
                        return true;
                    }
                    else if ((tmp[2] == "r7") && (ZugaraStatusPackReel1.FixZugara[0] == "r7") && (ZugaraStatusPackReel2.FixZugara[1] == "bar"))
                    {
                        // key --- ---
                        // --- key ---
                        // --- --- key
                        return true;
                    }
                    else if ((tmp[1] == "r7") && (ZugaraStatusPackReel1.FixZugara[1] == "r7") && (ZugaraStatusPackReel2.FixZugara[1] == "bar"))
                    {
                        // --- --- ---
                        // key key key
                        // --- --- ---
                        return true;
                    }
                    else if ((tmp[0] == "r7") && (ZugaraStatusPackReel1.FixZugara[2] == "r7") && (ZugaraStatusPackReel2.FixZugara[1] == "bar"))
                    {
                        // --- --- key
                        // --- key ---
                        // key --- ---
                        return true;
                    }
                    else if ((tmp[2] == "r7") && (ZugaraStatusPackReel1.FixZugara[2] == "r7") && (ZugaraStatusPackReel2.FixZugara[2] == "bar"))
                    {
                        // --- --- ---
                        // --- --- ---
                        // key key key
                        return true;
                    }
                    return false;
                }
            }
        }

        return false;
    }


    private int GetSrideBadRoleHit(int line, ZugaraStatusPack zugaraStatusPack)
    {
        for( var i=0; i<5;i++ )
        {
            if (!CheckBadRoleHit(line,i, zugaraStatusPack))
            {
                return i;
            }
        }
        return 0; // err
    }


    private int FixSride_O_O_O( string key, ZugaraStatusPack zugaraStatusPack )
    {
        var tmp = new string[3] { "", "", "" };
        for (var i = 0; i < 5; i++)
        {
            tmp = zugaraStatusPack.GetFixZugara(i);
            if ((tmp[0] == key) || (tmp[1] == key) || (tmp[2] == key))
            {
                return i;
            }
        }
        return -1;
    }

    private int FixSride_O_O_X(string key, ZugaraStatusPack zugaraStatusPack)
    {
        var tmp = new string[3] { "", "", "" };
        for (var i = 0; i < 5; i++)
        {
            tmp = zugaraStatusPack.GetFixZugara(i);
            if ((tmp[0] == key) || (tmp[1] == key) )
            {
                return i;
            }
        }
        return -1;
    }
    private int FixSride_O_X_O(string key, ZugaraStatusPack zugaraStatusPack)
    {
        var tmp = new string[3] { "", "", "" };
        for (var i = 0; i < 5; i++)
        {
            tmp = zugaraStatusPack.GetFixZugara(i);
            if ((tmp[0] == key) || (tmp[2] == key))
            {
                return i;
            }
        }
        return -1;
    }
    private int FixSride_X_O_O(string key, ZugaraStatusPack zugaraStatusPack)
    {
        var tmp = new string[3] { "", "", "" };
        for (var i = 0; i < 5; i++)
        {
            tmp = zugaraStatusPack.GetFixZugara(i);
            if ((tmp[1] == key) || (tmp[2] == key))
            {
                return i;
            }
        }
        return -1;
    }
    private int FixSride_O_X_X(string key, ZugaraStatusPack zugaraStatusPack)
    {
        var tmp = new string[3] { "", "", "" };
        for (var i = 0; i < 5; i++)
        {
            tmp = zugaraStatusPack.GetFixZugara(i);
            if (tmp[0] == key)
            {
                return i;
            }
        }
        return -1;
    }
    private int FixSride_X_O_X(string key, ZugaraStatusPack zugaraStatusPack)
    {
        var tmp = new string[3] { "", "", "" };
        for (var i = 0; i < 5; i++)
        {
            tmp = zugaraStatusPack.GetFixZugara(i);
            if (tmp[1] == key)
            {
                return i;
            }
        }
        return -1;
    }
    private int FixSride_X_X_O(string key, ZugaraStatusPack zugaraStatusPack)
    {
        var tmp = new string[3] { "", "", "" };
        for (var i = 0; i < 5; i++)
        {
            tmp = zugaraStatusPack.GetFixZugara(i);
            if (tmp[2] == key)
            {
                return i;
            }
        }
        return -1;
    }

    public void SetReelSpeed( float value )
    {
        //    public float ReelTimerMax = 0.0333f * 1; // 0.0333f 1分間80回転が上限
        //public float ReelTimerMaxBase = 0.0333f;

        var offsetBase = 5f;

        ZugaraStatusPackReel1.ReelTimerMaxOffset = value;
        ZugaraStatusPackReel1.ReelTimerMax = ZugaraStatusPackReel1.ReelTimerMaxBase * (1f + (1f - value) * offsetBase);

        ZugaraStatusPackReel2.ReelTimerMaxOffset = value;
        ZugaraStatusPackReel2.ReelTimerMax = ZugaraStatusPackReel2.ReelTimerMaxBase * (1f + (1f - value) * offsetBase);

        ZugaraStatusPackReel3.ReelTimerMaxOffset = value;
        ZugaraStatusPackReel3.ReelTimerMax = ZugaraStatusPackReel3.ReelTimerMaxBase * (1f + (1f - value) * offsetBase);
    }
}
