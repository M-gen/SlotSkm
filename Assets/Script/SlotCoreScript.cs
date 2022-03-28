using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotCoreScript : MonoBehaviour
{
    [System.Serializable]
    public class BaseLot
    {
        public string Bonus;
        public string Role;
        public int    LotValue; // ���I�̏d��
    }


    [System.Serializable]
    public class BonusStatus
    {
        public string Name;
        public int Coin;
    }

    [SerializeField]
    BaseLot[] baseLots;

    [SerializeField]
    BaseLot[] bonusInLots; // �{�[�i�X���m�肵�Ă���Ƃ��̒��I

    [SerializeField]
    BaseLot[] bonusLots;   // �{�[�i�X���̒��I

    [SerializeField]
    BonusStatus[] bonusType;


    [SerializeField]
    GameObject Stage;


    public enum GameStage
    {
        Normal,  // �ʏ펞
        BonusFix, // �{�[�i�X�m�蒆
        Bonus,   // �{�[�i�X��
    }

    public GameStage gameStage = GameStage.Normal;
    public string bonusTypeName = "";
    public LineScript LineScript;

    [SerializeField]
    AudioManager audioManager;

    [SerializeField]
    StageEffect stageEffect;


}
