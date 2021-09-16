using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region 싱글톤
    private static GameManager _instance;//실제이름
    public static GameManager Instance//밖에서 쓰이는 이름
    {
        get
        {
            if (_instance == null)
            {
                _instance = new GameManager();
            }
            return _instance;
        }


    }

    private void Awake()
    {
        _instance = this;//게임매니저는 나다
    }
    #endregion

    public Player player;
    public UiManager uiManager;
    public int currentCoin = 0;
    public int currentLife = 3;
    public bool gameOver = false;
    public bool gameClear = false;
    public bool gamePause = false;
    public float timeLeft = 100;

    public int score = 0;
    public int monsterCoin;

    void Start()
    {
        uiManager.OnOffPopUp(false);
        SaveLoadManager.Instance.Load();
        player.SetPlayer();
    }

    void Update()
    {
        //Todo: 과하게 찍히는 로그 안들어와도 될때 막자
        if (GameManager.Instance.gamePause == false)
        {
            timeLeft -= Time.deltaTime;
            uiManager.ShowTimeLeftCount(timeLeft);
            Debug.Log("timeLeft");
            if (timeLeft < 0.1f)
            {
                GameResult(false);
                Debug.Log("죽음");
            }
        }
    }
    
    public void CountCoin(int coin)
    {
        //코인갯수 더해줌
        currentCoin = currentCoin + coin;
        uiManager.ShowCoinCount(currentCoin);
        //값을 바뀔친구가 앞에
        //더해준 코인을 ui매니저가 알도록해서 Text로 띄워줌
    }
    public void CountLife(int life)
    {
        currentLife = currentLife + life;
        uiManager.ShowLifeCount(currentLife);
    }

    public void CountDieLife(int life)
    {
        Debug.Log("-Life");
        currentLife = currentLife - life;
        uiManager.ShowLifeCount(currentLife);
    }

    public void GameResult(bool Clear)//게임결과(클리어or데스)시 매개변수 호출하여 해당 결과 보여줌
    {
        if (Clear)
        {
            gameClear = true;
            
        }
        else
        {
            gameOver = true;
        }
        CaculateScore();//게임오버시 스코어계산
        SaveLoadManager.Instance.Save();
        uiManager.OnOffPopUp(true);
        uiManager.ShowPopupText(currentCoin, currentLife);
    }

    public void GamePause()
    {
        if (gamePause == true)
        {
            gamePause = false;
        }
        else
        {
            gamePause = true;
            Debug.Log("멈춤");
        }
    }

    //결과화면나오는 시점에 호출
    public void CaculateScore()
    {
        //코인+몬스터점수
        score = currentCoin + monsterCoin;
    }

}



