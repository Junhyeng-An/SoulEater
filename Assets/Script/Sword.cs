using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    [HideInInspector]
    public float angle;

    GameObject target;

    Vector2 mouse;

    void Awake()
    {
        target = transform.parent.gameObject;
    }

    void Update()
    {
        if (target.GetComponent<PlayerController>().isThrowing == true)
        {
            Time.timeScale = 0.25f;
            transform.position = target.transform.position;
            transform.Rotate(0, 0, 500 * Time.deltaTime);

            GetComponent<SpriteRenderer>().color = Color.white;
        }
        else if (target.GetComponent<PlayerController>().isThrowing == false)
        {
            mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            angle = Mathf.Atan2(mouse.y - target.transform.position.y, mouse.x - target.transform.position.x);
            transform.rotation = Quaternion.AngleAxis(angle * Mathf.Rad2Deg, Vector3.forward); // 마우스방향 바라보기

            transform.position = new Vector2(target.transform.position.x + Mathf.Cos(angle), target.transform.position.y + Mathf.Sin(angle)); // 마우스 위치에따른 검 위치

            if (Input.GetMouseButtonDown(0)) //마우스 좌클릭 시
            {
                GetComponent<SpriteRenderer>().color = Color.red;
            }
            if (Input.GetMouseButtonUp(0)) // 마우스 좌클릭 뗄 시
            {
                GetComponent<SpriteRenderer>().color = Color.white;
            }
            if (Input.GetMouseButtonDown(1)) // 마우스 우클릭 시
            {
                GetComponent<SpriteRenderer>().color = Color.blue;
            }
            if (Input.GetMouseButtonUp(1)) // 마우스 우클리 뗄 시
            {
                GetComponent<SpriteRenderer>().color = Color.white;
            }
        }
    }
}