using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReelZugaraStatusPack
{
    public List<ReelZugaraStatus> Reel = new List<ReelZugaraStatus>();
    public int index = 0;

    public float ReelTimer = 0f;
    public float ReelTimerMax = 0.0333f * 1; // 0.0333f 1分間80回転が上限
    public float ReelTimerMaxBase = 0.0333f;
    public float ReelTimerMaxOffset = 1.0f;

    public bool IsFix = true;
    public string[] FixZugara = new string[] { "", "", "" };

    public float Speed = 15.0f;
    public float OffsetX = 0f;
    public float OffsetY = 0f;

    public int srideIndex = 1;

    float FixTimer = 0f;
    float FixTimerMax = 0.19f; // 規定があるらしい0.19秒以内に停止しないといけない

    float scaleSize = 0.53f;
    float marginY = 1.80f * 0.45f;
    float marginYMax = 0;
    float returnYLine = 8f;
    LineScript lineScript;

    public void Init(float offsetX, float offsetY, string[] reelZugaraSource, GameObject zugaraPrefab, Transform parent, LineScript lineScript)
    {
        this.lineScript = lineScript;
        this.OffsetX = offsetX;
        this.OffsetY = offsetY;

        marginYMax = marginY * reelZugaraSource.Length;
        var y = 0.0f - index * marginY;
        if (y < -returnYLine)
        {
            y += marginYMax;
        }

        foreach (var i in reelZugaraSource)
        {
            var o = lineScript._Instantiate(zugaraPrefab, parent);
            var zugara = lineScript.GetZugaraByKey(i);
            o.GetComponent<SpriteRenderer>().sprite = zugara.Sprite;
            o.transform.position = new Vector3(OffsetX, y + OffsetY, 0);
            o.transform.localScale = new Vector3(scaleSize, scaleSize, 1);

            y -= marginY;

            if (y < -returnYLine)
            {
                y += marginYMax;
            }

            var zs = new ReelZugaraStatus() { Index = Reel.Count, GameObject = o, Key = i, Line = 0 };
            Reel.Add(zs);
        }
    }

    public void Update()
    {
        if (!IsFix)
        {
            // ひとまず、固定場所で流れるのはできたのであとは、ここに補正で滑らかに動くように見せかける
            ReelTimer += Time.deltaTime;
            while (ReelTimerMax <= ReelTimer) // 何重にもしないとダメ
            {
                ReelTimer -= ReelTimerMax;
                index = (index + 1) % Reel.Count;
            }

            // 滑らかに見せかける仕組み
            // 一つ手前で動かす
            var mix = ReelTimer / ReelTimerMax;
            var offsetY2 = marginY * mix;


            var y = 0.0f - index * marginY - offsetY2;
            if (y < -returnYLine)
            {
                y += marginYMax;
            }

            foreach (var i in Reel)
            {
                i.GameObject.transform.position = new Vector3(OffsetX, y + OffsetY, 0);
                y -= marginY;

                if (y < -returnYLine)
                {
                    y += marginYMax;
                }
            }
        }
        else
        {
            FixTimer += Time.deltaTime;
            if (FixTimerMax <= FixTimer)
            {
                FixTimer = FixTimerMax;
            }

            var mix = Mathf.Sin((FixTimer / FixTimerMax) * Mathf.PI / 2f);
            var offsetY2 = marginY * mix * (1 + srideIndex);

            var y = 0.0f - index * marginY - offsetY2 + marginY * (1 + srideIndex);
            if (y < -returnYLine)
            {
                y += marginYMax;
            }

            foreach (var i in Reel)
            {
                i.GameObject.transform.position = new Vector3(OffsetX, y + OffsetY, 0);
                y -= marginY;

                if (y < -returnYLine)
                {
                    y += marginYMax;
                }
            }

        }
    }

    public void Fix(int srideIndex = 0)
    {
        if (!IsFix)
        {
            IsFix = true;

            // すべる量を確定させる
            this.srideIndex = srideIndex;

            //
            FixTimer = (ReelTimer / ReelTimerMax) * (1f / (float)(srideIndex + 1));
            ReelTimer = 0;
            index = (index + 1 + srideIndex) % Reel.Count;

            // 確定した図柄の検出
            var i2 = 0;
            var z1 = "";
            var z2 = "";
            var z3 = "";
            foreach (var i in Reel)
            {
                var j = (i2 + index) % Reel.Count;
                if (j == 19) z1 = i.Key;
                if (j == 0) z2 = i.Key;
                if (j == 1) z3 = i.Key;
                i2++;
            }

            FixZugara[0] = z1;
            FixZugara[1] = z2;
            FixZugara[2] = z3;
        }
    }

    public string[] GetFixZugara(int slide)
    {
        var tmp = new string[] { "", "", "" };
        var tmp2 = (index + 1 + slide) % Reel.Count;

        var i2 = 0;
        foreach (var i in Reel)
        {
            var j = (i2 + tmp2) % Reel.Count;
            if (j == 19) tmp[0] = i.Key;
            if (j == 0) tmp[1] = i.Key;
            if (j == 1) tmp[2] = i.Key;
            i2++;
        }

        return tmp;
    }

    public void Play()
    {
        if (IsFix)
        {
            FixZugara[0] = "";
            FixZugara[1] = "";
            FixZugara[2] = "";
            IsFix = false;
        }
    }

    public bool IsReelStop()
    {
        if (IsFix)
        {
            if (FixTimer == FixTimerMax) return true;
        }

        return false;
    }
}
