using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lamp : MonoBehaviour
{
    [SerializeField]
    AnimationImageCore.ImageData[] imageDatas;

    AnimationImageCore animationImage;


    [SerializeField]
    GameObject background;

    // Start is called before the first frame update
    void Start()
    {
        animationImage = new AnimationImageCore(gameObject, GetImageByKey);

        {
            var animation = new AnimationImageDataSet() { Key = "def", IsLoop = false };
            animation.ImageDatas.Add(new AnimationImageData() { Key = "def", Time = 1.0f });

            animationImage.DataSets.Add(animation);
        }

        {
            var animation = new AnimationImageDataSet() { Key = "ao", IsLoop = false };
            animation.ImageDatas.Add(new AnimationImageData() { Key = "ao", Time = 1.0f });

            animationImage.DataSets.Add(animation);
        }

        {
            var animation = new AnimationImageDataSet() { Key = "aka", IsLoop = false };
            animation.ImageDatas.Add(new AnimationImageData() { Key = "aka", Time = 1.0f });

            animationImage.DataSets.Add(animation);
        }

        {
            var animation = new AnimationImageDataSet() { Key = "ki", IsLoop = false };
            animation.ImageDatas.Add(new AnimationImageData() { Key = "ki", Time = 1.0f });

            animationImage.DataSets.Add(animation);
        }

        {
            var animation = new AnimationImageDataSet() { Key = "midori", IsLoop = false };
            animation.ImageDatas.Add(new AnimationImageData() { Key = "midori", Time = 1.0f });

            animationImage.DataSets.Add(animation);
        }

        {
            var animation = new AnimationImageDataSet() { Key = "shiro", IsLoop = false };
            animation.ImageDatas.Add(new AnimationImageData() { Key = "shiro", Time = 1.0f });

            animationImage.DataSets.Add(animation);
        }

        SetAnimation("def");
    }

    // Update is called once per frame
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
