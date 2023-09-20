using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public GameObject sword;
    [SerializeField]
    private float speed = 5.0f; // �̵��ӵ�
    private float jumpForce = 8.0f; // ���� �Ŀ�
    private Rigidbody2D rigid2D;

    [HideInInspector]
    public bool isLongJump = false; // ���� ����, ���� ���� üũ

    private void Awake()
    {
        rigid2D = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        // ���� ����, ���� ���� ������ ���� �߷� ���(gravityScale) ���� (Jump Up�� ���� ����)
        // �߷� ����� ���� if ���� ���� ������ �ǰ�, �߷� ����� ���� else ���� ���� ������ �ȴ�
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
        // jumpForce�� ũ�⸸ŭ ���� �������� �ӷ� ����
        rigid2D.velocity = Vector2.up * jumpForce;
    }

    public void Move(float x)
    {
        // x�� �̵��� x * speed��, y�� �̵��� ������ �ӷ� �� (����� �߷�)
        rigid2D.velocity = new Vector2(x * speed, rigid2D.velocity.y);
    }
    public void Throwing()
    {
        //float angle = sword.GetComponent<Sword>().angle2;
        rigid2D.velocity = Vector2.up * jumpForce;
        //rigid2D.velocity = Vector2.right * jumpForce;
        //rigid2D.AddForce(new Vector2(Mathf.Cos(angle)*10, Mathf.Sin(angle)*10));
        //Debug.Log(new Vector2(Mathf.Cos(angle) * 10, Mathf.Sin(angle) * 10));
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        GetComponent<PlayerController>().isThrowing = false;
    }
}