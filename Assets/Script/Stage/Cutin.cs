using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cutin : MonoBehaviour
{
    [SerializeField]
    AnimationImageCore.ImageData[] imageDatas;

    AnimationImageCore animationImage;

    void Start()
    {
        animationImage = new AnimationImageCore(gameObject, GetImageByKey);

        {
            var animation = new AnimationImageDataSet() { Key = "", IsLoop = false };
            animation.ImageDatas.Add(new AnimationImageData() { Key = "", Time = 1.0f });

            animationImage.DataSets.Add(animation);
        }

        {
            var animation = new AnimationImageDataSet() { Key = "group_in", IsLoop = false };
            animation.ImageDatas.Add(new AnimationImageData() { Key = "group_in", Time = 1.0f });

            animationImage.DataSets.Add(animation);
        }

        {
            var animation = new AnimationImageDataSet() { Key = "group_1", IsLoop = false };
            animation.ImageDatas.Add(new AnimationImageData() { Key = "group_1", Time = 1.0f });

            animationImage.DataSets.Add(animation);
        }

        {
            var animation = new AnimationImageDataSet() { Key = "group_2", IsLoop = false };
            animation.ImageDatas.Add(new AnimationImageData() { Key = "group_2", Time = 1.0f });

            animationImage.DataSets.Add(animation);
        }

        {
            var animation = new AnimationImageDataSet() { Key = "group_3", IsLoop = false };
            animation.ImageDatas.Add(new AnimationImageData() { Key = "group_3", Time = 1.0f });

            animationImage.DataSets.Add(animation);
        }

        {
            var animation = new AnimationImageDataSet() { Key = "group_4", IsLoop = false };
            animation.ImageDatas.Add(new AnimationImageData() { Key = "group_4", Time = 1.0f });

            animationImage.DataSets.Add(animation);
        }

        SetAnimation("");
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
