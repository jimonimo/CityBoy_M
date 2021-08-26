using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SaveLoadManager : MonoBehaviour
{
    #region 싱글톤
    private static SaveLoadManager _instance;
    public static SaveLoadManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new SaveLoadManager();
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

    [Header("씬이동")]
    public GameObject scenesPopup;
    public GameObject popup;
    public Button mainBtn;
    public Button ingameBtn;
    bool isGame = false;

    void Start()
    {
        isGame = false;
        SetBtn();
    }

    #region 씬이동
    public void ShowScenesPopup(bool on)
    {
        scenesPopup.SetActive(on);
    }
    public void ShowPopUp()
    {
        popup.gameObject.SetActive(!popup.activeSelf);
        Debug.Log(popup.activeSelf);
       
    }
    void SetBtn()
    {
        if (isGame)
        {
            ingameBtn.gameObject.SetActive(false);
            mainBtn.gameObject.SetActive(true);
        }
        else
        {
            ingameBtn.gameObject.SetActive(true);
            mainBtn.gameObject.SetActive(false);
        }
    }

    public void Play()
    {
        SceneManager.LoadScene("Scenes/Stage1");
        //ShowScenesPopup(false);
        
        isGame = true;
        SetBtn();
        popup.SetActive(false);
    }
    public void MainScene()
    {
        SceneManager.LoadScene("Scenes/MainScene");
        //ShowScenesPopup(true);

        isGame = false;
        SetBtn();
    }
    #endregion

    //Todo: 세이브로드메소드 SaveRoadManager로 이동
    public void Save()
    {
        Debug.Log("저장" + GameManager.Instance.score);
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
