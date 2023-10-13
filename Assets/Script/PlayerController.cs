using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Movement movement;
    private Sword sword;

    [HideInInspector]
    public bool isThrowing = false;

    void Awake()
    {
        movement = GetComponent<Movement>();
        sword = GetComponentInChildren<Sword>();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            movement.Throw_Ready();
        }
        if(Input.GetKey(KeyCode.Q))
        {
            movement.Throw_Line();
        }
        if(Input.GetKeyUp(KeyCode.Q))
        {
            movement.Throw();
            isThrowing = true;
        }

        if(isThrowing == true)
        {
            sword.Throw();
        }
        else
        {
            //player movement
            float x = Input.GetAxisRaw("Horizontal");
            movement.Move(x);

            //player jump
            if (Input.GetKeyDown(KeyCode.Space))
            {
                movement.Jump();
            }

            //player dash
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                movement.Dash();
            }

            //sword
            sword.PosRot();
            sword.OnMouseEvent();
        }
    }
}