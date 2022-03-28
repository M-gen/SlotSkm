using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField]
    SlotCore slotCore;

    [SerializeField]
    Text gameStatusViewText;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateGameStatusViewText()
    {
        //var text = $"--COIN--\nIN : {CoinIn}\nOUT : {CoinOut}\n\n---\nGame : {GameCount}\nMode : {gameStage}";
        var text = $"--COIN--\nIN : {slotCore.longGame.inCoin}\nOUT : {slotCore.longGame.outCoin}\n\n---\nGame : {slotCore.longGame.gameCount}";
        if (slotCore.longGame.status == SlotCoreLongGame.Status.BonusGame) 
        {
            text += "\nMode : Bonus";
        }
        else
        {
            text += "\nMode : Normal";
        }
        gameStatusViewText.GetComponent<UnityEngine.UI.Text>().text = text;
    }
}
