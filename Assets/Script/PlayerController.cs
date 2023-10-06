using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Movement movement;
    private Sword sword;

    [HideInInspector]
    public bool isThrowing = false;
    public bool readyThrow = false;

    void Awake()
    {
        movement = GetComponent<Movement>();
        sword = GetComponentInChildren<Sword>();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            readyThrow = true;
        }
        if(Input.GetKeyUp(KeyCode.Q))
        {
            readyThrow = false;
            isThrowing = true;
            movement.Throwing();
        }

        if(readyThrow == true)
        {
            sword.ReadyThrow();
        }
        if(isThrowing == true)
        {
            sword.Throwing();
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