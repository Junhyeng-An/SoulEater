using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    [HideInInspector]
    public float angle;

    GameObject target;

    Vector2 mouse;

    bool isThrowing;

    void Awake()
    {
        target = transform.parent.gameObject;
    }

    void Update()
    {

    }

    public void ReadyThrow()
    {
        // 초기 속도와 발사 각도를 라디안으로 변환합니다.
        float radians = angle * Mathf.Deg2Rad;

        // 초기 속도를 x, y 성분으로 분리합니다.
        float initialVelocityX = Mathf.Cos(angle) * 50 * 6;
        float initialVelocityY = 100 * 6;

        // 중력 가속도를 가져옵니다.
        float gravity = Mathf.Abs(Physics.gravity.y);

        // 시간 간격
        float timeStep = 0.02f;

        // 초기 위치 설정
        Vector3 currentPosition = new Vector2(0, 0);

        // 포물선 궤적 그리기
        for (float t = 0; t < 10f; t += timeStep)
        {
            float x = initialVelocityX * t;
            float y = (initialVelocityY * t) - (0.5f * gravity * t * t);

            // 현재 시간에 따른 위치 계산
            Vector3 newPosition = new Vector3(x, y);

            // 궤적 선 그리기
            Debug.DrawLine(currentPosition, currentPosition + newPosition, Color.red, 0.1f);

            // 현재 위치 갱신
            //currentPosition = newPosition;
        }
    }
    public void Throwing() //when throw sword
    {
        transform.position = target.transform.position; 
        transform.Rotate(0, 0, 500 * Time.deltaTime);

        GetComponent<SpriteRenderer>().color = Color.white;
    }
    public void PosRot()//sword's position & rotation
    {
        mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition); //mouse position
        angle = Mathf.Atan2(mouse.y - target.transform.position.y, mouse.x - target.transform.position.x); //angle of mouse to player
        transform.rotation = Quaternion.AngleAxis(angle * Mathf.Rad2Deg, Vector3.forward); // look mouse

        transform.position = new Vector2(target.transform.position.x + Mathf.Cos(angle), target.transform.position.y + Mathf.Sin(angle)); // sword position
    }
    public void OnMouseEvent() //When click mouse
    {
        if (Input.GetMouseButtonDown(0)) //left
        {
            GetComponent<SpriteRenderer>().color = Color.red;
        }
        if (Input.GetMouseButtonUp(0))
        {
            GetComponent<SpriteRenderer>().color = Color.white;
        }
        if (Input.GetMouseButtonDown(1)) //right
        {
            GetComponent<SpriteRenderer>().color = Color.blue;
        }
        if (Input.GetMouseButtonUp(1))
        {
            GetComponent<SpriteRenderer>().color = Color.white;
        }
    }
}