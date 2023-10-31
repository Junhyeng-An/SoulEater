using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Movement movement;
    private Sword sword;
    private StatController stat;

    [HideInInspector]
    public bool isThrowing;

    bool isSwing;

    void Awake()
    {
        movement = GetComponent<Movement>();
        sword = GetComponentInChildren<Sword>();
        stat = GameObject.Find("GameManager").GetComponent<StatController>();

        isThrowing = true;
    }
    void LateUpdate()
    {
        movement.Return();
        movement.Landing();
        if (isThrowing == true)
        {
            sword.Throw();
        }
        else
        {
            //player movement
            float x = Input.GetAxisRaw("Horizontal");
            movement.Move(x);

            //player jump
            if (Input.GetKey(KeyCode.Space))
            {
                movement.Jump();
            }

            //player dash
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                if (stat.Player_CurST >= 3)
                {
                    movement.Dash();
                    stat.Stat("ST", -3);
                }
            }

            if (Input.GetKeyDown(KeyCode.Q))
            {
                movement.Throw_Ready();
            }
            if (Input.GetKey(KeyCode.Q))
            {
                movement.Throw_Line();
            }
            if (Input.GetKeyUp(KeyCode.Q))
            {
                movement.Throw();
                isThrowing = true;
            }

            //sword 
            sword.PosRot();
            /*if (Input.GetMouseButtonDown(0)) //left
            {
                if (stat.Player_CurST >= 3)
                {
                    sword.gameObject.tag = "Attack";
                    stat.Stat("ST", -3);
                    sword.GetComponent<SpriteRenderer>().color = Color.red;
                }
            }*/
            if (sword.swingForce >= 3)
            {
                if (Input.GetMouseButton(0) && sword.swingForce >= 3 && stat.Player_CurST >= 3) //left
                {
                    if(isSwing == false)
                    {
                        stat.Stat("ST", -3);
                        isSwing = true;
                    }
                    sword.gameObject.tag = "Attack";
                    sword.GetComponent<SpriteRenderer>().color = Color.red;
                }
                if (Input.GetMouseButton(1) && sword.swingForce >= 3 && stat.Player_CurST >= 6) //right
                {
                    if (isSwing == false)
                    {
                        stat.Stat("ST", -6);
                        isSwing = true;
                    }
                    sword.gameObject.tag = "Parrying";
                    sword.GetComponent<SpriteRenderer>().color = Color.blue;
                }
            }
            else if(sword.swingForce < 1.5f)
            {
                sword.gameObject.tag = "Sword";
                isSwing = false;
                sword.GetComponent<SpriteRenderer>().color = Color.white;
            }
        }
    }
}