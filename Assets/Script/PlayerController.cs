using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private Movement movement;
    private Sword sword;
    private TimeScale time;
    private StatController stat;
    private EnemyController enemy;
    private SkillController skill;
    private VolumeController volume;

    [HideInInspector]
    public bool isThrowing;

    void Awake()
    {
        movement = GetComponent<Movement>();
        sword = GetComponentInChildren<Sword>();
        time = GameObject.Find("GameManager").GetComponent<TimeScale>();
        stat = GameObject.Find("GameManager").GetComponent<StatController>();
        skill = GameObject.Find("GameManager").GetComponent<SkillController>();
        volume = GameObject.Find("GameManager").GetComponent<VolumeController>();

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
            volume.ZoomOut();
        }
        else if(skill.onSkill == false)// setting miss 
        {
            //player movement
            float x = Input.GetAxisRaw("Horizontal");
            movement.Move(x);

            //player jump
            if (Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.W))
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

            if (Input.GetKeyDown(KeyCode.R))    // Throw Key
            {
                movement.Throw_Ready();
            }
            if (Input.GetKey(KeyCode.R))
            {
                movement.Throw_Line();
                volume.ZoomIn();
            }
            else
                volume.ZoomOut();
            if (Input.GetKeyUp(KeyCode.R))
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
            if (Input.GetMouseButton(1)) //right
            {
                sword.Parrying();
            }
            if (Input.GetMouseButtonUp(0) || Input.GetMouseButtonUp(1))
            {
                sword.Idle();
            }
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            time.SlowMotion(1);
        }
    }

   
    
    
    
}