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
    SlotCore slotCore;

    [SerializeField]
    List<Zugara> zugaras = new List<Zugara>();

    [SerializeField]
    GameObject zugaraPrefab;

    [SerializeField]
    Lamp lampLeft;

    [SerializeField]
    Lamp lampRight;

    [SerializeField]
    SoundResource soundResource;

    public ReelZugaraStatusPack zugaraStatusPackReelL1 = new ReelZugaraStatusPack();
    public ReelZugaraStatusPack zugaraStatusPackReelL2 = new ReelZugaraStatusPack();
    public ReelZugaraStatusPack zugaraStatusPackReelL3 = new ReelZugaraStatusPack();

    [SerializeField]
    string[] reel1ZugaraSource;

    [SerializeField]
    string[] reel2ZugaraSource;

    [SerializeField]
    string[] reel3ZugaraSource;

    [SerializeField]
    int slideMax = 5;

    void Start()
    {
        zugaraStatusPackReelL1.Init(-2.15f, -0.41f, reel1ZugaraSource, zugaraPrefab, transform, this);
        zugaraStatusPackReelL2.Init(    0f, -0.41f, reel2ZugaraSource, zugaraPrefab, transform, this);
        zugaraStatusPackReelL3.Init( 2.15f, -0.41f, reel3ZugaraSource, zugaraPrefab, transform, this);
    }

    public Zugara GetZugaraByKey(string key)
    {
        foreach (var i in zugaras)
        {
            if (i.Key == key) return i;
        }
        return null;
    }

    public void StartRollAll()
    {
        zugaraStatusPackReelL1.Play();
        zugaraStatusPackReelL2.Play();
        zugaraStatusPackReelL3.Play();

        lampLeft.SetAnimation("def");
        lampRight.SetAnimation("def");
    }

    public void StopRollOne( SlotCore.ButtonType buttonType )
    {
        switch (buttonType)
        {
            case SlotCore.ButtonType.ReelStopL1:
                StopRollOneL1();
                break;
            case SlotCore.ButtonType.ReelStopL2:
                StopRollOneL2();
                break;
            case SlotCore.ButtonType.ReelStopL3:
                StopRollOneL3();
                break;
        }
    }

    private void StopRollOneL1()
    {
        if (zugaraStatusPackReelL1.IsFix) return;
        var srideIndex = 0;
        var key = "";

        var role = slotCore.oneGame.flagRole;
        var bonus = slotCore.oneGame.flagBounus;

        switch (role)
        {
            default:
                switch (bonus)
                {
                    case "Big-r7":
                        Debug.Log($"Big-r7");
                        key = "r7";
                        if (slotCore.oneGame.buttonCount == 1)
                        {
                            srideIndex = FixSride_O_O_O(key, zugaraStatusPackReelL1);
                            if (srideIndex == -1) srideIndex = 0;
                        }
                        else if (slotCore.oneGame.buttonCount == 2)
                        {
                            if (zugaraStatusPackReelL2.IsFix)
                            {
                                if (zugaraStatusPackReelL2.FixZugara[0] == key)
                                {
                                    // key key ???
                                    // --- --- ???
                                    // --- --- ???
                                    srideIndex = FixSride_O_X_X(key, zugaraStatusPackReelL1);
                                    if (srideIndex == -1) srideIndex = 0;
                                }
                                else if (zugaraStatusPackReelL2.FixZugara[1] == key)
                                {
                                    // key --- ???
                                    // key key ???
                                    // key --- ???
                                    srideIndex = FixSride_O_O_O(key, zugaraStatusPackReelL1);
                                    if (srideIndex == -1) srideIndex = 0;
                                }
                                else if (zugaraStatusPackReelL2.FixZugara[2] == key)
                                {
                                    // --- --- ???
                                    // --- --- ???
                                    // key key ???
                                    srideIndex = FixSride_X_X_O(key, zugaraStatusPackReelL1);
                                    if (srideIndex == -1) srideIndex = 0;
                                }
                            }
                            else
                            {
                                if (zugaraStatusPackReelL3.FixZugara[0] == key)
                                {
                                    // key ??? key
                                    // --- ??? ---
                                    // key ??? ---
                                    srideIndex = FixSride_O_X_O(key, zugaraStatusPackReelL1);
                                    if (srideIndex == -1) srideIndex = 0;
                                }
                                else if (zugaraStatusPackReelL3.FixZugara[1] == key)
                                {
                                    // --- ??? ---
                                    // key ??? key
                                    // --- ??? ---
                                    srideIndex = FixSride_X_O_X(key, zugaraStatusPackReelL1);
                                    if (srideIndex == -1) srideIndex = 0;
                                }
                                else if (zugaraStatusPackReelL3.FixZugara[2] == key)
                                {
                                    // key ??? ---
                                    // --- ??? ---
                                    // key ??? key
                                    srideIndex = FixSride_O_X_O(key, zugaraStatusPackReelL1);
                                    if (srideIndex == -1) srideIndex = 0;
                                }
                            }
                        }
                        else if (slotCore.oneGame.buttonCount == 3)
                        {
                            if ((zugaraStatusPackReelL2.FixZugara[0] == key) && (zugaraStatusPackReelL3.FixZugara[0] == key))
                            {
                                // key key key
                                // --- --- ---
                                // --- --- ---
                                srideIndex = FixSride_O_X_X(key, zugaraStatusPackReelL1);
                                if (srideIndex == -1) srideIndex = 0;
                            }
                            else if ((zugaraStatusPackReelL2.FixZugara[1] == key) && (zugaraStatusPackReelL3.FixZugara[2] == key))
                            {
                                // key --- ---
                                // --- key ---
                                // --- --- key
                                srideIndex = FixSride_O_X_X(key, zugaraStatusPackReelL1);
                                if (srideIndex == -1) srideIndex = 0;
                            }
                            else if ((zugaraStatusPackReelL2.FixZugara[1] == key) && (zugaraStatusPackReelL3.FixZugara[1] == key))
                            {
                                // --- --- ---
                                // key key key
                                // --- --- ---
                                srideIndex = FixSride_X_O_X(key, zugaraStatusPackReelL1);
                                if (srideIndex == -1) srideIndex = 0;
                            }
                            else if ((zugaraStatusPackReelL2.FixZugara[1] == key) && (zugaraStatusPackReelL3.FixZugara[0] == key))
                            {
                                // --- --- key
                                // --- key ---
                                // key --- ---
                                srideIndex = FixSride_X_X_O(key, zugaraStatusPackReelL1);
                                if (srideIndex == -1) srideIndex = 0;
                            }
                            else if ((zugaraStatusPackReelL2.FixZugara[2] == key) && (zugaraStatusPackReelL3.FixZugara[2] == key))
                            {
                                // --- --- ---
                                // --- --- ---
                                // key key key
                                srideIndex = FixSride_X_X_O(key, zugaraStatusPackReelL1);
                                if (srideIndex == -1) srideIndex = 0;
                            }
                        }
                        break;
                    case "Big-b7":
                        key = "b7";
                        if (slotCore.oneGame.buttonCount == 1)
                        {
                            srideIndex = FixSride_O_O_O(key, zugaraStatusPackReelL1);
                            if (srideIndex == -1) srideIndex = 0;
                        }
                        else if (slotCore.oneGame.buttonCount == 2)
                        {
                            if (zugaraStatusPackReelL2.IsFix)
                            {
                                if (zugaraStatusPackReelL2.FixZugara[0] == key)
                                {
                                    // key key ???
                                    // --- --- ???
                                    // --- --- ???
                                    srideIndex = FixSride_O_X_X(key, zugaraStatusPackReelL1);
                                    if (srideIndex == -1) srideIndex = 0;
                                }
                                else if (zugaraStatusPackReelL2.FixZugara[1] == key)
                                {
                                    // key --- ???
                                    // key key ???
                                    // key --- ???
                                    srideIndex = FixSride_O_O_O(key, zugaraStatusPackReelL1);
                                    if (srideIndex == -1) srideIndex = 0;
                                }
                                else if (zugaraStatusPackReelL2.FixZugara[2] == key)
                                {
                                    // --- --- ???
                                    // --- --- ???
                                    // key key ???
                                    srideIndex = FixSride_X_X_O(key, zugaraStatusPackReelL1);
                                    if (srideIndex == -1) srideIndex = 0;
                                }
                            }
                            else
                            {
                                if (zugaraStatusPackReelL3.FixZugara[0] == key)
                                {
                                    // key ??? key
                                    // --- ??? ---
                                    // key ??? ---
                                    srideIndex = FixSride_O_X_O(key, zugaraStatusPackReelL1);
                                    if (srideIndex == -1) srideIndex = 0;
                                }
                                else if (zugaraStatusPackReelL3.FixZugara[1] == key)
                                {
                                    // --- ??? ---
                                    // key ??? key
                                    // --- ??? ---
                                    srideIndex = FixSride_X_O_X(key, zugaraStatusPackReelL1);
                                    if (srideIndex == -1) srideIndex = 0;
                                }
                                else if (zugaraStatusPackReelL3.FixZugara[2] == key)
                                {
                                    // key ??? ---
                                    // --- ??? ---
                                    // key ??? key
                                    srideIndex = FixSride_O_X_O(key, zugaraStatusPackReelL1);
                                    if (srideIndex == -1) srideIndex = 0;
                                }
                            }
                        }
                        else if (slotCore.oneGame.buttonCount == 3)
                        {
                            if ((zugaraStatusPackReelL2.FixZugara[0] == key) && (zugaraStatusPackReelL3.FixZugara[0] == key))
                            {
                                // key key key
                                // --- --- ---
                                // --- --- ---
                                srideIndex = FixSride_O_X_X(key, zugaraStatusPackReelL1);
                                if (srideIndex == -1) srideIndex = 0;
                            }
                            else if ((zugaraStatusPackReelL2.FixZugara[1] == key) && (zugaraStatusPackReelL3.FixZugara[2] == key))
                            {
                                // key --- ---
                                // --- key ---
                                // --- --- key
                                srideIndex = FixSride_O_X_X(key, zugaraStatusPackReelL1);
                                if (srideIndex == -1) srideIndex = 0;
                            }
                            else if ((zugaraStatusPackReelL2.FixZugara[1] == key) && (zugaraStatusPackReelL3.FixZugara[1] == key))
                            {
                                // --- --- ---
                                // key key key
                                // --- --- ---
                                srideIndex = FixSride_X_O_X(key, zugaraStatusPackReelL1);
                                if (srideIndex == -1) srideIndex = 0;
                            }
                            else if ((zugaraStatusPackReelL2.FixZugara[1] == key) && (zugaraStatusPackReelL3.FixZugara[0] == key))
                            {
                                // --- --- key
                                // --- key ---
                                // key --- ---
                                srideIndex = FixSride_X_X_O(key, zugaraStatusPackReelL1);
                                if (srideIndex == -1) srideIndex = 0;
                            }
                            else if ((zugaraStatusPackReelL2.FixZugara[2] == key) && (zugaraStatusPackReelL3.FixZugara[2] == key))
                            {
                                // --- --- ---
                                // --- --- ---
                                // key key key
                                srideIndex = FixSride_X_X_O(key, zugaraStatusPackReelL1);
                                if (srideIndex == -1) srideIndex = 0;
                            }
                        }
                        break;
                    case "Reg":
                        key = "r7";
                        if (slotCore.oneGame.buttonCount == 1)
                        {
                            srideIndex = FixSride_O_O_O(key, zugaraStatusPackReelL1);
                            if (srideIndex == -1) srideIndex = 0;
                        }
                        else if (slotCore.oneGame.buttonCount == 2)
                        {
                            if (zugaraStatusPackReelL2.IsFix)
                            {
                                if (zugaraStatusPackReelL2.FixZugara[0] == key)
                                {
                                    // key key ???
                                    // --- --- ???
                                    // --- --- ???
                                    srideIndex = FixSride_O_X_X(key, zugaraStatusPackReelL1);
                                    if (srideIndex == -1) srideIndex = 0;
                                }
                                else if (zugaraStatusPackReelL2.FixZugara[1] == key)
                                {
                                    // key --- ???
                                    // key key ???
                                    // key --- ???
                                    srideIndex = FixSride_O_O_O(key, zugaraStatusPackReelL1);
                                    if (srideIndex == -1) srideIndex = 0;
                                }
                                else if (zugaraStatusPackReelL2.FixZugara[2] == key)
                                {
                                    // --- --- ???
                                    // --- --- ???
                                    // key key ???
                                    srideIndex = FixSride_X_X_O(key, zugaraStatusPackReelL1);
                                    if (srideIndex == -1) srideIndex = 0;
                                }
                            }
                            else
                            {
                                if (zugaraStatusPackReelL3.FixZugara[0] == "bar")
                                {
                                    // key ??? key
                                    // --- ??? ---
                                    // key ??? ---
                                    srideIndex = FixSride_O_X_O(key, zugaraStatusPackReelL1);
                                    if (srideIndex == -1) srideIndex = 0;
                                }
                                else if (zugaraStatusPackReelL3.FixZugara[1] == "bar")
                                {
                                    // --- ??? ---
                                    // key ??? key
                                    // --- ??? ---
                                    srideIndex = FixSride_X_O_X(key, zugaraStatusPackReelL1);
                                    if (srideIndex == -1) srideIndex = 0;
                                }
                                else if (zugaraStatusPackReelL3.FixZugara[2] == "bar")
                                {
                                    // key ??? ---
                                    // --- ??? ---
                                    // key ??? key
                                    srideIndex = FixSride_O_X_O(key, zugaraStatusPackReelL1);
                                    if (srideIndex == -1) srideIndex = 0;
                                }
                            }
                        }
                        else if (slotCore.oneGame.buttonCount == 3)
                        {
                            if ((zugaraStatusPackReelL2.FixZugara[0] == key) && (zugaraStatusPackReelL3.FixZugara[0] == "bar"))
                            {
                                // key key key
                                // --- --- ---
                                // --- --- ---
                                srideIndex = FixSride_O_X_X(key, zugaraStatusPackReelL1);
                                if (srideIndex == -1) srideIndex = 0;
                            }
                            else if ((zugaraStatusPackReelL2.FixZugara[1] == key) && (zugaraStatusPackReelL3.FixZugara[2] == "bar"))
                            {
                                // key --- ---
                                // --- key ---
                                // --- --- key
                                srideIndex = FixSride_O_X_X(key, zugaraStatusPackReelL1);
                                if (srideIndex == -1) srideIndex = 0;
                            }
                            else if ((zugaraStatusPackReelL2.FixZugara[1] == key) && (zugaraStatusPackReelL3.FixZugara[1] == "bar"))
                            {
                                // --- --- ---
                                // key key key
                                // --- --- ---
                                srideIndex = FixSride_X_O_X(key, zugaraStatusPackReelL1);
                                if (srideIndex == -1) srideIndex = 0;
                            }
                            else if ((zugaraStatusPackReelL2.FixZugara[1] == key) && (zugaraStatusPackReelL3.FixZugara[0] == "bar"))
                            {
                                // --- --- key
                                // --- key ---
                                // key --- ---
                                srideIndex = FixSride_X_X_O(key, zugaraStatusPackReelL1);
                                if (srideIndex == -1) srideIndex = 0;
                            }
                            else if ((zugaraStatusPackReelL2.FixZugara[2] == key) && (zugaraStatusPackReelL3.FixZugara[2] == "bar"))
                            {
                                // --- --- ---
                                // --- --- ---
                                // key key key
                                srideIndex = FixSride_X_X_O(key, zugaraStatusPackReelL1);
                                if (srideIndex == -1) srideIndex = 0;
                            }
                        }
                        break;
                }
                break;
            case "rep":
                key = "rep";
                if (slotCore.oneGame.buttonCount == 1)
                {
                    srideIndex = FixSride_O_O_O(key, zugaraStatusPackReelL1);
                    if (srideIndex == -1) srideIndex = 0;
                }
                else if (slotCore.oneGame.buttonCount == 2)
                {
                    if (zugaraStatusPackReelL2.IsFix)
                    {
                        if (zugaraStatusPackReelL2.FixZugara[0] == key)
                        {
                            // key key ???
                            // --- --- ???
                            // --- --- ???
                            srideIndex = FixSride_O_X_X(key, zugaraStatusPackReelL1);
                            if (srideIndex == -1) srideIndex = 0;
                        }
                        else if (zugaraStatusPackReelL2.FixZugara[1] == key)
                        {
                            // key --- ???
                            // key key ???
                            // key --- ???
                            srideIndex = FixSride_O_O_O(key, zugaraStatusPackReelL1);
                            if (srideIndex == -1) srideIndex = 0;
                        }
                        else if (zugaraStatusPackReelL2.FixZugara[2] == key)
                        {
                            // --- --- ???
                            // --- --- ???
                            // key key ???
                            srideIndex = FixSride_X_X_O(key, zugaraStatusPackReelL1);
                            if (srideIndex == -1) srideIndex = 0;
                        }
                    }
                    else
                    {
                        if (zugaraStatusPackReelL3.FixZugara[0] == key)
                        {
                            // key ??? key
                            // --- ??? ---
                            // key ??? ---
                            srideIndex = FixSride_O_X_O(key, zugaraStatusPackReelL1);
                            if (srideIndex == -1) srideIndex = 0;
                        }
                        else if (zugaraStatusPackReelL3.FixZugara[1] == key)
                        {
                            // --- ??? ---
                            // key ??? key
                            // --- ??? ---
                            srideIndex = FixSride_X_O_X(key, zugaraStatusPackReelL1);
                            if (srideIndex == -1) srideIndex = 0;
                        }
                        else if (zugaraStatusPackReelL3.FixZugara[2] == key)
                        {
                            // key ??? ---
                            // --- ??? ---
                            // key ??? key
                            srideIndex = FixSride_O_X_O(key, zugaraStatusPackReelL1);
                            if (srideIndex == -1) srideIndex = 0;
                        }
                    }
                }
                else if (slotCore.oneGame.buttonCount == 3)
                {
                    if ((zugaraStatusPackReelL2.FixZugara[0] == key) && (zugaraStatusPackReelL3.FixZugara[0] == key))
                    {
                        // key key key
                        // --- --- ---
                        // --- --- ---
                        srideIndex = FixSride_O_X_X(key, zugaraStatusPackReelL1);
                        if (srideIndex == -1) srideIndex = 0;
                    }
                    else if ((zugaraStatusPackReelL2.FixZugara[1] == key) && (zugaraStatusPackReelL3.FixZugara[2] == key))
                    {
                        // key --- ---
                        // --- key ---
                        // --- --- key
                        srideIndex = FixSride_O_X_X(key, zugaraStatusPackReelL1);
                        if (srideIndex == -1) srideIndex = 0;
                    }
                    else if ((zugaraStatusPackReelL2.FixZugara[1] == key) && (zugaraStatusPackReelL3.FixZugara[1] == key))
                    {
                        // --- --- ---
                        // key key key
                        // --- --- ---
                        srideIndex = FixSride_X_O_X(key, zugaraStatusPackReelL1);
                        if (srideIndex == -1) srideIndex = 0;
                    }
                    else if ((zugaraStatusPackReelL2.FixZugara[1] == key) && (zugaraStatusPackReelL3.FixZugara[0] == key))
                    {
                        // --- --- key
                        // --- key ---
                        // key --- ---
                        srideIndex = FixSride_X_X_O(key, zugaraStatusPackReelL1);
                        if (srideIndex == -1) srideIndex = 0;
                    }
                    else if ((zugaraStatusPackReelL2.FixZugara[2] == key) && (zugaraStatusPackReelL3.FixZugara[2] == key))
                    {
                        // --- --- ---
                        // --- --- ---
                        // key key key
                        srideIndex = FixSride_X_X_O(key, zugaraStatusPackReelL1);
                        if (srideIndex == -1) srideIndex = 0;
                    }
                }
                break;
            case "bell":
                key = "bell";
                if (slotCore.oneGame.buttonCount == 1)
                {
                    srideIndex = FixSride_O_O_O(key, zugaraStatusPackReelL1);
                    if (srideIndex == -1) srideIndex = 0;
                }
                else if (slotCore.oneGame.buttonCount == 2)
                {
                    if (zugaraStatusPackReelL2.IsFix)
                    {
                        if (zugaraStatusPackReelL2.FixZugara[0] == key)
                        {
                            // key key ???
                            // --- --- ???
                            // --- --- ???
                            srideIndex = FixSride_O_X_X(key, zugaraStatusPackReelL1);
                            if (srideIndex == -1) srideIndex = 0;
                        }
                        else if (zugaraStatusPackReelL2.FixZugara[1] == key)
                        {
                            // key --- ???
                            // key key ???
                            // key --- ???
                            srideIndex = FixSride_O_O_O(key, zugaraStatusPackReelL1);
                            if (srideIndex == -1) srideIndex = 0;
                        }
                        else if (zugaraStatusPackReelL2.FixZugara[2] == key)
                        {
                            // --- --- ???
                            // --- --- ???
                            // key key ???
                            srideIndex = FixSride_X_X_O(key, zugaraStatusPackReelL1);
                            if (srideIndex == -1) srideIndex = 0;
                        }
                    }
                    else
                    {
                        if (zugaraStatusPackReelL3.FixZugara[0] == key)
                        {
                            // key ??? key
                            // --- ??? ---
                            // key ??? ---
                            srideIndex = FixSride_O_X_O(key, zugaraStatusPackReelL1);
                            if (srideIndex == -1) srideIndex = 0;
                        }
                        else if (zugaraStatusPackReelL3.FixZugara[1] == key)
                        {
                            // --- ??? ---
                            // key ??? key
                            // --- ??? ---
                            srideIndex = FixSride_X_O_X(key, zugaraStatusPackReelL1);
                            if (srideIndex == -1) srideIndex = 0;
                        }
                        else if (zugaraStatusPackReelL3.FixZugara[2] == key)
                        {
                            // key ??? ---
                            // --- ??? ---
                            // key ??? key
                            srideIndex = FixSride_O_X_O(key, zugaraStatusPackReelL1);
                            if (srideIndex == -1) srideIndex = 0;
                        }

                    }
                }
                else if (slotCore.oneGame.buttonCount == 3)
                {
                    if ((zugaraStatusPackReelL2.FixZugara[0] == key) && (zugaraStatusPackReelL3.FixZugara[0] == key))
                    {
                        // key key key
                        // --- --- ---
                        // --- --- ---
                        srideIndex = FixSride_O_X_X(key, zugaraStatusPackReelL1);
                        if (srideIndex == -1) srideIndex = 0;
                    }
                    else if ((zugaraStatusPackReelL2.FixZugara[1] == key) && (zugaraStatusPackReelL3.FixZugara[2] == key))
                    {
                        // key --- ---
                        // --- key ---
                        // --- --- key
                        srideIndex = FixSride_O_X_X(key, zugaraStatusPackReelL1);
                        if (srideIndex == -1) srideIndex = 0;
                    }
                    else if ((zugaraStatusPackReelL2.FixZugara[1] == key) && (zugaraStatusPackReelL3.FixZugara[1] == key))
                    {
                        // --- --- ---
                        // key key key
                        // --- --- ---
                        srideIndex = FixSride_X_O_X(key, zugaraStatusPackReelL1);
                        if (srideIndex == -1) srideIndex = 0;
                    }
                    else if ((zugaraStatusPackReelL2.FixZugara[1] == key) && (zugaraStatusPackReelL3.FixZugara[0] == key))
                    {
                        // --- --- key
                        // --- key ---
                        // key --- ---
                        srideIndex = FixSride_X_X_O(key, zugaraStatusPackReelL1);
                        if (srideIndex == -1) srideIndex = 0;
                    }
                    else if ((zugaraStatusPackReelL2.FixZugara[2] == key) && (zugaraStatusPackReelL3.FixZugara[2] == key))
                    {
                        // --- --- ---
                        // --- --- ---
                        // key key key
                        srideIndex = FixSride_X_X_O(key, zugaraStatusPackReelL1);
                        if (srideIndex == -1) srideIndex = 0;
                    }

                }
                break;
            case "suika":
                key = "suika";
                if (slotCore.oneGame.buttonCount == 1)
                {
                    srideIndex = FixSride_O_O_O(key, zugaraStatusPackReelL1);
                    if (srideIndex == -1) srideIndex = 0;
                }
                else if (slotCore.oneGame.buttonCount == 2)
                {
                    if (zugaraStatusPackReelL2.IsFix)
                    {
                        if (zugaraStatusPackReelL2.FixZugara[0] == key)
                        {
                            // key key ???
                            // --- --- ???
                            // --- --- ???
                            srideIndex = FixSride_O_X_X(key, zugaraStatusPackReelL1);
                            if (srideIndex == -1) srideIndex = 0;
                        }
                        else if (zugaraStatusPackReelL2.FixZugara[1] == key)
                        {
                            // key --- ???
                            // key key ???
                            // key --- ???
                            srideIndex = FixSride_O_O_O(key, zugaraStatusPackReelL1);
                            if (srideIndex == -1) srideIndex = 0;
                        }
                        else if (zugaraStatusPackReelL2.FixZugara[2] == key)
                        {
                            // --- --- ???
                            // --- --- ???
                            // key key ???
                            srideIndex = FixSride_X_X_O(key, zugaraStatusPackReelL1);
                            if (srideIndex == -1) srideIndex = 0;
                        }
                    }
                    else
                    {
                        if (zugaraStatusPackReelL3.FixZugara[0] == key)
                        {
                            // key ??? key
                            // --- ??? ---
                            // key ??? ---
                            srideIndex = FixSride_O_X_O(key, zugaraStatusPackReelL1);
                            if (srideIndex == -1) srideIndex = 0;
                        }
                        else if (zugaraStatusPackReelL3.FixZugara[1] == key)
                        {
                            // --- ??? ---
                            // key ??? key
                            // --- ??? ---
                            srideIndex = FixSride_X_O_X(key, zugaraStatusPackReelL1);
                            if (srideIndex == -1) srideIndex = 0;
                        }
                        else if (zugaraStatusPackReelL3.FixZugara[2] == key)
                        {
                            // key ??? ---
                            // --- ??? ---
                            // key ??? key
                            srideIndex = FixSride_O_X_O(key, zugaraStatusPackReelL1);
                            if (srideIndex == -1) srideIndex = 0;
                        }

                    }
                }
                else if (slotCore.oneGame.buttonCount == 3)
                {
                    if ((zugaraStatusPackReelL2.FixZugara[0] == key) && (zugaraStatusPackReelL3.FixZugara[0] == key))
                    {
                        // key key key
                        // --- --- ---
                        // --- --- ---
                        srideIndex = FixSride_O_X_X(key, zugaraStatusPackReelL1);
                        if (srideIndex == -1) srideIndex = 0;
                    }
                    else if ((zugaraStatusPackReelL2.FixZugara[1] == key) && (zugaraStatusPackReelL3.FixZugara[2] == key))
                    {
                        // key --- ---
                        // --- key ---
                        // --- --- key
                        srideIndex = FixSride_O_X_X(key, zugaraStatusPackReelL1);
                        if (srideIndex == -1) srideIndex = 0;
                    }
                    else if ((zugaraStatusPackReelL2.FixZugara[1] == key) && (zugaraStatusPackReelL3.FixZugara[1] == key))
                    {
                        // --- --- ---
                        // key key key
                        // --- --- ---
                        srideIndex = FixSride_X_O_X(key, zugaraStatusPackReelL1);
                        if (srideIndex == -1) srideIndex = 0;
                    }
                    else if ((zugaraStatusPackReelL2.FixZugara[1] == key) && (zugaraStatusPackReelL3.FixZugara[0] == key))
                    {
                        // --- --- key
                        // --- key ---
                        // key --- ---
                        srideIndex = FixSride_X_X_O(key, zugaraStatusPackReelL1);
                        if (srideIndex == -1) srideIndex = 0;
                    }
                    else if ((zugaraStatusPackReelL2.FixZugara[2] == key) && (zugaraStatusPackReelL3.FixZugara[2] == key))
                    {
                        // --- --- ---
                        // --- --- ---
                        // key key key
                        srideIndex = FixSride_X_X_O(key, zugaraStatusPackReelL1);
                        if (srideIndex == -1) srideIndex = 0;
                    }

                }
                break;
        }

        if (CheckBadRoleHit(0, srideIndex, zugaraStatusPackReelL1))
        {
            srideIndex = GetSrideBadRoleHit(0, zugaraStatusPackReelL1);
            Debug.Log($"GetSrideBadRoleHit L1 {srideIndex} ");
        }

        zugaraStatusPackReelL1.Fix(srideIndex);
    }

    private void StopRollOneL2()
    {
        if (zugaraStatusPackReelL2.IsFix) return;

        var srideIndex = 0;
        var role = slotCore.oneGame.flagRole;
        var bonus = slotCore.oneGame.flagBounus;
        var key = "";
        switch (role)
        {
            default:
                switch (bonus)
                {
                    case "Big-r7":
                        key = "r7";
                        if (slotCore.oneGame.buttonCount == 1)
                        {
                            srideIndex = FixSride_O_O_O(key, zugaraStatusPackReelL2);
                            if (srideIndex == -1) srideIndex = 0;
                        }
                        else if (slotCore.oneGame.buttonCount == 2)
                        {
                            if (zugaraStatusPackReelL1.IsFix)
                            {
                                if (zugaraStatusPackReelL1.FixZugara[0] == key)
                                {
                                    // key key ???
                                    // --- key ???
                                    // --- --- ???
                                    srideIndex = FixSride_O_O_X(key, zugaraStatusPackReelL2);
                                    if (srideIndex == -1) srideIndex = 0;

                                }
                                else if (zugaraStatusPackReelL1.FixZugara[1] == key)
                                {
                                    // --- --- ???
                                    // key key ???
                                    // --- --- ???
                                    srideIndex = FixSride_X_O_X(key, zugaraStatusPackReelL2);
                                    if (srideIndex == -1) srideIndex = 0;
                                }
                                else if (zugaraStatusPackReelL1.FixZugara[2] == key)
                                {
                                    // --- --- ???
                                    // --- key ???
                                    // key key ???
                                    srideIndex = FixSride_X_O_O(key, zugaraStatusPackReelL2);
                                    if (srideIndex == -1) srideIndex = 0;
                                }
                            }
                            else
                            {
                                if (zugaraStatusPackReelL3.FixZugara[0] == key)
                                {
                                    // ??? key key
                                    // ??? key ---
                                    // ??? --- ---
                                    srideIndex = FixSride_O_O_X(key, zugaraStatusPackReelL2);
                                    if (srideIndex == -1) srideIndex = 0;

                                }
                                else if (zugaraStatusPackReelL3.FixZugara[1] == key)
                                {
                                    // ??? --- ---
                                    // ??? key key
                                    // ??? --- ---
                                    srideIndex = FixSride_X_O_X(key, zugaraStatusPackReelL2);
                                    if (srideIndex == -1) srideIndex = 0;
                                }
                                else if (zugaraStatusPackReelL3.FixZugara[2] == key)
                                {
                                    // ??? --- ---
                                    // ??? key ---
                                    // ??? key key
                                    srideIndex = FixSride_X_O_O(key, zugaraStatusPackReelL2);
                                    if (srideIndex == -1) srideIndex = 0;
                                }
                            }
                        }
                        else if (slotCore.oneGame.buttonCount == 3)
                        {
                            if ((zugaraStatusPackReelL1.FixZugara[0] == key) && (zugaraStatusPackReelL3.FixZugara[0] == key))
                            {
                                // key key key
                                // --- --- ---
                                // --- --- ---
                                srideIndex = FixSride_O_X_X(key, zugaraStatusPackReelL2);
                                if (srideIndex == -1) srideIndex = 0;
                            }
                            else if ((zugaraStatusPackReelL1.FixZugara[0] == key) && (zugaraStatusPackReelL3.FixZugara[2] == key))
                            {
                                // key --- ---
                                // --- key ---
                                // --- --- key
                                srideIndex = FixSride_X_O_X(key, zugaraStatusPackReelL2);
                                if (srideIndex == -1) srideIndex = 0;
                            }
                            else if ((zugaraStatusPackReelL1.FixZugara[1] == key) && (zugaraStatusPackReelL3.FixZugara[1] == key))
                            {
                                // --- --- ---
                                // key key key
                                // --- --- ---
                                srideIndex = FixSride_X_O_X(key, zugaraStatusPackReelL2);
                                if (srideIndex == -1) srideIndex = 0;
                            }
                            else if ((zugaraStatusPackReelL1.FixZugara[2] == key) && (zugaraStatusPackReelL3.FixZugara[0] == key))
                            {
                                // --- --- key
                                // --- key ---
                                // key --- ---
                                srideIndex = FixSride_X_O_X(key, zugaraStatusPackReelL2);
                                if (srideIndex == -1) srideIndex = 0;
                            }
                            else if ((zugaraStatusPackReelL1.FixZugara[2] == key) && (zugaraStatusPackReelL3.FixZugara[2] == key))
                            {
                                // --- --- ---
                                // --- --- ---
                                // key key key
                                srideIndex = FixSride_X_X_O(key, zugaraStatusPackReelL2);
                                if (srideIndex == -1) srideIndex = 0;
                            }

                        }
                        break;
                    case "Big-b7":
                        key = "b7";
                        if (slotCore.oneGame.buttonCount == 1)
                        {
                            srideIndex = FixSride_O_O_O(key, zugaraStatusPackReelL2);
                            if (srideIndex == -1) srideIndex = 0;
                        }
                        else if (slotCore.oneGame.buttonCount == 2)
                        {
                            if (zugaraStatusPackReelL1.IsFix)
                            {
                                if (zugaraStatusPackReelL1.FixZugara[0] == key)
                                {
                                    // key key ???
                                    // --- key ???
                                    // --- --- ???
                                    srideIndex = FixSride_O_O_X(key, zugaraStatusPackReelL2);
                                    if (srideIndex == -1) srideIndex = 0;

                                }
                                else if (zugaraStatusPackReelL1.FixZugara[1] == key)
                                {
                                    // --- --- ???
                                    // key key ???
                                    // --- --- ???
                                    srideIndex = FixSride_X_O_X(key, zugaraStatusPackReelL2);
                                    if (srideIndex == -1) srideIndex = 0;
                                }
                                else if (zugaraStatusPackReelL1.FixZugara[2] == key)
                                {
                                    // --- --- ???
                                    // --- key ???
                                    // key key ???
                                    srideIndex = FixSride_X_O_O(key, zugaraStatusPackReelL2);
                                    if (srideIndex == -1) srideIndex = 0;
                                }
                            }
                            else
                            {
                                if (zugaraStatusPackReelL3.FixZugara[0] == key)
                                {
                                    // ??? key key
                                    // ??? key ---
                                    // ??? --- ---
                                    srideIndex = FixSride_O_O_X(key, zugaraStatusPackReelL2);
                                    if (srideIndex == -1) srideIndex = 0;

                                }
                                else if (zugaraStatusPackReelL3.FixZugara[1] == key)
                                {
                                    // ??? --- ---
                                    // ??? key key
                                    // ??? --- ---
                                    srideIndex = FixSride_X_O_X(key, zugaraStatusPackReelL2);
                                    if (srideIndex == -1) srideIndex = 0;
                                }
                                else if (zugaraStatusPackReelL3.FixZugara[2] == key)
                                {
                                    // ??? --- ---
                                    // ??? key ---
                                    // ??? key key
                                    srideIndex = FixSride_X_O_O(key, zugaraStatusPackReelL2);
                                    if (srideIndex == -1) srideIndex = 0;
                                }
                            }
                        }
                        else if (slotCore.oneGame.buttonCount == 3)
                        {
                            if ((zugaraStatusPackReelL1.FixZugara[0] == key) && (zugaraStatusPackReelL3.FixZugara[0] == key))
                            {
                                // key key key
                                // --- --- ---
                                // --- --- ---
                                srideIndex = FixSride_O_X_X(key, zugaraStatusPackReelL2);
                                if (srideIndex == -1) srideIndex = 0;
                            }
                            else if ((zugaraStatusPackReelL1.FixZugara[0] == key) && (zugaraStatusPackReelL3.FixZugara[2] == key))
                            {
                                // key --- ---
                                // --- key ---
                                // --- --- key
                                srideIndex = FixSride_X_O_X(key, zugaraStatusPackReelL2);
                                if (srideIndex == -1) srideIndex = 0;
                            }
                            else if ((zugaraStatusPackReelL1.FixZugara[1] == key) && (zugaraStatusPackReelL3.FixZugara[1] == key))
                            {
                                // --- --- ---
                                // key key key
                                // --- --- ---
                                srideIndex = FixSride_X_O_X(key, zugaraStatusPackReelL2);
                                if (srideIndex == -1) srideIndex = 0;
                            }
                            else if ((zugaraStatusPackReelL1.FixZugara[2] == key) && (zugaraStatusPackReelL3.FixZugara[0] == key))
                            {
                                // --- --- key
                                // --- key ---
                                // key --- ---
                                srideIndex = FixSride_X_O_X(key, zugaraStatusPackReelL2);
                                if (srideIndex == -1) srideIndex = 0;
                            }
                            else if ((zugaraStatusPackReelL1.FixZugara[2] == key) && (zugaraStatusPackReelL3.FixZugara[2] == key))
                            {
                                // --- --- ---
                                // --- --- ---
                                // key key key
                                srideIndex = FixSride_X_X_O(key, zugaraStatusPackReelL2);
                                if (srideIndex == -1) srideIndex = 0;
                            }

                        }
                        break;
                    case "Reg":
                        key = "r7";
                        if (slotCore.oneGame.buttonCount == 1)
                        {
                            srideIndex = FixSride_O_O_O(key, zugaraStatusPackReelL2);
                            if (srideIndex == -1) srideIndex = 0;
                        }
                        else if (slotCore.oneGame.buttonCount == 2)
                        {
                            if (zugaraStatusPackReelL1.IsFix)
                            {
                                if (zugaraStatusPackReelL1.FixZugara[0] == key)
                                {
                                    // key key ???
                                    // --- key ???
                                    // --- --- ???
                                    srideIndex = FixSride_O_O_X(key, zugaraStatusPackReelL2);
                                    if (srideIndex == -1) srideIndex = 0;

                                }
                                else if (zugaraStatusPackReelL1.FixZugara[1] == key)
                                {
                                    // --- --- ???
                                    // key key ???
                                    // --- --- ???
                                    srideIndex = FixSride_X_O_X(key, zugaraStatusPackReelL2);
                                    if (srideIndex == -1) srideIndex = 0;
                                }
                                else if (zugaraStatusPackReelL1.FixZugara[2] == key)
                                {
                                    // --- --- ???
                                    // --- key ???
                                    // key key ???
                                    srideIndex = FixSride_X_O_O(key, zugaraStatusPackReelL2);
                                    if (srideIndex == -1) srideIndex = 0;
                                }
                            }
                            else
                            {
                                if (zugaraStatusPackReelL3.FixZugara[0] == "bar")
                                {
                                    // ??? key key
                                    // ??? key ---
                                    // ??? --- ---
                                    srideIndex = FixSride_O_O_X(key, zugaraStatusPackReelL2);
                                    if (srideIndex == -1) srideIndex = 0;

                                }
                                else if (zugaraStatusPackReelL3.FixZugara[1] == "bar")
                                {
                                    // ??? --- ---
                                    // ??? key key
                                    // ??? --- ---
                                    srideIndex = FixSride_X_O_X(key, zugaraStatusPackReelL2);
                                    if (srideIndex == -1) srideIndex = 0;
                                }
                                else if (zugaraStatusPackReelL3.FixZugara[2] == "bar")
                                {
                                    // ??? --- ---
                                    // ??? key ---
                                    // ??? key key
                                    srideIndex = FixSride_X_O_O(key, zugaraStatusPackReelL2);
                                    if (srideIndex == -1) srideIndex = 0;
                                }
                            }
                        }
                        else if (slotCore.oneGame.buttonCount == 3)
                        {
                            if ((zugaraStatusPackReelL1.FixZugara[0] == key) && (zugaraStatusPackReelL3.FixZugara[0] == "bar"))
                            {
                                // key key key
                                // --- --- ---
                                // --- --- ---
                                srideIndex = FixSride_O_X_X(key, zugaraStatusPackReelL2);
                                if (srideIndex == -1) srideIndex = 0;
                            }
                            else if ((zugaraStatusPackReelL1.FixZugara[0] == key) && (zugaraStatusPackReelL3.FixZugara[2] == "bar"))
                            {
                                // key --- ---
                                // --- key ---
                                // --- --- key
                                srideIndex = FixSride_X_O_X(key, zugaraStatusPackReelL2);
                                if (srideIndex == -1) srideIndex = 0;
                            }
                            else if ((zugaraStatusPackReelL1.FixZugara[1] == key) && (zugaraStatusPackReelL3.FixZugara[1] == "bar"))
                            {
                                // --- --- ---
                                // key key key
                                // --- --- ---
                                srideIndex = FixSride_X_O_X(key, zugaraStatusPackReelL2);
                                if (srideIndex == -1) srideIndex = 0;
                            }
                            else if ((zugaraStatusPackReelL1.FixZugara[2] == key) && (zugaraStatusPackReelL3.FixZugara[0] == "bar"))
                            {
                                // --- --- key
                                // --- key ---
                                // key --- ---
                                srideIndex = FixSride_X_O_X(key, zugaraStatusPackReelL2);
                                if (srideIndex == -1) srideIndex = 0;
                            }
                            else if ((zugaraStatusPackReelL1.FixZugara[2] == key) && (zugaraStatusPackReelL3.FixZugara[2] == "bar"))
                            {
                                // --- --- ---
                                // --- --- ---
                                // key key key
                                srideIndex = FixSride_X_X_O(key, zugaraStatusPackReelL2);
                                if (srideIndex == -1) srideIndex = 0;
                            }

                        }
                        break;
                }
                break;
            case "rep":
                key = "rep";
                if (slotCore.oneGame.buttonCount == 1)
                {
                    srideIndex = FixSride_O_O_O(key, zugaraStatusPackReelL2);
                    if (srideIndex == -1) srideIndex = 0;
                }
                else if (slotCore.oneGame.buttonCount == 2)
                {
                    if (zugaraStatusPackReelL1.IsFix)
                    {
                        if (zugaraStatusPackReelL1.FixZugara[0] == key)
                        {
                            // key key ???
                            // --- key ???
                            // --- --- ???
                            srideIndex = FixSride_O_O_X(key, zugaraStatusPackReelL2);
                            if (srideIndex == -1) srideIndex = 0;

                        }
                        else if (zugaraStatusPackReelL1.FixZugara[1] == key)
                        {
                            // --- --- ???
                            // key key ???
                            // --- --- ???
                            srideIndex = FixSride_X_O_X(key, zugaraStatusPackReelL2);
                            if (srideIndex == -1) srideIndex = 0;
                        }
                        else if (zugaraStatusPackReelL1.FixZugara[2] == key)
                        {
                            // --- --- ???
                            // --- key ???
                            // key key ???
                            srideIndex = FixSride_X_O_O(key, zugaraStatusPackReelL2);
                            if (srideIndex == -1) srideIndex = 0;
                        }
                    }
                    else
                    {
                        if (zugaraStatusPackReelL3.FixZugara[0] == key)
                        {
                            // ??? key key
                            // ??? key ---
                            // ??? --- ---
                            srideIndex = FixSride_O_O_X(key, zugaraStatusPackReelL2);
                            if (srideIndex == -1) srideIndex = 0;

                        }
                        else if (zugaraStatusPackReelL3.FixZugara[1] == key)
                        {
                            // ??? --- ---
                            // ??? key key
                            // ??? --- ---
                            srideIndex = FixSride_X_O_X(key, zugaraStatusPackReelL2);
                            if (srideIndex == -1) srideIndex = 0;
                        }
                        else if (zugaraStatusPackReelL3.FixZugara[2] == key)
                        {
                            // ??? --- ---
                            // ??? key ---
                            // ??? key key
                            srideIndex = FixSride_X_O_O(key, zugaraStatusPackReelL2);
                            if (srideIndex == -1) srideIndex = 0;
                        }
                    }
                }
                else if (slotCore.oneGame.buttonCount == 3)
                {
                    if ((zugaraStatusPackReelL1.FixZugara[0] == key) && (zugaraStatusPackReelL3.FixZugara[0] == key))
                    {
                        // key key key
                        // --- --- ---
                        // --- --- ---
                        srideIndex = FixSride_O_X_X(key, zugaraStatusPackReelL2);
                        if (srideIndex == -1) srideIndex = 0;
                    }
                    else if ((zugaraStatusPackReelL1.FixZugara[0] == key) && (zugaraStatusPackReelL3.FixZugara[2] == key))
                    {
                        // key --- ---
                        // --- key ---
                        // --- --- key
                        srideIndex = FixSride_X_O_X(key, zugaraStatusPackReelL2);
                        if (srideIndex == -1) srideIndex = 0;
                    }
                    else if ((zugaraStatusPackReelL1.FixZugara[1] == key) && (zugaraStatusPackReelL3.FixZugara[1] == key))
                    {
                        // --- --- ---
                        // key key key
                        // --- --- ---
                        srideIndex = FixSride_X_O_X(key, zugaraStatusPackReelL2);
                        if (srideIndex == -1) srideIndex = 0;
                    }
                    else if ((zugaraStatusPackReelL1.FixZugara[2] == key) && (zugaraStatusPackReelL3.FixZugara[0] == key))
                    {
                        // --- --- key
                        // --- key ---
                        // key --- ---
                        srideIndex = FixSride_X_O_X(key, zugaraStatusPackReelL2);
                        if (srideIndex == -1) srideIndex = 0;
                    }
                    else if ((zugaraStatusPackReelL1.FixZugara[2] == key) && (zugaraStatusPackReelL3.FixZugara[2] == key))
                    {
                        // --- --- ---
                        // --- --- ---
                        // key key key
                        srideIndex = FixSride_X_X_O(key, zugaraStatusPackReelL2);
                        if (srideIndex == -1) srideIndex = 0;
                    }

                }
                break;
            case "bell":
                key = "bell";
                if (slotCore.oneGame.buttonCount == 1)
                {
                    srideIndex = FixSride_O_O_O(key, zugaraStatusPackReelL2);
                    if (srideIndex == -1) srideIndex = 0;
                }
                else if (slotCore.oneGame.buttonCount == 2)
                {
                    if (zugaraStatusPackReelL1.IsFix)
                    {
                        if (zugaraStatusPackReelL1.FixZugara[0] == key)
                        {
                            // key key ???
                            // --- key ???
                            // --- --- ???
                            srideIndex = FixSride_O_O_X(key, zugaraStatusPackReelL2);
                            if (srideIndex == -1) srideIndex = 0;

                        }
                        else if (zugaraStatusPackReelL1.FixZugara[1] == key)
                        {
                            // --- --- ???
                            // key key ???
                            // --- --- ???
                            srideIndex = FixSride_X_O_X(key, zugaraStatusPackReelL2);
                            if (srideIndex == -1) srideIndex = 0;
                        }
                        else if (zugaraStatusPackReelL1.FixZugara[2] == key)
                        {
                            // --- --- ???
                            // --- key ???
                            // key key ???
                            srideIndex = FixSride_X_O_O(key, zugaraStatusPackReelL2);
                            if (srideIndex == -1) srideIndex = 0;
                        }
                    }
                    else
                    {
                        if (zugaraStatusPackReelL3.FixZugara[0] == key)
                        {
                            // ??? key key
                            // ??? key ---
                            // ??? --- ---
                            srideIndex = FixSride_O_O_X(key, zugaraStatusPackReelL2);
                            if (srideIndex == -1) srideIndex = 0;

                        }
                        else if (zugaraStatusPackReelL3.FixZugara[1] == key)
                        {
                            // ??? --- ---
                            // ??? key key
                            // ??? --- ---
                            srideIndex = FixSride_X_O_X(key, zugaraStatusPackReelL2);
                            if (srideIndex == -1) srideIndex = 0;
                        }
                        else if (zugaraStatusPackReelL3.FixZugara[2] == key)
                        {
                            // ??? --- ---
                            // ??? key ---
                            // ??? key key
                            srideIndex = FixSride_X_O_O(key, zugaraStatusPackReelL2);
                            if (srideIndex == -1) srideIndex = 0;
                        }
                    }
                }
                else if (slotCore.oneGame.buttonCount == 3)
                {
                    if ((zugaraStatusPackReelL1.FixZugara[0] == key) && (zugaraStatusPackReelL3.FixZugara[0] == key))
                    {
                        // key key key
                        // --- --- ---
                        // --- --- ---
                        srideIndex = FixSride_O_X_X(key, zugaraStatusPackReelL2);
                        if (srideIndex == -1) srideIndex = 0;
                    }
                    else if ((zugaraStatusPackReelL1.FixZugara[0] == key) && (zugaraStatusPackReelL3.FixZugara[2] == key))
                    {
                        // key --- ---
                        // --- key ---
                        // --- --- key
                        srideIndex = FixSride_X_O_X(key, zugaraStatusPackReelL2);
                        if (srideIndex == -1) srideIndex = 0;
                    }
                    else if ((zugaraStatusPackReelL1.FixZugara[1] == key) && (zugaraStatusPackReelL3.FixZugara[1] == key))
                    {
                        // --- --- ---
                        // key key key
                        // --- --- ---
                        srideIndex = FixSride_X_O_X(key, zugaraStatusPackReelL2);
                        if (srideIndex == -1) srideIndex = 0;
                    }
                    else if ((zugaraStatusPackReelL1.FixZugara[2] == key) && (zugaraStatusPackReelL3.FixZugara[0] == key))
                    {
                        // --- --- key
                        // --- key ---
                        // key --- ---
                        srideIndex = FixSride_X_O_X(key, zugaraStatusPackReelL2);
                        if (srideIndex == -1) srideIndex = 0;
                    }
                    else if ((zugaraStatusPackReelL1.FixZugara[2] == key) && (zugaraStatusPackReelL3.FixZugara[2] == key))
                    {
                        // --- --- ---
                        // --- --- ---
                        // key key key
                        srideIndex = FixSride_X_X_O(key, zugaraStatusPackReelL2);
                        if (srideIndex == -1) srideIndex = 0;
                    }

                }
                break;
            case "suika":
                key = "suika";
                if (slotCore.oneGame.buttonCount == 1)
                {
                    srideIndex = FixSride_O_O_O(key, zugaraStatusPackReelL2);
                    if (srideIndex == -1) srideIndex = 0;
                }
                else if (slotCore.oneGame.buttonCount == 2)
                {
                    if (zugaraStatusPackReelL1.IsFix)
                    {
                        if (zugaraStatusPackReelL1.FixZugara[0] == key)
                        {
                            // key key ???
                            // --- key ???
                            // --- --- ???
                            srideIndex = FixSride_O_O_X(key, zugaraStatusPackReelL2);
                            if (srideIndex == -1) srideIndex = 0;

                        }
                        else if (zugaraStatusPackReelL1.FixZugara[1] == key)
                        {
                            // --- --- ???
                            // key key ???
                            // --- --- ???
                            srideIndex = FixSride_X_O_X(key, zugaraStatusPackReelL2);
                            if (srideIndex == -1) srideIndex = 0;
                        }
                        else if (zugaraStatusPackReelL1.FixZugara[2] == key)
                        {
                            // --- --- ???
                            // --- key ???
                            // key key ???
                            srideIndex = FixSride_X_O_O(key, zugaraStatusPackReelL2);
                            if (srideIndex == -1) srideIndex = 0;
                        }
                    }
                    else
                    {
                        if (zugaraStatusPackReelL3.FixZugara[0] == key)
                        {
                            // ??? key key
                            // ??? key ---
                            // ??? --- ---
                            srideIndex = FixSride_O_O_X(key, zugaraStatusPackReelL2);
                            if (srideIndex == -1) srideIndex = 0;

                        }
                        else if (zugaraStatusPackReelL3.FixZugara[1] == key)
                        {
                            // ??? --- ---
                            // ??? key key
                            // ??? --- ---
                            srideIndex = FixSride_X_O_X(key, zugaraStatusPackReelL2);
                            if (srideIndex == -1) srideIndex = 0;
                        }
                        else if (zugaraStatusPackReelL3.FixZugara[2] == key)
                        {
                            // ??? --- ---
                            // ??? key ---
                            // ??? key key
                            srideIndex = FixSride_X_O_O(key, zugaraStatusPackReelL2);
                            if (srideIndex == -1) srideIndex = 0;
                        }
                    }
                }
                else if (slotCore.oneGame.buttonCount == 3)
                {
                    if ((zugaraStatusPackReelL1.FixZugara[0] == key) && (zugaraStatusPackReelL3.FixZugara[0] == key))
                    {
                        // key key key
                        // --- --- ---
                        // --- --- ---
                        srideIndex = FixSride_O_X_X(key, zugaraStatusPackReelL2);
                        if (srideIndex == -1) srideIndex = 0;
                    }
                    else if ((zugaraStatusPackReelL1.FixZugara[0] == key) && (zugaraStatusPackReelL3.FixZugara[2] == key))
                    {
                        // key --- ---
                        // --- key ---
                        // --- --- key
                        srideIndex = FixSride_X_O_X(key, zugaraStatusPackReelL2);
                        if (srideIndex == -1) srideIndex = 0;
                    }
                    else if ((zugaraStatusPackReelL1.FixZugara[1] == key) && (zugaraStatusPackReelL3.FixZugara[1] == key))
                    {
                        // --- --- ---
                        // key key key
                        // --- --- ---
                        srideIndex = FixSride_X_O_X(key, zugaraStatusPackReelL2);
                        if (srideIndex == -1) srideIndex = 0;
                    }
                    else if ((zugaraStatusPackReelL1.FixZugara[2] == key) && (zugaraStatusPackReelL3.FixZugara[0] == key))
                    {
                        // --- --- key
                        // --- key ---
                        // key --- ---
                        srideIndex = FixSride_X_O_X(key, zugaraStatusPackReelL2);
                        if (srideIndex == -1) srideIndex = 0;
                    }
                    else if ((zugaraStatusPackReelL1.FixZugara[2] == key) && (zugaraStatusPackReelL3.FixZugara[2] == key))
                    {
                        // --- --- ---
                        // --- --- ---
                        // key key key
                        srideIndex = FixSride_X_X_O(key, zugaraStatusPackReelL2);
                        if (srideIndex == -1) srideIndex = 0;
                    }

                }
                break;
        }

        if (CheckBadRoleHit(1, srideIndex, zugaraStatusPackReelL2))
        {
            srideIndex = GetSrideBadRoleHit(1, zugaraStatusPackReelL2);
            Debug.Log($"GetSrideBadRoleHit L2 {srideIndex} ");
        }

        zugaraStatusPackReelL2.Fix(srideIndex);
    }

    private void StopRollOneL3()
    {
        if (zugaraStatusPackReelL3.IsFix) return;

        var role = slotCore.oneGame.flagRole;
        var bonus = slotCore.oneGame.flagBounus;
        var srideIndex = 0;
        var key = "";
        switch (role)
        {
            default:
                switch (bonus)
                {
                    case "Big-r7":
                        key = "r7";
                        if (slotCore.oneGame.buttonCount == 1)
                        {
                            srideIndex = FixSride_O_O_O(key, zugaraStatusPackReelL3);
                            if (srideIndex == -1) srideIndex = 0;
                        }
                        else if (slotCore.oneGame.buttonCount == 2)
                        {
                            if (zugaraStatusPackReelL2.IsFix)
                            {
                                if (zugaraStatusPackReelL2.FixZugara[0] == key)
                                {
                                    // ??? key key
                                    // ??? --- ---
                                    // ??? --- ---
                                    srideIndex = FixSride_O_X_X(key, zugaraStatusPackReelL3);
                                    if (srideIndex == -1) srideIndex = 0;
                                }
                                else if (zugaraStatusPackReelL2.FixZugara[1] == key)
                                {
                                    // ??? --- key
                                    // ??? key key
                                    // ??? --- key
                                    srideIndex = FixSride_O_O_O(key, zugaraStatusPackReelL3);
                                    if (srideIndex == -1) srideIndex = 0;
                                }
                                else if (zugaraStatusPackReelL2.FixZugara[2] == key)
                                {
                                    // ??? --- ---
                                    // ??? --- ---
                                    // ??? key key
                                    srideIndex = FixSride_X_X_O(key, zugaraStatusPackReelL3);
                                    if (srideIndex == -1) srideIndex = 0;
                                }
                            }
                            else
                            {
                                if (zugaraStatusPackReelL1.FixZugara[0] == key)
                                {
                                    // key ??? key
                                    // --- ??? ---
                                    // --- ??? key
                                    srideIndex = FixSride_O_X_O(key, zugaraStatusPackReelL3);
                                    if (srideIndex == -1) srideIndex = 0;
                                }
                                else if (zugaraStatusPackReelL1.FixZugara[1] == key)
                                {
                                    // --- ??? ---
                                    // key ??? key
                                    // --- ??? ---
                                    srideIndex = FixSride_X_O_X(key, zugaraStatusPackReelL3);
                                    if (srideIndex == -1) srideIndex = 0;
                                }
                                else if (zugaraStatusPackReelL2.FixZugara[2] == key)
                                {
                                    // --- ??? key
                                    // --- ??? ---
                                    // key ??? key
                                    srideIndex = FixSride_O_X_O(key, zugaraStatusPackReelL3);
                                    if (srideIndex == -1) srideIndex = 0;
                                }

                            }
                        }
                        else if (slotCore.oneGame.buttonCount == 3)
                        {
                            if ((zugaraStatusPackReelL1.FixZugara[0] == key) && (zugaraStatusPackReelL2.FixZugara[0] == key))
                            {
                                // key key key
                                // --- --- ---
                                // --- --- ---
                                srideIndex = FixSride_O_X_X(key, zugaraStatusPackReelL3);
                                if (srideIndex == -1) srideIndex = 0;
                            }
                            else if ((zugaraStatusPackReelL1.FixZugara[0] == key) && (zugaraStatusPackReelL2.FixZugara[1] == key))
                            {
                                // key --- ---
                                // --- key ---
                                // --- --- key
                                srideIndex = FixSride_X_X_O(key, zugaraStatusPackReelL3); // todo : 不具合ありそう、赤7がそろわないことがあった、はじいてるかも
                                if (srideIndex == -1) srideIndex = 0;
                            }
                            else if ((zugaraStatusPackReelL1.FixZugara[1] == key) && (zugaraStatusPackReelL2.FixZugara[1] == key))
                            {
                                // --- --- ---
                                // key key key
                                // --- --- ---
                                srideIndex = FixSride_X_O_X(key, zugaraStatusPackReelL3);
                                if (srideIndex == -1) srideIndex = 0;
                            }
                            else if ((zugaraStatusPackReelL1.FixZugara[2] == key) && (zugaraStatusPackReelL2.FixZugara[1] == key))
                            {
                                // --- --- key
                                // --- key ---
                                // key --- ---
                                srideIndex = FixSride_O_X_X(key, zugaraStatusPackReelL3);
                                if (srideIndex == -1) srideIndex = 0;
                            }
                            else if ((zugaraStatusPackReelL1.FixZugara[2] == key) && (zugaraStatusPackReelL2.FixZugara[2] == key))
                            {
                                // --- --- ---
                                // --- --- ---
                                // key key key
                                srideIndex = FixSride_X_X_O(key, zugaraStatusPackReelL3);
                                if (srideIndex == -1) srideIndex = 0;
                            }
                        }
                        break;
                    case "Big-b7":
                        key = "b7";
                        if (slotCore.oneGame.buttonCount == 1)
                        {
                            srideIndex = FixSride_O_O_O(key, zugaraStatusPackReelL3);
                            if (srideIndex == -1) srideIndex = 0;
                        }
                        else if (slotCore.oneGame.buttonCount == 2)
                        {
                            if (zugaraStatusPackReelL2.IsFix)
                            {
                                if (zugaraStatusPackReelL2.FixZugara[0] == key)
                                {
                                    // ??? key key
                                    // ??? --- ---
                                    // ??? --- ---
                                    srideIndex = FixSride_O_X_X(key, zugaraStatusPackReelL3);
                                    if (srideIndex == -1) srideIndex = 0;
                                }
                                else if (zugaraStatusPackReelL2.FixZugara[1] == key)
                                {
                                    // ??? --- key
                                    // ??? key key
                                    // ??? --- key
                                    srideIndex = FixSride_O_O_O(key, zugaraStatusPackReelL3);
                                    if (srideIndex == -1) srideIndex = 0;
                                }
                                else if (zugaraStatusPackReelL2.FixZugara[2] == key)
                                {
                                    // ??? --- ---
                                    // ??? --- ---
                                    // ??? key key
                                    srideIndex = FixSride_X_X_O(key, zugaraStatusPackReelL3);
                                    if (srideIndex == -1) srideIndex = 0;
                                }
                            }
                            else
                            {
                                if (zugaraStatusPackReelL1.FixZugara[0] == key)
                                {
                                    // key ??? key
                                    // --- ??? ---
                                    // --- ??? key
                                    srideIndex = FixSride_O_X_O(key, zugaraStatusPackReelL3);
                                    if (srideIndex == -1) srideIndex = 0;
                                }
                                else if (zugaraStatusPackReelL1.FixZugara[1] == key)
                                {
                                    // --- ??? ---
                                    // key ??? key
                                    // --- ??? ---
                                    srideIndex = FixSride_X_O_X(key, zugaraStatusPackReelL3);
                                    if (srideIndex == -1) srideIndex = 0;
                                }
                                else if (zugaraStatusPackReelL2.FixZugara[2] == key)
                                {
                                    // --- ??? key
                                    // --- ??? ---
                                    // key ??? key
                                    srideIndex = FixSride_O_X_O(key, zugaraStatusPackReelL3);
                                    if (srideIndex == -1) srideIndex = 0;
                                }

                            }
                        }
                        else if (slotCore.oneGame.buttonCount == 3)
                        {
                            if ((zugaraStatusPackReelL1.FixZugara[0] == key) && (zugaraStatusPackReelL2.FixZugara[0] == key))
                            {
                                // key key key
                                // --- --- ---
                                // --- --- ---
                                srideIndex = FixSride_O_X_X(key, zugaraStatusPackReelL3);
                                if (srideIndex == -1) srideIndex = 0;
                            }
                            else if ((zugaraStatusPackReelL1.FixZugara[0] == key) && (zugaraStatusPackReelL2.FixZugara[1] == key))
                            {
                                // key --- ---
                                // --- key ---
                                // --- --- key
                                srideIndex = FixSride_X_X_O(key, zugaraStatusPackReelL3);
                                if (srideIndex == -1) srideIndex = 0;
                            }
                            else if ((zugaraStatusPackReelL1.FixZugara[1] == key) && (zugaraStatusPackReelL2.FixZugara[1] == key))
                            {
                                // --- --- ---
                                // key key key
                                // --- --- ---
                                srideIndex = FixSride_X_O_X(key, zugaraStatusPackReelL3);
                                if (srideIndex == -1) srideIndex = 0;
                            }
                            else if ((zugaraStatusPackReelL1.FixZugara[2] == key) && (zugaraStatusPackReelL2.FixZugara[1] == key))
                            {
                                // --- --- key
                                // --- key ---
                                // key --- ---
                                srideIndex = FixSride_O_X_X(key, zugaraStatusPackReelL3);
                                if (srideIndex == -1) srideIndex = 0;
                            }
                            else if ((zugaraStatusPackReelL1.FixZugara[2] == key) && (zugaraStatusPackReelL2.FixZugara[2] == key))
                            {
                                // --- --- ---
                                // --- --- ---
                                // key key key
                                srideIndex = FixSride_X_X_O(key, zugaraStatusPackReelL3);
                                if (srideIndex == -1) srideIndex = 0;
                            }
                        }
                        break;
                    case "Reg":
                        key = "bar";
                        if (slotCore.oneGame.buttonCount == 1)
                        {
                            srideIndex = FixSride_O_O_O(key, zugaraStatusPackReelL3);
                            if (srideIndex == -1) srideIndex = 0;
                        }
                        else if (slotCore.oneGame.buttonCount == 2)
                        {
                            if (zugaraStatusPackReelL2.IsFix)
                            {
                                if (zugaraStatusPackReelL2.FixZugara[0] == "r7")
                                {
                                    // ??? key key
                                    // ??? --- ---
                                    // ??? --- ---
                                    srideIndex = FixSride_O_X_X(key, zugaraStatusPackReelL3);
                                    if (srideIndex == -1) srideIndex = 0;
                                }
                                else if (zugaraStatusPackReelL2.FixZugara[1] == "r7")
                                {
                                    // ??? --- key
                                    // ??? key key
                                    // ??? --- key
                                    srideIndex = FixSride_O_O_O(key, zugaraStatusPackReelL3);
                                    if (srideIndex == -1) srideIndex = 0;
                                }
                                else if (zugaraStatusPackReelL2.FixZugara[2] == "r7")
                                {
                                    // ??? --- ---
                                    // ??? --- ---
                                    // ??? key key
                                    srideIndex = FixSride_X_X_O(key, zugaraStatusPackReelL3);
                                    if (srideIndex == -1) srideIndex = 0;
                                }
                            }
                            else
                            {
                                if (zugaraStatusPackReelL1.FixZugara[0] == "r7")
                                {
                                    // key ??? key
                                    // --- ??? ---
                                    // --- ??? key
                                    srideIndex = FixSride_O_X_O(key, zugaraStatusPackReelL3);
                                    if (srideIndex == -1) srideIndex = 0;
                                }
                                else if (zugaraStatusPackReelL1.FixZugara[1] == "r7")
                                {
                                    // --- ??? ---
                                    // key ??? key
                                    // --- ??? ---
                                    srideIndex = FixSride_X_O_X(key, zugaraStatusPackReelL3);
                                    if (srideIndex == -1) srideIndex = 0;
                                }
                                else if (zugaraStatusPackReelL2.FixZugara[2] == "r7")
                                {
                                    // --- ??? key
                                    // --- ??? ---
                                    // key ??? key
                                    srideIndex = FixSride_O_X_O(key, zugaraStatusPackReelL3);
                                    if (srideIndex == -1) srideIndex = 0;
                                }

                            }
                        }
                        else if (slotCore.oneGame.buttonCount == 3)
                        {
                            if ((zugaraStatusPackReelL1.FixZugara[0] == "r7") && (zugaraStatusPackReelL2.FixZugara[0] == "r7"))
                            {
                                // key key key
                                // --- --- ---
                                // --- --- ---
                                srideIndex = FixSride_O_X_X(key, zugaraStatusPackReelL3);
                                if (srideIndex == -1) srideIndex = 0;
                            }
                            else if ((zugaraStatusPackReelL1.FixZugara[0] == "r7") && (zugaraStatusPackReelL2.FixZugara[1] == "r7"))
                            {
                                // key --- ---
                                // --- key ---
                                // --- --- key
                                srideIndex = FixSride_X_X_O(key, zugaraStatusPackReelL3);
                                if (srideIndex == -1) srideIndex = 0;
                            }
                            else if ((zugaraStatusPackReelL1.FixZugara[1] == "r7") && (zugaraStatusPackReelL2.FixZugara[1] == "r7"))
                            {
                                // --- --- ---
                                // key key key
                                // --- --- ---
                                srideIndex = FixSride_X_O_X(key, zugaraStatusPackReelL3);
                                if (srideIndex == -1) srideIndex = 0;
                            }
                            else if ((zugaraStatusPackReelL1.FixZugara[2] == "r7") && (zugaraStatusPackReelL2.FixZugara[1] == "r7"))
                            {
                                // --- --- key
                                // --- key ---
                                // key --- ---
                                srideIndex = FixSride_O_X_X(key, zugaraStatusPackReelL3);
                                if (srideIndex == -1) srideIndex = 0;
                            }
                            else if ((zugaraStatusPackReelL1.FixZugara[2] == "r7") && (zugaraStatusPackReelL2.FixZugara[2] == "r7"))
                            {
                                // --- --- ---
                                // --- --- ---
                                // key key key
                                srideIndex = FixSride_X_X_O(key, zugaraStatusPackReelL3);
                                if (srideIndex == -1) srideIndex = 0;
                            }
                        }
                        break;
                }
                break;
            case "rep":
                key = "rep";
                if (slotCore.oneGame.buttonCount == 1)
                {
                    srideIndex = FixSride_O_O_O(key, zugaraStatusPackReelL3);
                    if (srideIndex == -1) srideIndex = 0;
                }
                else if (slotCore.oneGame.buttonCount == 2)
                {
                    if (zugaraStatusPackReelL2.IsFix)
                    {
                        if (zugaraStatusPackReelL2.FixZugara[0] == key)
                        {
                            // ??? key key
                            // ??? --- ---
                            // ??? --- ---
                            srideIndex = FixSride_O_X_X(key, zugaraStatusPackReelL3);
                            if (srideIndex == -1) srideIndex = 0;
                        }
                        else if (zugaraStatusPackReelL2.FixZugara[1] == key)
                        {
                            // ??? --- key
                            // ??? key key
                            // ??? --- key
                            srideIndex = FixSride_O_O_O(key, zugaraStatusPackReelL3);
                            if (srideIndex == -1) srideIndex = 0;
                        }
                        else if (zugaraStatusPackReelL2.FixZugara[2] == key)
                        {
                            // ??? --- ---
                            // ??? --- ---
                            // ??? key key
                            srideIndex = FixSride_X_X_O(key, zugaraStatusPackReelL3);
                            if (srideIndex == -1) srideIndex = 0;
                        }
                    }
                    else
                    {
                        if (zugaraStatusPackReelL1.FixZugara[0] == key)
                        {
                            // key ??? key
                            // --- ??? ---
                            // --- ??? key
                            srideIndex = FixSride_O_X_O(key, zugaraStatusPackReelL3);
                            if (srideIndex == -1) srideIndex = 0;
                        }
                        else if (zugaraStatusPackReelL1.FixZugara[1] == key)
                        {
                            // --- ??? ---
                            // key ??? key
                            // --- ??? ---
                            srideIndex = FixSride_X_O_X(key, zugaraStatusPackReelL3);
                            if (srideIndex == -1) srideIndex = 0;
                        }
                        else if (zugaraStatusPackReelL2.FixZugara[2] == key)
                        {
                            // --- ??? key
                            // --- ??? ---
                            // key ??? key
                            srideIndex = FixSride_O_X_O(key, zugaraStatusPackReelL3);
                            if (srideIndex == -1) srideIndex = 0;
                        }

                    }
                }
                else if (slotCore.oneGame.buttonCount == 3)
                {
                    if ((zugaraStatusPackReelL1.FixZugara[0] == key) && (zugaraStatusPackReelL2.FixZugara[0] == key))
                    {
                        // key key key
                        // --- --- ---
                        // --- --- ---
                        srideIndex = FixSride_O_X_X(key, zugaraStatusPackReelL3);
                        if (srideIndex == -1) srideIndex = 0;
                    }
                    else if ((zugaraStatusPackReelL1.FixZugara[0] == key) && (zugaraStatusPackReelL2.FixZugara[1] == key))
                    {
                        // key --- ---
                        // --- key ---
                        // --- --- key
                        srideIndex = FixSride_X_X_O(key, zugaraStatusPackReelL3);
                        if (srideIndex == -1) srideIndex = 0;
                    }
                    else if ((zugaraStatusPackReelL1.FixZugara[1] == key) && (zugaraStatusPackReelL2.FixZugara[1] == key))
                    {
                        // --- --- ---
                        // key key key
                        // --- --- ---
                        srideIndex = FixSride_X_O_X(key, zugaraStatusPackReelL3);
                        if (srideIndex == -1) srideIndex = 0;
                    }
                    else if ((zugaraStatusPackReelL1.FixZugara[2] == key) && (zugaraStatusPackReelL2.FixZugara[1] == key))
                    {
                        // --- --- key
                        // --- key ---
                        // key --- ---
                        srideIndex = FixSride_O_X_X(key, zugaraStatusPackReelL3);
                        if (srideIndex == -1) srideIndex = 0;
                    }
                    else if ((zugaraStatusPackReelL1.FixZugara[2] == key) && (zugaraStatusPackReelL2.FixZugara[2] == key))
                    {
                        // --- --- ---
                        // --- --- ---
                        // key key key
                        srideIndex = FixSride_X_X_O(key, zugaraStatusPackReelL3);
                        if (srideIndex == -1) srideIndex = 0;
                    }
                }
                break;
            case "bell":
                key = "bell";
                if (slotCore.oneGame.buttonCount == 1)
                {
                    srideIndex = FixSride_O_O_O(key, zugaraStatusPackReelL3);
                    if (srideIndex == -1) srideIndex = 0;
                }
                else if (slotCore.oneGame.buttonCount == 2)
                {
                    if (zugaraStatusPackReelL2.IsFix)
                    {
                        if (zugaraStatusPackReelL2.FixZugara[0] == key)
                        {
                            // ??? key key
                            // ??? --- ---
                            // ??? --- ---
                            srideIndex = FixSride_O_X_X(key, zugaraStatusPackReelL3);
                            if (srideIndex == -1) srideIndex = 0;
                        }
                        else if (zugaraStatusPackReelL2.FixZugara[1] == key)
                        {
                            // ??? --- key
                            // ??? key key
                            // ??? --- key
                            srideIndex = FixSride_O_O_O(key, zugaraStatusPackReelL3);
                            if (srideIndex == -1) srideIndex = 0;
                        }
                        else if (zugaraStatusPackReelL2.FixZugara[2] == key)
                        {
                            // ??? --- ---
                            // ??? --- ---
                            // ??? key key
                            srideIndex = FixSride_X_X_O(key, zugaraStatusPackReelL3);
                            if (srideIndex == -1) srideIndex = 0;
                        }
                    }
                    else
                    {
                        if (zugaraStatusPackReelL1.FixZugara[0] == key)
                        {
                            // key ??? key
                            // --- ??? ---
                            // --- ??? key
                            srideIndex = FixSride_O_X_O(key, zugaraStatusPackReelL3);
                            if (srideIndex == -1) srideIndex = 0;
                        }
                        else if (zugaraStatusPackReelL1.FixZugara[1] == key)
                        {
                            // --- ??? ---
                            // key ??? key
                            // --- ??? ---
                            srideIndex = FixSride_X_O_X(key, zugaraStatusPackReelL3);
                            if (srideIndex == -1) srideIndex = 0;
                        }
                        else if (zugaraStatusPackReelL2.FixZugara[2] == key)
                        {
                            // --- ??? key
                            // --- ??? ---
                            // key ??? key
                            srideIndex = FixSride_O_X_O(key, zugaraStatusPackReelL3);
                            if (srideIndex == -1) srideIndex = 0;
                        }

                    }
                }
                else if (slotCore.oneGame.buttonCount == 3)
                {
                    if ((zugaraStatusPackReelL1.FixZugara[0] == key) && (zugaraStatusPackReelL2.FixZugara[0] == key))
                    {
                        // key key key
                        // --- --- ---
                        // --- --- ---
                        srideIndex = FixSride_O_X_X(key, zugaraStatusPackReelL3);
                        if (srideIndex == -1) srideIndex = 0;
                    }
                    else if ((zugaraStatusPackReelL1.FixZugara[0] == key) && (zugaraStatusPackReelL2.FixZugara[1] == key))
                    {
                        // key --- ---
                        // --- key ---
                        // --- --- key
                        srideIndex = FixSride_X_X_O(key, zugaraStatusPackReelL3);
                        if (srideIndex == -1) srideIndex = 0;
                    }
                    else if ((zugaraStatusPackReelL1.FixZugara[1] == key) && (zugaraStatusPackReelL2.FixZugara[1] == key))
                    {
                        // --- --- ---
                        // key key key
                        // --- --- ---
                        srideIndex = FixSride_X_O_X(key, zugaraStatusPackReelL3);
                        if (srideIndex == -1) srideIndex = 0;
                    }
                    else if ((zugaraStatusPackReelL1.FixZugara[2] == key) && (zugaraStatusPackReelL2.FixZugara[1] == key))
                    {
                        // --- --- key
                        // --- key ---
                        // key --- ---
                        srideIndex = FixSride_O_X_X(key, zugaraStatusPackReelL3);
                        if (srideIndex == -1) srideIndex = 0;
                    }
                    else if ((zugaraStatusPackReelL1.FixZugara[2] == key) && (zugaraStatusPackReelL2.FixZugara[2] == key))
                    {
                        // --- --- ---
                        // --- --- ---
                        // key key key
                        srideIndex = FixSride_X_X_O(key, zugaraStatusPackReelL3);
                        if (srideIndex == -1) srideIndex = 0;
                    }
                }
                break;
            case "suika":
                key = "suika";
                if (slotCore.oneGame.buttonCount == 1)
                {
                    srideIndex = FixSride_O_O_O(key, zugaraStatusPackReelL3);
                    if (srideIndex == -1) srideIndex = 0;
                }
                else if (slotCore.oneGame.buttonCount == 2)
                {
                    if (zugaraStatusPackReelL2.IsFix)
                    {
                        if (zugaraStatusPackReelL2.FixZugara[0] == key)
                        {
                            // ??? key key
                            // ??? --- ---
                            // ??? --- ---
                            srideIndex = FixSride_O_X_X(key, zugaraStatusPackReelL3);
                            if (srideIndex == -1) srideIndex = 0;
                        }
                        else if (zugaraStatusPackReelL2.FixZugara[1] == key)
                        {
                            // ??? --- key
                            // ??? key key
                            // ??? --- key
                            srideIndex = FixSride_O_O_O(key, zugaraStatusPackReelL3);
                            if (srideIndex == -1) srideIndex = 0;
                        }
                        else if (zugaraStatusPackReelL2.FixZugara[2] == key)
                        {
                            // ??? --- ---
                            // ??? --- ---
                            // ??? key key
                            srideIndex = FixSride_X_X_O(key, zugaraStatusPackReelL3);
                            if (srideIndex == -1) srideIndex = 0;
                        }
                    }
                    else
                    {
                        if (zugaraStatusPackReelL1.FixZugara[0] == key)
                        {
                            // key ??? key
                            // --- ??? ---
                            // --- ??? key
                            srideIndex = FixSride_O_X_O(key, zugaraStatusPackReelL3);
                            if (srideIndex == -1) srideIndex = 0;
                        }
                        else if (zugaraStatusPackReelL1.FixZugara[1] == key)
                        {
                            // --- ??? ---
                            // key ??? key
                            // --- ??? ---
                            srideIndex = FixSride_X_O_X(key, zugaraStatusPackReelL3);
                            if (srideIndex == -1) srideIndex = 0;
                        }
                        else if (zugaraStatusPackReelL2.FixZugara[2] == key)
                        {
                            // --- ??? key
                            // --- ??? ---
                            // key ??? key
                            srideIndex = FixSride_O_X_O(key, zugaraStatusPackReelL3);
                            if (srideIndex == -1) srideIndex = 0;
                        }

                    }
                }
                else if (slotCore.oneGame.buttonCount == 3)
                {
                    if ((zugaraStatusPackReelL1.FixZugara[0] == key) && (zugaraStatusPackReelL2.FixZugara[0] == key))
                    {
                        // key key key
                        // --- --- ---
                        // --- --- ---
                        srideIndex = FixSride_O_X_X(key, zugaraStatusPackReelL3);
                        if (srideIndex == -1) srideIndex = 0;
                    }
                    else if ((zugaraStatusPackReelL1.FixZugara[0] == key) && (zugaraStatusPackReelL2.FixZugara[1] == key))
                    {
                        // key --- ---
                        // --- key ---
                        // --- --- key
                        srideIndex = FixSride_X_X_O(key, zugaraStatusPackReelL3);
                        if (srideIndex == -1) srideIndex = 0;
                    }
                    else if ((zugaraStatusPackReelL1.FixZugara[1] == key) && (zugaraStatusPackReelL2.FixZugara[1] == key))
                    {
                        // --- --- ---
                        // key key key
                        // --- --- ---
                        srideIndex = FixSride_X_O_X(key, zugaraStatusPackReelL3);
                        if (srideIndex == -1) srideIndex = 0;
                    }
                    else if ((zugaraStatusPackReelL1.FixZugara[2] == key) && (zugaraStatusPackReelL2.FixZugara[1] == key))
                    {
                        // --- --- key
                        // --- key ---
                        // key --- ---
                        srideIndex = FixSride_O_X_X(key, zugaraStatusPackReelL3);
                        if (srideIndex == -1) srideIndex = 0;
                    }
                    else if ((zugaraStatusPackReelL1.FixZugara[2] == key) && (zugaraStatusPackReelL2.FixZugara[2] == key))
                    {
                        // --- --- ---
                        // --- --- ---
                        // key key key
                        srideIndex = FixSride_X_X_O(key, zugaraStatusPackReelL3);
                        if (srideIndex == -1) srideIndex = 0;
                    }
                }
                break;
        }

        if (CheckBadRoleHit(2, srideIndex, zugaraStatusPackReelL3))
        {
            srideIndex = GetSrideBadRoleHit(2, zugaraStatusPackReelL3);
            Debug.Log($"GetSrideBadRoleHit L3 {srideIndex} ");
        }

        zugaraStatusPackReelL3.Fix(srideIndex);
    }


    void Update()
    {
        zugaraStatusPackReelL1.Update();
        zugaraStatusPackReelL2.Update();
        zugaraStatusPackReelL3.Update();
    }

    public void FixOneGame()
    {
        if (CheckHitLine("rep", "rep", "rep"))
        {
            slotCore.oneGame.hit = "rep";
        }
        else if (CheckHitLine("bell", "bell", "bell"))
        {
            slotCore.oneGame.hit = "bell";
            lampLeft.SetAnimation("ki");
            lampRight.SetAnimation("ki");
        }
        else if (CheckHitLine("suika", "suika", "suika"))
        {
            slotCore.oneGame.hit = "suika";
        }
        else if (CheckHitLine("chery", "", ""))
        {
            slotCore.oneGame.hit = "chery";
        }
        else if (CheckHitLine("r7", "r7", "r7"))
        {
            slotCore.oneGame.hit = "Big-r7";
        }
        else if (CheckHitLine("b7", "b7", "b7"))
        {
            slotCore.oneGame.hit = "Big-b7";
        }
        else if (CheckHitLine("r7", "r7", "bar"))
        {
            slotCore.oneGame.hit = "Reg";
        }
    }
    public bool CheckHitLine( string key1, string key2, string key3)
    {
        if (key1=="chery")
        {
            if ((zugaraStatusPackReelL1.FixZugara[0] == key1) || (zugaraStatusPackReelL1.FixZugara[1] == key1) || (zugaraStatusPackReelL1.FixZugara[2] == key1))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        if ((zugaraStatusPackReelL1.FixZugara[0] == key1) && (zugaraStatusPackReelL2.FixZugara[0] == key2) && (zugaraStatusPackReelL3.FixZugara[0] == key3))
        {
            // key key key
            // --- --- ---
            // --- --- ---
            return true;
        }
        else if ((zugaraStatusPackReelL1.FixZugara[0] == key1) && (zugaraStatusPackReelL2.FixZugara[1] == key2) && (zugaraStatusPackReelL3.FixZugara[2] == key3))
        {
            // key --- ---
            // --- key ---
            // --- --- key
            return true;
        }
        else if ((zugaraStatusPackReelL1.FixZugara[1] == key1) && (zugaraStatusPackReelL2.FixZugara[1] == key2) && (zugaraStatusPackReelL3.FixZugara[1] == key3))
        {
            // --- --- ---
            // key key key
            // --- --- ---
            return true;
        }
        else if ((zugaraStatusPackReelL1.FixZugara[2] == key1) && (zugaraStatusPackReelL2.FixZugara[1] == key2) && (zugaraStatusPackReelL3.FixZugara[0] == key3))
        {
            // --- --- key
            // --- key ---
            // key --- ---
            return true;
        }
        else if ((zugaraStatusPackReelL1.FixZugara[2] == key1) && (zugaraStatusPackReelL2.FixZugara[2] == key2) && (zugaraStatusPackReelL3.FixZugara[2] == key3))
        {
            // --- --- ---
            // --- --- ---
            // key key key
            return true;
        }
        return false;
    }

    // 抽選されていないものがヒットしてしまうかどうか
    private bool CheckBadRoleHit( int line, int sride, ReelZugaraStatusPack zugaraStatusPack)
    {
        var role = slotCore.oneGame.flagRole;
        var bonus = slotCore.oneGame.flagBounus;

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

            if (slotCore.oneGame.buttonCount == 3 )
            {
                System.Func<string, bool> func1 = (key) =>
                 {
                     if ((tmp[0] == key) && (zugaraStatusPackReelL2.FixZugara[0] == key) && (zugaraStatusPackReelL3.FixZugara[0] == key))
                     {
                        // key key key
                        // --- --- ---
                        // --- --- ---
                        return true;
                     }
                     else if ((tmp[0] == key) && (zugaraStatusPackReelL2.FixZugara[1] == key) && (zugaraStatusPackReelL3.FixZugara[2] == key))
                     {
                        // key --- ---
                        // --- key ---
                        // --- --- key
                        return true;
                     }
                     else if ((tmp[1] == key) && (zugaraStatusPackReelL2.FixZugara[1] == key) && (zugaraStatusPackReelL3.FixZugara[1] == key))
                     {
                        // --- --- ---
                        // key key key
                        // --- --- ---
                        return true;
                     }
                     else if ((tmp[2] == key) && (zugaraStatusPackReelL2.FixZugara[1] == key) && (zugaraStatusPackReelL3.FixZugara[0] == key))
                     {
                        // --- --- key
                        // --- key ---
                        // key --- ---
                        return true;
                     }
                     else if ((tmp[2] == key) && (zugaraStatusPackReelL2.FixZugara[2] == key) && (zugaraStatusPackReelL3.FixZugara[2] == key))
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
                    if ((tmp[0] == "r7") && (zugaraStatusPackReelL2.FixZugara[0] == "r7") && (zugaraStatusPackReelL3.FixZugara[0] == "bar"))
                    {
                        // key key key
                        // --- --- ---
                        // --- --- ---
                        return true;
                    }
                    else if ((tmp[0] == "r7") && (zugaraStatusPackReelL2.FixZugara[1] == "r7") && (zugaraStatusPackReelL3.FixZugara[2] == "bar"))
                    {
                        // key --- ---
                        // --- key ---
                        // --- --- key
                        return true;
                    }
                    else if ((tmp[1] == "r7") && (zugaraStatusPackReelL2.FixZugara[1] == "r7") && (zugaraStatusPackReelL3.FixZugara[1] == "bar"))
                    {
                        // --- --- ---
                        // key key key
                        // --- --- ---
                        return true;
                    }
                    else if ((tmp[2] == "r7") && (zugaraStatusPackReelL2.FixZugara[1] == "r7") && (zugaraStatusPackReelL3.FixZugara[0] == "bar"))
                    {
                        // --- --- key
                        // --- key ---
                        // key --- ---
                        return true;
                    }
                    else if ((tmp[2] == "r7") && (zugaraStatusPackReelL2.FixZugara[2] == "r7") && (zugaraStatusPackReelL3.FixZugara[2] == "bar"))
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

            if (slotCore.oneGame.buttonCount == 3)
            {
                System.Func<string, bool> func2 = (key) =>
                {
                    if ((tmp[0] == key) && (zugaraStatusPackReelL1.FixZugara[0] == key) && (zugaraStatusPackReelL3.FixZugara[0] == key))
                    {
                        // key key key
                        // --- --- ---
                        // --- --- ---
                        return true;
                    }
                    else if ((tmp[1] == key) && (zugaraStatusPackReelL1.FixZugara[0] == key) && (zugaraStatusPackReelL3.FixZugara[2] == key))
                    {
                        // key --- ---
                        // --- key ---
                        // --- --- key
                        return true;
                    }
                    else if ((tmp[1] == key) && (zugaraStatusPackReelL1.FixZugara[1] == key) && (zugaraStatusPackReelL3.FixZugara[1] == key))
                    {
                        // --- --- ---
                        // key key key
                        // --- --- ---
                        return true;
                    }
                    else if ((tmp[1] == key) && (zugaraStatusPackReelL1.FixZugara[2] == key) && (zugaraStatusPackReelL3.FixZugara[0] == key))
                    {
                        // --- --- key
                        // --- key ---
                        // key --- ---
                        return true;
                    }
                    else if ((tmp[2] == key) && (zugaraStatusPackReelL1.FixZugara[2] == key) && (zugaraStatusPackReelL3.FixZugara[2] == key))
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
                    if ((tmp[0] == "r7") && (zugaraStatusPackReelL1.FixZugara[0] == "r7") && (zugaraStatusPackReelL3.FixZugara[0] == "bar"))
                    {
                        // key key key
                        // --- --- ---
                        // --- --- ---
                        return true;
                    }
                    else if ((tmp[1] == "r7") && (zugaraStatusPackReelL1.FixZugara[0] == "r7") && (zugaraStatusPackReelL3.FixZugara[2] == "bar"))
                    {
                        // key --- ---
                        // --- key ---
                        // --- --- key
                        return true;
                    }
                    else if ((tmp[1] == "r7") && (zugaraStatusPackReelL1.FixZugara[1] == "r7") && (zugaraStatusPackReelL3.FixZugara[1] == "bar"))
                    {
                        // --- --- ---
                        // key key key
                        // --- --- ---
                        return true;
                    }
                    else if ((tmp[1] == "r7") && (zugaraStatusPackReelL1.FixZugara[2] == "r7") && (zugaraStatusPackReelL3.FixZugara[0] == "bar"))
                    {
                        // --- --- key
                        // --- key ---
                        // key --- ---
                        return true;
                    }
                    else if ((tmp[2] == "r7") && (zugaraStatusPackReelL1.FixZugara[2] == "r7") && (zugaraStatusPackReelL3.FixZugara[2] == "bar"))
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

            if (slotCore.oneGame.buttonCount == 3)
            {
                System.Func<string, bool> func3 = (key) =>
                {
                    if ((tmp[0] == key) && (zugaraStatusPackReelL1.FixZugara[0] == key) && (zugaraStatusPackReelL2.FixZugara[0] == key))
                    {
                        // key key key
                        // --- --- ---
                        // --- --- ---
                        return true;
                    }
                    else if ((tmp[2] == key) && (zugaraStatusPackReelL1.FixZugara[0] == key) && (zugaraStatusPackReelL2.FixZugara[1] == key))
                    {
                        // key --- ---
                        // --- key ---
                        // --- --- key
                        return true;
                    }
                    else if ((tmp[1] == key) && (zugaraStatusPackReelL1.FixZugara[1] == key) && (zugaraStatusPackReelL2.FixZugara[1] == key))
                    {
                        // --- --- ---
                        // key key key
                        // --- --- ---
                        return true;
                    }
                    else if ((tmp[0] == key) && (zugaraStatusPackReelL1.FixZugara[2] == key) && (zugaraStatusPackReelL2.FixZugara[1] == key))
                    {
                        // --- --- key
                        // --- key ---
                        // key --- ---
                        return true;
                    }
                    else if ((tmp[2] == key) && (zugaraStatusPackReelL1.FixZugara[2] == key) && (zugaraStatusPackReelL2.FixZugara[2] == key))
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
                    if ((tmp[0] == "r7") && (zugaraStatusPackReelL1.FixZugara[0] == "r7") && (zugaraStatusPackReelL2.FixZugara[0] == "bar"))
                    {
                        // key key key
                        // --- --- ---
                        // --- --- ---
                        return true;
                    }
                    else if ((tmp[2] == "r7") && (zugaraStatusPackReelL1.FixZugara[0] == "r7") && (zugaraStatusPackReelL2.FixZugara[1] == "bar"))
                    {
                        // key --- ---
                        // --- key ---
                        // --- --- key
                        return true;
                    }
                    else if ((tmp[1] == "r7") && (zugaraStatusPackReelL1.FixZugara[1] == "r7") && (zugaraStatusPackReelL2.FixZugara[1] == "bar"))
                    {
                        // --- --- ---
                        // key key key
                        // --- --- ---
                        return true;
                    }
                    else if ((tmp[0] == "r7") && (zugaraStatusPackReelL1.FixZugara[2] == "r7") && (zugaraStatusPackReelL2.FixZugara[1] == "bar"))
                    {
                        // --- --- key
                        // --- key ---
                        // key --- ---
                        return true;
                    }
                    else if ((tmp[2] == "r7") && (zugaraStatusPackReelL1.FixZugara[2] == "r7") && (zugaraStatusPackReelL2.FixZugara[2] == "bar"))
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


    private int GetSrideBadRoleHit(int line, ReelZugaraStatusPack zugaraStatusPack)
    {
        for( var i=0; i<slideMax; i++ )
        {
            if (!CheckBadRoleHit(line,i, zugaraStatusPack))
            {
                return i;
            }
        }
        return 0; // err
    }


    private int FixSride_O_O_O( string key, ReelZugaraStatusPack zugaraStatusPack )
    {
        var tmp = new string[3] { "", "", "" };
        for (var i = 0; i < slideMax; i++)
        {
            tmp = zugaraStatusPack.GetFixZugara(i);
            if ((tmp[0] == key) || (tmp[1] == key) || (tmp[2] == key))
            {
                return i;
            }
        }
        return -1;
    }

    private int FixSride_O_O_X(string key, ReelZugaraStatusPack zugaraStatusPack)
    {
        var tmp = new string[3] { "", "", "" };
        for (var i = 0; i < slideMax; i++)
        {
            tmp = zugaraStatusPack.GetFixZugara(i);
            if ((tmp[0] == key) || (tmp[1] == key) )
            {
                return i;
            }
        }
        return -1;
    }
    private int FixSride_O_X_O(string key, ReelZugaraStatusPack zugaraStatusPack)
    {
        var tmp = new string[3] { "", "", "" };
        for (var i = 0; i < slideMax; i++)
        {
            tmp = zugaraStatusPack.GetFixZugara(i);
            if ((tmp[0] == key) || (tmp[2] == key))
            {
                return i;
            }
        }
        return -1;
    }
    private int FixSride_X_O_O(string key, ReelZugaraStatusPack zugaraStatusPack)
    {
        var tmp = new string[3] { "", "", "" };
        for (var i = 0; i < slideMax; i++)
        {
            tmp = zugaraStatusPack.GetFixZugara(i);
            if ((tmp[1] == key) || (tmp[2] == key))
            {
                return i;
            }
        }
        return -1;
    }
    private int FixSride_O_X_X(string key, ReelZugaraStatusPack zugaraStatusPack)
    {
        var tmp = new string[3] { "", "", "" };
        for (var i = 0; i < slideMax; i++)
        {
            tmp = zugaraStatusPack.GetFixZugara(i);
            if (tmp[0] == key)
            {
                return i;
            }
        }
        return -1;
    }
    private int FixSride_X_O_X(string key, ReelZugaraStatusPack zugaraStatusPack)
    {
        var tmp = new string[3] { "", "", "" };
        for (var i = 0; i < slideMax; i++)
        {
            tmp = zugaraStatusPack.GetFixZugara(i);
            if (tmp[1] == key)
            {
                return i;
            }
        }
        return -1;
    }
    private int FixSride_X_X_O(string key, ReelZugaraStatusPack zugaraStatusPack)
    {
        var tmp = new string[3] { "", "", "" };
        for (var i = 0; i < slideMax; i++)
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

        zugaraStatusPackReelL1.ReelTimerMaxOffset = value;
        zugaraStatusPackReelL1.ReelTimerMax = zugaraStatusPackReelL1.ReelTimerMaxBase * (1f + (1f - value) * offsetBase);

        zugaraStatusPackReelL2.ReelTimerMaxOffset = value;
        zugaraStatusPackReelL2.ReelTimerMax = zugaraStatusPackReelL2.ReelTimerMaxBase * (1f + (1f - value) * offsetBase);

        zugaraStatusPackReelL3.ReelTimerMaxOffset = value;
        zugaraStatusPackReelL3.ReelTimerMax = zugaraStatusPackReelL3.ReelTimerMaxBase * (1f + (1f - value) * offsetBase);
    }

    public GameObject _Instantiate( GameObject gameObject, Transform parent )
    {
        return Instantiate(gameObject, parent);
    }
}
