using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch_v2: MonoBehaviour
{
    private bool isPlayer; //플레이어의 여부
    public float speed = 5; //이동속도

    public Color playerColor = Color.blue; // 플레이어 색상
    public Color nonPlayerColor = Color.red; // 플레이어가 아닌 경우의 색상
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
            //    // 충돌 대상의 태그도 변경합니다.
            //    collision.gameObject.tag = isPlayer ? "Player" : "Enemy";
            //    gameObject.tag = isPlayer ? "Enemy" : "Player";
            //    Debug.Log("변경됨");
            //}
    }
}
