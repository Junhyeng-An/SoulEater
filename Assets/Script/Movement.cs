using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public GameObject sword;
    [SerializeField]
    private float speed = 5.0f; // 이동속도
    private float jumpForce = 8.0f; // 점프 파워
    private float throwForce = 6.0f; // 던지기 파워
    private Rigidbody2D rigid;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {

    }
    public void Jump()
    {
        // jumpForce의 크기만큼 윗쪽 방향으로 속력 설정
        rigid.velocity = Vector2.up * jumpForce;
    }

    public void Move(float x)
    {
        // x축 이동은 x * speed로, y축 이동은 기존의 속력 값 (현재는 중력)
        rigid.velocity = new Vector2(x * speed, rigid.velocity.y);
    }
    public void Throwing()
    {
        float angle = sword.GetComponent<Sword>().angle;
        rigid.AddForce(new Vector2(Mathf.Cos(angle) * 50 * throwForce, 100 * throwForce));
        GetComponent<CircleCollider2D>().isTrigger = true;
    }
    public void Dash()
    {
        float angle = sword.GetComponent<Sword>().angle;
        rigid.AddForce(new Vector2(Mathf.Cos(angle) * 10, Mathf.Sin(angle) * 10), ForceMode2D.Impulse);
        //GetComponent<CircleCollider2D>().isTrigger = true;
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.layer == 12) // 던지고 적과 충돌 시
        {
            GetComponent<PlayerController>().isThrowing = false;
            GetComponent<CircleCollider2D>().isTrigger = false;
        }
        if (col.gameObject.layer == 20) // 던지고 바닥과 충돌 시
        {
            GetComponent<PlayerController>().isThrowing = false;
            GetComponent<CircleCollider2D>().isTrigger = false;
        }
    }
}
