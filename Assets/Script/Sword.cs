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

    [HideInInspector]
    public bool isSwing;
    [HideInInspector]
    public bool isKnock;

    float stretch;

    public float stretch_Min;
    public float stretch_Max;
    [Range(0.0f, 1.0f)]
    public float stretch_Speed;

    bool attack_Ani = false;
    bool isMax = false;

    void Awake()
    {
        target = transform.parent.gameObject;
        rigid = GetComponent<Rigidbody2D>();

        stat = GameObject.Find("GameManager").GetComponent<StatController>();

        stretch = 1.0f;
        stretch_Min = 1.0f;
        stretch_Max = 2.0f;
        stretch_Speed = 0.1f;
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
            //Debug.Log(angleDeltaDelta*10000);
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
        if(stat.Player_CurST >= 3 && attack_Ani == false)
        {
            isSwing = true;
            stat.Stat("ST", -3);
            gameObject.tag = "Attack";
            //GetComponentInChildren<SpriteRenderer>().color = Color.red;
            Debug.Log(GetComponentInChildren<SpriteRenderer>().color);

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
        if (stretch < stretch_Max && isMax == false)
        {
            stretch += stretch_Speed;
        }
        else if(stretch >= stretch_Max && isMax == false)
        {
            isMax = true;
        }
        else if(stretch > stretch_Min && isMax == true)
        {
            stretch -= stretch_Speed;
        }
        else if(stretch <= stretch_Min && isMax == true)
        {
            attack_Ani = false;
            isMax = false;
            Idle();
        }

    }
    public void GameOver()
    {
        transform.rotation = Quaternion.Euler(0, 0, 80);
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        Debug.Log(col.gameObject.layer);

        if (col.gameObject.layer == LayerMask.NameToLayer("E_Attack"))
        {
            //col.enabled = false;
            Debug.Log("����");
        }
    }
}