using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharaSDKazama : MonoBehaviour
{
    [SerializeField]
    AnimationImageCore.ImageData[] imageDatas;

    AnimationImageCore animationImage;

    Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;
        animationImage = new AnimationImageCore(gameObject, GetImageByKey);

        {
            var animation = new AnimationImageDataSet() { Key = "ao" };
            var speed = 0.15f;
            animation.ImageDatas.Add(new AnimationImageData() { Key = "chara_sd_kazama_iroha_walk_ao_0000", Time = speed });
            animation.ImageDatas.Add(new AnimationImageData() { Key = "chara_sd_kazama_iroha_walk_ao_0001", Time = speed });
            animation.ImageDatas.Add(new AnimationImageData() { Key = "chara_sd_kazama_iroha_walk_ao_0002", Time = speed });
            animation.ImageDatas.Add(new AnimationImageData() { Key = "chara_sd_kazama_iroha_walk_ao_0003", Time = speed });
            animation.ImageDatas.Add(new AnimationImageData() { Key = "chara_sd_kazama_iroha_walk_ao_0004", Time = speed });
            animation.ImageDatas.Add(new AnimationImageData() { Key = "chara_sd_kazama_iroha_walk_ao_0005", Time = speed });

            animationImage.DataSets.Add(animation);
        }

        {
            var animation = new AnimationImageDataSet() { Key = "ki" };
            var speed = 0.15f;
            animation.ImageDatas.Add(new AnimationImageData() { Key = "chara_sd_kazama_iroha_walk_ki_0000", Time = speed });
            animation.ImageDatas.Add(new AnimationImageData() { Key = "chara_sd_kazama_iroha_walk_ki_0001", Time = speed });
            animation.ImageDatas.Add(new AnimationImageData() { Key = "chara_sd_kazama_iroha_walk_ki_0002", Time = speed });
            animation.ImageDatas.Add(new AnimationImageData() { Key = "chara_sd_kazama_iroha_walk_ki_0003", Time = speed });
            animation.ImageDatas.Add(new AnimationImageData() { Key = "chara_sd_kazama_iroha_walk_ki_0004", Time = speed });
            animation.ImageDatas.Add(new AnimationImageData() { Key = "chara_sd_kazama_iroha_walk_ki_0005", Time = speed });

            animationImage.DataSets.Add(animation);
        }

        {
            var animation = new AnimationImageDataSet() { Key = "midori" };
            var speed = 0.15f;
            animation.ImageDatas.Add(new AnimationImageData() { Key = "chara_sd_kazama_iroha_walk_midori_0000", Time = speed });
            animation.ImageDatas.Add(new AnimationImageData() { Key = "chara_sd_kazama_iroha_walk_midori_0001", Time = speed });
            animation.ImageDatas.Add(new AnimationImageData() { Key = "chara_sd_kazama_iroha_walk_midori_0002", Time = speed });
            animation.ImageDatas.Add(new AnimationImageData() { Key = "chara_sd_kazama_iroha_walk_midori_0003", Time = speed });
            animation.ImageDatas.Add(new AnimationImageData() { Key = "chara_sd_kazama_iroha_walk_midori_0004", Time = speed });
            animation.ImageDatas.Add(new AnimationImageData() { Key = "chara_sd_kazama_iroha_walk_midori_0005", Time = speed });

            animationImage.DataSets.Add(animation);
        }

        {
            var animation = new AnimationImageDataSet() { Key = "aka" };
            var speed = 0.15f;
            animation.ImageDatas.Add(new AnimationImageData() { Key = "chara_sd_kazama_iroha_walk_aka_0000", Time = speed });
            animation.ImageDatas.Add(new AnimationImageData() { Key = "chara_sd_kazama_iroha_walk_aka_0001", Time = speed });
            animation.ImageDatas.Add(new AnimationImageData() { Key = "chara_sd_kazama_iroha_walk_aka_0002", Time = speed });
            animation.ImageDatas.Add(new AnimationImageData() { Key = "chara_sd_kazama_iroha_walk_aka_0003", Time = speed });
            animation.ImageDatas.Add(new AnimationImageData() { Key = "chara_sd_kazama_iroha_walk_aka_0004", Time = speed });
            animation.ImageDatas.Add(new AnimationImageData() { Key = "chara_sd_kazama_iroha_walk_aka_0005", Time = speed });

            animationImage.DataSets.Add(animation);
        }

        {
            var animation = new AnimationImageDataSet() { Key = "shiro" };
            var speed = 0.15f;
            animation.ImageDatas.Add(new AnimationImageData() { Key = "chara_sd_kazama_iroha_walk_shiro_0000", Time = speed });
            animation.ImageDatas.Add(new AnimationImageData() { Key = "chara_sd_kazama_iroha_walk_shiro_0001", Time = speed });
            animation.ImageDatas.Add(new AnimationImageData() { Key = "chara_sd_kazama_iroha_walk_shiro_0002", Time = speed });
            animation.ImageDatas.Add(new AnimationImageData() { Key = "chara_sd_kazama_iroha_walk_shiro_0003", Time = speed });
            animation.ImageDatas.Add(new AnimationImageData() { Key = "chara_sd_kazama_iroha_walk_shiro_0004", Time = speed });
            animation.ImageDatas.Add(new AnimationImageData() { Key = "chara_sd_kazama_iroha_walk_shiro_0005", Time = speed });

            animationImage.DataSets.Add(animation);
        }

        {
            var animation = new AnimationImageDataSet() { Key = "", IsLoop = false };
            animation.ImageDatas.Add(new AnimationImageData() { Key = "", Time = 1.0f });

            animationImage.DataSets.Add(animation);
        }

        SetAnimation("ao");
    }

    void Update()
    {
        animationImage.Update();
        if (animationImage.TargetDataSet.Key != "") { 
            transform.position += new Vector3(2.0f * Time.deltaTime, 0, 0);
        }
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
        if ( key!="")
        {
            transform.position = startPosition;
        }

        animationImage.SetAnimation(key);
    }
}
