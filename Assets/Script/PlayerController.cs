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
            // �¿� �̵� ���� ����
            movement.Move(x);

            // �÷��̾� ���� (�����̽� Ű�� ������ ����)
            if (Input.GetKeyDown(KeyCode.Space))
            {
                movement.Jump();
            }

            // �����̽� Ű�� ������ ������ isLongJump = true
            if (Input.GetKey(KeyCode.Space))
            {
                movement.isLongJump = true;
            }

            // �����̽� Ű�� ���� isLongJump = false
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