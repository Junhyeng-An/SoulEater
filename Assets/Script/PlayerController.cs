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

    void Awake()
    {
        movement = GetComponent<Movement>();
        sword = GetComponentInChildren<Sword>();
        stat = GameObject.Find("GameManager").GetComponent<StatController>();

        isThrowing = true;
    }
    void Update()
    {
        if(isThrowing == true)
        {
            sword.Throw();
        }
        else
        {
            movement.Landing();
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
            if (Input.GetMouseButtonDown(0)) //left
            {
                if (stat.Player_CurST >= 3)
                {
                    sword.gameObject.tag = "Attack";
                    stat.Stat("ST", -3);
                    sword.GetComponent<SpriteRenderer>().color = Color.red;
                }
            }
            if (Input.GetMouseButtonUp(0))
            {
                sword.gameObject.tag = "Sword";
                sword.GetComponent<SpriteRenderer>().color = Color.white;
            }
            if (Input.GetMouseButtonDown(1)) //right
            {
                if (stat.Player_CurST >= 6)
                {
                    sword.gameObject.tag = "Parrying";
                    stat.Stat("ST", -6);
                    sword.GetComponent<SpriteRenderer>().color = Color.blue;
                }
            }
            if (Input.GetMouseButtonUp(1))
            {
                sword.gameObject.tag = "Sword";
                sword.GetComponent<SpriteRenderer>().color = Color.white;
            }
        }
    }
}