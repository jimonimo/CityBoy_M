using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;
using UnityEngine.UI;


public class Player : MonoBehaviour
{
    public bool playerDied;
    public GameObject coinSlot;
    public int coinNum = 1;
    public int lifeNum = 1;
    
    public void SetPlayer()
    {
        playerDied = false;
    }

    void Update()
    {
        if (GameManager.Instance.gameOver == false)
        {
            if (GameManager.Instance.gamePause == false)
            {
                if (gameObject.transform.position.y < -7)
                {
                    playerDied = true;
                    StartCoroutine("Die");
                    GameManager.Instance.GameOver();
                }
            }
        }


    }
    IEnumerator Die()
    {
        
        yield return new WaitForSeconds(2);
        
        GameManager.Instance.CountDieLife(lifeNum);
        GameManager.Instance.Save();
    }



    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.tag == "Coin")
        {
            GameObject c = other.gameObject;//c는 임시변수
            c.transform.position = Vector3.MoveTowards(transform.position, coinSlot.transform.position, 1f);
            c.transform.SetParent(coinSlot.transform, true);
            c.GetComponent<Coin>().isCoinTime = true;

            //여기서 코인을 게임매니저에게 카운트 하게한다
            GameManager.Instance.CountCoin(coinNum);
        }

        else if (other.transform.tag == "Life")
        {
            
            GameObject d = other.gameObject;
            d.GetComponent<LifePlus>().getLife = true;
            GameManager.Instance.CountLife(lifeNum);
        }

    }
    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Enemy")
        {
            Debug.Log("Damage10");
        }
    }
}


