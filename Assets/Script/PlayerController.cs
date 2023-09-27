using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Movement movement;

    [HideInInspector]
    public bool isThrowing = false;

    void Awake()
    {
        movement = GetComponent<Movement>();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            isThrowing = true;

            movement.Throwing();
        }

        if (isThrowing == false)
        {
            //GetComponent<CircleCollider2D>().isTrigger = false;
            // left or a = -1 / right or d = 1
            float x = Input.GetAxisRaw("Horizontal");
            // 좌우 이동 방향 제어
            movement.Move(x);

            // 플레이어 점프 (스페이스 키를 누르면 점프)
            if (Input.GetKeyDown(KeyCode.Space))
            {
                movement.Jump();
            }

            // 스페이스 키를 누르고 있으면 isLongJump = true
            if (Input.GetKey(KeyCode.Space))
            {
                movement.isLongJump = true;
            }

            // 스페이스 키를 때면 isLongJump = false
            else if (Input.GetKeyUp(KeyCode.Space))
            {
                movement.isLongJump = false;
            }
        }
        else
        {
            //GetComponent<CircleCollider2D>().isTrigger = true;
        }
    }
}