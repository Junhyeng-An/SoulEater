using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private Movement movement;
    private Sword sword;
    private StatController stat;
    private Setting_UI setting;
    private EnemyController enemy;

    [HideInInspector]
    public bool isThrowing;

    void Awake()
    {
        movement = GetComponent<Movement>();
        sword = GetComponentInChildren<Sword>();
        stat = GameObject.Find("GameManager").GetComponent<StatController>();
        setting = GameObject.Find("GameManager").GetComponent<Setting_UI>();

        isThrowing = true;
    }
    void LateUpdate()
    {
        movement.Return();
        movement.Landing();
        movement.WallCheck();
        if (movement.gameover == true)
        {
            sword.GameOver();
        }
        else if (isThrowing == true)
        {
            sword.Throw();
        }
        else if(setting.settingsMenu.activeSelf == false)
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
            //sword.SwingCheck();
            if (Input.GetMouseButtonDown(0)) //left
            {
                sword.Attack();
            }
            if (Input.GetMouseButtonDown(1)) //right
            {
                sword.Parrying();
            }
            if (Input.GetMouseButtonUp(0) || Input.GetMouseButtonUp(1))
            {
                sword.Idle();
            }
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}