using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationImageCore
{
    [System.Serializable]
    public class ImageData
    {
        public string Key;
        public Sprite Sprite;
    }

    public AnimationImageDataSet TargetDataSet = new AnimationImageDataSet();
    public List<AnimationImageDataSet> DataSets = new List<AnimationImageDataSet>();

    GameObject gameObject;
    System.Func<string, Sprite> GetImageByKey;

    public AnimationImageCore(GameObject gameObject, System.Func<string, Sprite> GetImageByKey)
    {
        this.gameObject = gameObject;
        this.GetImageByKey = GetImageByKey;
    }

    public void Update()
    {

        TargetDataSet.Timer += Time.deltaTime;
        while (TargetDataSet.ImageDatas[TargetDataSet.Index].Time <= TargetDataSet.Timer)
        {
            TargetDataSet.Timer -= TargetDataSet.ImageDatas[TargetDataSet.Index].Time;
            if (TargetDataSet.IsLoop)
            {
                TargetDataSet.Index = (TargetDataSet.Index + 1) % TargetDataSet.ImageDatas.Count;
            }
            else
            {
                TargetDataSet.Index = TargetDataSet.Index + 1;
                if (TargetDataSet.ImageDatas.Count <= TargetDataSet.Index) TargetDataSet.Index = TargetDataSet.ImageDatas.Count - 1;
            }

            var render = gameObject.GetComponent<SpriteRenderer>();
            render.sprite = GetImageByKey(TargetDataSet.ImageDatas[TargetDataSet.Index].Key);
        }
    }

    public void SetAnimation(string key)
    {
        if (TargetDataSet.Key == key) return;

        foreach (var i in DataSets)
        {
            if (i.Key == key)
            {
                TargetDataSet = i;
                TargetDataSet.Timer = 0;
                TargetDataSet.Index = 0;

                var render = gameObject.GetComponent<SpriteRenderer>();
                render.sprite = GetImageByKey(TargetDataSet.ImageDatas[TargetDataSet.Index].Key);
            }
        }
    }

}
