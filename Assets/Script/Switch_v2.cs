using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch_v2: MonoBehaviour
{
    private bool isPlayer; //�÷��̾��� ����
    public float speed = 5; //�̵��ӵ�
    public float jumpforce = 20.0f;

    public Color playerColor = Color.blue; // �÷��̾� ����
    public Color nonPlayerColor = Color.red; // �÷��̾ �ƴ� ����� ����
    private SpriteRenderer spriteRenderer;
    Rigidbody2D rb;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
    }


    private void Update()
    {
        if (gameObject.tag == "Player")
        {
            float horizontalInput = Input.GetAxis("Horizontal");
            Vector2 movement = new Vector2(horizontalInput, 0);
            rb.velocity = new Vector2(movement.x * speed, rb.velocity.y); // x �ӵ��� �����ӿ� ���� �����ϰ�, y �ӵ��� �״�� ����

            spriteRenderer.color = playerColor;

            if (Input.GetKeyDown(KeyCode.Space))
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpforce);
            }
        }
        else
        {
            isPlayer = false;
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            spriteRenderer.color = nonPlayerColor;
        }

        //if (Input.GetKeyDown("q") && gameObject.tag == "Player")
        //{
        //    GetSword = false;
        //}
    }


}
