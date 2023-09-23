using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch_v2: MonoBehaviour
{
    private bool isPlayer; //플레이어의 여부
    public float speed = 5; //이동속도
    public float jumpforce = 20.0f;

    public Color playerColor = Color.blue; // 플레이어 색상
    public Color nonPlayerColor = Color.red; // 플레이어가 아닌 경우의 색상
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
            rb.velocity = new Vector2(movement.x * speed, rb.velocity.y); // x 속도는 움직임에 따라 변경하고, y 속도는 그대로 유지

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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Sword") || collision.gameObject.CompareTag("Player"))
        {
            // 충돌 대상의 태그도 변경합니다.
            collision.gameObject.tag = isPlayer ? "Player" : "Enemy";
            gameObject.tag = isPlayer ? "Enemy" : "Player";
            Debug.Log("변경됨");
        }
    }
}
