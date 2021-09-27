using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{

    
    
    public int walkSpeed;
    public int XMoveDirection;

    public int enemyCoin = 10;

    

   
    //Todo  :  머리 위쪽 제외 충돌시 플레이어 -hp
    //머리 콜라이더 플레이 충돌 적이 사망
    //적이 죽으면 코인으로 플러스
    
    void Start()
    {
        
    }

    void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast (transform.position, new Vector2 (XMoveDirection, 0));
        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(XMoveDirection, 0) * walkSpeed;
        if (hit.distance < 0.9f)
        {
            
            Flip();
        }
        void Flip()
        {
            if (XMoveDirection > 0)
            {
                XMoveDirection = -1;
            }
            else
            {
                XMoveDirection = 1;
            }
        }
    }
    
    
    
    
    public void EnemyDie()
    {
        //코인올릴변수를 죽기전 게임매니저한테알려준다
        //죽으면 없어짐
        GameManager.Instance.CountCoin(enemyCoin);
        
    }
   
}
