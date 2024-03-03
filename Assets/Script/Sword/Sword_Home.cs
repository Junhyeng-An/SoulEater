using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Sword_Home : MonoBehaviour
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

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();

    }

    void Update()
    {
        damage_playerAttack = DataManager.Instance._SwordData.player_damage_attack;
        damage_playerParrying = DataManager.Instance._SwordData.player_parrying_attack;
        sword_reach = DataManager.Instance._SwordData.player_sword_reach;
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

        if (angleDeltaDelta * 10000 < 0)
        {
            //Debug.Log(angleDeltaDelta*10000);
        }
        else
        {

        }

        if (swingForce > limitSpeed)
        {
            if (angleDelta > 0)
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

        Vector2 mousePos = new Vector2(target.transform.position.x + Mathf.Cos(angle), target.transform.position.y + Mathf.Sin(angle)); // sword position
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
    public void Idle()
    {
        isSwing = false;
        gameObject.tag = "Sword";
        GetComponent<SpriteRenderer>().color = Color.white;
    }
}