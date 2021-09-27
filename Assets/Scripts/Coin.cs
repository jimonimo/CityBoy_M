using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public bool isCoinTime = false;
    private float DelayCoolTime = 0f;
    void Update()
    {
        if (GameManager.Instance.gameOver == false)
        {
            if (GameManager.Instance.gamePause == false)
            {
                if (isCoinTime)
                {
                    isCoinTime = false;
                    StartCoroutine(CoinFadeOut(DelayCoolTime));
                }
            }
            
        }
        
    }
    IEnumerator CoinFadeOut(float cool)
    {

        while (cool < 2f)
        {
            cool += Time.deltaTime;
            yield return new WaitForFixedUpdate();

        }
        this.gameObject.SetActive(false);
    }
    }
