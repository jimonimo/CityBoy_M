using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public int playerSpeed = 10;
    private bool facingRight = false;//향하는 방향
    public int playerJumpPower = 100;
    private float moveX;
    public int hp = 2; //체력 = 2(1이 되면 작아짐. 0이되면 사망)
    public bool isGrounded = false;



    void Start()
    {
        
    }
    void Update()
    {
        if (GameManager.Instance.gamePause == false)
        {
            Move();
        }

    }

    void Move()
    {
        //콘트롤
        moveX = Input.GetAxis("Horizontal");
        if (Input.GetButton("Jump"))
        {
            Jump();
        }
        //애니
        //플레이어 방향
        if (moveX < 0.0f && facingRight == false)
        {
            FlipPlayer();
        }
        else if(moveX > 0.0f && facingRight == true)
        {
            FlipPlayer();
        }
        //물리
        gameObject.GetComponent<Rigidbody2D>().velocity =
            new Vector2(moveX * playerSpeed, gameObject.GetComponent<Rigidbody2D>().velocity.y);
    }

    void Jump() 
    {
        GetComponent<Rigidbody2D>().AddForce(Vector2.up * playerJumpPower);
        //오브젝트에 윗방향으로 힘을주어 이동
        isGrounded = false;
    }

    void FlipPlayer()
    {
        facingRight = !facingRight;
        Vector2 localScale = gameObject.transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }


    void OnCollisionEnter2D(Collision2D col)
    {
        //접촉하는 콜라이더 이름 디버그
        Debug.Log("player has collided with" + col.collider.name);
        if (col.gameObject.tag == "Ground")
        {
            //!!!!!!!!!!!!!작동안함!!!!!!!!!!!!!
            isGrounded = true;
            
        }
        if (col.gameObject.tag == "Clear")
        {
            GameManager.Instance.GameResult(true);

        }

    }
    
}
