using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Sword : MonoBehaviour
{
    [HideInInspector]
    public float angle;
    [HideInInspector]
    public float swingForce;

    public float limitSpeed;

    GameObject target;
    Rigidbody2D rigid;

    private Vector2 mouseBefore;
    private Vector2 mouse;

    bool isThrowing;

    void Awake()
    {
        target = transform.parent.gameObject;
        rigid = GetComponent<Rigidbody2D>();
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
        Vector2 posBefore = transform.position;

        float angleBefore = angle;

        mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition); //mouse position
        float mouseDelta = (mouse - mouseBefore).magnitude;
        mouseBefore = mouse;

        angle = Mathf.Atan2(mouse.y - target.transform.position.y, mouse.x - target.transform.position.x); //angle of mouse to player

        float angleAfter = angle;
        float angleDelta = angleAfter - angleBefore;
        swingForce = Mathf.Abs(angleDelta * Mathf.Rad2Deg);

        Debug.Log("Before" + angleBefore);
        Debug.Log("After" + angleAfter);
        if (swingForce > 180)
        {
            swingForce = Mathf.Abs(swingForce - 360);
        }
        if(angleDelta*Mathf.Rad2Deg > 180)
        {
            angleDelta -= 360;
        }
        else if(angleDelta * Mathf.Rad2Deg < -180)
        {
            angleDelta += 360;
        }



        if (swingForce > limitSpeed)
        {
            //Debug.Log((angleAfter - angleBefore) * Mathf.Rad2Deg);
            //Debug.Log(angleDelta);

            if(angleDelta > 0)
            {
                
                    angle = angleBefore + limitSpeed * Mathf.Deg2Rad;
            }
            else
            {
                
                    angle = angleBefore - limitSpeed * Mathf.Deg2Rad;
            }
            swingForce = limitSpeed;
        }

        if(angle * Mathf.Rad2Deg > 180)
        {
            angle -= Mathf.PI * 2;
        }
        else if (angle * Mathf.Rad2Deg < -180)
        {
            angle += Mathf.PI * 2;
        }

        transform.rotation = Quaternion.AngleAxis(angle * Mathf.Rad2Deg, Vector3.forward); // look mouse

        Vector2 mousePos = new Vector2(target.transform.position.x + Mathf.Cos(angle), target.transform.position.y + Mathf.Sin(angle)); // sword position
        //transform.position = Vector2.MoveTowards(transform.position, mousePos, 0.2f);
        transform.position = mousePos;

        Vector2 posAfter = transform.localPosition;
    }

    public void GameOver()
    {
        transform.rotation = Quaternion.Euler(0, 0, 80);
    }
}