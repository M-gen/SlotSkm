using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battle : MonoBehaviour
{
    [SerializeField]
    AnimationImageCore.ImageData[] imageDatas;

    AnimationImageCore animationImage;


    [SerializeField]
    GameObject background;

    void Start()
    {
        animationImage = new AnimationImageCore(gameObject, GetImageByKey);

        {
            var animation = new AnimationImageDataSet() { Key = "battle_01", IsLoop = false };
            animation.ImageDatas.Add(new AnimationImageData() { Key = "battle_01", Time = 1.0f });

            animationImage.DataSets.Add(animation);
        }

        {
            var animation = new AnimationImageDataSet() { Key = "battle_02", IsLoop = false };
            animation.ImageDatas.Add(new AnimationImageData() { Key = "battle_02", Time = 1.0f });

            animationImage.DataSets.Add(animation);
        }

        {
            var animation = new AnimationImageDataSet() { Key = "battle_02_b", IsLoop = false };
            animation.ImageDatas.Add(new AnimationImageData() { Key = "battle_02_b", Time = 1.0f });

            animationImage.DataSets.Add(animation);
        }

        {
            var animation = new AnimationImageDataSet() { Key = "battle_03", IsLoop = false };
            animation.ImageDatas.Add(new AnimationImageData() { Key = "battle_03", Time = 1.0f });

            animationImage.DataSets.Add(animation);
        }

        {
            var animation = new AnimationImageDataSet() { Key = "battle_04", IsLoop = false };
            animation.ImageDatas.Add(new AnimationImageData() { Key = "battle_04", Time = 1.0f });

            animationImage.DataSets.Add(animation);
        }

        {
            var animation = new AnimationImageDataSet() { Key = "battle_in", IsLoop = false };
            animation.ImageDatas.Add(new AnimationImageData() { Key = "battle_in", Time = 1.0f });

            animationImage.DataSets.Add(animation);
        }

        {
            var animation = new AnimationImageDataSet() { Key = "battle_in_b", IsLoop = false };
            animation.ImageDatas.Add(new AnimationImageData() { Key = "battle_in_b", Time = 1.0f });

            animationImage.DataSets.Add(animation);
        }

        {
            var animation = new AnimationImageDataSet() { Key = "bonus_fix", IsLoop = false };
            animation.ImageDatas.Add(new AnimationImageData() { Key = "bonus_fix", Time = 1.0f });

            animationImage.DataSets.Add(animation);
        }

        {
            var animation = new AnimationImageDataSet() { Key = "bonus_fix_big_r7", IsLoop = false };
            animation.ImageDatas.Add(new AnimationImageData() { Key = "bonus_fix_big_r7", Time = 1.0f });

            animationImage.DataSets.Add(animation);
        }

        {
            var animation = new AnimationImageDataSet() { Key = "bonus_fix_big_b7", IsLoop = false };
            animation.ImageDatas.Add(new AnimationImageData() { Key = "bonus_fix_big_b7", Time = 1.0f });

            animationImage.DataSets.Add(animation);
        }

        {
            var animation = new AnimationImageDataSet() { Key = "bonus_fix_reg", IsLoop = false };
            animation.ImageDatas.Add(new AnimationImageData() { Key = "bonus_fix_reg", Time = 1.0f });

            animationImage.DataSets.Add(animation);
        }

        {
            var animation = new AnimationImageDataSet() { Key = "bonus_game", IsLoop = false };
            animation.ImageDatas.Add(new AnimationImageData() { Key = "bonus_game", Time = 1.0f });

            animationImage.DataSets.Add(animation);
        }

        {
            var animation = new AnimationImageDataSet() { Key = "", IsLoop = false };
            animation.ImageDatas.Add(new AnimationImageData() { Key = "", Time = 1.0f });

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

        if ( key=="" )
        {
            SetBackground("");
        }
    }

    public void SetBackground(string key)
    {
        Debug.Log($"SetBackground {key} ");
        switch(key)
        {
            default:
                background.GetComponent<SpriteRenderer>().sprite = null;
                break;
            case "battle_bg_01":
                background.GetComponent<SpriteRenderer>().sprite = GetImageByKey("battle_bg_01");
                break;
            case "battle_bg_02":
                background.GetComponent<SpriteRenderer>().sprite = GetImageByKey("battle_bg_02");
                break;
            case "battle_bg_03":
                background.GetComponent<SpriteRenderer>().sprite = GetImageByKey("battle_bg_03");
                break;
        }
    }


}
