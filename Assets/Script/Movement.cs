using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public GameObject sword;

    [SerializeField]
    public float speed = 5.0f;
    public float jumpForce = 8.0f;
    public float throwForce = 12.0f;
    public float dashForce = 5.0f;

    public int bounceCount = 2;
    public bool gameover = false;

    bool isJumping = false;
    bool isThrowing = false;

    private Rigidbody2D rigid;
    private LineRenderer line;

    Vector2 _velocity;

    Vector2 startDragPos;
    Vector2 endDragPos;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        line = GetComponent<LineRenderer>();
    }

    private void Update()
    {
        float angle = sword.GetComponent<Sword>().angle;
        Debug.DrawRay(transform.position, Vector2.down, new Color(1, 0, 0));
        Debug.DrawRay(transform.position, new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * dashForce, new Color(0, 1, 0));
        ///
        if (GameObject.FindGameObjectWithTag("Controlled") != null)
        {
            GameObject controlled = GameObject.FindGameObjectWithTag("Controlled");
            string clone_Name = controlled.name + "(Clone)";
            GameObject Clone = GameObject.Find(clone_Name);
            Destroy(Clone, 0.1f);
        }
    }
    public void Landing() //check can jump and can distance dash
    {
        LayerMask mask = 1 << 20;
        LayerMask mask2 = 1 << 21;
        float angle = sword.GetComponent<Sword>().angle;
        RaycastHit2D rayHit_Jump = Physics2D.Raycast(transform.position, Vector2.down, 1, mask | mask2);
        RaycastHit2D rayHit_Dash = Physics2D.Raycast(transform.position, new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)), dashForce, mask);

        if (rayHit_Jump.collider != null)
        {
            if(rayHit_Jump.distance <= 0.55f && rigid.velocity.y <= 0)
            {
                isJumping = false;
            }
        }

        if (rayHit_Dash.collider != null)
        {
            dashForce = rayHit_Dash.distance - 0.5f;
        }
        else
        {
            if(dashForce < 5.0f)
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
    public void Move(float x)
    {
        rigid.velocity = new Vector2(x * speed, rigid.velocity.y);
    }
    public void Dash()
    {
        int cloneCount = 3;
        Vector2 posBefore = transform.position;

        float angle = sword.GetComponent<Sword>().angle;
        transform.Translate(new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * dashForce);
        rigid.velocity = new Vector2(rigid.velocity.x, 0);

        Vector2 posAfter = transform.position;

        Vector2 posA = posAfter - posBefore;

        Vector3 posZ = new Vector3(0,0,-2);
        
        GameObject controlled = GameObject.FindGameObjectWithTag("Controlled");
        for(int i = 0; i < cloneCount; i++)
        {
            GameObject clone = Instantiate(controlled, posAfter - posA / Mathf.Pow(2, i + 1), transform.rotation);
            clone.GetComponent<SpriteRenderer>().color = new Color(0.75f, 0.5f, 1, 0.1f * (i + 1)*2);
        }
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        float E = 0.75f; //Modulus of Elasticity
        if (col.gameObject.layer == 20)
        {
            Vector2 posPlayer = transform.position;
            Vector2 posCol = col.ClosestPoint(transform.position);
            Vector2 v = posCol - posPlayer;
            float angle = Mathf.Atan2(v.y, v.x) * Mathf.Rad2Deg;

            //GetComponent<PlayerController>().isThrowing = false;
            if (bounceCount > 0 && isThrowing == true)
            {
                if (rigid.velocity.y <= 0)
                    bounceCount--;
                rigid.velocity = new Vector2(rigid.velocity.x, -rigid.velocity.y) * E;
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
                    // 좌우 벽과 충돌한 경우, x 방향의 속도를 반전
                    rigid.velocity = new Vector2(rigid.velocity.x, -rigid.velocity.y);
                    Debug.Log("LR");
                }
                else
                {
                    // 상하 벽과 충돌한 경우, y 방향의 속도를 반전
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
        startDragPos = transform.position;
        line.enabled = true;

        Time.timeScale = 0.25f;
    }
    public void Throw_Line()
    {
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
    }
    public void Throw()
    {
        isThrowing = true;
        rigid.velocity = _velocity;

        GetComponent<CircleCollider2D>().isTrigger = true;
        line.enabled = false;

        Time.timeScale = 1;
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
        if (rigid.velocity.y > 0)
        {
            Physics2D.IgnoreLayerCollision(10, 21, true);
        }
        else
        {
            Physics2D.IgnoreLayerCollision(10, 21, false);
        }
    }
}
