using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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

    public void Throw() //when throw sword
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