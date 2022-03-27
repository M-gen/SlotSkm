using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotCoreLongGame
{
    // �������
    public enum Status
    {
        Normal,     // �ʏ�
        BonusFix,   // �{�[�i�X�m��i�����I�ɁA�\���ς݂�����炸�j
        BonusGame,  // �{�[�i�X��
    }
    public Status status = Status.BonusFix;

    // �\�����
    public enum ViewStatus
    {
        Normal,                         // ��{�[�i�X�A�ʏ�w�i�\����
        NormalComboGame,                // ��{�[�i�X�A�A�����o��
        BonusFixShowIn,                 // �{�[�i�X�m���ʂɓ˓�
        BonusFixShow,                   // �{�[�i�X�m���ʁA�p��
        BonusFixShowAndBonusTypeShow,   // �{�[�i�X�m���ʁA�p���A�}���\���ς�
    }
    public ViewStatus viewStatus = ViewStatus.Normal;

    public int hotLevel = 0;            // �M��
    public bool flagReplay = false;     // 1�Q�[���x�b�g�s�v
    public int inCoin = 0;
    public int outCoin = 0;
    public int bonusOutCoin = 0;        // �{�[�i�X���̊l���R�C��
}
