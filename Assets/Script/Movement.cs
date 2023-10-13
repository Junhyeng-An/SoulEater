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

    }

    public void Jump()
    {
        rigid.velocity = Vector2.up * jumpForce;
    }
    public void Move(float x)
    {
        rigid.velocity = new Vector2(x * speed, rigid.velocity.y);
    }
    public void Dash()
    {
        float angle = sword.GetComponent<Sword>().angle;
        rigid.AddForce(new Vector2(Mathf.Cos(angle) * 10, Mathf.Sin(angle) * 10), ForceMode2D.Impulse);
        //GetComponent<CircleCollider2D>().isTrigger = true;
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        
        if (col.gameObject.layer == 20)
        {
            GetComponent<PlayerController>().isThrowing = false;
            GetComponent<CircleCollider2D>().isTrigger = false;
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
}
