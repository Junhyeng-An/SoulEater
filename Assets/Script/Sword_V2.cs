using System.Collections;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Sword_V2 : MonoBehaviour
{
    public float force = 25.0f;
    float angle;
    Rigidbody2D rb;
    GameObject P; //�÷��̾�
    [HideInInspector]
    public float angle2;
    bool isThrowing = false;
    float throwStartTime = 0f;
    Vector2 mouse;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        P = GameObject.FindWithTag("Player"); //�÷��̾ ã��
        if (!isThrowing)
        {
            GameObject target = GameObject.FindGameObjectWithTag("Player");

            //�÷��̾� ������ Į�� ������ �ڵ� ////////////////////////

            mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            angle = Mathf.Atan2(mouse.y - target.transform.position.y, mouse.x - target.transform.position.x) * Mathf.Rad2Deg;
            this.transform.rotation = Quaternion.AngleAxis(angle , Vector3.forward);
            Vector2 v2 = new Vector2(mouse.y - target.transform.position.y, mouse.x - target.transform.position.x);
            angle2 = Mathf.Atan2(v2.x, v2.y);
            transform.position = new Vector2(target.transform.position.x + Mathf.Cos(angle2), target.transform.position.y + Mathf.Sin(angle2));

            /////////////////////////////////////////////////////////
            

            if (Input.GetMouseButton(0))
            {
                GetComponent<SpriteRenderer>().color = Color.red;
                // ���콺 ���� ��ư�� ������ �ִ� ������ ó��
            }
            if (Input.GetMouseButtonUp(0))
            {
                GetComponent<SpriteRenderer>().color = Color.white;
                // ���콺 ���� ��ư�� �� ���� ó��
            }
            if (Input.GetMouseButton(1))
            {
                GetComponent<SpriteRenderer>().color = Color.blue;
                // ���콺 ������ ��ư�� ������ �ִ� ������ ó��
            }
            if (Input.GetMouseButtonUp(1))
            {
                GetComponent<SpriteRenderer>().color = Color.white;
                // ���콺 ������ ��ư�� �� ���� ó��
            }
           
        }

        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition); //���콺 ��ġ�� ������
        mousePosition.z = 0f; // Z ��ǥ�� 0���� ���� (2D ȯ��)

        // �÷��̾��� ���� ��ġ
        Vector3 playerPosition = transform.position;

        if (Input.GetKeyDown("q") && isThrowing ==false)
        {
            isThrowing = true;
            if (mousePosition.x > playerPosition.x)
            {
                // ���콺�� �����ʿ� ���� �� Į�� ���������� ������.
                Throwing(Vector2.right);
            }
            else
            {
                // ���콺�� ���ʿ� ���� �� Į�� �������� ������.
                Throwing(Vector2.left);
            }
        }
        if (isThrowing == true) 
        {
            throwStartTime += Time.deltaTime; // �ð��� �߰��մϴ�.
            Debug.Log(throwStartTime);
            if (throwStartTime >= 2.0f) //������ 3�ʵڿ� �ٽ� �÷��̾�� ���ƿ�
            {
                Debug.Log("���ƿ�");
                isThrowing = false;
                throwStartTime = 0;
            }
        }
    }

    private void Throwing(Vector2 direction)
    {
        rb.velocity = direction.normalized * 10f;
        //rb.AddForce(direction * force, ForceMode2D.Impulse);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            isThrowing = false;
            P.tag = collision.gameObject.tag;
            P.layer = 0;
            collision.gameObject.tag = "Player";
            collision.gameObject.layer = 6;
            Debug.Log("�����");
            throwStartTime = 0;
        }
    }

}

