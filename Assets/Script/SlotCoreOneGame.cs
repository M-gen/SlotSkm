using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotCoreOneGame
{

    public string flagRole = "";    // ���I���Ă���{�[�i�X�ȊO�̖��ɂ���
    public string flagBounus = "";  // ���I���Ă���{�[�i�X�ɂ���

    public enum Status
    {
        BetWait,                // �x�b�g�҂�
        LeverOnWait,            // ���o�[ON�҂�
        ReelStartWait,          // ���[����]�J�n�҂�
        ButtonStep1Wait,        // ����~�҂�
        ButtonStep2Wait,        // ����~�҂�
        ButtonStep3Wait,        // ��O��~�҂�
        ButtonStep3ReleaseWait, // ��O��~�{�^�������҂�
    }
    public Status status = Status.BetWait;

    public string hit = ""; // ���������ɂ���

    public int buttonCount
    {
        get
        {
            {
                switch (status)
                {
                    case Status.BetWait:
                        return 0;
                    case Status.LeverOnWait:
                        return 0;
                    case Status.ReelStartWait:
                        return 0;
                    case Status.ButtonStep1Wait:
                        return 0;
                    case Status.ButtonStep2Wait:
                        return 1;
                    case Status.ButtonStep3Wait:
                        return 2;
                    case Status.ButtonStep3ReleaseWait:
                        return 3;
                }
                return 0;
            }
        }
    }

    public const float oneButtonWaitTimerMax = 0.3f;
    public float oneButtonWaitTimer = 0;

    public const float oneGameWaitTimerMax = 4.1f;
    public float oneGameWaitTimer = oneGameWaitTimerMax;

    public SlotCore.ButtonType downButtonType = SlotCore.ButtonType.None;

}
