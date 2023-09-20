using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    float angle;

    public GameObject target;

    [HideInInspector]
    public float angle2;

    Vector2 mouse;

    // Start is called before the first frame update
    void Awake()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (target.GetComponent<PlayerController>().isThrowing == true)
        {
            transform.position = target.transform.position;
            transform.Rotate(0, 0, 10);

            GetComponent<SpriteRenderer>().color = Color.white;
        }
        else
        {
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
    }
}