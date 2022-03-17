using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lottery
{
    // 宝箱やドロップにまつわる抽選を実現する基礎クラス
    public class LotteryBase
    {
        public class Pack
        {
            public int    Value; // 重み
            public object Item;  // 抽選されるオブジェクト
        }

        public List<Pack> Packs = new List<Pack>();

        public LotteryBase()
        {
        }

        public object Do( int offset_value = 0 )
        {
            if (Packs.Count == 0) return null;

            var sumValue = 0;
            foreach( var i in Packs )
            {
                sumValue += i.Value;
            }

            var r = UnityEngine.Random.Range( 0, sumValue) - offset_value; // オフセット値を設定しておくと、先頭のものが抽選されやすくなる

            foreach( var i in Packs)
            {
                if ( r < i.Value )
                {
                    return i.Item;
                }
                r -= i.Value;
            }

            // 入らないはずだが
            return null;
        }

        public static int DefaultLottery( int[] vs )
        {
            var lot = new Lottery.LotteryBase();
            var i = 0;
            foreach (var v in vs)
            {
                lot.Packs.Add(new Lottery.LotteryBase.Pack() { Item = i, Value = v });
                i++;
            }

            return (int)lot.Do();
        }

    }
}
