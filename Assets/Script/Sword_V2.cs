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
                // 마우스 왼쪽 버튼을 누르고 있는 도중의 처리
            }
            if (Input.GetMouseButtonUp(0))
            {
                GetComponent<SpriteRenderer>().color = Color.white;
                // 마우스 왼쪽 버튼을 뗄 때의 처리
            }
            if (Input.GetMouseButton(1))
            {
                GetComponent<SpriteRenderer>().color = Color.blue;
                // 마우스 오른쪽 버튼을 누르고 있는 도중의 처리
            }
            if (Input.GetMouseButtonUp(1))
            {
                GetComponent<SpriteRenderer>().color = Color.white;
                // 마우스 오른쪽 버튼을 뗄 때의 처리
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
        // 마우스 포인터의 위치를 월드 좌표로 가져옵니다.
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // 현재 오브젝트의 위치를 가져옵니다.
        Vector3 currentPosition = transform.position;

        // 마우스 포인터 방향 벡터를 계산합니다.
        Vector3 direction = mousePosition - currentPosition;

        // 힘의 크기를 조절합니다.
        float forceMagnitude = 50.0f; // 힘의 크기를 원하는 값으로 조절하세요.

        // 방향 벡터를 정규화하고 힘을 가할 Rigidbody2D 컴포넌트를 가져옵니다.
        direction.Normalize();
        Rigidbody2D rb = GetComponent<Rigidbody2D>();

        // 힘을 가합니다. 방향 벡터의 크기에 forceMagnitude를 곱하여 힘을 조절합니다.
        rb.AddForce(direction * forceMagnitude, ForceMode2D.Impulse);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy") 
        {
         
        }
    }

}

