using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_Ani : MonoBehaviour
{
    Animator ani_body;
    Animator ani_eye;
    GameObject player;
    Rigidbody2D rigid;
    SpriteRenderer render_body;
    SpriteRenderer render_head;
    SpriteRenderer render_eye;
    Vector2 vel;

    GameObject body;
    GameObject head;
    GameObject eye;

    bool isAni = false;
    bool isRun = false;

    float time;
    float time_blink = 0;
    float ran_blink = 3;
    float cycle;

    float moveBody;
    Vector2 moveHead;
    Vector2 moveHead_ani;
    Vector2 moveHead_;

    void Awake()
    {
        body = transform.Find("Body").gameObject;
        head = transform.Find("Head").gameObject;
        eye = head.transform.Find("Eye").gameObject;
        ani_body = body.GetComponent<Animator>();
        ani_eye = eye.GetComponent<Animator>();
        player = GameObject.Find("Player");
        rigid = player.GetComponent<Rigidbody2D>();
        render_body = body.GetComponent<SpriteRenderer>();
        render_head = head.GetComponent<SpriteRenderer>();
        render_eye = eye.GetComponent<SpriteRenderer>();
        cycle = 0.1f;
    }

    void LateUpdate()
    {
        float posX = 0;
        float posY = 0;

        float basicX = 0.0f;
        float basicY = 0.95f;

        vel = rigid.velocity;

        if (vel.y > -1 && vel.y < 1)
        {
            ani_body.SetFloat("TestAni_Run", Mathf.Abs(vel.x));
            isRun = true;
        }
        else
        {
            ani_body.SetFloat("TestAni_Run", Mathf.Abs(0));
            ani_body.SetFloat("TestAni_Jump", vel.y);
            isRun = false;
        }


        if (vel.y == 0)
            ani_body.SetFloat("TestAni_Jump", vel.y);



        if (vel.x < 0)
        {
            render_body.flipX = true;
            render_head.flipX = true;
            render_eye.flipX = true;
        }
        else if (vel.x > 0)
        {
            render_body.flipX = false;
            render_head.flipX = false;
            render_eye.flipX = false;
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

        if (Mathf.Abs(vel.x) > 0.1f && ani_body.GetCurrentAnimatorStateInfo(0).IsName("TestAni_Run"))
        {
            time += Time.deltaTime;

            if (time < cycle)
            {
                moveBody = change_run;
                moveHead.y = -change_run;
            }
            else if (time > cycle)
            {
                moveBody = -change_run;
                moveHead.y = change_run;
            }
            
            if (time > cycle * 2)
                time = 0;

            if (vel.x > 0)
            {
                moveHead.x = 0.1f;
            }
            else
            {
                moveHead.x = -0.1f;
            }
        }
        else
        {
            time = 0;
            moveBody = 0;
            moveHead.x = 0;
            moveHead.y = 0;
        }

        if (ani_body.GetCurrentAnimatorStateInfo(0).IsName("TestAni_Jump01"))
        {
            moveHead.y = 0.1f;

            if (render_head.flipX == false)
                moveHead.x = 0.075f;
            else
                moveHead.x = -0.075f;
        }

        if (ani_body.GetCurrentAnimatorStateInfo(0).IsName("TestAni_Jump02"))
        {
            moveHead.y = 0.1f;

            if (render_head.flipX == false)
                moveHead.x = -0.025f;
            else
                moveHead.x = 0.025f;
        }

        time_blink += Time.deltaTime;
        if(ran_blink <= time_blink)
        {
            ani_eye.SetBool("Blink", true);

            if (ran_blink + 0.5f <= time_blink)
            {
                ran_blink = Random.Range(2f, 4f);
                ani_eye.SetBool("Blink", false);
                time_blink = 0;
            }
        }


        head.transform.position = transform.parent.position + new Vector3(posX + basicX + moveHead.x, posY + basicY + moveHead.y);
        transform.position = transform.parent.position + Vector3.up * moveBody;
    }
}
