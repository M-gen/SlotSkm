using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationImageDataSet
{
    public string Key;
    public List<AnimationImageData> ImageDatas = new List<AnimationImageData>();
    public int Index = 0;
    public float Timer = 0;
    public float TimerMax = 0;
    public bool IsLoop = true;

    public void Init()
    {
        TimerMax = 0;
        foreach (var i in ImageDatas)
        {
            TimerMax += i.Time;
        }
    }
}