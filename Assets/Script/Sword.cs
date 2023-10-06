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
        // �ʱ� �ӵ��� �߻� ������ �������� ��ȯ�մϴ�.
        float radians = angle * Mathf.Deg2Rad;

        // �ʱ� �ӵ��� x, y �������� �и��մϴ�.
        float initialVelocityX = Mathf.Cos(angle) * 50 * 6;
        float initialVelocityY = 100 * 6;

        // �߷� ���ӵ��� �����ɴϴ�.
        float gravity = Mathf.Abs(Physics.gravity.y);

        // �ð� ����
        float timeStep = 0.02f;

        // �ʱ� ��ġ ����
        Vector3 currentPosition = new Vector2(0, 0);

        // ������ ���� �׸���
        for (float t = 0; t < 10f; t += timeStep)
        {
            float x = initialVelocityX * t;
            float y = (initialVelocityY * t) - (0.5f * gravity * t * t);

            // ���� �ð��� ���� ��ġ ���
            Vector3 newPosition = new Vector3(x, y);

            // ���� �� �׸���
            Debug.DrawLine(currentPosition, currentPosition + newPosition, Color.red, 0.1f);

            // ���� ��ġ ����
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