using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public GameObject sword;
    [SerializeField]
    private float speed = 5.0f; // �̵��ӵ�
    private float jumpForce = 8.0f; // ���� �Ŀ�
    private float throwForce = 6.0f; // ������ �Ŀ�
    private Rigidbody2D rigid;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {

    }
    public void Jump()
    {
        // jumpForce�� ũ�⸸ŭ ���� �������� �ӷ� ����
        rigid.velocity = Vector2.up * jumpForce;
    }

    public void Move(float x)
    {
        // x�� �̵��� x * speed��, y�� �̵��� ������ �ӷ� �� (����� �߷�)
        rigid.velocity = new Vector2(x * speed, rigid.velocity.y);
    }
    public void Throwing()
    {
        float angle = sword.GetComponent<Sword>().angle;
        rigid.AddForce(new Vector2(Mathf.Cos(angle) * 50 * throwForce, 100 * throwForce));
        GetComponent<CircleCollider2D>().isTrigger = true;
    }
    public void Dash()
    {
        float angle = sword.GetComponent<Sword>().angle;
        rigid.AddForce(new Vector2(Mathf.Cos(angle) * 10, Mathf.Sin(angle) * 10), ForceMode2D.Impulse);
        //GetComponent<CircleCollider2D>().isTrigger = true;
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        
        if (col.gameObject.layer == 20) // ������ �ٴڰ� �浹 ��
        {
            GetComponent<PlayerController>().isThrowing = false;
            GetComponent<CircleCollider2D>().isTrigger = false;
        }
    }
}
