using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Maku : MonoBehaviour
{
    [SerializeField]
    AnimationImageCore.ImageData[] imageDatas;

    AnimationImageCore animationImage;


    [SerializeField]
    GameObject logo;

    void Start()
    {
        animationImage = new AnimationImageCore(gameObject, GetImageByKey);

        {
            var animation = new AnimationImageDataSet() { Key = "close", IsLoop = false };
            animation.ImageDatas.Add(new AnimationImageData() { Key = "a1", Time = 0.4f });
            animation.ImageDatas.Add(new AnimationImageData() { Key = "a2", Time = 0.4f });
            animation.ImageDatas.Add(new AnimationImageData() { Key = "a3", Time = 1.0f });

            animationImage.DataSets.Add(animation);
        }

        {
            var animation = new AnimationImageDataSet() { Key = "open", IsLoop = false };
            animation.ImageDatas.Add(new AnimationImageData() { Key = "a3", Time = 0.4f });
            animation.ImageDatas.Add(new AnimationImageData() { Key = "a2", Time = 0.4f });
            animation.ImageDatas.Add(new AnimationImageData() { Key = "a1", Time = 0.4f });
            animation.ImageDatas.Add(new AnimationImageData() { Key = "",   Time = 1.0f });

            animationImage.DataSets.Add(animation);
        }

        {
            var animation = new AnimationImageDataSet() { Key = "a1", IsLoop = false };
            animation.ImageDatas.Add(new AnimationImageData() { Key = "a1", Time = 1.0f });

            animationImage.DataSets.Add(animation);
        }

        {
            var animation = new AnimationImageDataSet() { Key = "", IsLoop = false };
            animation.ImageDatas.Add(new AnimationImageData() { Key = "", Time = 1.0f });

            animationImage.DataSets.Add(animation);
        }

        SetAnimation("a1");
        IsLogoShow(false);
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

    public void IsLogoShow( bool isShow )
    {
        logo.SetActive(isShow);
    }
}
