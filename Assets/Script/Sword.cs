using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Sword : MonoBehaviour
{
    [HideInInspector] public float swingForce;
    
    [HideInInspector] public float damage_playerAttack;
    [HideInInspector] public float damage_playerParrying;
    [HideInInspector] public float sword_reach;

    public float limitSpeed;

    private StatController stat;
    GameObject controlledObject;
    GameObject target;
    Rigidbody2D rigid;
    
    private Vector2 mouseBefore;
    private Vector2 mouse;

    public float angle;
    float angleDelta;
    float angleDeltaDelta;

    bool isThrowing;

    [HideInInspector]
    public bool isSwing;
    [HideInInspector]
    public bool isKnock;
    [HideInInspector]
    public bool isParrying;

    float stretch;

    public float stretch_Min;
    public float stretch_Max;
    [Range(0.0f, 1.0f)]
    public float stretch_Speed;

    public bool attack_Ani = false;
    bool isMax = false;

    void Awake()
    {
        target = transform.parent.gameObject;
        rigid = GetComponent<Rigidbody2D>();

        stat = GameObject.Find("GameManager").GetComponent<StatController>();
        stretch = 1.4f;
        stretch_Min = 1.4f;
        stretch_Max = 2.2f;
        stretch_Speed = 0.05f;

       


        
    }

    void Update()
    {
        damage_playerAttack = DataManager.Instance._SwordData.player_damage_attack;
        damage_playerParrying = DataManager.Instance._SwordData.player_parrying_attack;
        sword_reach = DataManager.Instance._SwordData.player_sword_reach;


        if (attack_Ani == true)
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

        //Debug.Log(swingForce);

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
        if(attack_Ani == false)
        {
            isSwing = true;
            //stat.Stat("ST", -3);
            gameObject.tag = "Attack";
            GetComponent<SpriteRenderer>().color = Color.red;
            
            attack_Ani = true;
        }
    }
    public void Parrying()
    {
        if (stat.Player_CurST >= 6 && swingForce > 5.0f && isSwing == false)
        {
       
            isSwing = true;
            stat.Stat("ST", -6);
            gameObject.tag = "Parrying";
            GetComponent<SpriteRenderer>().color = Color.blue;
        }

        if (swingForce < 1.0f)
        {
            Idle();
        }
    }
    public void Idle()
    {
        isSwing = false;
        isKnock = false;
        gameObject.tag = "Sword";
        GetComponent<SpriteRenderer>().color = Color.white;
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
        transform.rotation = Quaternion.Euler(0, 0, -100);
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        //Debug.Log(col.gameObject.layer);

        if (gameObject.tag == "Parrying")
        {
            if (col.gameObject.layer == LayerMask.NameToLayer("E_Attack"))
            {
                col.GetComponentInParent<EnemyController>().isParried = true;
                col.gameObject.SetActive(false);
            }
            if (col.gameObject.layer == LayerMask.NameToLayer("bullet"))
            {
                col.gameObject.SetActive(false);
                Destroy(col);
            }
        }
    }
        
    // Weapon enfocement


    
    
    
    
}