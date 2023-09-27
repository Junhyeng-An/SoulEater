using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public GameObject sword;
    [SerializeField]
    private float speed = 5.0f; // �̵��ӵ�
    private float jumpForce = 8.0f; // ���� �Ŀ�
    private float throwForce = 6.0f; // ������ �Ŀ�
    private Rigidbody2D rigid;

    [HideInInspector]
    public bool isLongJump = false; // ���� ����, ���� ���� üũ

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        // ���� ����, ���� ���� ������ ���� �߷� ���(gravityScale) ���� (Jump Up�� ���� ����)
        // �߷� ����� ���� if ���� ���� ������ �ǰ�, �߷� ����� ���� else ���� ���� ������ �ȴ�
        if (isLongJump && rigid.velocity.y > 0)
        {
            rigid.gravityScale = 1.0f;
        }
        else
        {
            rigid.gravityScale = 2.5f;
        }
    }
    public void Jump()
    {
        // jumpForce�� ũ�⸸ŭ ���� �������� �ӷ� ����
        rigid.velocity = Vector2.up * jumpForce;
    }

    public void Move(float x)
    {
        // x�� �̵��� x * speed��, y�� �̵��� ������ �ӷ� �� (����� �߷�)
        rigid.velocity = new Vector2(x * speed, rigid.velocity.y);
    }
    public void Throwing()
    {
        float angle = sword.GetComponent<Sword>().angle;
        rigid.AddForce(new Vector2(Mathf.Cos(angle) * 50 * throwForce, 100 * throwForce));
        GetComponent<CircleCollider2D>().isTrigger = true;
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.layer == 12) // ������ ���� �浹 ��
        {
            GetComponent<PlayerController>().isThrowing = false;
            GetComponent<CircleCollider2D>().isTrigger = false;
        }
        if (col.gameObject.layer == 20) // ������ �ٴڰ� �浹 ��
        {
            GetComponent<PlayerController>().isThrowing = false;
            GetComponent<CircleCollider2D>().isTrigger = false;
        }
    }
}
