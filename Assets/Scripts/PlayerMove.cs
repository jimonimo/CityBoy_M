using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public GameObject coinSlot;
    public int coinNum = 1;
    public int lifeNum = 1;
    public bool playerDied;

    public float jumpVelocity;//점프속도
    public Vector2 velocity;
    public float gravity;
    public LayerMask wallMask;
    public LayerMask floorMask;

    public bool walk, walk_left, walk_right, jump;

    public enum PlayerState
    {
        jumping,
        idle,
        walking
    }

    private PlayerState playerState = PlayerState.idle;

    private bool grounded = false;

    public void SetPlayer()
    {
        playerDied = false;
    }
    void Start()
    {
        //Fall();
    }
    void Update()
    {
        if (GameManager.Instance.gamePause == false)
        {
            //Move();
        }
        
        CheckPlayerInput();

        UpdatePlayerPosition();

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

    void UpdatePlayerPosition()
    {
        Vector3 pos = transform.localPosition;
        Vector3 scale = transform.localScale;

        if (walk)
        {
            if (walk_left)
            {
                pos.x -= velocity.x * Time.deltaTime;

                scale.x = -1;

            }

            if (walk_right)
            {
                pos.x += velocity.x * Time.deltaTime;

                scale.x = 1;

            }
            //pos = CheckWallRays(pos, scale.x);

        }
        if (jump && playerState != PlayerState.jumping)
        {
            playerState = PlayerState.jumping;

            velocity = new Vector2(velocity.x, jumpVelocity);
        }
        //Hack: 점프시 y축이 땅을 뚫고 나감
        if (playerState == PlayerState.jumping)
        {
            pos.y += velocity.y * Time.deltaTime;

            velocity.y -= gravity * Time.deltaTime;
        }

        //if (velocity.y <= 0)
        //pos = CheckFloorRays(pos);

        transform.localPosition = pos;
        transform.localScale = scale;
    }


    void CheckPlayerInput()
    {
        bool input_left = Input.GetKey(KeyCode.LeftArrow);
        bool input_right = Input.GetKey(KeyCode.RightArrow);
        bool input_space = Input.GetKeyDown(KeyCode.Space);

        walk = input_left || input_right;

        walk_left = input_left && !input_right;

        walk_right = !input_left && input_right;

        jump = input_space;

    }
    /*
    Vector3 CheckWallRays (Vector3 pos, float direction)//벽이있는지확인
    {

        Vector2 originTop = new Vector2(pos.x + direction * .4f, pos.y + 1f - 0.2f);
        Vector2 originMiddle = new Vector2(pos.x + direction * .4f, pos.y);
        Vector2 originBottom = new Vector2(pos.x + direction * .4f, pos.y - 1f + 0.2f);

        RaycastHit2D wallTop = Physics2D.Raycast(originTop, new Vector2(direction, 0), velocity.x * Time.deltaTime, wallMask);
        RaycastHit2D wallMiddle = Physics2D.Raycast(originMiddle, new Vector2(direction, 0), velocity.x * Time.deltaTime, wallMask);
        RaycastHit2D wallBottom = Physics2D.Raycast(originBottom, new Vector2(direction, 0), velocity.x * Time.deltaTime, wallMask);

        if (wallTop.collider != null || wallMiddle.collider != null || wallBottom.collider != null)
        {
            pos.x -= velocity.x * Time.deltaTime * direction;
        }

        return pos;
    }
    */

    

    Vector3 CheckFloorRays(Vector3 pos)
    {
        Vector2 originLeft = new Vector2(pos.x - 0.5f + 0.2f, pos.y - 1f);
        Vector2 originMiddle = new Vector2(pos.x, pos.y - 1f);
        Vector2 originRight = new Vector2(pos.x + 0.5f - 0.2f, pos.y - 1f);

        RaycastHit2D floorLeft = Physics2D.Raycast(originLeft, Vector2.down, velocity.y * Time.deltaTime, floorMask);
        RaycastHit2D floorMiddle = Physics2D.Raycast(originMiddle, Vector2.down, velocity.y * Time.deltaTime, floorMask);
        RaycastHit2D floorRight = Physics2D.Raycast(originRight, Vector2.down, velocity.y * Time.deltaTime, floorMask);

        if (floorLeft.collider != null || floorMiddle.collider != null || floorRight.collider != null)
        {
            RaycastHit2D hitRay = floorRight;

            if (floorLeft)
            {
                hitRay = floorLeft;
            }
            else if (floorMiddle)
            {
                hitRay = floorMiddle;
            }
            else if (floorRight)
            {
                hitRay = floorRight;
            }

            playerState = PlayerState.idle;

            grounded = true;

            velocity.y = 0;

            pos.y = hitRay.collider.bounds.center.y + hitRay.collider.bounds.size.y / 2 + 1;

        }

        else
        {
            if (playerState != PlayerState.jumping)
            {
                Fall();
            }
        }
        return pos;
        
    }

    private void Fall()
    {
        velocity.y = 0;

        playerState = PlayerState.jumping;

        grounded = false;
        Debug.Log("fall");
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
