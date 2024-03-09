using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimaionController : MonoBehaviour
{
    EnemyController enemy;
    Sword sword;

    AnimatorOverrideController ani_;
    Animator ani_body;
    Animator ani_eye;
    Animator ani_weapon;
    GameObject player;
    Rigidbody2D rigid;
    Rigidbody2D rigid_player;
    SpriteRenderer render_body;
    SpriteRenderer render_head;
    SpriteRenderer render_eye;
    SpriteRenderer render_weapon;
    Vector2 vel;

    GameObject root;
    GameObject body;
    GameObject head;
    GameObject eye;
    GameObject weapon;
    bool isAni = false;
    bool isRun = false;

    float time;
    float time_blink = 0;
    float ran_blink = 3;
    float cycle;

    float moveBody;
    float moveBody_basic;

    Vector2 moveHead;
    Vector2 moveHead_basic;
    Vector2 moveHead_ani;
    Vector2 moveHead_view;

    Vector2 moveHead_type;

    void Awake()
    {
        root = transform.Find("Root").gameObject;
        body = root.transform.Find("Body").gameObject;
        head = root.transform.Find("Head").gameObject;
        eye = head.transform.Find("Eye").gameObject;
        weapon = root.transform.Find("Weapon").gameObject;
        ani_body = body.GetComponent<Animator>();
        ani_eye = eye.GetComponent<Animator>();
        ani_weapon = weapon.GetComponent<Animator>();
        player = GameObject.Find("Player");

        rigid = GetComponent<Rigidbody2D>();
        rigid_player = player.GetComponent<Rigidbody2D>();
        render_body = body.GetComponent<SpriteRenderer>();
        render_head = head.GetComponent<SpriteRenderer>();
        render_eye = eye.GetComponent<SpriteRenderer>();
        render_weapon = weapon.transform.Find("Sword").GetComponent<SpriteRenderer>();
        cycle = 0.1f;

        enemy = transform.GetComponent<EnemyController>();
        sword = player.transform.Find("Sword").GetComponent<Sword>();

        Setting_Type();
    }
    void Setting_Type()
    {
        if(enemy.enemyType == EnemyController.EnemyType.Enemy_A)
        {
            ani_body.runtimeAnimatorController = Resources.Load("Ani_A_Body") as RuntimeAnimatorController;
            moveBody_basic = 0;
            moveHead_type = new Vector2(0, 0);
        }
        else if (enemy.enemyType == EnemyController.EnemyType.Enemy_B)
        {
            ani_body.runtimeAnimatorController = Resources.Load("Ani_B_Body") as RuntimeAnimatorController;
            moveBody_basic = 0;
            moveHead_type = new Vector2(0.2f, 0);
        }
        else if (enemy.enemyType == EnemyController.EnemyType.Enemy_C)
        {
            ani_body.runtimeAnimatorController = Resources.Load("Ani_C_Body") as RuntimeAnimatorController;
            moveBody_basic = 0.15f;
            moveHead_type = new Vector2(0.1f, 0.3f);
        }
    }
    void Update()
    {
        if (CompareTag("Controlled"))
        {
            Setting_Tag(); // basic setting

            Player_Movement(); // about animation move

            // about code move
            MoveHead_View(0.03f); // move head to mouse pointer
            MoveBody_Run(0.015f); // change pos of haed, body at run state
            MoveHead_Jump();

            Positioning(); // final code move
        }
        if (CompareTag("Enemy"))
        {
            Setting_Tag(); // basic setting

            Enemy_Movement(); // about animation move

            MoveBody_Run(0.015f); // about code move (change pos of haed, body at run state)

            Positioning(); // final code move
        }
        if (CompareTag("Disarmed"))
        {
            Setting_Tag(); // basic setting

            Disarmed_Movement(); // about animation move

            MoveHead_Disarmed(0.1f); // about code move (change pos of haed at disarmed state)

            Positioning(); // final code move
        }

        Flip();
    }
    
    void Setting_Tag()
    {
        if(CompareTag("Controlled"))
        {
            moveHead_basic = new Vector2(0.0f, 0.95f + moveHead_type.y);
            vel = rigid_player.velocity;
        }
        if (CompareTag("Enemy"))
        {
            moveHead_basic = new Vector2(0.0f, 1.45f + moveHead_type.y);
            vel = rigid.velocity;
        }
        if (CompareTag("Disarmed"))
        {
            moveHead_basic = new Vector2(0.0f, 1.45f + moveHead_type.y);
            vel = rigid.velocity;
        }
    }
    void Player_Movement()
    {
        //run & jump
        if (vel.y > -1 && vel.y < 1)
        {
            ani_body.SetFloat("Run", Mathf.Abs(vel.x));
            isRun = true;
        }
        else
        {
            ani_body.SetFloat("Run", 0);
            ani_body.SetFloat("Jump", vel.y);
            isRun = false;
        }

        if (vel.y == 0)
            ani_body.SetFloat("Jump", vel.y);

        //attack
        if (sword.attack_Ani == true)
            ani_body.SetBool("IsAttack", true);
        else
            ani_body.SetBool("IsAttack", false);

        //not die
        ani_body.SetBool("IsDie", false);

        //blink
        Blink();
    }
    void Enemy_Movement()
    {
        //run
        ani_body.SetFloat("Run", Mathf.Abs(vel.x));

        //attack
        if (enemy.isAttake == true)
        {
            ani_weapon.SetBool("IsAttack", true);
            ani_body.SetBool("IsAttack", true);
            ani_body.speed = 0.15f;
        }
        else
        {
            ani_weapon.SetBool("IsAttack", false);
            ani_body.SetBool("IsAttack", false);
            ani_body.speed = 1f;
        }

        //blink
        Blink();
    }
    void MoveHead_View(float change)
    {
        Vector3 mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 check = mouse - player.transform.position;

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
    }
    void MoveHead_Jump()
    {
        if (ani_body.GetCurrentAnimatorStateInfo(0).IsName("Jump_Up"))
        {
            moveHead_ani.y = 0.075f;

            if (render_head.flipX == false)
                moveHead_ani.x = 0.15f;
            else
                moveHead_ani.x = -0.15f;
        }
        else if (ani_body.GetCurrentAnimatorStateInfo(0).IsName("Jump_Down"))
        {
            moveHead_ani.y = 0.075f;

            if (render_head.flipX == false)
                moveHead_ani.x = -0.1f;
            else
                moveHead_ani.x = 0.1f;
        }
    }
    void MoveHead_Disarmed(float speed)
    {
        time += Time.deltaTime;

        float cycle = 1f;

        if (time < cycle && moveHead_ani.x > -0.04f)
            moveHead_ani.x -= speed * Time.deltaTime;
        else if (time > cycle && moveHead_ani.x < 0.04f)
            moveHead_ani.x += speed * Time.deltaTime;

        if(time > 0.5f)
            moveHead_ani.y = -0.15f;
        
        if (time > cycle * 2)
            time = 0;
    }
    void MoveBody_Run(float change_run)
    {
        if (Mathf.Abs(vel.x) > 0.1f && ani_body.GetCurrentAnimatorStateInfo(0).IsName("Run"))
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
                moveHead_ani.x = 0.1f + moveHead_type.x;
            }
            else
            {
                moveHead_ani.x = -0.1f - moveHead_type.x;
            }
        }
        else if (enemy.isAttake == true)
        {
            
             if (render_head.flipX == true)
                moveHead_ani.x = -0.1f;
             else
                moveHead_ani.x = 0.1f;
        }
        else
        {
            time = 0;
            moveBody = 0;
            moveHead_ani.x = 0;
            moveHead_ani.y = 0;
        }
    }

    void Disarmed_Movement()
    {
        ani_body.speed = 1f;
        ani_body.SetBool("IsDie", true);
        ani_eye.SetBool("Blink", true);
    }

    void Blink()
    {
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
    }

    void Positioning()
    {
        if (CompareTag("Controlled"))
        {
            moveHead = new Vector3(moveHead_view.x + moveHead_basic.x + moveHead_ani.x, moveHead_view.y + moveHead_basic.y + moveHead_ani.y);
            head.transform.position = transform.position + new Vector3(moveHead.x, moveHead.y);
            body.transform.position = transform.position + Vector3.up * (moveBody + moveBody_basic);
        }
        if (CompareTag("Enemy"))
        {
            moveHead = new Vector3(moveHead_basic.x + moveHead_ani.x, moveHead_basic.y + moveHead_ani.y);
            head.transform.position = transform.position + new Vector3(moveHead.x, moveHead.y);
            body.transform.position = transform.position + Vector3.up * (moveBody + moveBody_basic + 0.5f);
        }
        if (CompareTag("Disarmed"))
        {
            moveHead = new Vector3(moveHead_basic.x + moveHead_ani.x, moveHead_basic.y + moveHead_ani.y);
            head.transform.position = transform.position + new Vector3(moveHead.x, moveHead.y);
            body.transform.position = transform.position + Vector3.up * (moveBody_basic + 0.5f);
        }
    }
    void Flip()
    {
        if (vel.x < 0)
        {
            render_body.flipX = true;
            render_head.flipX = true;
            render_eye.flipX = true;
            render_weapon.flipX = true;
            weapon.transform.position = weapon.transform.parent.position + Vector3.right * 0.75f;
        }
        else if (vel.x > 0)
        {
            render_body.flipX = false;
            render_head.flipX = false;
            render_eye.flipX = false;
            render_weapon.flipX = false;
            weapon.transform.position = weapon.transform.parent.position;
        }
        else
        {
            if (gameObject.tag == "Enemy")
            {
                if (player.transform.position.x - transform.position.x < 0)
                {
                    render_body.flipX = true;
                    render_head.flipX = true;
                    render_eye.flipX = true;
                    render_weapon.flipX = true;
                    weapon.transform.position = weapon.transform.parent.position + Vector3.right * 0.75f;
                }
                else
                {
                    render_body.flipX = false;
                    render_head.flipX = false;
                    render_eye.flipX = false;
                    render_weapon.flipX = false;
                    weapon.transform.position = weapon.transform.parent.position;
                }
            }
        }
    }
}
