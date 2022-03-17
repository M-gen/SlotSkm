using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageBackGroundGroup : MonoBehaviour
{
    [SerializeField]
    AnimationImageCore.ImageData[] imageDatas;

    AnimationImageCore animationImage;

    void Start()
    {
        animationImage = new AnimationImageCore(gameObject, GetImageByKey);

        {
            var animation = new AnimationImageDataSet() { Key = "a1", IsLoop = false };
            animation.ImageDatas.Add(new AnimationImageData() { Key = "a1", Time = 1.0f });

            animationImage.DataSets.Add(animation);
        }

        {
            var animation = new AnimationImageDataSet() { Key = "a2", IsLoop = false };
            animation.ImageDatas.Add(new AnimationImageData() { Key = "a2", Time = 1.0f });

            animationImage.DataSets.Add(animation);
        }

        {
            var animation = new AnimationImageDataSet() { Key = "a3", IsLoop = false };
            animation.ImageDatas.Add(new AnimationImageData() { Key = "a3", Time = 1.0f });

            animationImage.DataSets.Add(animation);
        }

        {
            var animation = new AnimationImageDataSet() { Key = "a4", IsLoop = false };
            animation.ImageDatas.Add(new AnimationImageData() { Key = "a4", Time = 1.0f });

            animationImage.DataSets.Add(animation);
        }


        {
            var animation = new AnimationImageDataSet() { Key = "a5", IsLoop = false };
            animation.ImageDatas.Add(new AnimationImageData() { Key = "a5", Time = 1.0f });

            animationImage.DataSets.Add(animation);
        }

    }

    void Update()
    {
        animationImage.Update();
    }

    Sprite GetImageByKey(string key)
    {
        foreach (var i in imageDatas)
        {
            if (i.Key == key) return i.Sprite;
        }
        return null;
    }

    public void SetAnimation(string key)
    {
        animationImage.SetAnimation(key);
    }

}
