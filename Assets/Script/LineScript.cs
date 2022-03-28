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
    List<Zugara> Zugaras = new List<Zugara>();

    [SerializeField]
    GameObject ZugaraPrefab;

    [SerializeField]
    Lamp LampLeft;

    [SerializeField]
    Lamp LampRight;

    [SerializeField]
    SoundResource soundResource;

    ReelZugaraStatusPack ZugaraStatusPackReelL1 = new ReelZugaraStatusPack();
    ReelZugaraStatusPack ZugaraStatusPackReelL2 = new ReelZugaraStatusPack();
    ReelZugaraStatusPack ZugaraStatusPackReelL3 = new ReelZugaraStatusPack();

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

    const float oneButtonWaitTimerMax = 0.3f;
    float oneButtonWaitTimer = 0;

    // Start is called before the first frame update
    void Start()
    {
        ZugaraStatusPackReelL1.Init(-2.15f, -0.41f, Reel1ZugaraSource, ZugaraPrefab, transform, this);
        ZugaraStatusPackReelL2.Init(    0f, -0.41f, Reel2ZugaraSource, ZugaraPrefab, transform, this);
        ZugaraStatusPackReelL3.Init( 2.15f, -0.41f, Reel3ZugaraSource, ZugaraPrefab, transform, this);
    }

    public Zugara GetZugaraByKey(string key)
    {
        foreach (var i in Zugaras)
        {
            if (i.Key == key) return i;
        }
        return null;
    }

    public void StartRollAll()
    {
        ZugaraStatusPackReelL1.Play();
        ZugaraStatusPackReelL2.Play();
        ZugaraStatusPackReelL3.Play();

        //SlotCoreScript.DoBaseLot(ref bonus, ref role);
        //Debug.Log($"DoBaseLot {bonus}, {role}");

        LampLeft.SetAnimation("def");
        LampRight.SetAnimation("def");

        oneButtonWaitTimer = 0;
    }

    public void StopRollOne( SlotCore.ButtonType buttonType )
    {
        switch (buttonType)
        {
            case SlotCore.ButtonType.ReelStopL1:
                StopRollOneLeft();
                break;
            case SlotCore.ButtonType.ReelStopL2:
                StopRollOneCenter();
                break;
            case SlotCore.ButtonType.ReelStopL3:
                StopRollOneRight();
                break;
        }
    }

    private void StopRollOneLeft()
    {
        if (ZugaraStatusPackReelL1.IsFix) return;
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
                        if (slotCore.oneGame.buttonCount == 0)
                        {
                            srideIndex = FixSride_O_O_O(key, ZugaraStatusPackReelL1);
                            if (srideIndex == -1) srideIndex = 0;
                        }
                        else if (slotCore.oneGame.buttonCount == 1)
                        {
                            if (ZugaraStatusPackReelL2.IsFix)
                            {
                                if (ZugaraStatusPackReelL2.FixZugara[0] == key)
                                {
                                    // key key ???
                                    // --- --- ???
                                    // --- --- ???
                                    srideIndex = FixSride_O_X_X(key, ZugaraStatusPackReelL1);
                                    if (srideIndex == -1) srideIndex = 0;
                                }
                                else if (ZugaraStatusPackReelL2.FixZugara[1] == key)
                                {
                                    // key --- ???
                                    // key key ???
                                    // key --- ???
                                    srideIndex = FixSride_O_O_O(key, ZugaraStatusPackReelL1);
                                    if (srideIndex == -1) srideIndex = 0;
                                }
                                else if (ZugaraStatusPackReelL2.FixZugara[2] == key)
                                {
                                    // --- --- ???
                                    // --- --- ???
                                    // key key ???
                                    srideIndex = FixSride_X_X_O(key, ZugaraStatusPackReelL1);
                                    if (srideIndex == -1) srideIndex = 0;
                                }
                            }
                            else
                            {
                                if (ZugaraStatusPackReelL3.FixZugara[0] == key)
                                {
                                    // key ??? key
                                    // --- ??? ---
                                    // key ??? ---
                                    srideIndex = FixSride_O_X_O(key, ZugaraStatusPackReelL1);
                                    if (srideIndex == -1) srideIndex = 0;
                                }
                                else if (ZugaraStatusPackReelL3.FixZugara[1] == key)
                                {
                                    // --- ??? ---
                                    // key ??? key
                                    // --- ??? ---
                                    srideIndex = FixSride_X_O_X(key, ZugaraStatusPackReelL1);
                                    if (srideIndex == -1) srideIndex = 0;
                                }
                                else if (ZugaraStatusPackReelL3.FixZugara[2] == key)
                                {
                                    // key ??? ---
                                    // --- ??? ---
                                    // key ??? key
                                    srideIndex = FixSride_O_X_O(key, ZugaraStatusPackReelL1);
                                    if (srideIndex == -1) srideIndex = 0;
                                }
                            }
                        }
                        else if (slotCore.oneGame.buttonCount == 2)
                        {
                            if ((ZugaraStatusPackReelL2.FixZugara[0] == key) && (ZugaraStatusPackReelL3.FixZugara[0] == key))
                            {
                                // key key key
                                // --- --- ---
                                // --- --- ---
                                srideIndex = FixSride_O_X_X(key, ZugaraStatusPackReelL1);
                                if (srideIndex == -1) srideIndex = 0;
                            }
                            else if ((ZugaraStatusPackReelL2.FixZugara[1] == key) && (ZugaraStatusPackReelL3.FixZugara[2] == key))
                            {
                                // key --- ---
                                // --- key ---
                                // --- --- key
                                srideIndex = FixSride_O_X_X(key, ZugaraStatusPackReelL1);
                                if (srideIndex == -1) srideIndex = 0;
                            }
                            else if ((ZugaraStatusPackReelL2.FixZugara[1] == key) && (ZugaraStatusPackReelL3.FixZugara[1] == key))
                            {
                                // --- --- ---
                                // key key key
                                // --- --- ---
                                srideIndex = FixSride_X_O_X(key, ZugaraStatusPackReelL1);
                                if (srideIndex == -1) srideIndex = 0;
                            }
                            else if ((ZugaraStatusPackReelL2.FixZugara[1] == key) && (ZugaraStatusPackReelL3.FixZugara[0] == key))
                            {
                                // --- --- key
                                // --- key ---
                                // key --- ---
                                srideIndex = FixSride_X_X_O(key, ZugaraStatusPackReelL1);
                                if (srideIndex == -1) srideIndex = 0;
                            }
                            else if ((ZugaraStatusPackReelL2.FixZugara[2] == key) && (ZugaraStatusPackReelL3.FixZugara[2] == key))
                            {
                                // --- --- ---
                                // --- --- ---
                                // key key key
                                srideIndex = FixSride_X_X_O(key, ZugaraStatusPackReelL1);
                                if (srideIndex == -1) srideIndex = 0;
                            }
                        }
                        break;
                    case "Big-b7":
                        key = "b7";
                        if (slotCore.oneGame.buttonCount == 0)
                        {
                            srideIndex = FixSride_O_O_O(key, ZugaraStatusPackReelL1);
                            if (srideIndex == -1) srideIndex = 0;
                        }
                        else if (slotCore.oneGame.buttonCount == 1)
                        {
                            if (ZugaraStatusPackReelL2.IsFix)
                            {
                                if (ZugaraStatusPackReelL2.FixZugara[0] == key)
                                {
                                    // key key ???
                                    // --- --- ???
                                    // --- --- ???
                                    srideIndex = FixSride_O_X_X(key, ZugaraStatusPackReelL1);
                                    if (srideIndex == -1) srideIndex = 0;
                                }
                                else if (ZugaraStatusPackReelL2.FixZugara[1] == key)
                                {
                                    // key --- ???
                                    // key key ???
                                    // key --- ???
                                    srideIndex = FixSride_O_O_O(key, ZugaraStatusPackReelL1);
                                    if (srideIndex == -1) srideIndex = 0;
                                }
                                else if (ZugaraStatusPackReelL2.FixZugara[2] == key)
                                {
                                    // --- --- ???
                                    // --- --- ???
                                    // key key ???
                                    srideIndex = FixSride_X_X_O(key, ZugaraStatusPackReelL1);
                                    if (srideIndex == -1) srideIndex = 0;
                                }
                            }
                            else
                            {
                                if (ZugaraStatusPackReelL3.FixZugara[0] == key)
                                {
                                    // key ??? key
                                    // --- ??? ---
                                    // key ??? ---
                                    srideIndex = FixSride_O_X_O(key, ZugaraStatusPackReelL1);
                                    if (srideIndex == -1) srideIndex = 0;
                                }
                                else if (ZugaraStatusPackReelL3.FixZugara[1] == key)
                                {
                                    // --- ??? ---
                                    // key ??? key
                                    // --- ??? ---
                                    srideIndex = FixSride_X_O_X(key, ZugaraStatusPackReelL1);
                                    if (srideIndex == -1) srideIndex = 0;
                                }
                                else if (ZugaraStatusPackReelL3.FixZugara[2] == key)
                                {
                                    // key ??? ---
                                    // --- ??? ---
                                    // key ??? key
                                    srideIndex = FixSride_O_X_O(key, ZugaraStatusPackReelL1);
                                    if (srideIndex == -1) srideIndex = 0;
                                }
                            }
                        }
                        else if (slotCore.oneGame.buttonCount == 2)
                        {
                            if ((ZugaraStatusPackReelL2.FixZugara[0] == key) && (ZugaraStatusPackReelL3.FixZugara[0] == key))
                            {
                                // key key key
                                // --- --- ---
                                // --- --- ---
                                srideIndex = FixSride_O_X_X(key, ZugaraStatusPackReelL1);
                                if (srideIndex == -1) srideIndex = 0;
                            }
                            else if ((ZugaraStatusPackReelL2.FixZugara[1] == key) && (ZugaraStatusPackReelL3.FixZugara[2] == key))
                            {
                                // key --- ---
                                // --- key ---
                                // --- --- key
                                srideIndex = FixSride_O_X_X(key, ZugaraStatusPackReelL1);
                                if (srideIndex == -1) srideIndex = 0;
                            }
                            else if ((ZugaraStatusPackReelL2.FixZugara[1] == key) && (ZugaraStatusPackReelL3.FixZugara[1] == key))
                            {
                                // --- --- ---
                                // key key key
                                // --- --- ---
                                srideIndex = FixSride_X_O_X(key, ZugaraStatusPackReelL1);
                                if (srideIndex == -1) srideIndex = 0;
                            }
                            else if ((ZugaraStatusPackReelL2.FixZugara[1] == key) && (ZugaraStatusPackReelL3.FixZugara[0] == key))
                            {
                                // --- --- key
                                // --- key ---
                                // key --- ---
                                srideIndex = FixSride_X_X_O(key, ZugaraStatusPackReelL1);
                                if (srideIndex == -1) srideIndex = 0;
                            }
                            else if ((ZugaraStatusPackReelL2.FixZugara[2] == key) && (ZugaraStatusPackReelL3.FixZugara[2] == key))
                            {
                                // --- --- ---
                                // --- --- ---
                                // key key key
                                srideIndex = FixSride_X_X_O(key, ZugaraStatusPackReelL1);
                                if (srideIndex == -1) srideIndex = 0;
                            }
                        }
                        break;
                    case "Reg":
                        key = "r7";
                        if (slotCore.oneGame.buttonCount == 0)
                        {
                            srideIndex = FixSride_O_O_O(key, ZugaraStatusPackReelL1);
                            if (srideIndex == -1) srideIndex = 0;
                        }
                        else if (slotCore.oneGame.buttonCount == 1)
                        {
                            if (ZugaraStatusPackReelL2.IsFix)
                            {
                                if (ZugaraStatusPackReelL2.FixZugara[0] == key)
                                {
                                    // key key ???
                                    // --- --- ???
                                    // --- --- ???
                                    srideIndex = FixSride_O_X_X(key, ZugaraStatusPackReelL1);
                                    if (srideIndex == -1) srideIndex = 0;
                                }
                                else if (ZugaraStatusPackReelL2.FixZugara[1] == key)
                                {
                                    // key --- ???
                                    // key key ???
                                    // key --- ???
                                    srideIndex = FixSride_O_O_O(key, ZugaraStatusPackReelL1);
                                    if (srideIndex == -1) srideIndex = 0;
                                }
                                else if (ZugaraStatusPackReelL2.FixZugara[2] == key)
                                {
                                    // --- --- ???
                                    // --- --- ???
                                    // key key ???
                                    srideIndex = FixSride_X_X_O(key, ZugaraStatusPackReelL1);
                                    if (srideIndex == -1) srideIndex = 0;
                                }
                            }
                            else
                            {
                                if (ZugaraStatusPackReelL3.FixZugara[0] == "bar")
                                {
                                    // key ??? key
                                    // --- ??? ---
                                    // key ??? ---
                                    srideIndex = FixSride_O_X_O(key, ZugaraStatusPackReelL1);
                                    if (srideIndex == -1) srideIndex = 0;
                                }
                                else if (ZugaraStatusPackReelL3.FixZugara[1] == "bar")
                                {
                                    // --- ??? ---
                                    // key ??? key
                                    // --- ??? ---
                                    srideIndex = FixSride_X_O_X(key, ZugaraStatusPackReelL1);
                                    if (srideIndex == -1) srideIndex = 0;
                                }
                                else if (ZugaraStatusPackReelL3.FixZugara[2] == "bar")
                                {
                                    // key ??? ---
                                    // --- ??? ---
                                    // key ??? key
                                    srideIndex = FixSride_O_X_O(key, ZugaraStatusPackReelL1);
                                    if (srideIndex == -1) srideIndex = 0;
                                }
                            }
                        }
                        else if (slotCore.oneGame.buttonCount == 2)
                        {
                            if ((ZugaraStatusPackReelL2.FixZugara[0] == key) && (ZugaraStatusPackReelL3.FixZugara[0] == "bar"))
                            {
                                // key key key
                                // --- --- ---
                                // --- --- ---
                                srideIndex = FixSride_O_X_X(key, ZugaraStatusPackReelL1);
                                if (srideIndex == -1) srideIndex = 0;
                            }
                            else if ((ZugaraStatusPackReelL2.FixZugara[1] == key) && (ZugaraStatusPackReelL3.FixZugara[2] == "bar"))
                            {
                                // key --- ---
                                // --- key ---
                                // --- --- key
                                srideIndex = FixSride_O_X_X(key, ZugaraStatusPackReelL1);
                                if (srideIndex == -1) srideIndex = 0;
                            }
                            else if ((ZugaraStatusPackReelL2.FixZugara[1] == key) && (ZugaraStatusPackReelL3.FixZugara[1] == "bar"))
                            {
                                // --- --- ---
                                // key key key
                                // --- --- ---
                                srideIndex = FixSride_X_O_X(key, ZugaraStatusPackReelL1);
                                if (srideIndex == -1) srideIndex = 0;
                            }
                            else if ((ZugaraStatusPackReelL2.FixZugara[1] == key) && (ZugaraStatusPackReelL3.FixZugara[0] == "bar"))
                            {
                                // --- --- key
                                // --- key ---
                                // key --- ---
                                srideIndex = FixSride_X_X_O(key, ZugaraStatusPackReelL1);
                                if (srideIndex == -1) srideIndex = 0;
                            }
                            else if ((ZugaraStatusPackReelL2.FixZugara[2] == key) && (ZugaraStatusPackReelL3.FixZugara[2] == "bar"))
                            {
                                // --- --- ---
                                // --- --- ---
                                // key key key
                                srideIndex = FixSride_X_X_O(key, ZugaraStatusPackReelL1);
                                if (srideIndex == -1) srideIndex = 0;
                            }
                        }
                        break;
                }
                break;
            case "rep":
                key = "rep";
                if (slotCore.oneGame.buttonCount == 0)
                {
                    srideIndex = FixSride_O_O_O(key, ZugaraStatusPackReelL1);
                    if (srideIndex == -1) srideIndex = 0;
                }
                else if (slotCore.oneGame.buttonCount == 1)
                {
                    if (ZugaraStatusPackReelL2.IsFix)
                    {
                        if (ZugaraStatusPackReelL2.FixZugara[0] == key)
                        {
                            // key key ???
                            // --- --- ???
                            // --- --- ???
                            srideIndex = FixSride_O_X_X(key, ZugaraStatusPackReelL1);
                            if (srideIndex == -1) srideIndex = 0;
                        }
                        else if (ZugaraStatusPackReelL2.FixZugara[1] == key)
                        {
                            // key --- ???
                            // key key ???
                            // key --- ???
                            srideIndex = FixSride_O_O_O(key, ZugaraStatusPackReelL1);
                            if (srideIndex == -1) srideIndex = 0;
                        }
                        else if (ZugaraStatusPackReelL2.FixZugara[2] == key)
                        {
                            // --- --- ???
                            // --- --- ???
                            // key key ???
                            srideIndex = FixSride_X_X_O(key, ZugaraStatusPackReelL1);
                            if (srideIndex == -1) srideIndex = 0;
                        }
                    }
                    else
                    {
                        if (ZugaraStatusPackReelL3.FixZugara[0] == key)
                        {
                            // key ??? key
                            // --- ??? ---
                            // key ??? ---
                            srideIndex = FixSride_O_X_O(key, ZugaraStatusPackReelL1);
                            if (srideIndex == -1) srideIndex = 0;
                        }
                        else if (ZugaraStatusPackReelL3.FixZugara[1] == key)
                        {
                            // --- ??? ---
                            // key ??? key
                            // --- ??? ---
                            srideIndex = FixSride_X_O_X(key, ZugaraStatusPackReelL1);
                            if (srideIndex == -1) srideIndex = 0;
                        }
                        else if (ZugaraStatusPackReelL3.FixZugara[2] == key)
                        {
                            // key ??? ---
                            // --- ??? ---
                            // key ??? key
                            srideIndex = FixSride_O_X_O(key, ZugaraStatusPackReelL1);
                            if (srideIndex == -1) srideIndex = 0;
                        }
                    }
                }
                else if (slotCore.oneGame.buttonCount == 2)
                {
                    if ((ZugaraStatusPackReelL2.FixZugara[0] == key) && (ZugaraStatusPackReelL3.FixZugara[0] == key))
                    {
                        // key key key
                        // --- --- ---
                        // --- --- ---
                        srideIndex = FixSride_O_X_X(key, ZugaraStatusPackReelL1);
                        if (srideIndex == -1) srideIndex = 0;
                    }
                    else if ((ZugaraStatusPackReelL2.FixZugara[1] == key) && (ZugaraStatusPackReelL3.FixZugara[2] == key))
                    {
                        // key --- ---
                        // --- key ---
                        // --- --- key
                        srideIndex = FixSride_O_X_X(key, ZugaraStatusPackReelL1);
                        if (srideIndex == -1) srideIndex = 0;
                    }
                    else if ((ZugaraStatusPackReelL2.FixZugara[1] == key) && (ZugaraStatusPackReelL3.FixZugara[1] == key))
                    {
                        // --- --- ---
                        // key key key
                        // --- --- ---
                        srideIndex = FixSride_X_O_X(key, ZugaraStatusPackReelL1);
                        if (srideIndex == -1) srideIndex = 0;
                    }
                    else if ((ZugaraStatusPackReelL2.FixZugara[1] == key) && (ZugaraStatusPackReelL3.FixZugara[0] == key))
                    {
                        // --- --- key
                        // --- key ---
                        // key --- ---
                        srideIndex = FixSride_X_X_O(key, ZugaraStatusPackReelL1);
                        if (srideIndex == -1) srideIndex = 0;
                    }
                    else if ((ZugaraStatusPackReelL2.FixZugara[2] == key) && (ZugaraStatusPackReelL3.FixZugara[2] == key))
                    {
                        // --- --- ---
                        // --- --- ---
                        // key key key
                        srideIndex = FixSride_X_X_O(key, ZugaraStatusPackReelL1);
                        if (srideIndex == -1) srideIndex = 0;
                    }
                }
                break;
            case "bell":
                key = "bell";
                if (slotCore.oneGame.buttonCount == 0)
                {
                    srideIndex = FixSride_O_O_O(key, ZugaraStatusPackReelL1);
                    if (srideIndex == -1) srideIndex = 0;
                }
                else if (slotCore.oneGame.buttonCount == 1)
                {
                    if (ZugaraStatusPackReelL2.IsFix)
                    {
                        if (ZugaraStatusPackReelL2.FixZugara[0] == key)
                        {
                            // key key ???
                            // --- --- ???
                            // --- --- ???
                            srideIndex = FixSride_O_X_X(key, ZugaraStatusPackReelL1);
                            if (srideIndex == -1) srideIndex = 0;
                        }
                        else if (ZugaraStatusPackReelL2.FixZugara[1] == key)
                        {
                            // key --- ???
                            // key key ???
                            // key --- ???
                            srideIndex = FixSride_O_O_O(key, ZugaraStatusPackReelL1);
                            if (srideIndex == -1) srideIndex = 0;
                        }
                        else if (ZugaraStatusPackReelL2.FixZugara[2] == key)
                        {
                            // --- --- ???
                            // --- --- ???
                            // key key ???
                            srideIndex = FixSride_X_X_O(key, ZugaraStatusPackReelL1);
                            if (srideIndex == -1) srideIndex = 0;
                        }
                    }
                    else
                    {
                        if (ZugaraStatusPackReelL3.FixZugara[0] == key)
                        {
                            // key ??? key
                            // --- ??? ---
                            // key ??? ---
                            srideIndex = FixSride_O_X_O(key, ZugaraStatusPackReelL1);
                            if (srideIndex == -1) srideIndex = 0;
                        }
                        else if (ZugaraStatusPackReelL3.FixZugara[1] == key)
                        {
                            // --- ??? ---
                            // key ??? key
                            // --- ??? ---
                            srideIndex = FixSride_X_O_X(key, ZugaraStatusPackReelL1);
                            if (srideIndex == -1) srideIndex = 0;
                        }
                        else if (ZugaraStatusPackReelL3.FixZugara[2] == key)
                        {
                            // key ??? ---
                            // --- ??? ---
                            // key ??? key
                            srideIndex = FixSride_O_X_O(key, ZugaraStatusPackReelL1);
                            if (srideIndex == -1) srideIndex = 0;
                        }

                    }
                }
                else if (slotCore.oneGame.buttonCount == 2)
                {
                    if ((ZugaraStatusPackReelL2.FixZugara[0] == key) && (ZugaraStatusPackReelL3.FixZugara[0] == key))
                    {
                        // key key key
                        // --- --- ---
                        // --- --- ---
                        srideIndex = FixSride_O_X_X(key, ZugaraStatusPackReelL1);
                        if (srideIndex == -1) srideIndex = 0;
                    }
                    else if ((ZugaraStatusPackReelL2.FixZugara[1] == key) && (ZugaraStatusPackReelL3.FixZugara[2] == key))
                    {
                        // key --- ---
                        // --- key ---
                        // --- --- key
                        srideIndex = FixSride_O_X_X(key, ZugaraStatusPackReelL1);
                        if (srideIndex == -1) srideIndex = 0;
                    }
                    else if ((ZugaraStatusPackReelL2.FixZugara[1] == key) && (ZugaraStatusPackReelL3.FixZugara[1] == key))
                    {
                        // --- --- ---
                        // key key key
                        // --- --- ---
                        srideIndex = FixSride_X_O_X(key, ZugaraStatusPackReelL1);
                        if (srideIndex == -1) srideIndex = 0;
                    }
                    else if ((ZugaraStatusPackReelL2.FixZugara[1] == key) && (ZugaraStatusPackReelL3.FixZugara[0] == key))
                    {
                        // --- --- key
                        // --- key ---
                        // key --- ---
                        srideIndex = FixSride_X_X_O(key, ZugaraStatusPackReelL1);
                        if (srideIndex == -1) srideIndex = 0;
                    }
                    else if ((ZugaraStatusPackReelL2.FixZugara[2] == key) && (ZugaraStatusPackReelL3.FixZugara[2] == key))
                    {
                        // --- --- ---
                        // --- --- ---
                        // key key key
                        srideIndex = FixSride_X_X_O(key, ZugaraStatusPackReelL1);
                        if (srideIndex == -1) srideIndex = 0;
                    }

                }
                break;
            case "suika":
                key = "suika";
                if (slotCore.oneGame.buttonCount == 0)
                {
                    srideIndex = FixSride_O_O_O(key, ZugaraStatusPackReelL1);
                    if (srideIndex == -1) srideIndex = 0;
                }
                else if (slotCore.oneGame.buttonCount == 1)
                {
                    if (ZugaraStatusPackReelL2.IsFix)
                    {
                        if (ZugaraStatusPackReelL2.FixZugara[0] == key)
                        {
                            // key key ???
                            // --- --- ???
                            // --- --- ???
                            srideIndex = FixSride_O_X_X(key, ZugaraStatusPackReelL1);
                            if (srideIndex == -1) srideIndex = 0;
                        }
                        else if (ZugaraStatusPackReelL2.FixZugara[1] == key)
                        {
                            // key --- ???
                            // key key ???
                            // key --- ???
                            srideIndex = FixSride_O_O_O(key, ZugaraStatusPackReelL1);
                            if (srideIndex == -1) srideIndex = 0;
                        }
                        else if (ZugaraStatusPackReelL2.FixZugara[2] == key)
                        {
                            // --- --- ???
                            // --- --- ???
                            // key key ???
                            srideIndex = FixSride_X_X_O(key, ZugaraStatusPackReelL1);
                            if (srideIndex == -1) srideIndex = 0;
                        }
                    }
                    else
                    {
                        if (ZugaraStatusPackReelL3.FixZugara[0] == key)
                        {
                            // key ??? key
                            // --- ??? ---
                            // key ??? ---
                            srideIndex = FixSride_O_X_O(key, ZugaraStatusPackReelL1);
                            if (srideIndex == -1) srideIndex = 0;
                        }
                        else if (ZugaraStatusPackReelL3.FixZugara[1] == key)
                        {
                            // --- ??? ---
                            // key ??? key
                            // --- ??? ---
                            srideIndex = FixSride_X_O_X(key, ZugaraStatusPackReelL1);
                            if (srideIndex == -1) srideIndex = 0;
                        }
                        else if (ZugaraStatusPackReelL3.FixZugara[2] == key)
                        {
                            // key ??? ---
                            // --- ??? ---
                            // key ??? key
                            srideIndex = FixSride_O_X_O(key, ZugaraStatusPackReelL1);
                            if (srideIndex == -1) srideIndex = 0;
                        }

                    }
                }
                else if (slotCore.oneGame.buttonCount == 2)
                {
                    if ((ZugaraStatusPackReelL2.FixZugara[0] == key) && (ZugaraStatusPackReelL3.FixZugara[0] == key))
                    {
                        // key key key
                        // --- --- ---
                        // --- --- ---
                        srideIndex = FixSride_O_X_X(key, ZugaraStatusPackReelL1);
                        if (srideIndex == -1) srideIndex = 0;
                    }
                    else if ((ZugaraStatusPackReelL2.FixZugara[1] == key) && (ZugaraStatusPackReelL3.FixZugara[2] == key))
                    {
                        // key --- ---
                        // --- key ---
                        // --- --- key
                        srideIndex = FixSride_O_X_X(key, ZugaraStatusPackReelL1);
                        if (srideIndex == -1) srideIndex = 0;
                    }
                    else if ((ZugaraStatusPackReelL2.FixZugara[1] == key) && (ZugaraStatusPackReelL3.FixZugara[1] == key))
                    {
                        // --- --- ---
                        // key key key
                        // --- --- ---
                        srideIndex = FixSride_X_O_X(key, ZugaraStatusPackReelL1);
                        if (srideIndex == -1) srideIndex = 0;
                    }
                    else if ((ZugaraStatusPackReelL2.FixZugara[1] == key) && (ZugaraStatusPackReelL3.FixZugara[0] == key))
                    {
                        // --- --- key
                        // --- key ---
                        // key --- ---
                        srideIndex = FixSride_X_X_O(key, ZugaraStatusPackReelL1);
                        if (srideIndex == -1) srideIndex = 0;
                    }
                    else if ((ZugaraStatusPackReelL2.FixZugara[2] == key) && (ZugaraStatusPackReelL3.FixZugara[2] == key))
                    {
                        // --- --- ---
                        // --- --- ---
                        // key key key
                        srideIndex = FixSride_X_X_O(key, ZugaraStatusPackReelL1);
                        if (srideIndex == -1) srideIndex = 0;
                    }

                }
                break;
        }

        if (CheckBadRoleHit(0, srideIndex, ZugaraStatusPackReelL1))
        {
            srideIndex = GetSrideBadRoleHit(0, ZugaraStatusPackReelL1);
            Debug.Log($"GetSrideBadRoleHit L1 {srideIndex} ");
        }

        ZugaraStatusPackReelL1.Fix(srideIndex);
    }

    private void StopRollOneCenter()
    {
        if (ZugaraStatusPackReelL2.IsFix) return;

        var srideIndex = 0;
        var key = "";
        switch (role)
        {
            default:
                switch (bonus)
                {
                    case "Big-r7":
                        key = "r7";
                        if (slotCore.oneGame.buttonCount == 0)
                        {
                            srideIndex = FixSride_O_O_O(key, ZugaraStatusPackReelL2);
                            if (srideIndex == -1) srideIndex = 0;
                        }
                        else if (slotCore.oneGame.buttonCount == 1)
                        {
                            if (ZugaraStatusPackReelL1.IsFix)
                            {
                                if (ZugaraStatusPackReelL1.FixZugara[0] == key)
                                {
                                    // key key ???
                                    // --- key ???
                                    // --- --- ???
                                    srideIndex = FixSride_O_O_X(key, ZugaraStatusPackReelL2);
                                    if (srideIndex == -1) srideIndex = 0;

                                }
                                else if (ZugaraStatusPackReelL1.FixZugara[1] == key)
                                {
                                    // --- --- ???
                                    // key key ???
                                    // --- --- ???
                                    srideIndex = FixSride_X_O_X(key, ZugaraStatusPackReelL2);
                                    if (srideIndex == -1) srideIndex = 0;
                                }
                                else if (ZugaraStatusPackReelL1.FixZugara[2] == key)
                                {
                                    // --- --- ???
                                    // --- key ???
                                    // key key ???
                                    srideIndex = FixSride_X_O_O(key, ZugaraStatusPackReelL2);
                                    if (srideIndex == -1) srideIndex = 0;
                                }
                            }
                            else
                            {
                                if (ZugaraStatusPackReelL3.FixZugara[0] == key)
                                {
                                    // ??? key key
                                    // ??? key ---
                                    // ??? --- ---
                                    srideIndex = FixSride_O_O_X(key, ZugaraStatusPackReelL2);
                                    if (srideIndex == -1) srideIndex = 0;

                                }
                                else if (ZugaraStatusPackReelL3.FixZugara[1] == key)
                                {
                                    // ??? --- ---
                                    // ??? key key
                                    // ??? --- ---
                                    srideIndex = FixSride_X_O_X(key, ZugaraStatusPackReelL2);
                                    if (srideIndex == -1) srideIndex = 0;
                                }
                                else if (ZugaraStatusPackReelL3.FixZugara[2] == key)
                                {
                                    // ??? --- ---
                                    // ??? key ---
                                    // ??? key key
                                    srideIndex = FixSride_X_O_O(key, ZugaraStatusPackReelL2);
                                    if (srideIndex == -1) srideIndex = 0;
                                }
                            }
                        }
                        else if (slotCore.oneGame.buttonCount == 2)
                        {
                            if ((ZugaraStatusPackReelL1.FixZugara[0] == key) && (ZugaraStatusPackReelL3.FixZugara[0] == key))
                            {
                                // key key key
                                // --- --- ---
                                // --- --- ---
                                srideIndex = FixSride_O_X_X(key, ZugaraStatusPackReelL2);
                                if (srideIndex == -1) srideIndex = 0;
                            }
                            else if ((ZugaraStatusPackReelL1.FixZugara[0] == key) && (ZugaraStatusPackReelL3.FixZugara[2] == key))
                            {
                                // key --- ---
                                // --- key ---
                                // --- --- key
                                srideIndex = FixSride_X_O_X(key, ZugaraStatusPackReelL2);
                                if (srideIndex == -1) srideIndex = 0;
                            }
                            else if ((ZugaraStatusPackReelL1.FixZugara[1] == key) && (ZugaraStatusPackReelL3.FixZugara[1] == key))
                            {
                                // --- --- ---
                                // key key key
                                // --- --- ---
                                srideIndex = FixSride_X_O_X(key, ZugaraStatusPackReelL2);
                                if (srideIndex == -1) srideIndex = 0;
                            }
                            else if ((ZugaraStatusPackReelL1.FixZugara[2] == key) && (ZugaraStatusPackReelL3.FixZugara[0] == key))
                            {
                                // --- --- key
                                // --- key ---
                                // key --- ---
                                srideIndex = FixSride_X_O_X(key, ZugaraStatusPackReelL2);
                                if (srideIndex == -1) srideIndex = 0;
                            }
                            else if ((ZugaraStatusPackReelL1.FixZugara[2] == key) && (ZugaraStatusPackReelL3.FixZugara[2] == key))
                            {
                                // --- --- ---
                                // --- --- ---
                                // key key key
                                srideIndex = FixSride_X_X_O(key, ZugaraStatusPackReelL2);
                                if (srideIndex == -1) srideIndex = 0;
                            }

                        }
                        break;
                    case "Big-b7":
                        key = "b7";
                        if (slotCore.oneGame.buttonCount == 0)
                        {
                            srideIndex = FixSride_O_O_O(key, ZugaraStatusPackReelL2);
                            if (srideIndex == -1) srideIndex = 0;
                        }
                        else if (slotCore.oneGame.buttonCount == 1)
                        {
                            if (ZugaraStatusPackReelL1.IsFix)
                            {
                                if (ZugaraStatusPackReelL1.FixZugara[0] == key)
                                {
                                    // key key ???
                                    // --- key ???
                                    // --- --- ???
                                    srideIndex = FixSride_O_O_X(key, ZugaraStatusPackReelL2);
                                    if (srideIndex == -1) srideIndex = 0;

                                }
                                else if (ZugaraStatusPackReelL1.FixZugara[1] == key)
                                {
                                    // --- --- ???
                                    // key key ???
                                    // --- --- ???
                                    srideIndex = FixSride_X_O_X(key, ZugaraStatusPackReelL2);
                                    if (srideIndex == -1) srideIndex = 0;
                                }
                                else if (ZugaraStatusPackReelL1.FixZugara[2] == key)
                                {
                                    // --- --- ???
                                    // --- key ???
                                    // key key ???
                                    srideIndex = FixSride_X_O_O(key, ZugaraStatusPackReelL2);
                                    if (srideIndex == -1) srideIndex = 0;
                                }
                            }
                            else
                            {
                                if (ZugaraStatusPackReelL3.FixZugara[0] == key)
                                {
                                    // ??? key key
                                    // ??? key ---
                                    // ??? --- ---
                                    srideIndex = FixSride_O_O_X(key, ZugaraStatusPackReelL2);
                                    if (srideIndex == -1) srideIndex = 0;

                                }
                                else if (ZugaraStatusPackReelL3.FixZugara[1] == key)
                                {
                                    // ??? --- ---
                                    // ??? key key
                                    // ??? --- ---
                                    srideIndex = FixSride_X_O_X(key, ZugaraStatusPackReelL2);
                                    if (srideIndex == -1) srideIndex = 0;
                                }
                                else if (ZugaraStatusPackReelL3.FixZugara[2] == key)
                                {
                                    // ??? --- ---
                                    // ??? key ---
                                    // ??? key key
                                    srideIndex = FixSride_X_O_O(key, ZugaraStatusPackReelL2);
                                    if (srideIndex == -1) srideIndex = 0;
                                }
                            }
                        }
                        else if (slotCore.oneGame.buttonCount == 2)
                        {
                            if ((ZugaraStatusPackReelL1.FixZugara[0] == key) && (ZugaraStatusPackReelL3.FixZugara[0] == key))
                            {
                                // key key key
                                // --- --- ---
                                // --- --- ---
                                srideIndex = FixSride_O_X_X(key, ZugaraStatusPackReelL2);
                                if (srideIndex == -1) srideIndex = 0;
                            }
                            else if ((ZugaraStatusPackReelL1.FixZugara[0] == key) && (ZugaraStatusPackReelL3.FixZugara[2] == key))
                            {
                                // key --- ---
                                // --- key ---
                                // --- --- key
                                srideIndex = FixSride_X_O_X(key, ZugaraStatusPackReelL2);
                                if (srideIndex == -1) srideIndex = 0;
                            }
                            else if ((ZugaraStatusPackReelL1.FixZugara[1] == key) && (ZugaraStatusPackReelL3.FixZugara[1] == key))
                            {
                                // --- --- ---
                                // key key key
                                // --- --- ---
                                srideIndex = FixSride_X_O_X(key, ZugaraStatusPackReelL2);
                                if (srideIndex == -1) srideIndex = 0;
                            }
                            else if ((ZugaraStatusPackReelL1.FixZugara[2] == key) && (ZugaraStatusPackReelL3.FixZugara[0] == key))
                            {
                                // --- --- key
                                // --- key ---
                                // key --- ---
                                srideIndex = FixSride_X_O_X(key, ZugaraStatusPackReelL2);
                                if (srideIndex == -1) srideIndex = 0;
                            }
                            else if ((ZugaraStatusPackReelL1.FixZugara[2] == key) && (ZugaraStatusPackReelL3.FixZugara[2] == key))
                            {
                                // --- --- ---
                                // --- --- ---
                                // key key key
                                srideIndex = FixSride_X_X_O(key, ZugaraStatusPackReelL2);
                                if (srideIndex == -1) srideIndex = 0;
                            }

                        }
                        break;
                    case "Reg":
                        key = "r7";
                        if (slotCore.oneGame.buttonCount == 0)
                        {
                            srideIndex = FixSride_O_O_O(key, ZugaraStatusPackReelL2);
                            if (srideIndex == -1) srideIndex = 0;
                        }
                        else if (slotCore.oneGame.buttonCount == 1)
                        {
                            if (ZugaraStatusPackReelL1.IsFix)
                            {
                                if (ZugaraStatusPackReelL1.FixZugara[0] == key)
                                {
                                    // key key ???
                                    // --- key ???
                                    // --- --- ???
                                    srideIndex = FixSride_O_O_X(key, ZugaraStatusPackReelL2);
                                    if (srideIndex == -1) srideIndex = 0;

                                }
                                else if (ZugaraStatusPackReelL1.FixZugara[1] == key)
                                {
                                    // --- --- ???
                                    // key key ???
                                    // --- --- ???
                                    srideIndex = FixSride_X_O_X(key, ZugaraStatusPackReelL2);
                                    if (srideIndex == -1) srideIndex = 0;
                                }
                                else if (ZugaraStatusPackReelL1.FixZugara[2] == key)
                                {
                                    // --- --- ???
                                    // --- key ???
                                    // key key ???
                                    srideIndex = FixSride_X_O_O(key, ZugaraStatusPackReelL2);
                                    if (srideIndex == -1) srideIndex = 0;
                                }
                            }
                            else
                            {
                                if (ZugaraStatusPackReelL3.FixZugara[0] == "bar")
                                {
                                    // ??? key key
                                    // ??? key ---
                                    // ??? --- ---
                                    srideIndex = FixSride_O_O_X(key, ZugaraStatusPackReelL2);
                                    if (srideIndex == -1) srideIndex = 0;

                                }
                                else if (ZugaraStatusPackReelL3.FixZugara[1] == "bar")
                                {
                                    // ??? --- ---
                                    // ??? key key
                                    // ??? --- ---
                                    srideIndex = FixSride_X_O_X(key, ZugaraStatusPackReelL2);
                                    if (srideIndex == -1) srideIndex = 0;
                                }
                                else if (ZugaraStatusPackReelL3.FixZugara[2] == "bar")
                                {
                                    // ??? --- ---
                                    // ??? key ---
                                    // ??? key key
                                    srideIndex = FixSride_X_O_O(key, ZugaraStatusPackReelL2);
                                    if (srideIndex == -1) srideIndex = 0;
                                }
                            }
                        }
                        else if (slotCore.oneGame.buttonCount == 2)
                        {
                            if ((ZugaraStatusPackReelL1.FixZugara[0] == key) && (ZugaraStatusPackReelL3.FixZugara[0] == "bar"))
                            {
                                // key key key
                                // --- --- ---
                                // --- --- ---
                                srideIndex = FixSride_O_X_X(key, ZugaraStatusPackReelL2);
                                if (srideIndex == -1) srideIndex = 0;
                            }
                            else if ((ZugaraStatusPackReelL1.FixZugara[0] == key) && (ZugaraStatusPackReelL3.FixZugara[2] == "bar"))
                            {
                                // key --- ---
                                // --- key ---
                                // --- --- key
                                srideIndex = FixSride_X_O_X(key, ZugaraStatusPackReelL2);
                                if (srideIndex == -1) srideIndex = 0;
                            }
                            else if ((ZugaraStatusPackReelL1.FixZugara[1] == key) && (ZugaraStatusPackReelL3.FixZugara[1] == "bar"))
                            {
                                // --- --- ---
                                // key key key
                                // --- --- ---
                                srideIndex = FixSride_X_O_X(key, ZugaraStatusPackReelL2);
                                if (srideIndex == -1) srideIndex = 0;
                            }
                            else if ((ZugaraStatusPackReelL1.FixZugara[2] == key) && (ZugaraStatusPackReelL3.FixZugara[0] == "bar"))
                            {
                                // --- --- key
                                // --- key ---
                                // key --- ---
                                srideIndex = FixSride_X_O_X(key, ZugaraStatusPackReelL2);
                                if (srideIndex == -1) srideIndex = 0;
                            }
                            else if ((ZugaraStatusPackReelL1.FixZugara[2] == key) && (ZugaraStatusPackReelL3.FixZugara[2] == "bar"))
                            {
                                // --- --- ---
                                // --- --- ---
                                // key key key
                                srideIndex = FixSride_X_X_O(key, ZugaraStatusPackReelL2);
                                if (srideIndex == -1) srideIndex = 0;
                            }

                        }
                        break;
                }
                break;
            case "rep":
                key = "rep";
                if (slotCore.oneGame.buttonCount == 0)
                {
                    srideIndex = FixSride_O_O_O(key, ZugaraStatusPackReelL2);
                    if (srideIndex == -1) srideIndex = 0;
                }
                else if (slotCore.oneGame.buttonCount == 1)
                {
                    if (ZugaraStatusPackReelL1.IsFix)
                    {
                        if (ZugaraStatusPackReelL1.FixZugara[0] == key)
                        {
                            // key key ???
                            // --- key ???
                            // --- --- ???
                            srideIndex = FixSride_O_O_X(key, ZugaraStatusPackReelL2);
                            if (srideIndex == -1) srideIndex = 0;

                        }
                        else if (ZugaraStatusPackReelL1.FixZugara[1] == key)
                        {
                            // --- --- ???
                            // key key ???
                            // --- --- ???
                            srideIndex = FixSride_X_O_X(key, ZugaraStatusPackReelL2);
                            if (srideIndex == -1) srideIndex = 0;
                        }
                        else if (ZugaraStatusPackReelL1.FixZugara[2] == key)
                        {
                            // --- --- ???
                            // --- key ???
                            // key key ???
                            srideIndex = FixSride_X_O_O(key, ZugaraStatusPackReelL2);
                            if (srideIndex == -1) srideIndex = 0;
                        }
                    }
                    else
                    {
                        if (ZugaraStatusPackReelL3.FixZugara[0] == key)
                        {
                            // ??? key key
                            // ??? key ---
                            // ??? --- ---
                            srideIndex = FixSride_O_O_X(key, ZugaraStatusPackReelL2);
                            if (srideIndex == -1) srideIndex = 0;

                        }
                        else if (ZugaraStatusPackReelL3.FixZugara[1] == key)
                        {
                            // ??? --- ---
                            // ??? key key
                            // ??? --- ---
                            srideIndex = FixSride_X_O_X(key, ZugaraStatusPackReelL2);
                            if (srideIndex == -1) srideIndex = 0;
                        }
                        else if (ZugaraStatusPackReelL3.FixZugara[2] == key)
                        {
                            // ??? --- ---
                            // ??? key ---
                            // ??? key key
                            srideIndex = FixSride_X_O_O(key, ZugaraStatusPackReelL2);
                            if (srideIndex == -1) srideIndex = 0;
                        }
                    }
                }
                else if (slotCore.oneGame.buttonCount == 2)
                {
                    if ((ZugaraStatusPackReelL1.FixZugara[0] == key) && (ZugaraStatusPackReelL3.FixZugara[0] == key))
                    {
                        // key key key
                        // --- --- ---
                        // --- --- ---
                        srideIndex = FixSride_O_X_X(key, ZugaraStatusPackReelL2);
                        if (srideIndex == -1) srideIndex = 0;
                    }
                    else if ((ZugaraStatusPackReelL1.FixZugara[0] == key) && (ZugaraStatusPackReelL3.FixZugara[2] == key))
                    {
                        // key --- ---
                        // --- key ---
                        // --- --- key
                        srideIndex = FixSride_X_O_X(key, ZugaraStatusPackReelL2);
                        if (srideIndex == -1) srideIndex = 0;
                    }
                    else if ((ZugaraStatusPackReelL1.FixZugara[1] == key) && (ZugaraStatusPackReelL3.FixZugara[1] == key))
                    {
                        // --- --- ---
                        // key key key
                        // --- --- ---
                        srideIndex = FixSride_X_O_X(key, ZugaraStatusPackReelL2);
                        if (srideIndex == -1) srideIndex = 0;
                    }
                    else if ((ZugaraStatusPackReelL1.FixZugara[2] == key) && (ZugaraStatusPackReelL3.FixZugara[0] == key))
                    {
                        // --- --- key
                        // --- key ---
                        // key --- ---
                        srideIndex = FixSride_X_O_X(key, ZugaraStatusPackReelL2);
                        if (srideIndex == -1) srideIndex = 0;
                    }
                    else if ((ZugaraStatusPackReelL1.FixZugara[2] == key) && (ZugaraStatusPackReelL3.FixZugara[2] == key))
                    {
                        // --- --- ---
                        // --- --- ---
                        // key key key
                        srideIndex = FixSride_X_X_O(key, ZugaraStatusPackReelL2);
                        if (srideIndex == -1) srideIndex = 0;
                    }

                }
                break;
            case "bell":
                key = "bell";
                if (slotCore.oneGame.buttonCount == 0)
                {
                    srideIndex = FixSride_O_O_O(key, ZugaraStatusPackReelL2);
                    if (srideIndex == -1) srideIndex = 0;
                }
                else if (slotCore.oneGame.buttonCount == 1)
                {
                    if (ZugaraStatusPackReelL1.IsFix)
                    {
                        if (ZugaraStatusPackReelL1.FixZugara[0] == key)
                        {
                            // key key ???
                            // --- key ???
                            // --- --- ???
                            srideIndex = FixSride_O_O_X(key, ZugaraStatusPackReelL2);
                            if (srideIndex == -1) srideIndex = 0;

                        }
                        else if (ZugaraStatusPackReelL1.FixZugara[1] == key)
                        {
                            // --- --- ???
                            // key key ???
                            // --- --- ???
                            srideIndex = FixSride_X_O_X(key, ZugaraStatusPackReelL2);
                            if (srideIndex == -1) srideIndex = 0;
                        }
                        else if (ZugaraStatusPackReelL1.FixZugara[2] == key)
                        {
                            // --- --- ???
                            // --- key ???
                            // key key ???
                            srideIndex = FixSride_X_O_O(key, ZugaraStatusPackReelL2);
                            if (srideIndex == -1) srideIndex = 0;
                        }
                    }
                    else
                    {
                        if (ZugaraStatusPackReelL3.FixZugara[0] == key)
                        {
                            // ??? key key
                            // ??? key ---
                            // ??? --- ---
                            srideIndex = FixSride_O_O_X(key, ZugaraStatusPackReelL2);
                            if (srideIndex == -1) srideIndex = 0;

                        }
                        else if (ZugaraStatusPackReelL3.FixZugara[1] == key)
                        {
                            // ??? --- ---
                            // ??? key key
                            // ??? --- ---
                            srideIndex = FixSride_X_O_X(key, ZugaraStatusPackReelL2);
                            if (srideIndex == -1) srideIndex = 0;
                        }
                        else if (ZugaraStatusPackReelL3.FixZugara[2] == key)
                        {
                            // ??? --- ---
                            // ??? key ---
                            // ??? key key
                            srideIndex = FixSride_X_O_O(key, ZugaraStatusPackReelL2);
                            if (srideIndex == -1) srideIndex = 0;
                        }
                    }
                }
                else if (slotCore.oneGame.buttonCount == 2)
                {
                    if ((ZugaraStatusPackReelL1.FixZugara[0] == key) && (ZugaraStatusPackReelL3.FixZugara[0] == key))
                    {
                        // key key key
                        // --- --- ---
                        // --- --- ---
                        srideIndex = FixSride_O_X_X(key, ZugaraStatusPackReelL2);
                        if (srideIndex == -1) srideIndex = 0;
                    }
                    else if ((ZugaraStatusPackReelL1.FixZugara[0] == key) && (ZugaraStatusPackReelL3.FixZugara[2] == key))
                    {
                        // key --- ---
                        // --- key ---
                        // --- --- key
                        srideIndex = FixSride_X_O_X(key, ZugaraStatusPackReelL2);
                        if (srideIndex == -1) srideIndex = 0;
                    }
                    else if ((ZugaraStatusPackReelL1.FixZugara[1] == key) && (ZugaraStatusPackReelL3.FixZugara[1] == key))
                    {
                        // --- --- ---
                        // key key key
                        // --- --- ---
                        srideIndex = FixSride_X_O_X(key, ZugaraStatusPackReelL2);
                        if (srideIndex == -1) srideIndex = 0;
                    }
                    else if ((ZugaraStatusPackReelL1.FixZugara[2] == key) && (ZugaraStatusPackReelL3.FixZugara[0] == key))
                    {
                        // --- --- key
                        // --- key ---
                        // key --- ---
                        srideIndex = FixSride_X_O_X(key, ZugaraStatusPackReelL2);
                        if (srideIndex == -1) srideIndex = 0;
                    }
                    else if ((ZugaraStatusPackReelL1.FixZugara[2] == key) && (ZugaraStatusPackReelL3.FixZugara[2] == key))
                    {
                        // --- --- ---
                        // --- --- ---
                        // key key key
                        srideIndex = FixSride_X_X_O(key, ZugaraStatusPackReelL2);
                        if (srideIndex == -1) srideIndex = 0;
                    }

                }
                break;
            case "suika":
                key = "suika";
                if (slotCore.oneGame.buttonCount == 0)
                {
                    srideIndex = FixSride_O_O_O(key, ZugaraStatusPackReelL2);
                    if (srideIndex == -1) srideIndex = 0;
                }
                else if (slotCore.oneGame.buttonCount == 1)
                {
                    if (ZugaraStatusPackReelL1.IsFix)
                    {
                        if (ZugaraStatusPackReelL1.FixZugara[0] == key)
                        {
                            // key key ???
                            // --- key ???
                            // --- --- ???
                            srideIndex = FixSride_O_O_X(key, ZugaraStatusPackReelL2);
                            if (srideIndex == -1) srideIndex = 0;

                        }
                        else if (ZugaraStatusPackReelL1.FixZugara[1] == key)
                        {
                            // --- --- ???
                            // key key ???
                            // --- --- ???
                            srideIndex = FixSride_X_O_X(key, ZugaraStatusPackReelL2);
                            if (srideIndex == -1) srideIndex = 0;
                        }
                        else if (ZugaraStatusPackReelL1.FixZugara[2] == key)
                        {
                            // --- --- ???
                            // --- key ???
                            // key key ???
                            srideIndex = FixSride_X_O_O(key, ZugaraStatusPackReelL2);
                            if (srideIndex == -1) srideIndex = 0;
                        }
                    }
                    else
                    {
                        if (ZugaraStatusPackReelL3.FixZugara[0] == key)
                        {
                            // ??? key key
                            // ??? key ---
                            // ??? --- ---
                            srideIndex = FixSride_O_O_X(key, ZugaraStatusPackReelL2);
                            if (srideIndex == -1) srideIndex = 0;

                        }
                        else if (ZugaraStatusPackReelL3.FixZugara[1] == key)
                        {
                            // ??? --- ---
                            // ??? key key
                            // ??? --- ---
                            srideIndex = FixSride_X_O_X(key, ZugaraStatusPackReelL2);
                            if (srideIndex == -1) srideIndex = 0;
                        }
                        else if (ZugaraStatusPackReelL3.FixZugara[2] == key)
                        {
                            // ??? --- ---
                            // ??? key ---
                            // ??? key key
                            srideIndex = FixSride_X_O_O(key, ZugaraStatusPackReelL2);
                            if (srideIndex == -1) srideIndex = 0;
                        }
                    }
                }
                else if (slotCore.oneGame.buttonCount == 2)
                {
                    if ((ZugaraStatusPackReelL1.FixZugara[0] == key) && (ZugaraStatusPackReelL3.FixZugara[0] == key))
                    {
                        // key key key
                        // --- --- ---
                        // --- --- ---
                        srideIndex = FixSride_O_X_X(key, ZugaraStatusPackReelL2);
                        if (srideIndex == -1) srideIndex = 0;
                    }
                    else if ((ZugaraStatusPackReelL1.FixZugara[0] == key) && (ZugaraStatusPackReelL3.FixZugara[2] == key))
                    {
                        // key --- ---
                        // --- key ---
                        // --- --- key
                        srideIndex = FixSride_X_O_X(key, ZugaraStatusPackReelL2);
                        if (srideIndex == -1) srideIndex = 0;
                    }
                    else if ((ZugaraStatusPackReelL1.FixZugara[1] == key) && (ZugaraStatusPackReelL3.FixZugara[1] == key))
                    {
                        // --- --- ---
                        // key key key
                        // --- --- ---
                        srideIndex = FixSride_X_O_X(key, ZugaraStatusPackReelL2);
                        if (srideIndex == -1) srideIndex = 0;
                    }
                    else if ((ZugaraStatusPackReelL1.FixZugara[2] == key) && (ZugaraStatusPackReelL3.FixZugara[0] == key))
                    {
                        // --- --- key
                        // --- key ---
                        // key --- ---
                        srideIndex = FixSride_X_O_X(key, ZugaraStatusPackReelL2);
                        if (srideIndex == -1) srideIndex = 0;
                    }
                    else if ((ZugaraStatusPackReelL1.FixZugara[2] == key) && (ZugaraStatusPackReelL3.FixZugara[2] == key))
                    {
                        // --- --- ---
                        // --- --- ---
                        // key key key
                        srideIndex = FixSride_X_X_O(key, ZugaraStatusPackReelL2);
                        if (srideIndex == -1) srideIndex = 0;
                    }

                }
                break;
        }

        if (CheckBadRoleHit(1, srideIndex, ZugaraStatusPackReelL2))
        {
            srideIndex = GetSrideBadRoleHit(1, ZugaraStatusPackReelL2);
            Debug.Log($"GetSrideBadRoleHit L2 {srideIndex} ");
        }

        ZugaraStatusPackReelL2.Fix(srideIndex);
    }

    private void StopRollOneRight()
    {
        if (ZugaraStatusPackReelL3.IsFix) return;

        var srideIndex = 0;
        var key = "";
        switch (role)
        {
            default:
                switch (bonus)
                {
                    case "Big-r7":
                        key = "r7";
                        if (slotCore.oneGame.buttonCount == 0)
                        {
                            srideIndex = FixSride_O_O_O(key, ZugaraStatusPackReelL3);
                            if (srideIndex == -1) srideIndex = 0;
                        }
                        else if (slotCore.oneGame.buttonCount == 1)
                        {
                            if (ZugaraStatusPackReelL2.IsFix)
                            {
                                if (ZugaraStatusPackReelL2.FixZugara[0] == key)
                                {
                                    // ??? key key
                                    // ??? --- ---
                                    // ??? --- ---
                                    srideIndex = FixSride_O_X_X(key, ZugaraStatusPackReelL3);
                                    if (srideIndex == -1) srideIndex = 0;
                                }
                                else if (ZugaraStatusPackReelL2.FixZugara[1] == key)
                                {
                                    // ??? --- key
                                    // ??? key key
                                    // ??? --- key
                                    srideIndex = FixSride_O_O_O(key, ZugaraStatusPackReelL3);
                                    if (srideIndex == -1) srideIndex = 0;
                                }
                                else if (ZugaraStatusPackReelL2.FixZugara[2] == key)
                                {
                                    // ??? --- ---
                                    // ??? --- ---
                                    // ??? key key
                                    srideIndex = FixSride_X_X_O(key, ZugaraStatusPackReelL3);
                                    if (srideIndex == -1) srideIndex = 0;
                                }
                            }
                            else
                            {
                                if (ZugaraStatusPackReelL1.FixZugara[0] == key)
                                {
                                    // key ??? key
                                    // --- ??? ---
                                    // --- ??? key
                                    srideIndex = FixSride_O_X_O(key, ZugaraStatusPackReelL3);
                                    if (srideIndex == -1) srideIndex = 0;
                                }
                                else if (ZugaraStatusPackReelL1.FixZugara[1] == key)
                                {
                                    // --- ??? ---
                                    // key ??? key
                                    // --- ??? ---
                                    srideIndex = FixSride_X_O_X(key, ZugaraStatusPackReelL3);
                                    if (srideIndex == -1) srideIndex = 0;
                                }
                                else if (ZugaraStatusPackReelL2.FixZugara[2] == key)
                                {
                                    // --- ??? key
                                    // --- ??? ---
                                    // key ??? key
                                    srideIndex = FixSride_O_X_O(key, ZugaraStatusPackReelL3);
                                    if (srideIndex == -1) srideIndex = 0;
                                }

                            }
                        }
                        else if (slotCore.oneGame.buttonCount == 2)
                        {
                            if ((ZugaraStatusPackReelL1.FixZugara[0] == key) && (ZugaraStatusPackReelL2.FixZugara[0] == key))
                            {
                                // key key key
                                // --- --- ---
                                // --- --- ---
                                srideIndex = FixSride_O_X_X(key, ZugaraStatusPackReelL3);
                                if (srideIndex == -1) srideIndex = 0;
                            }
                            else if ((ZugaraStatusPackReelL1.FixZugara[0] == key) && (ZugaraStatusPackReelL2.FixZugara[1] == key))
                            {
                                // key --- ---
                                // --- key ---
                                // --- --- key
                                srideIndex = FixSride_X_X_O(key, ZugaraStatusPackReelL3); // todo : 不具合ありそう、赤7がそろわないことがあった、はじいてるかも
                                if (srideIndex == -1) srideIndex = 0;
                            }
                            else if ((ZugaraStatusPackReelL1.FixZugara[1] == key) && (ZugaraStatusPackReelL2.FixZugara[1] == key))
                            {
                                // --- --- ---
                                // key key key
                                // --- --- ---
                                srideIndex = FixSride_X_O_X(key, ZugaraStatusPackReelL3);
                                if (srideIndex == -1) srideIndex = 0;
                            }
                            else if ((ZugaraStatusPackReelL1.FixZugara[2] == key) && (ZugaraStatusPackReelL2.FixZugara[1] == key))
                            {
                                // --- --- key
                                // --- key ---
                                // key --- ---
                                srideIndex = FixSride_O_X_X(key, ZugaraStatusPackReelL3);
                                if (srideIndex == -1) srideIndex = 0;
                            }
                            else if ((ZugaraStatusPackReelL1.FixZugara[2] == key) && (ZugaraStatusPackReelL2.FixZugara[2] == key))
                            {
                                // --- --- ---
                                // --- --- ---
                                // key key key
                                srideIndex = FixSride_X_X_O(key, ZugaraStatusPackReelL3);
                                if (srideIndex == -1) srideIndex = 0;
                            }
                        }
                        break;
                    case "Big-b7":
                        key = "b7";
                        if (slotCore.oneGame.buttonCount == 0)
                        {
                            srideIndex = FixSride_O_O_O(key, ZugaraStatusPackReelL3);
                            if (srideIndex == -1) srideIndex = 0;
                        }
                        else if (slotCore.oneGame.buttonCount == 1)
                        {
                            if (ZugaraStatusPackReelL2.IsFix)
                            {
                                if (ZugaraStatusPackReelL2.FixZugara[0] == key)
                                {
                                    // ??? key key
                                    // ??? --- ---
                                    // ??? --- ---
                                    srideIndex = FixSride_O_X_X(key, ZugaraStatusPackReelL3);
                                    if (srideIndex == -1) srideIndex = 0;
                                }
                                else if (ZugaraStatusPackReelL2.FixZugara[1] == key)
                                {
                                    // ??? --- key
                                    // ??? key key
                                    // ??? --- key
                                    srideIndex = FixSride_O_O_O(key, ZugaraStatusPackReelL3);
                                    if (srideIndex == -1) srideIndex = 0;
                                }
                                else if (ZugaraStatusPackReelL2.FixZugara[2] == key)
                                {
                                    // ??? --- ---
                                    // ??? --- ---
                                    // ??? key key
                                    srideIndex = FixSride_X_X_O(key, ZugaraStatusPackReelL3);
                                    if (srideIndex == -1) srideIndex = 0;
                                }
                            }
                            else
                            {
                                if (ZugaraStatusPackReelL1.FixZugara[0] == key)
                                {
                                    // key ??? key
                                    // --- ??? ---
                                    // --- ??? key
                                    srideIndex = FixSride_O_X_O(key, ZugaraStatusPackReelL3);
                                    if (srideIndex == -1) srideIndex = 0;
                                }
                                else if (ZugaraStatusPackReelL1.FixZugara[1] == key)
                                {
                                    // --- ??? ---
                                    // key ??? key
                                    // --- ??? ---
                                    srideIndex = FixSride_X_O_X(key, ZugaraStatusPackReelL3);
                                    if (srideIndex == -1) srideIndex = 0;
                                }
                                else if (ZugaraStatusPackReelL2.FixZugara[2] == key)
                                {
                                    // --- ??? key
                                    // --- ??? ---
                                    // key ??? key
                                    srideIndex = FixSride_O_X_O(key, ZugaraStatusPackReelL3);
                                    if (srideIndex == -1) srideIndex = 0;
                                }

                            }
                        }
                        else if (slotCore.oneGame.buttonCount == 2)
                        {
                            if ((ZugaraStatusPackReelL1.FixZugara[0] == key) && (ZugaraStatusPackReelL2.FixZugara[0] == key))
                            {
                                // key key key
                                // --- --- ---
                                // --- --- ---
                                srideIndex = FixSride_O_X_X(key, ZugaraStatusPackReelL3);
                                if (srideIndex == -1) srideIndex = 0;
                            }
                            else if ((ZugaraStatusPackReelL1.FixZugara[0] == key) && (ZugaraStatusPackReelL2.FixZugara[1] == key))
                            {
                                // key --- ---
                                // --- key ---
                                // --- --- key
                                srideIndex = FixSride_X_X_O(key, ZugaraStatusPackReelL3);
                                if (srideIndex == -1) srideIndex = 0;
                            }
                            else if ((ZugaraStatusPackReelL1.FixZugara[1] == key) && (ZugaraStatusPackReelL2.FixZugara[1] == key))
                            {
                                // --- --- ---
                                // key key key
                                // --- --- ---
                                srideIndex = FixSride_X_O_X(key, ZugaraStatusPackReelL3);
                                if (srideIndex == -1) srideIndex = 0;
                            }
                            else if ((ZugaraStatusPackReelL1.FixZugara[2] == key) && (ZugaraStatusPackReelL2.FixZugara[1] == key))
                            {
                                // --- --- key
                                // --- key ---
                                // key --- ---
                                srideIndex = FixSride_O_X_X(key, ZugaraStatusPackReelL3);
                                if (srideIndex == -1) srideIndex = 0;
                            }
                            else if ((ZugaraStatusPackReelL1.FixZugara[2] == key) && (ZugaraStatusPackReelL2.FixZugara[2] == key))
                            {
                                // --- --- ---
                                // --- --- ---
                                // key key key
                                srideIndex = FixSride_X_X_O(key, ZugaraStatusPackReelL3);
                                if (srideIndex == -1) srideIndex = 0;
                            }
                        }
                        break;
                    case "Reg":
                        key = "bar";
                        if (slotCore.oneGame.buttonCount == 0)
                        {
                            srideIndex = FixSride_O_O_O(key, ZugaraStatusPackReelL3);
                            if (srideIndex == -1) srideIndex = 0;
                        }
                        else if (slotCore.oneGame.buttonCount == 1)
                        {
                            if (ZugaraStatusPackReelL2.IsFix)
                            {
                                if (ZugaraStatusPackReelL2.FixZugara[0] == "r7")
                                {
                                    // ??? key key
                                    // ??? --- ---
                                    // ??? --- ---
                                    srideIndex = FixSride_O_X_X(key, ZugaraStatusPackReelL3);
                                    if (srideIndex == -1) srideIndex = 0;
                                }
                                else if (ZugaraStatusPackReelL2.FixZugara[1] == "r7")
                                {
                                    // ??? --- key
                                    // ??? key key
                                    // ??? --- key
                                    srideIndex = FixSride_O_O_O(key, ZugaraStatusPackReelL3);
                                    if (srideIndex == -1) srideIndex = 0;
                                }
                                else if (ZugaraStatusPackReelL2.FixZugara[2] == "r7")
                                {
                                    // ??? --- ---
                                    // ??? --- ---
                                    // ??? key key
                                    srideIndex = FixSride_X_X_O(key, ZugaraStatusPackReelL3);
                                    if (srideIndex == -1) srideIndex = 0;
                                }
                            }
                            else
                            {
                                if (ZugaraStatusPackReelL1.FixZugara[0] == "r7")
                                {
                                    // key ??? key
                                    // --- ??? ---
                                    // --- ??? key
                                    srideIndex = FixSride_O_X_O(key, ZugaraStatusPackReelL3);
                                    if (srideIndex == -1) srideIndex = 0;
                                }
                                else if (ZugaraStatusPackReelL1.FixZugara[1] == "r7")
                                {
                                    // --- ??? ---
                                    // key ??? key
                                    // --- ??? ---
                                    srideIndex = FixSride_X_O_X(key, ZugaraStatusPackReelL3);
                                    if (srideIndex == -1) srideIndex = 0;
                                }
                                else if (ZugaraStatusPackReelL2.FixZugara[2] == "r7")
                                {
                                    // --- ??? key
                                    // --- ??? ---
                                    // key ??? key
                                    srideIndex = FixSride_O_X_O(key, ZugaraStatusPackReelL3);
                                    if (srideIndex == -1) srideIndex = 0;
                                }

                            }
                        }
                        else if (slotCore.oneGame.buttonCount == 2)
                        {
                            if ((ZugaraStatusPackReelL1.FixZugara[0] == "r7") && (ZugaraStatusPackReelL2.FixZugara[0] == "r7"))
                            {
                                // key key key
                                // --- --- ---
                                // --- --- ---
                                srideIndex = FixSride_O_X_X(key, ZugaraStatusPackReelL3);
                                if (srideIndex == -1) srideIndex = 0;
                            }
                            else if ((ZugaraStatusPackReelL1.FixZugara[0] == "r7") && (ZugaraStatusPackReelL2.FixZugara[1] == "r7"))
                            {
                                // key --- ---
                                // --- key ---
                                // --- --- key
                                srideIndex = FixSride_X_X_O(key, ZugaraStatusPackReelL3);
                                if (srideIndex == -1) srideIndex = 0;
                            }
                            else if ((ZugaraStatusPackReelL1.FixZugara[1] == "r7") && (ZugaraStatusPackReelL2.FixZugara[1] == "r7"))
                            {
                                // --- --- ---
                                // key key key
                                // --- --- ---
                                srideIndex = FixSride_X_O_X(key, ZugaraStatusPackReelL3);
                                if (srideIndex == -1) srideIndex = 0;
                            }
                            else if ((ZugaraStatusPackReelL1.FixZugara[2] == "r7") && (ZugaraStatusPackReelL2.FixZugara[1] == "r7"))
                            {
                                // --- --- key
                                // --- key ---
                                // key --- ---
                                srideIndex = FixSride_O_X_X(key, ZugaraStatusPackReelL3);
                                if (srideIndex == -1) srideIndex = 0;
                            }
                            else if ((ZugaraStatusPackReelL1.FixZugara[2] == "r7") && (ZugaraStatusPackReelL2.FixZugara[2] == "r7"))
                            {
                                // --- --- ---
                                // --- --- ---
                                // key key key
                                srideIndex = FixSride_X_X_O(key, ZugaraStatusPackReelL3);
                                if (srideIndex == -1) srideIndex = 0;
                            }
                        }
                        break;
                }
                break;
            case "rep":
                key = "rep";
                if (slotCore.oneGame.buttonCount == 0)
                {
                    srideIndex = FixSride_O_O_O(key, ZugaraStatusPackReelL3);
                    if (srideIndex == -1) srideIndex = 0;
                }
                else if (slotCore.oneGame.buttonCount == 1)
                {
                    if (ZugaraStatusPackReelL2.IsFix)
                    {
                        if (ZugaraStatusPackReelL2.FixZugara[0] == key)
                        {
                            // ??? key key
                            // ??? --- ---
                            // ??? --- ---
                            srideIndex = FixSride_O_X_X(key, ZugaraStatusPackReelL3);
                            if (srideIndex == -1) srideIndex = 0;
                        }
                        else if (ZugaraStatusPackReelL2.FixZugara[1] == key)
                        {
                            // ??? --- key
                            // ??? key key
                            // ??? --- key
                            srideIndex = FixSride_O_O_O(key, ZugaraStatusPackReelL3);
                            if (srideIndex == -1) srideIndex = 0;
                        }
                        else if (ZugaraStatusPackReelL2.FixZugara[2] == key)
                        {
                            // ??? --- ---
                            // ??? --- ---
                            // ??? key key
                            srideIndex = FixSride_X_X_O(key, ZugaraStatusPackReelL3);
                            if (srideIndex == -1) srideIndex = 0;
                        }
                    }
                    else
                    {
                        if (ZugaraStatusPackReelL1.FixZugara[0] == key)
                        {
                            // key ??? key
                            // --- ??? ---
                            // --- ??? key
                            srideIndex = FixSride_O_X_O(key, ZugaraStatusPackReelL3);
                            if (srideIndex == -1) srideIndex = 0;
                        }
                        else if (ZugaraStatusPackReelL1.FixZugara[1] == key)
                        {
                            // --- ??? ---
                            // key ??? key
                            // --- ??? ---
                            srideIndex = FixSride_X_O_X(key, ZugaraStatusPackReelL3);
                            if (srideIndex == -1) srideIndex = 0;
                        }
                        else if (ZugaraStatusPackReelL2.FixZugara[2] == key)
                        {
                            // --- ??? key
                            // --- ??? ---
                            // key ??? key
                            srideIndex = FixSride_O_X_O(key, ZugaraStatusPackReelL3);
                            if (srideIndex == -1) srideIndex = 0;
                        }

                    }
                }
                else if (slotCore.oneGame.buttonCount == 2)
                {
                    if ((ZugaraStatusPackReelL1.FixZugara[0] == key) && (ZugaraStatusPackReelL2.FixZugara[0] == key))
                    {
                        // key key key
                        // --- --- ---
                        // --- --- ---
                        srideIndex = FixSride_O_X_X(key, ZugaraStatusPackReelL3);
                        if (srideIndex == -1) srideIndex = 0;
                    }
                    else if ((ZugaraStatusPackReelL1.FixZugara[0] == key) && (ZugaraStatusPackReelL2.FixZugara[1] == key))
                    {
                        // key --- ---
                        // --- key ---
                        // --- --- key
                        srideIndex = FixSride_X_X_O(key, ZugaraStatusPackReelL3);
                        if (srideIndex == -1) srideIndex = 0;
                    }
                    else if ((ZugaraStatusPackReelL1.FixZugara[1] == key) && (ZugaraStatusPackReelL2.FixZugara[1] == key))
                    {
                        // --- --- ---
                        // key key key
                        // --- --- ---
                        srideIndex = FixSride_X_O_X(key, ZugaraStatusPackReelL3);
                        if (srideIndex == -1) srideIndex = 0;
                    }
                    else if ((ZugaraStatusPackReelL1.FixZugara[2] == key) && (ZugaraStatusPackReelL2.FixZugara[1] == key))
                    {
                        // --- --- key
                        // --- key ---
                        // key --- ---
                        srideIndex = FixSride_O_X_X(key, ZugaraStatusPackReelL3);
                        if (srideIndex == -1) srideIndex = 0;
                    }
                    else if ((ZugaraStatusPackReelL1.FixZugara[2] == key) && (ZugaraStatusPackReelL2.FixZugara[2] == key))
                    {
                        // --- --- ---
                        // --- --- ---
                        // key key key
                        srideIndex = FixSride_X_X_O(key, ZugaraStatusPackReelL3);
                        if (srideIndex == -1) srideIndex = 0;
                    }
                }
                break;
            case "bell":
                key = "bell";
                if (slotCore.oneGame.buttonCount == 0)
                {
                    srideIndex = FixSride_O_O_O(key, ZugaraStatusPackReelL3);
                    if (srideIndex == -1) srideIndex = 0;
                }
                else if (slotCore.oneGame.buttonCount == 1)
                {
                    if (ZugaraStatusPackReelL2.IsFix)
                    {
                        if (ZugaraStatusPackReelL2.FixZugara[0] == key)
                        {
                            // ??? key key
                            // ??? --- ---
                            // ??? --- ---
                            srideIndex = FixSride_O_X_X(key, ZugaraStatusPackReelL3);
                            if (srideIndex == -1) srideIndex = 0;
                        }
                        else if (ZugaraStatusPackReelL2.FixZugara[1] == key)
                        {
                            // ??? --- key
                            // ??? key key
                            // ??? --- key
                            srideIndex = FixSride_O_O_O(key, ZugaraStatusPackReelL3);
                            if (srideIndex == -1) srideIndex = 0;
                        }
                        else if (ZugaraStatusPackReelL2.FixZugara[2] == key)
                        {
                            // ??? --- ---
                            // ??? --- ---
                            // ??? key key
                            srideIndex = FixSride_X_X_O(key, ZugaraStatusPackReelL3);
                            if (srideIndex == -1) srideIndex = 0;
                        }
                    }
                    else
                    {
                        if (ZugaraStatusPackReelL1.FixZugara[0] == key)
                        {
                            // key ??? key
                            // --- ??? ---
                            // --- ??? key
                            srideIndex = FixSride_O_X_O(key, ZugaraStatusPackReelL3);
                            if (srideIndex == -1) srideIndex = 0;
                        }
                        else if (ZugaraStatusPackReelL1.FixZugara[1] == key)
                        {
                            // --- ??? ---
                            // key ??? key
                            // --- ??? ---
                            srideIndex = FixSride_X_O_X(key, ZugaraStatusPackReelL3);
                            if (srideIndex == -1) srideIndex = 0;
                        }
                        else if (ZugaraStatusPackReelL2.FixZugara[2] == key)
                        {
                            // --- ??? key
                            // --- ??? ---
                            // key ??? key
                            srideIndex = FixSride_O_X_O(key, ZugaraStatusPackReelL3);
                            if (srideIndex == -1) srideIndex = 0;
                        }

                    }
                }
                else if (slotCore.oneGame.buttonCount == 2)
                {
                    if ((ZugaraStatusPackReelL1.FixZugara[0] == key) && (ZugaraStatusPackReelL2.FixZugara[0] == key))
                    {
                        // key key key
                        // --- --- ---
                        // --- --- ---
                        srideIndex = FixSride_O_X_X(key, ZugaraStatusPackReelL3);
                        if (srideIndex == -1) srideIndex = 0;
                    }
                    else if ((ZugaraStatusPackReelL1.FixZugara[0] == key) && (ZugaraStatusPackReelL2.FixZugara[1] == key))
                    {
                        // key --- ---
                        // --- key ---
                        // --- --- key
                        srideIndex = FixSride_X_X_O(key, ZugaraStatusPackReelL3);
                        if (srideIndex == -1) srideIndex = 0;
                    }
                    else if ((ZugaraStatusPackReelL1.FixZugara[1] == key) && (ZugaraStatusPackReelL2.FixZugara[1] == key))
                    {
                        // --- --- ---
                        // key key key
                        // --- --- ---
                        srideIndex = FixSride_X_O_X(key, ZugaraStatusPackReelL3);
                        if (srideIndex == -1) srideIndex = 0;
                    }
                    else if ((ZugaraStatusPackReelL1.FixZugara[2] == key) && (ZugaraStatusPackReelL2.FixZugara[1] == key))
                    {
                        // --- --- key
                        // --- key ---
                        // key --- ---
                        srideIndex = FixSride_O_X_X(key, ZugaraStatusPackReelL3);
                        if (srideIndex == -1) srideIndex = 0;
                    }
                    else if ((ZugaraStatusPackReelL1.FixZugara[2] == key) && (ZugaraStatusPackReelL2.FixZugara[2] == key))
                    {
                        // --- --- ---
                        // --- --- ---
                        // key key key
                        srideIndex = FixSride_X_X_O(key, ZugaraStatusPackReelL3);
                        if (srideIndex == -1) srideIndex = 0;
                    }
                }
                break;
            case "suika":
                key = "suika";
                if (slotCore.oneGame.buttonCount == 0)
                {
                    srideIndex = FixSride_O_O_O(key, ZugaraStatusPackReelL3);
                    if (srideIndex == -1) srideIndex = 0;
                }
                else if (slotCore.oneGame.buttonCount == 1)
                {
                    if (ZugaraStatusPackReelL2.IsFix)
                    {
                        if (ZugaraStatusPackReelL2.FixZugara[0] == key)
                        {
                            // ??? key key
                            // ??? --- ---
                            // ??? --- ---
                            srideIndex = FixSride_O_X_X(key, ZugaraStatusPackReelL3);
                            if (srideIndex == -1) srideIndex = 0;
                        }
                        else if (ZugaraStatusPackReelL2.FixZugara[1] == key)
                        {
                            // ??? --- key
                            // ??? key key
                            // ??? --- key
                            srideIndex = FixSride_O_O_O(key, ZugaraStatusPackReelL3);
                            if (srideIndex == -1) srideIndex = 0;
                        }
                        else if (ZugaraStatusPackReelL2.FixZugara[2] == key)
                        {
                            // ??? --- ---
                            // ??? --- ---
                            // ??? key key
                            srideIndex = FixSride_X_X_O(key, ZugaraStatusPackReelL3);
                            if (srideIndex == -1) srideIndex = 0;
                        }
                    }
                    else
                    {
                        if (ZugaraStatusPackReelL1.FixZugara[0] == key)
                        {
                            // key ??? key
                            // --- ??? ---
                            // --- ??? key
                            srideIndex = FixSride_O_X_O(key, ZugaraStatusPackReelL3);
                            if (srideIndex == -1) srideIndex = 0;
                        }
                        else if (ZugaraStatusPackReelL1.FixZugara[1] == key)
                        {
                            // --- ??? ---
                            // key ??? key
                            // --- ??? ---
                            srideIndex = FixSride_X_O_X(key, ZugaraStatusPackReelL3);
                            if (srideIndex == -1) srideIndex = 0;
                        }
                        else if (ZugaraStatusPackReelL2.FixZugara[2] == key)
                        {
                            // --- ??? key
                            // --- ??? ---
                            // key ??? key
                            srideIndex = FixSride_O_X_O(key, ZugaraStatusPackReelL3);
                            if (srideIndex == -1) srideIndex = 0;
                        }

                    }
                }
                else if (slotCore.oneGame.buttonCount == 2)
                {
                    if ((ZugaraStatusPackReelL1.FixZugara[0] == key) && (ZugaraStatusPackReelL2.FixZugara[0] == key))
                    {
                        // key key key
                        // --- --- ---
                        // --- --- ---
                        srideIndex = FixSride_O_X_X(key, ZugaraStatusPackReelL3);
                        if (srideIndex == -1) srideIndex = 0;
                    }
                    else if ((ZugaraStatusPackReelL1.FixZugara[0] == key) && (ZugaraStatusPackReelL2.FixZugara[1] == key))
                    {
                        // key --- ---
                        // --- key ---
                        // --- --- key
                        srideIndex = FixSride_X_X_O(key, ZugaraStatusPackReelL3);
                        if (srideIndex == -1) srideIndex = 0;
                    }
                    else if ((ZugaraStatusPackReelL1.FixZugara[1] == key) && (ZugaraStatusPackReelL2.FixZugara[1] == key))
                    {
                        // --- --- ---
                        // key key key
                        // --- --- ---
                        srideIndex = FixSride_X_O_X(key, ZugaraStatusPackReelL3);
                        if (srideIndex == -1) srideIndex = 0;
                    }
                    else if ((ZugaraStatusPackReelL1.FixZugara[2] == key) && (ZugaraStatusPackReelL2.FixZugara[1] == key))
                    {
                        // --- --- key
                        // --- key ---
                        // key --- ---
                        srideIndex = FixSride_O_X_X(key, ZugaraStatusPackReelL3);
                        if (srideIndex == -1) srideIndex = 0;
                    }
                    else if ((ZugaraStatusPackReelL1.FixZugara[2] == key) && (ZugaraStatusPackReelL2.FixZugara[2] == key))
                    {
                        // --- --- ---
                        // --- --- ---
                        // key key key
                        srideIndex = FixSride_X_X_O(key, ZugaraStatusPackReelL3);
                        if (srideIndex == -1) srideIndex = 0;
                    }
                }
                break;
        }

        if (CheckBadRoleHit(2, srideIndex, ZugaraStatusPackReelL3))
        {
            srideIndex = GetSrideBadRoleHit(2, ZugaraStatusPackReelL3);
            Debug.Log($"GetSrideBadRoleHit L3 {srideIndex} ");
        }

        ZugaraStatusPackReelL3.Fix(srideIndex);
    }


    void Update()
    {
        ZugaraStatusPackReelL1.Update();
        ZugaraStatusPackReelL2.Update();
        ZugaraStatusPackReelL3.Update();

        switch (slotStep)
        {
            case SlotStep.EndGame:

                //if (Input.GetKeyUp(KeyCode.Backspace))
                //{
                //    if(!SlotCoreScript.IsPlay && !SlotCoreScript.IsReplay )
                //    {
                //        SlotCoreScript.SetCoinIn(3);
                //        SlotCoreScript.BetOnToRelease();
                //        SlotCoreScript.UpdateText();
                //        SlotCoreScript.IsPlay = true;
                //        SlotCoreScript.PlayAudio("game_01_bet", new string[] { "SE" });

                //        LampL.SetAnimation("def");
                //        LampR.SetAnimation("def");
                //    }
                //}
                //else if ( (Input.GetKeyUp(KeyCode.Return)) || preLeberOn )
                //{
                //    if (oneGameWaitTimerMax <= oneGameWaitTimer)
                //    {
                //        if (SlotCoreScript.IsPlay || SlotCoreScript.IsReplay)
                //        {
                //            SlotCoreScript.IsPlay = false;
                //            SlotCoreScript.IsReplay = false;

                //            ZugaraStatusPackReel1.Play();
                //            ZugaraStatusPackReel2.Play();
                //            ZugaraStatusPackReel3.Play();

                //            SlotCoreScript.DoBaseLot(ref bonus, ref role);
                //            Debug.Log($"DoBaseLot {bonus}, {role}");

                //            buttonCount = 0;
                //            slotStep = SlotStep.NowGame;
                //            SlotCoreScript.LeverOn(role, bonus);
                //            SlotCoreScript.PlayAudio("game_02_start_a", new string[] { "SE" });

                //            SlotCoreScript.GameCount++;
                //            SlotCoreScript.UpdateText();

                //            LampL.SetAnimation("def");
                //            LampR.SetAnimation("def");

                //            oneButtonWaitTimer = 0;
                //            oneGameWaitTimer = 0;
                //            preLeberOn = false;
                //        }
                //    }
                //    else
                //    {
                //        preLeberOn = true;
                //    }
                //}
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

    public void FixOneGame()
    {
        if (CheckHitLine("rep", "rep", "rep"))
        {
            slotCore.oneGame.hit = "rep";
            //LampLeft.SetAnimation("ao");
            //LampRight.SetAnimation("ao");
        }
        else if (CheckHitLine("bell", "bell", "bell"))
        {
            slotCore.oneGame.hit = "bell";
            //if (slotCore.longGame.status == SlotCoreLongGame.Status.BonusGame)
            //{
            //    slotCore.longGame.AddOutCoin(10);
            //}
            //else
            //{
            //    slotCore.longGame.AddOutCoin(15);
            //}
            //SlotCoreScript.UpdateText();
            //SlotCoreScript.PlayAudio("game_sorou_koyaku_bell", new string[] { "SE" });
            LampLeft.SetAnimation("ki");
            LampRight.SetAnimation("ki");
        }
        else if (CheckHitLine("suika", "suika", "suika"))
        {
            slotCore.oneGame.hit = "suika";
            //SlotCoreScript.SetCoinOut(6);
            //SlotCoreScript.UpdateText();
            //SlotCoreScript.PlayAudio("game_sorou_koyaku_suika", new string[] { "SE" });
            //LampLeft.SetAnimation("midori");
            //LampRight.SetAnimation("midori");
        }
        else if (CheckHitLine("chery", "", ""))
        {
            slotCore.oneGame.hit = "chery";
            //SlotCoreScript.SetCoinOut(3);
            //SlotCoreScript.UpdateText();
            //SlotCoreScript.PlayAudio("game_sorou_koyaku_chery", new string[] { "SE" });
            //LampLeft.SetAnimation("aka");
            //LampRight.SetAnimation("aka");
        }
        else if (CheckHitLine("r7", "r7", "r7"))
        {
            slotCore.oneGame.hit = "Big-r7";
            //SlotCoreScript.gameStage = SlotCoreScript.GameStage.Bonus;
            //SlotCoreScript.bonusTypeName = "Big-r7";
            //SlotCoreScript.PlayAudio("bonus_in_bgm", new string[] { "BGM" });
            //SlotCoreScript.PlayAudio("game_sorou_bonus", new string[] { "SE" });
            //SlotCoreScript.UpdateText();
            //LampLeft.SetAnimation("shiro");
            //LampRight.SetAnimation("shiro");

            //SlotCoreScript.OnBonusIn();
        }
        else if (CheckHitLine("b7", "b7", "b7"))
        {
            slotCore.oneGame.hit = "Big-b7";
            //SlotCoreScript.gameStage = SlotCoreScript.GameStage.Bonus;
            //SlotCoreScript.bonusTypeName = "Big-b7";
            //SlotCoreScript.PlayAudio("bonus_in_bgm", new string[] { "BGM" });
            //SlotCoreScript.PlayAudio("game_sorou_bonus", new string[] { "SE" });
            //SlotCoreScript.UpdateText();
            //LampLeft.SetAnimation("shiro");
            //LampRight.SetAnimation("shiro");

            //SlotCoreScript.OnBonusIn();
        }
        else if (CheckHitLine("r7", "r7", "bar"))
        {
            slotCore.oneGame.hit = "Reg";
            //SlotCoreScript.gameStage = SlotCoreScript.GameStage.Bonus;
            //SlotCoreScript.bonusTypeName = "Reg";
            //SlotCoreScript.PlayAudio("bonus_in_bgm", new string[] { "BGM" });
            //SlotCoreScript.PlayAudio("game_sorou_bonus", new string[] { "SE" });
            //SlotCoreScript.UpdateText();
            //LampLeft.SetAnimation("shiro");
            //LampRight.SetAnimation("shiro");

            //SlotCoreScript.OnBonusIn();
        }
    }

    // todo : 削除
    public void NextStepEndGame()
    {
        //slotStep = SlotStep.EndGame;

        //if (CheckHitLine("rep", "rep", "rep"))
        //{
        //    //SlotCoreScript.IsReplay = true;
        //    //SlotCoreScript.PlayAudio("game_sorou_koyaku_rep", new string[] { "SE" });
        //    //LampLeft.SetAnimation("ao");
        //    //LampRight.SetAnimation("ao");
        //}
        //else if (CheckHitLine("bell", "bell", "bell"))
        //{
        //    //if (SlotCoreScript.gameStage == SlotCoreScript.GameStage.Normal)
        //    //{
        //    //    SlotCoreScript.SetCoinOut(10);
        //    //}
        //    //else
        //    //{
        //    //    SlotCoreScript.SetCoinOut(15);
        //    //}
        //    //SlotCoreScript.UpdateText();
        //    //SlotCoreScript.PlayAudio("game_sorou_koyaku_bell", new string[] { "SE" });
        //    //LampLeft.SetAnimation("ki");
        //    //LampRight.SetAnimation("ki");
        //}
        //else if (CheckHitLine("suika", "suika", "suika"))
        //{
        //    SlotCoreScript.SetCoinOut(6);
        //    SlotCoreScript.UpdateText();
        //    SlotCoreScript.PlayAudio("game_sorou_koyaku_suika", new string[] { "SE" });
        //    LampLeft.SetAnimation("midori");
        //    LampRight.SetAnimation("midori");
        //}
        //else if (CheckHitLine("chery", "", ""))
        //{
        //    SlotCoreScript.SetCoinOut(3);
        //    SlotCoreScript.UpdateText();
        //    SlotCoreScript.PlayAudio("game_sorou_koyaku_chery", new string[] { "SE" });
        //    LampLeft.SetAnimation("aka");
        //    LampRight.SetAnimation("aka");
        //}
        //else if (CheckHitLine("r7", "r7", "r7"))
        //{
        //    SlotCoreScript.gameStage = SlotCoreScript.GameStage.Bonus;
        //    SlotCoreScript.bonusTypeName = "Big-r7";
        //    SlotCoreScript.PlayAudio("bonus_in_bgm", new string[] { "BGM" });
        //    SlotCoreScript.PlayAudio("game_sorou_bonus", new string[] { "SE" });
        //    SlotCoreScript.UpdateText();
        //    LampLeft.SetAnimation("shiro");
        //    LampRight.SetAnimation("shiro");

        //    SlotCoreScript.OnBonusIn();
        //}
        //else if (CheckHitLine("b7", "b7", "b7"))
        //{
        //    SlotCoreScript.gameStage = SlotCoreScript.GameStage.Bonus;
        //    SlotCoreScript.bonusTypeName = "Big-b7";
        //    SlotCoreScript.PlayAudio("bonus_in_bgm", new string[] { "BGM" });
        //    SlotCoreScript.PlayAudio("game_sorou_bonus", new string[] { "SE" });
        //    SlotCoreScript.UpdateText();
        //    LampLeft.SetAnimation("shiro");
        //    LampRight.SetAnimation("shiro");

        //    SlotCoreScript.OnBonusIn();
        //}
        //else if (CheckHitLine("r7", "r7", "bar"))
        //{
        //    SlotCoreScript.gameStage = SlotCoreScript.GameStage.Bonus;
        //    SlotCoreScript.bonusTypeName = "Reg";
        //    SlotCoreScript.PlayAudio("bonus_in_bgm", new string[] { "BGM" });
        //    SlotCoreScript.PlayAudio("game_sorou_bonus", new string[] { "SE" });
        //    SlotCoreScript.UpdateText();
        //    LampLeft.SetAnimation("shiro");
        //    LampRight.SetAnimation("shiro");

        //    SlotCoreScript.OnBonusIn();
        //}
    }

    public bool CheckHitLine( string key1, string key2, string key3)
    {
        if (key1=="chery")
        {
            if ((ZugaraStatusPackReelL1.FixZugara[0] == key1) || (ZugaraStatusPackReelL1.FixZugara[1] == key1) || (ZugaraStatusPackReelL1.FixZugara[2] == key1))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        if ((ZugaraStatusPackReelL1.FixZugara[0] == key1) && (ZugaraStatusPackReelL2.FixZugara[0] == key2) && (ZugaraStatusPackReelL3.FixZugara[0] == key3))
        {
            // key key key
            // --- --- ---
            // --- --- ---
            return true;
        }
        else if ((ZugaraStatusPackReelL1.FixZugara[0] == key1) && (ZugaraStatusPackReelL2.FixZugara[1] == key2) && (ZugaraStatusPackReelL3.FixZugara[2] == key3))
        {
            // key --- ---
            // --- key ---
            // --- --- key
            return true;
        }
        else if ((ZugaraStatusPackReelL1.FixZugara[1] == key1) && (ZugaraStatusPackReelL2.FixZugara[1] == key2) && (ZugaraStatusPackReelL3.FixZugara[1] == key3))
        {
            // --- --- ---
            // key key key
            // --- --- ---
            return true;
        }
        else if ((ZugaraStatusPackReelL1.FixZugara[2] == key1) && (ZugaraStatusPackReelL2.FixZugara[1] == key2) && (ZugaraStatusPackReelL3.FixZugara[0] == key3))
        {
            // --- --- key
            // --- key ---
            // key --- ---
            return true;
        }
        else if ((ZugaraStatusPackReelL1.FixZugara[2] == key1) && (ZugaraStatusPackReelL2.FixZugara[2] == key2) && (ZugaraStatusPackReelL3.FixZugara[2] == key3))
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

            if (slotCore.oneGame.buttonCount == 2 )
            {
                System.Func<string, bool> func1 = (key) =>
                 {
                     if ((tmp[0] == key) && (ZugaraStatusPackReelL2.FixZugara[0] == key) && (ZugaraStatusPackReelL3.FixZugara[0] == key))
                     {
                        // key key key
                        // --- --- ---
                        // --- --- ---
                        return true;
                     }
                     else if ((tmp[0] == key) && (ZugaraStatusPackReelL2.FixZugara[1] == key) && (ZugaraStatusPackReelL3.FixZugara[2] == key))
                     {
                        // key --- ---
                        // --- key ---
                        // --- --- key
                        return true;
                     }
                     else if ((tmp[1] == key) && (ZugaraStatusPackReelL2.FixZugara[1] == key) && (ZugaraStatusPackReelL3.FixZugara[1] == key))
                     {
                        // --- --- ---
                        // key key key
                        // --- --- ---
                        return true;
                     }
                     else if ((tmp[2] == key) && (ZugaraStatusPackReelL2.FixZugara[1] == key) && (ZugaraStatusPackReelL3.FixZugara[0] == key))
                     {
                        // --- --- key
                        // --- key ---
                        // key --- ---
                        return true;
                     }
                     else if ((tmp[2] == key) && (ZugaraStatusPackReelL2.FixZugara[2] == key) && (ZugaraStatusPackReelL3.FixZugara[2] == key))
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
                    if ((tmp[0] == "r7") && (ZugaraStatusPackReelL2.FixZugara[0] == "r7") && (ZugaraStatusPackReelL3.FixZugara[0] == "bar"))
                    {
                        // key key key
                        // --- --- ---
                        // --- --- ---
                        return true;
                    }
                    else if ((tmp[0] == "r7") && (ZugaraStatusPackReelL2.FixZugara[1] == "r7") && (ZugaraStatusPackReelL3.FixZugara[2] == "bar"))
                    {
                        // key --- ---
                        // --- key ---
                        // --- --- key
                        return true;
                    }
                    else if ((tmp[1] == "r7") && (ZugaraStatusPackReelL2.FixZugara[1] == "r7") && (ZugaraStatusPackReelL3.FixZugara[1] == "bar"))
                    {
                        // --- --- ---
                        // key key key
                        // --- --- ---
                        return true;
                    }
                    else if ((tmp[2] == "r7") && (ZugaraStatusPackReelL2.FixZugara[1] == "r7") && (ZugaraStatusPackReelL3.FixZugara[0] == "bar"))
                    {
                        // --- --- key
                        // --- key ---
                        // key --- ---
                        return true;
                    }
                    else if ((tmp[2] == "r7") && (ZugaraStatusPackReelL2.FixZugara[2] == "r7") && (ZugaraStatusPackReelL3.FixZugara[2] == "bar"))
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

            if (slotCore.oneGame.buttonCount == 2)
            {
                System.Func<string, bool> func2 = (key) =>
                {
                    if ((tmp[0] == key) && (ZugaraStatusPackReelL1.FixZugara[0] == key) && (ZugaraStatusPackReelL3.FixZugara[0] == key))
                    {
                        // key key key
                        // --- --- ---
                        // --- --- ---
                        return true;
                    }
                    else if ((tmp[1] == key) && (ZugaraStatusPackReelL1.FixZugara[0] == key) && (ZugaraStatusPackReelL3.FixZugara[2] == key))
                    {
                        // key --- ---
                        // --- key ---
                        // --- --- key
                        return true;
                    }
                    else if ((tmp[1] == key) && (ZugaraStatusPackReelL1.FixZugara[1] == key) && (ZugaraStatusPackReelL3.FixZugara[1] == key))
                    {
                        // --- --- ---
                        // key key key
                        // --- --- ---
                        return true;
                    }
                    else if ((tmp[1] == key) && (ZugaraStatusPackReelL1.FixZugara[2] == key) && (ZugaraStatusPackReelL3.FixZugara[0] == key))
                    {
                        // --- --- key
                        // --- key ---
                        // key --- ---
                        return true;
                    }
                    else if ((tmp[2] == key) && (ZugaraStatusPackReelL1.FixZugara[2] == key) && (ZugaraStatusPackReelL3.FixZugara[2] == key))
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
                    if ((tmp[0] == "r7") && (ZugaraStatusPackReelL1.FixZugara[0] == "r7") && (ZugaraStatusPackReelL3.FixZugara[0] == "bar"))
                    {
                        // key key key
                        // --- --- ---
                        // --- --- ---
                        return true;
                    }
                    else if ((tmp[1] == "r7") && (ZugaraStatusPackReelL1.FixZugara[0] == "r7") && (ZugaraStatusPackReelL3.FixZugara[2] == "bar"))
                    {
                        // key --- ---
                        // --- key ---
                        // --- --- key
                        return true;
                    }
                    else if ((tmp[1] == "r7") && (ZugaraStatusPackReelL1.FixZugara[1] == "r7") && (ZugaraStatusPackReelL3.FixZugara[1] == "bar"))
                    {
                        // --- --- ---
                        // key key key
                        // --- --- ---
                        return true;
                    }
                    else if ((tmp[1] == "r7") && (ZugaraStatusPackReelL1.FixZugara[2] == "r7") && (ZugaraStatusPackReelL3.FixZugara[0] == "bar"))
                    {
                        // --- --- key
                        // --- key ---
                        // key --- ---
                        return true;
                    }
                    else if ((tmp[2] == "r7") && (ZugaraStatusPackReelL1.FixZugara[2] == "r7") && (ZugaraStatusPackReelL3.FixZugara[2] == "bar"))
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

            if (slotCore.oneGame.buttonCount == 2)
            {
                System.Func<string, bool> func3 = (key) =>
                {
                    if ((tmp[0] == key) && (ZugaraStatusPackReelL1.FixZugara[0] == key) && (ZugaraStatusPackReelL2.FixZugara[0] == key))
                    {
                        // key key key
                        // --- --- ---
                        // --- --- ---
                        return true;
                    }
                    else if ((tmp[2] == key) && (ZugaraStatusPackReelL1.FixZugara[0] == key) && (ZugaraStatusPackReelL2.FixZugara[1] == key))
                    {
                        // key --- ---
                        // --- key ---
                        // --- --- key
                        return true;
                    }
                    else if ((tmp[1] == key) && (ZugaraStatusPackReelL1.FixZugara[1] == key) && (ZugaraStatusPackReelL2.FixZugara[1] == key))
                    {
                        // --- --- ---
                        // key key key
                        // --- --- ---
                        return true;
                    }
                    else if ((tmp[0] == key) && (ZugaraStatusPackReelL1.FixZugara[2] == key) && (ZugaraStatusPackReelL2.FixZugara[1] == key))
                    {
                        // --- --- key
                        // --- key ---
                        // key --- ---
                        return true;
                    }
                    else if ((tmp[2] == key) && (ZugaraStatusPackReelL1.FixZugara[2] == key) && (ZugaraStatusPackReelL2.FixZugara[2] == key))
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
                    if ((tmp[0] == "r7") && (ZugaraStatusPackReelL1.FixZugara[0] == "r7") && (ZugaraStatusPackReelL2.FixZugara[0] == "bar"))
                    {
                        // key key key
                        // --- --- ---
                        // --- --- ---
                        return true;
                    }
                    else if ((tmp[2] == "r7") && (ZugaraStatusPackReelL1.FixZugara[0] == "r7") && (ZugaraStatusPackReelL2.FixZugara[1] == "bar"))
                    {
                        // key --- ---
                        // --- key ---
                        // --- --- key
                        return true;
                    }
                    else if ((tmp[1] == "r7") && (ZugaraStatusPackReelL1.FixZugara[1] == "r7") && (ZugaraStatusPackReelL2.FixZugara[1] == "bar"))
                    {
                        // --- --- ---
                        // key key key
                        // --- --- ---
                        return true;
                    }
                    else if ((tmp[0] == "r7") && (ZugaraStatusPackReelL1.FixZugara[2] == "r7") && (ZugaraStatusPackReelL2.FixZugara[1] == "bar"))
                    {
                        // --- --- key
                        // --- key ---
                        // key --- ---
                        return true;
                    }
                    else if ((tmp[2] == "r7") && (ZugaraStatusPackReelL1.FixZugara[2] == "r7") && (ZugaraStatusPackReelL2.FixZugara[2] == "bar"))
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
        for( var i=0; i<5;i++ )
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

    private int FixSride_O_O_X(string key, ReelZugaraStatusPack zugaraStatusPack)
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
    private int FixSride_O_X_O(string key, ReelZugaraStatusPack zugaraStatusPack)
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
    private int FixSride_X_O_O(string key, ReelZugaraStatusPack zugaraStatusPack)
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
    private int FixSride_O_X_X(string key, ReelZugaraStatusPack zugaraStatusPack)
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
    private int FixSride_X_O_X(string key, ReelZugaraStatusPack zugaraStatusPack)
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
    private int FixSride_X_X_O(string key, ReelZugaraStatusPack zugaraStatusPack)
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

        ZugaraStatusPackReelL1.ReelTimerMaxOffset = value;
        ZugaraStatusPackReelL1.ReelTimerMax = ZugaraStatusPackReelL1.ReelTimerMaxBase * (1f + (1f - value) * offsetBase);

        ZugaraStatusPackReelL2.ReelTimerMaxOffset = value;
        ZugaraStatusPackReelL2.ReelTimerMax = ZugaraStatusPackReelL2.ReelTimerMaxBase * (1f + (1f - value) * offsetBase);

        ZugaraStatusPackReelL3.ReelTimerMaxOffset = value;
        ZugaraStatusPackReelL3.ReelTimerMax = ZugaraStatusPackReelL3.ReelTimerMaxBase * (1f + (1f - value) * offsetBase);
    }
}
