using System;
using System.Collections;
using System.Collections.Generic;
using PixelCrushers;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Movement : MonoBehaviour
{
    public GameObject sword;

    [SerializeField]
    private float speed;
    private float jumpForce ;
    private float throwForce = 12.0f;

    [HideInInspector] public float dashForce = 5.0f;
    public float angle;

    public int bounceCount = 2;
    public bool gameover = false;
    public int maxJumps = 2;
    public int jumpsRemaining;
    [HideInInspector] public bool isJumping = false;
    [HideInInspector] public bool isDoubleJump = false;
    bool isThrowing = false;
    bool isDown = false;
    float time_down = 0;
    float Dash_D;
    [HideInInspector] public int jumpCount = 0;
    public Vector2 posMid;

    Rigidbody2D rigid;
    LineRenderer line;
    TimeScale timeScale;

    Vector2 _velocity;

    Vector2 startDragPos;
    Vector2 endDragPos;

    RaycastHit2D rayHit_Jump;

    private void Awake()
    {
        jumpForce = DataManager.Instance._PlayerData.jump;
        speed = DataManager.Instance._PlayerData.speed;
        
        Dash_D = DataManager.Instance._Player_Skill.Dash;
        
        
        rigid = GetComponent<Rigidbody2D>();
        line = GetComponent<LineRenderer>();
        timeScale = GameObject.Find("GameManager").GetComponent<TimeScale>();

        jumpsRemaining = maxJumps;
    }

    private void Update()
    {
        Dash_D = DataManager.Instance._Player_Skill.Dash;
        Vector2 mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition); //mouse position
        angle = Mathf.Atan2(mouse.y - transform.position.y, mouse.x - transform.position.x);
        Debug.DrawRay(transform.position, Vector2.down, new Color(1, 0, 0));
        Debug.DrawRay(transform.position, new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * dashForce, new Color(0, 1, 0));
        ///
        if (GameObject.FindGameObjectWithTag("Controlled") != null)
        {
            GameObject controlled = GameObject.FindGameObjectWithTag("Controlled").transform.Find("Root").gameObject;
            string clone_Name = controlled.name + "(Clone)";
            GameObject Clone = GameObject.Find(clone_Name);
            Destroy(Clone, 0.1f);
        }
        
        if(gameover == true)
            GameOver();
        
        
    }

    public void GameOver()
    {
        SettingManager.Instance.Game_Over_Panel_Active();
        gameover = false;
    }
    
    
    
    
    public void Landing() //check can jump and can distance dash
    {
        LayerMask mask = 1 << 20;
        LayerMask mask2 = 1 << 21;

        rayHit_Jump = Physics2D.Raycast(transform.position, Vector2.down, 1, mask | mask2);
        RaycastHit2D rayHit_Dash = Physics2D.Raycast(transform.position, new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)), dashForce, mask);

        if (rayHit_Jump.collider != null)
        {
            if (rayHit_Jump.distance <= 0.55f && rigid.velocity.y <= 0)
            {
                isJumping = false;
                jumpCount = 0;
                jumpsRemaining = maxJumps;
            }
        }

        if (rayHit_Dash.collider != null)
        {
            dashForce = rayHit_Dash.distance - 0.5f;
        }
        else
        {
            if (dashForce < Dash_D)
                dashForce += 0.1f;
        }
    }
    public void Jump()
    {
        if (isJumping == false)
        {
            rigid.velocity = Vector2.up * jumpForce;
            isJumping = true;
        }
    }
    public void Double_Jump()
    {
        rigid.velocity = Vector2.up * jumpForce;
        jumpsRemaining--;
    }
    public void Jump_Down()
    {
        try
        {
            if (rayHit_Jump.collider.gameObject.layer == LayerMask.NameToLayer("Platform"))
            {
                //Debug.Log(rayHit_Jump.collider.gameObject.layer);

                isDown = true;
                time_down = 0;
            } }
        catch (NullReferenceException e)
        {

        }
    }
    public void Move(float x)
    {
        rigid.velocity = new Vector2(x * (speed+DataManager.Instance._Player_Skill.Skill_Speed), rigid.velocity.y);
        //Debug.Log(rigid.velocity.x);
    }
    public void Dash()
    {
        int cloneCount = 5;
        Vector2 posBefore = transform.position;

        transform.Translate(new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * dashForce);
        rigid.velocity = new Vector2(rigid.velocity.x, 0);

        Vector2 posAfter = transform.position;

        posMid = posAfter - posBefore;

        Vector3 posZ = new Vector3(0, 0, -2);

        GameObject controlled = GameObject.FindGameObjectWithTag("Controlled").transform.Find("Root").gameObject;
        for (int i = 0; i < cloneCount; i++)
        {
            GameObject clone = Instantiate(controlled, posAfter - posMid / Mathf.Pow(1.8f, i + 1), transform.rotation);

            clone.transform.localScale = Vector3.one;

            ChangeColorRecursive(clone.transform, i);
        }
    }
    void ChangeColorRecursive(Transform clone, int i)
    {
        foreach (Transform child in clone.transform)
        {
            // 자식 객체의 Renderer 컴포넌트 가져오기
            Renderer childRenderer = child.GetComponent<Renderer>();

            // Renderer 컴포넌트가 있다면 색상 변경
            if (childRenderer != null)
            {
                childRenderer.material.color = new Color(0.75f, 0.5f, 1, 0.1f * (i + 1));
            }

            ChangeColorRecursive(child, i);
        }
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        float E = 0.75f; //Modulus of Elasticity
        if (col.gameObject.layer == 20)
        {
            isThrowing = GetComponent<PlayerController>().isThrowing;

            Vector2 posPlayer = transform.position;
            Vector2 posCol = col.ClosestPoint(transform.position);
            Vector2 v = posCol - posPlayer;
            float angle = Mathf.Atan2(v.y, v.x) * Mathf.Rad2Deg; //have to fix

            //GetComponent<PlayerController>().isThrowing = false;
            if (bounceCount > 0 && isThrowing == true)
            {
                //if (rigid.velocity.y <= 0)
                {
                    bounceCount--;
                    rigid.velocity = new Vector2(rigid.velocity.x, -rigid.velocity.y) * E;
                }
                /*if((angle >= 45 && angle < 135) || (angle >= -135 && angle < -45))
                    rigid.velocity = new Vector2(rigid.velocity.x, -rigid.velocity.y) * E;
                else if((angle >= 0 && angle < 45) || (angle >= 135 && angle <= 180)
                     || (angle >= -45 && angle < 0) || (angle >= -180 && angle < -135))
                    rigid.velocity = new Vector2(-rigid.velocity.x, rigid.velocity.y) * E;
                bounceCount--;*/

                /*Vector2 collisionDirection = (col.transform.position - transform.position).normalized;
                Debug.Log(collisionDirection);
                if (Mathf.Abs(collisionDirection.x) > Mathf.Abs(collisionDirection.y))
                {
                    // �¿� ���� �浹�� ���, x ������ �ӵ��� ����
                    rigid.velocity = new Vector2(rigid.velocity.x, -rigid.velocity.y);
                    Debug.Log("LR");
                }
                else
                {
                    // ���� ���� �浹�� ���, y ������ �ӵ��� ����
                    rigid.velocity = new Vector2(-rigid.velocity.x, rigid.velocity.y);
                    Debug.Log("UD");
                }*/
            }
            else
            {
                GetComponent<CircleCollider2D>().radius = 0.25f;
                GetComponent<CircleCollider2D>().isTrigger = false;
                gameover = true;
            }
        }
    }
    public void Throw_Ready()
    {
        //startDragPos = transform.position;
        line.enabled = true;
        //timeScale.SlowMotionUpdate(TimeScale.MotionType.throwing);
    }
    public void Throw_Line()
    {
        startDragPos = transform.position;
        endDragPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 fix = (endDragPos - startDragPos).normalized;
        _velocity = new Vector2(fix.x * throwForce / 2, throwForce);

        Vector2[] trajectory = Plot(rigid, (Vector2)transform.position, _velocity, 500);

        line.positionCount = trajectory.Length;

        Vector3[] positions = new Vector3[trajectory.Length];

        for (int i = 0; i < trajectory.Length; i++)
        {
            positions[i] = trajectory[i];
        }

        line.SetPositions(positions);

        timeScale.SlowMotion(TimeScale.MotionType.throwing);
    }
    public void Throw()
    {
        rigid.velocity = _velocity;

        GetComponent<CircleCollider2D>().isTrigger = true;
        line.enabled = false;

        timeScale.SlowMotionUpdate(TimeScale.MotionType.back);
    }
    public Vector2[] Plot(Rigidbody2D rigidbody, Vector2 pos, Vector2 velocity, int steps)
    {
        Vector2[] results = new Vector2[steps];

        float timestep = Time.fixedDeltaTime / Physics2D.velocityIterations;
        Vector2 gravityAccel = Physics2D.gravity * rigidbody.gravityScale * timestep * timestep;

        float drag = 1f - timestep * rigidbody.drag;
        Vector2 moveStep = velocity * timestep;

        for (int i = 0; i < steps; i++)
        {
            moveStep += gravityAccel;
            moveStep *= drag;
            pos += moveStep;
            results[i] = pos;
        }

        return results;
    }
    public void Return()
    {
        if(transform.position.y <= -10)
        {
            transform.position = new Vector2(0, 5);
            rigid.velocity = new Vector2(rigid.velocity.x, 0);
            gameover = false;
        }
    }
    public void WallCheck()
    {
        time_down += Time.deltaTime;

        if(time_down >= 0.375f)
        {
            isDown = false;
        }
        
        if (rigid.velocity.y > 0)
        {
            Physics2D.IgnoreLayerCollision(10, 21, true);
        }
        else
        {
            Physics2D.IgnoreLayerCollision(10, 21, false);
        }
        if (isDown == true)
        {
            Physics2D.IgnoreLayerCollision(10, 21, true);
        }
    }
}
