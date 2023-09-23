using System.Collections;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Sword_V2 : MonoBehaviour
{
    public float force = 25.0f;
    float angle;
    Rigidbody2D rb;
    GameObject P; //플레이어
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
        P = GameObject.FindWithTag("Player"); //플레이어를 찾음
        if (!isThrowing)
        {
            GameObject target = GameObject.FindGameObjectWithTag("Player");

            //플레이어 주위로 칼을 돌리는 코드 ////////////////////////

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

        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition); //마우스 위치를 가져옴
        mousePosition.z = 0f; // Z 좌표를 0으로 설정 (2D 환경)

        // 플레이어의 현재 위치
        Vector3 playerPosition = transform.position;

        if (Input.GetKeyDown("q") && isThrowing ==false)
        {
            isThrowing = true;
            if (mousePosition.x > playerPosition.x)
            {
                // 마우스가 오른쪽에 있을 때 칼을 오른쪽으로 날린다.
                Throwing(Vector2.right);
            }
            else
            {
                // 마우스가 왼쪽에 있을 때 칼을 왼쪽으로 날린다.
                Throwing(Vector2.left);
            }
        }
        if (isThrowing == true) 
        {
            throwStartTime += Time.deltaTime; // 시간을 추가합니다.
            Debug.Log(throwStartTime);
            if (throwStartTime >= 2.0f) //던지고 3초뒤에 다시 플레이어에게 돌아옴
            {
                Debug.Log("돌아옴");
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
            Debug.Log("변경됨");
            throwStartTime = 0;
        }
    }

}

