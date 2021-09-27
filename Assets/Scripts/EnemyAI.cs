using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public float gravity;//본인과 플레이어중력에영향(찌그러짐사망)
    public Vector2 velocity;
    public bool isWalkingLeft = true;

    public LayerMask floorMask;
    public LayerMask WallMask;


    public bool grounded = false;
    
    public int enemyCoin = 10;

    public bool shoudDie = false;
    public float deathTimer = 0;
    
    public float timeBeforeDestroy = 1.0f;

    private enum EnemyState
    {
        walking,
        falling,
        dead
    }

    private EnemyState state = EnemyState.falling;

    void Start()
    {
        enabled = false;
        Fall();
    }

    void Update()
    {
        UpdateEnemyPosition();

        CheckCrushed();
    }

    //state가 dead로 되었을때 바로적용해서 모든 행동 정지 위치고정
    public void Crush()
    {
        state = EnemyState.dead;

        //GetComponent<Animatior>.Setbool("isCrushed", true);

        GetComponent<Collider2D>().enabled = false;

        shoudDie = true; 
    }

    void CheckCrushed()
    {
        if (shoudDie)
        {
            if (deathTimer <= timeBeforeDestroy)
            {
                deathTimer += Time.deltaTime;
            }
            else
            {
                shoudDie = false;
                Destroy(this.gameObject);//디스트로이 이외의것으로 변경
            }
        }
    }

    private void UpdateEnemyPosition()
    {
        if (state != EnemyState.dead)
        {
            Vector3 pos = transform.localPosition;
            Vector3 scale = transform.localScale;
            //localPosition 부모위치기준, 인스펙터트랜스폼 숫자와 일치,부모가 존재하지않으면 월드의 원점기준 

            if (state == EnemyState.falling)
            {
                pos.y += velocity.y * Time.deltaTime;
                
                velocity.y -= gravity * Time.deltaTime;

            }

            if (state == EnemyState.walking)
            {
                if (isWalkingLeft)
                {
                    pos.x -= velocity.x * Time.deltaTime;

                    scale.x = -1;
                }

                else
                {
                    pos.x += velocity.x * Time.deltaTime;

                    scale.x = 1;
                }
            }

            //falling중인지 확인
            if (velocity.y <= 0)
                pos = CheckGround(pos);

            transform.localPosition = pos;
            transform.localScale = scale;
        }
    }

    //지상을 체크하고 걷기
    Vector3 CheckGround (Vector3 pos)
    {
        Vector2 originLeft = new Vector2(pos.x - 0.5f + 0.2f, pos.y - .5f);
        Vector2 originMiddle = new Vector2(pos.x, pos.y - .5f);
        Vector2 originRight = new Vector2(pos.x + 0.5f - 0.2f, pos.y - .5f);

        //RaycastHit2D앞에 있는 물체를 미리 감지 레이케스트히트2D 레이이름 = 믈리2D.레이캐스트(현재위치에서, 벡터값, 크기);
        RaycastHit2D groundLeft = Physics2D.Raycast(originLeft, Vector2.down, velocity.y * Time.deltaTime, floorMask);
        RaycastHit2D groundMiddle = Physics2D.Raycast(originMiddle, Vector2.down, velocity.y * Time.deltaTime, floorMask);
        RaycastHit2D groundRight = Physics2D.Raycast(originRight, Vector2.down, velocity.y * Time.deltaTime, floorMask);
        
        if (groundLeft.collider != null || groundMiddle.collider != null || groundRight.collider != null)
        {
            RaycastHit2D hitRay = groundLeft; 
            
            if (groundLeft)
            {
                hitRay = groundLeft;
            }
            else if (groundMiddle)
            {
                hitRay = groundMiddle;
            }
            else if (groundRight)
            {
                hitRay = groundRight;
            }

            //적이 지면과 충돌할때 캐릭터와동시에실행되고 y값을 먼저, 충돌한 땅위에 적이있는것럼 보이게
            pos.y = hitRay.collider.bounds.center.y + hitRay.collider.bounds.size.y / 2 + .5f;

            grounded = true;

            velocity.y = 0;//y값리셋

            state = EnemyState.walking;
        }
        else
        {
            if (state != EnemyState.falling)
            {
                Fall();
            }
        }
        return pos;
        
    }
    //카메라 혹은 플레이어의 시야에들어올때 동작시작
    
    private void OnBecameVisible()
    {
        enabled = true;
        // Component를 켜고 끄는 것이고 SetActive는 GameObject를 켜고 끄는 것
    }

    //플랫폼에서 떨어지거나 할때
    private void Fall()
    {
        velocity.y = 0;
        state = EnemyState.falling;
        grounded = false;

    }
    
    public void EnemyDie()
    {
        //코인올릴변수를 죽기전 게임매니저한테알려준다
        //죽으면 없어짐
        GameManager.Instance.CountCoin(enemyCoin);
    }
}
