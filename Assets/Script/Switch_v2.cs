using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch_v2: MonoBehaviour
{
    private bool isPlayer; //�÷��̾��� ����
    public float speed = 5; //�̵��ӵ�

    public Color playerColor = Color.blue; // �÷��̾� ����
    public Color nonPlayerColor = Color.red; // �÷��̾ �ƴ� ����� ����
    private SpriteRenderer spriteRenderer;

    bool GetSword;
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }


    private void Update()
    {
        if (GetSword == true)
        if (gameObject.tag == "Player")
        {
            isPlayer = true;
            GetSword = true;
            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");
            Vector2 movement = new Vector2(horizontalInput, verticalInput);
            GetComponent<Rigidbody2D>().velocity = movement * speed;
            spriteRenderer.color = playerColor;
        }
        else
        {
            isPlayer = false;
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            spriteRenderer.color = nonPlayerColor;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
            //if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("Player"))
            //{
            //    // �浹 ����� �±׵� �����մϴ�.
            //    collision.gameObject.tag = isPlayer ? "Player" : "Enemy";
            //    gameObject.tag = isPlayer ? "Enemy" : "Player";
            //    Debug.Log("�����");
            //}
    }
}
