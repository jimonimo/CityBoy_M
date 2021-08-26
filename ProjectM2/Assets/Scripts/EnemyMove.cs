using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    public int EnemySpeed;
    public int XMoveDirection;
    void Start()
    {
        
    }

    void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast (transform.position, new Vector2 (XMoveDirection, 0));
        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(XMoveDirection, 0) * EnemySpeed;
        if (hit.distance < 0.9f)
        {
            Debug.Log("flip");
            Flip();
        }
        void Flip()
        {
            if (XMoveDirection > 0)
            {
                Debug.Log("-1");
                XMoveDirection = -1;
            }
            else
            {
                Debug.Log("1");
                XMoveDirection = 1;
            }
        }
    }
}
