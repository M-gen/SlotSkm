using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharaSDSakamata : MonoBehaviour
{
    [SerializeField]
    AnimationImageCore.ImageData[] imageDatas;

    AnimationImageCore animationImage;

    float timer;

    void Start()
    {
        animationImage = new AnimationImageCore(gameObject, GetImageByKey);

        animationImage.TargetDataSet.Key = "walk";
        animationImage.TargetDataSet.ImageDatas.Add(new AnimationImageData() { Key = "walk_1", Time = 0.2f });
        animationImage.TargetDataSet.ImageDatas.Add(new AnimationImageData() { Key = "walk_stay", Time = 0.2f });
        animationImage.TargetDataSet.ImageDatas.Add(new AnimationImageData() { Key = "walk_2", Time = 0.2f });
        animationImage.TargetDataSet.ImageDatas.Add(new AnimationImageData() { Key = "walk_stay", Time = 0.2f });

        animationImage.DataSets.Add(animationImage.TargetDataSet);

        {
            var animation = new AnimationImageDataSet() { Key = "stop" };
            animation.ImageDatas.Add(new AnimationImageData() { Key = "stand_wait", Time = 1.0f });

            animationImage.DataSets.Add(animation);
        }

        {
            var animation = new AnimationImageDataSet() { Key = "stop_!?" };
            animation.ImageDatas.Add(new AnimationImageData() { Key = "action_l1", Time = 1.0f });

            animationImage.DataSets.Add(animation);
        }

        {
            var animation = new AnimationImageDataSet() { Key = "stop_air" };
            animation.ImageDatas.Add(new AnimationImageData() { Key = "action_l2", Time = 1.0f });

            animationImage.DataSets.Add(animation);
        }

        {
            var animation = new AnimationImageDataSet() { Key = "stop_?" };
            animation.ImageDatas.Add(new AnimationImageData() { Key = "action_l3", Time = 1.0f });

            animationImage.DataSets.Add(animation);
        }

        {
            var animation = new AnimationImageDataSet() { Key = "stop_moyamoya" };
            animation.ImageDatas.Add(new AnimationImageData() { Key = "action_l4", Time = 1.0f });

            animationImage.DataSets.Add(animation);
        }

        {
            var animation = new AnimationImageDataSet() { Key = "stop_image_makura_ao" };
            animation.ImageDatas.Add(new AnimationImageData() { Key = "action_l5", Time = 1.0f });

            animationImage.DataSets.Add(animation);
        }

        {
            var animation = new AnimationImageDataSet() { Key = "stop_image_makura_ki" };
            animation.ImageDatas.Add(new AnimationImageData() { Key = "action_l6", Time = 1.0f });

            animationImage.DataSets.Add(animation);
        }

        {
            var animation = new AnimationImageDataSet() { Key = "stop_image_makura_midori" };
            animation.ImageDatas.Add(new AnimationImageData() { Key = "action_l7", Time = 1.0f });

            animationImage.DataSets.Add(animation);
        }

        {
            var animation = new AnimationImageDataSet() { Key = "stop_image_makura_pink" };
            animation.ImageDatas.Add(new AnimationImageData() { Key = "action_l8", Time = 1.0f });

            animationImage.DataSets.Add(animation);
        }

        {
            var animation = new AnimationImageDataSet() { Key = "stop_mask" };
            animation.ImageDatas.Add(new AnimationImageData() { Key = "action_l9", Time = 1.0f });

            animationImage.DataSets.Add(animation);
        }
    }

    void Update()
    {
        animationImage.Update();
    }

    Sprite GetImageByKey( string key )
    {
        foreach( var i in imageDatas)
        {
            if (i.Key == key) return i.Sprite;
        }
        return null;
    }

    public void SetAnimation( string key)
    {
        animationImage.SetAnimation(key);
    }
}
