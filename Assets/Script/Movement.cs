using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField]
    private float speed = 5.0f; // 이동속도
    private float jumpForce = 8.0f; // 점프 파워
    private Rigidbody2D rigid2D;
    private Sword sword;
    [HideInInspector]
    public bool isThrowing = false;
    [HideInInspector]
    public bool isLongJump = false; // 낮은 점프, 높은 점프 체크

    private void Awake()
    {
        rigid2D = GetComponent<Rigidbody2D>();
        sword  = GetComponent<Sword>();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            isThrowing = true;
            sword.Throwing();
        }

        if (isThrowing == false)
        {
            // left or a = -1 / right or d = 1
            float x = Input.GetAxisRaw("Horizontal");
            // 좌우 이동 방향 제어
            Move(x);

            // 플레이어 점프 (스페이스 키를 누르면 점프)
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Jump();
            }

            // 스페이스 키를 누르고 있으면 isLongJump = true
            if (Input.GetKey(KeyCode.Space))
            {
                isLongJump = true;
            }

            // 스페이스 키를 때면 isLongJump = false
            else if (Input.GetKeyUp(KeyCode.Space))
            {
                isLongJump = false;
            }
        }
    }

    private void FixedUpdate()
    {
        // 낮은 점프, 높은 점프 구현을 위한 중력 계수(gravityScale) 조절 (Jump Up일 때만 적용)
        // 중력 계수가 낮은 if 문은 높은 점프가 되고, 중력 계수가 높은 else 문은 낮은 점프가 된다
        if (isLongJump && rigid2D.velocity.y > 0)
        {
            rigid2D.gravityScale = 1.0f;
        }
        else
        {
            rigid2D.gravityScale = 2.5f;
        }
    }
    public void Jump()
    {
        // jumpForce의 크기만큼 윗쪽 방향으로 속력 설정
        rigid2D.velocity = Vector2.up * jumpForce;
    }

    public void Move(float x)
    {
        // x축 이동은 x * speed로, y축 이동은 기존의 속력 값 (현재는 중력)
        rigid2D.velocity = new Vector2(x * speed, rigid2D.velocity.y);
    }
}