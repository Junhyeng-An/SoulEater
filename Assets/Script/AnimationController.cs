using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimaionController : MonoBehaviour
{
    Animator ani_body;
    Animator ani_eye;
    GameObject player;
    Rigidbody2D rigid;
    Rigidbody2D rigid_player;
    SpriteRenderer render_body;
    SpriteRenderer render_head;
    SpriteRenderer render_eye;
    Vector2 vel;

    GameObject root;
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
    Vector2 moveHead_basic;
    Vector2 moveHead_ani;
    Vector2 moveHead_view;

    void Awake()
    {
        root = transform.Find("Root").gameObject;
        body = root.transform.Find("Body").gameObject;
        head = root.transform.Find("Head").gameObject;
        eye = head.transform.Find("Eye").gameObject;
        ani_body = body.GetComponent<Animator>();
        ani_eye = eye.GetComponent<Animator>();
        player = GameObject.Find("Player");

        rigid = GetComponent<Rigidbody2D>();
        rigid_player = player.GetComponent<Rigidbody2D>();

        render_body = body.GetComponent<SpriteRenderer>();
        render_head = head.GetComponent<SpriteRenderer>();
        render_eye = eye.GetComponent<SpriteRenderer>();
        cycle = 0.1f;
    }

    void Update()
    {
        moveHead_basic = new Vector2(0.0f, 0.95f);

        if (CompareTag("Controlled"))
        {
            vel = rigid_player.velocity;

            //Debug.Log(vel);

            if (vel.y > -1 && vel.y < 1)
            {
                ani_body.SetFloat("TestAni_Run", Mathf.Abs(vel.x));
                isRun = true;
            }
            else
            {
                ani_body.SetFloat("TestAni_Run", 0);
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

            if (check.x > 0)
                moveHead_view.x = change;
            else
                moveHead_view.x = -change;


            if (check.y > 1.5)
                moveHead_view.y = change;
            else if (check.y < -1.5)
                moveHead_view.y = -change;
            else
                moveHead_view.y = 0;

            if (Mathf.Abs(vel.x) > 0.1f && ani_body.GetCurrentAnimatorStateInfo(0).IsName("TestAni_Run"))
            {
                time += Time.deltaTime;

                if (time < cycle)
                {
                    moveBody = change_run;
                    moveHead_ani.y = -change_run;
                }
                else if (time > cycle)
                {
                    moveBody = -change_run;
                    moveHead_ani.y = change_run;
                }

                if (time > cycle * 2)
                    time = 0;

                if (vel.x > 0)
                {
                    moveHead_ani.x = 0.1f;
                }
                else
                {
                    moveHead_ani.x = -0.1f;
                }
            }
            else
            {
                time = 0;
                moveBody = 0;
                moveHead_ani.x = 0;
                moveHead_ani.y = 0;
            }

            if (ani_body.GetCurrentAnimatorStateInfo(0).IsName("TestAni_Jump01"))
            {
                moveHead_ani.y = 0.1f;

                if (render_head.flipX == false)
                    moveHead_ani.x = 0.075f;
                else
                    moveHead_ani.x = -0.075f;
            }

            if (ani_body.GetCurrentAnimatorStateInfo(0).IsName("TestAni_Jump02"))
            {
                moveHead_ani.y = 0.1f;

                if (render_head.flipX == false)
                    moveHead_ani.x = -0.025f;
                else
                    moveHead_ani.x = 0.025f;
            }

            time_blink += Time.deltaTime;
            if (ran_blink <= time_blink)
            {
                ani_eye.SetBool("Blink", true);

                if (ran_blink + 0.5f <= time_blink)
                {
                    ran_blink = Random.Range(2f, 4f);
                    ani_eye.SetBool("Blink", false);
                    time_blink = 0;
                }
            }

            moveHead = new Vector3(moveHead_view.x + moveHead_basic.x + moveHead_ani.x, moveHead_view.y + moveHead_basic.y + moveHead_ani.y);
            head.transform.position = transform.position + new Vector3(moveHead.x, moveHead.y);
            body.transform.position = transform.position + Vector3.up * moveBody;
        }
        if (CompareTag("Enemy"))
        {
            moveHead_basic = new Vector2(0.0f, 1.45f);

            vel = rigid.velocity;
            ani_body.SetFloat("TestAni_Run", Mathf.Abs(vel.x));

            float change_run = 0.015f;
            if (Mathf.Abs(vel.x) > 0.1f && ani_body.GetCurrentAnimatorStateInfo(0).IsName("TestAni_Run"))
            {
                time += Time.deltaTime;

                if (time < cycle)
                {
                    moveBody = change_run;
                    moveHead_ani.y = -change_run;
                }
                else if (time > cycle)
                {
                    moveBody = -change_run;
                    moveHead_ani.y = change_run;
                }

                if (time > cycle * 2)
                    time = 0;

                if (vel.x > 0)
                {
                    moveHead_ani.x = 0.1f;
                }
                else
                {
                    moveHead_ani.x = -0.1f;
                }
            }

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

            else
            {
                time = 0;
                moveBody = 0;
                moveHead_ani.x = 0;
                moveHead_ani.y = 0;
            }

            moveHead = new Vector3(moveHead_basic.x + moveHead_ani.x, moveHead_basic.y + moveHead_ani.y);
            head.transform.position = transform.position + new Vector3(moveHead.x, moveHead.y);
            body.transform.position = transform.position + Vector3.up * (moveBody + 0.5f);
        }
    }
}
