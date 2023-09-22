using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Sword_V2 : MonoBehaviour
{
    public float force = 5.0f;
    float angle;

    [HideInInspector]
    public float angle2;
    bool isThrowing = false;
    Vector2 mouse;
    void Update()
    {
        if (!isThrowing)
        {
            GameObject target = GameObject.FindGameObjectWithTag("Player");
            //GameObject player = GameObject.FindGameObjectWithTag("Player");
            //transform.position = player.transform.position;

            mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            angle = Mathf.Atan2(mouse.y - target.transform.position.y, mouse.x - target.transform.position.x) * Mathf.Rad2Deg;
            this.transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
            Vector2 v2 = new Vector2(mouse.y - target.transform.position.y, mouse.x - target.transform.position.x);
            angle2 = Mathf.Atan2(v2.x, v2.y);
            transform.position = new Vector2(target.transform.position.x + Mathf.Cos(angle2), target.transform.position.y + Mathf.Sin(angle2));

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
        if (Input.GetKeyDown("q"))
        {
            isThrowing = true;
            Throwing();
        }
    }

    private void Throwing()
    {
        // ���콺 �������� ��ġ�� ���� ��ǥ�� �����ɴϴ�.
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // ���� ������Ʈ�� ��ġ�� �����ɴϴ�.
        Vector3 currentPosition = transform.position;

        // ���콺 ������ ���� ���͸� ����մϴ�.
        Vector3 direction = mousePosition - currentPosition;

        // ���� ũ�⸦ �����մϴ�.
        float forceMagnitude = 50.0f; // ���� ũ�⸦ ���ϴ� ������ �����ϼ���.

        // ���� ���͸� ����ȭ�ϰ� ���� ���� Rigidbody2D ������Ʈ�� �����ɴϴ�.
        direction.Normalize();
        Rigidbody2D rb = GetComponent<Rigidbody2D>();

        // ���� ���մϴ�. ���� ������ ũ�⿡ forceMagnitude�� ���Ͽ� ���� �����մϴ�.
        rb.AddForce(direction * forceMagnitude, ForceMode2D.Impulse);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy") 
        {
         
        }
    }

}

