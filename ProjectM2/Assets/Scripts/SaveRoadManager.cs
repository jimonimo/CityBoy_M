using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveRoadManager : MonoBehaviour
{
    #region 싱글톤
    private static SaveRoadManager _instance;
    public static SaveRoadManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new SaveRoadManager();
            }
            return _instance;
        }
    }
    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            DestroyImmediate(gameObject);

        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(this.gameObject);
        }

        
    }
    #endregion
    public void Play()
    {
        SceneManager.LoadScene("Scenes/Stage1");

    }
    public void MainScene()
    {
        SceneManager.LoadScene("Scenes/MainScene");

    }

    //Todo: 세이브로드메소드 SaveRoadManager로 이동
    public void Save()
    {
        Debug.Log("저장" +  GameManager.Instance.score);
        PlayerPrefs.SetInt("Score", GameManager.Instance.score);
        PlayerPrefs.SetInt("Life", GameManager.Instance.currentLife);
        PlayerPrefs.Save();
    }
    public void Load()
    {

        if (PlayerPrefs.HasKey("Coin"))
        {
            GameManager.Instance.currentCoin = PlayerPrefs.GetInt("Coin");
        }
        else
        {
            GameManager.Instance.currentCoin = 0;
        }

        if (PlayerPrefs.HasKey("Life"))
        {
            GameManager.Instance.currentLife = PlayerPrefs.GetInt("Life");
        }
        else
        {
            GameManager.Instance.currentLife = 0;
        }
        Debug.Log("로드" + GameManager.Instance.currentCoin);
    }

    public void ClearPlayerPrefs()
    {
        GameManager.Instance.currentCoin = 0;
        GameManager.Instance.currentLife = 0;
        Save();

    }
}
