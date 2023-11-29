using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Sword : MonoBehaviour
{
    [HideInInspector]
    public float swingForce;

    public float limitSpeed;

    private StatController stat;
    GameObject target;
    Rigidbody2D rigid;
    
    private Vector2 mouseBefore;
    private Vector2 mouse;

    float angle;
    float angleDelta;
    float angleDeltaDelta;

    bool isThrowing;
    public bool isSwing;
    public bool isKnock;

    float stretch;
    bool attack_Ani = false;
    void Awake()
    {
        target = transform.parent.gameObject;
        rigid = GetComponent<Rigidbody2D>();

        stat = GameObject.Find("GameManager").GetComponent<StatController>();

        stretch = 1.0f;
    }

    void Update()
    {
        if(attack_Ani == true)
        {
            Attack_Animation();
        }
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
        angleDeltaDelta = angleDelta;

        mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition); //mouse position

        angle = Mathf.Atan2(mouse.y - target.transform.position.y, mouse.x - target.transform.position.x); //angle of mouse to player

        float angleAfter = angle;
        angleDelta = NormalizeRadian(angleAfter - angleBefore);
        swingForce = Mathf.Abs(angleDelta * Mathf.Rad2Deg);

        angleDeltaDelta *= angleDelta;

        if(angleDeltaDelta *10000 < 0)
        {
            Debug.Log(angleDeltaDelta*10000);
        }
        else
        {

        }

        if (swingForce > limitSpeed)
        {
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

        angle = NormalizeRadian(angle);

        transform.rotation = Quaternion.AngleAxis(angle * Mathf.Rad2Deg, Vector3.forward); // look mouse

        Vector2 mousePos = new Vector2(target.transform.position.x + Mathf.Cos(angle) * stretch, target.transform.position.y + Mathf.Sin(angle) * stretch); // sword position
        transform.position = mousePos;

        Vector2 posAfter = transform.localPosition;
    }
    float NormalizeRadian(float radian)
    {
        if (radian * Mathf.Rad2Deg > 180)
        {
            radian -= Mathf.PI * 2;
        }
        else if (radian * Mathf.Rad2Deg < -180)
        {
            radian += Mathf.PI * 2;
        }
        return radian;
    }

    public void Attack()
    {
        if(stat.Player_CurST >= 3)
        {
            isSwing = true;
            stat.Stat("ST", -3);
            gameObject.tag = "Attack";
            GetComponentInChildren<SpriteRenderer>().color = Color.red;

            attack_Ani = true;
        }
    }
    public void Parrying()
    {
        if (stat.Player_CurST >= 6)
        {
            isSwing = true;
            stat.Stat("ST", -6);
            gameObject.tag = "Parrying";
            GetComponentInChildren<SpriteRenderer>().color = Color.blue;
        }
    }
    public void Idle()
    {
        isSwing = false;
        isKnock = false;
        gameObject.tag = "Sword";
        GetComponentInChildren<SpriteRenderer>().color = Color.white;
    }
    void Attack_Animation()
    {
        float stretch_max = 2.0f;
        if (stretch < stretch_max)
        {
            stretch += 0.1f;
        }
        else if(stretch >= stretch_max)
        {
            stretch = 1.0f;
            attack_Ani = false;
        }
    }
    public void GameOver()
    {
        transform.rotation = Quaternion.Euler(0, 0, 80);
    }
}