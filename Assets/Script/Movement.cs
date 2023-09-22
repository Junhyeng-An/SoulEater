using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField]
    private float speed = 5.0f; // �̵��ӵ�
    private float jumpForce = 8.0f; // ���� �Ŀ�
    private Rigidbody2D rigid2D;
    private Sword sword;
    [HideInInspector]
    public bool isThrowing = false;
    [HideInInspector]
    public bool isLongJump = false; // ���� ����, ���� ���� üũ

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
            // �¿� �̵� ���� ����
            Move(x);

            // �÷��̾� ���� (�����̽� Ű�� ������ ����)
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Jump();
            }

            // �����̽� Ű�� ������ ������ isLongJump = true
            if (Input.GetKey(KeyCode.Space))
            {
                isLongJump = true;
            }

            // �����̽� Ű�� ���� isLongJump = false
            else if (Input.GetKeyUp(KeyCode.Space))
            {
                isLongJump = false;
            }
        }
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
}