using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_Ani : MonoBehaviour
{
    Animator ani;
    GameObject player;
    Rigidbody2D rigid;
    SpriteRenderer render;
    SpriteRenderer render_head;
    Vector2 vel;

    public GameObject head;

    bool isAni = false;
    bool isRun = false;

    float time;
    float cycle;

    float moveBody;
    float moveHead;

    void Awake()
    {
        ani = GetComponent<Animator>();
        player = GameObject.Find("Player");
        rigid = player.GetComponent<Rigidbody2D>();
        render = GetComponent<SpriteRenderer>();
        render_head = head.GetComponent<SpriteRenderer>();

        cycle = 0.1f;
    }

    void LateUpdate()
    {
        float posX;
        float posY;

        float basicX = 0.0f;
        float basicY = 0.75f;

        vel = rigid.velocity;

        if (vel.y > -1 && vel.y < 1)
        {
            ani.SetFloat("TestAni_Run", Mathf.Abs(vel.x));
            isRun = true;
        }
        else
        {
            ani.SetFloat("TestAni_Run", Mathf.Abs(0));
            ani.SetFloat("TestAni_Jump", vel.y);
            isRun = false;
        }


        if (vel.y == 0)
            ani.SetFloat("TestAni_Jump", vel.y);



        if (vel.x < 0)
        {
            render.flipX = true;
            render_head.flipX = true;

            basicX = -0.0f;
        }
        else if (vel.x > 0)
        {
            render.flipX = false;
            render_head.flipX = false;
        }

        Vector3 mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Vector3 check = mouse - player.transform.position;

        float change = 0.03f;
        float change_run = 0.015f;

        if(check.x > 0)
            posX = change;
        else
            posX = -change;


        if (check.y > 1.5)
            posY = change;
        else if (check.y < -1.5)
            posY = -change;
        else
            posY = 0;

        if (Mathf.Abs(vel.x) > 0.1f && isRun == true)
        {
            time += Time.deltaTime;

            if (time < cycle)
            {
                moveBody = change_run;
                moveHead = -change_run;
            }
            else if (time > cycle)
            {
                moveBody = -change_run;
                moveHead = change_run;
            }
            
            if (time > cycle * 2)
                time = 0;
        }
        else
        {
            time = 0;
            moveBody = 0;
            moveHead = 0;
        }

        head.transform.position = transform.parent.position + new Vector3(posX + basicX, posY + basicY + moveHead);
        transform.position = transform.parent.position + Vector3.up * moveBody;
    }
}
