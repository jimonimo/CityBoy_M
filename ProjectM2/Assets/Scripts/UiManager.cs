using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    public Text coinCount;
    public Text lifeCount;
    public Text popupCoin;
    public Text popupLife;
    public Text pauseText;
    public Text timeLeftCount;//시간 저장로드
    
    public GameObject PopUp;

    

    void Start()
    {
       
    }

    
    void Update()
    {
       
    }
    public void ShowCoinCount(int currentCoin)
    {
        coinCount.text = currentCoin.ToString();
    }
    public void ShowLifeCount(int currentLife)
    {
        lifeCount.text = currentLife.ToString();
    }
    public void ShowTimeLeftCount(float timeLeft)
    {
        timeLeftCount.text = timeLeft.ToString();
    }


    public void ChangePauseText()
    {
        if (GameManager.Instance.gamePause)
        {
            pauseText.text = "Play";
        }
        else
        {
            pauseText.text = "Pause";
        }
       

    }

    public void OnOffPopUp(bool on)
    {
        PopUp.SetActive(on);
    }

    public void ShowPopupText(int currentcoin , int currentlife)
    {
        popupCoin.text = currentcoin.ToString();
        popupLife.text = currentlife.ToString();
    }
    

    
}


