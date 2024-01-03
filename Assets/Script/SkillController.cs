using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Security.Cryptography;
using UnityEngine;

public class SkillController : MonoBehaviour
{
    Sword sword;
    StatController stat;
    EnemyController enemy;
    Rigidbody2D rigid_player;

    GameObject player;
    GameObject controlled;

    private int active_langth;
    private bool[] isSkill = new bool[10];
    private bool condition = false;

    public enum Skill_Active
    {
        Slash,
        Smash,
        DashAttack,
        Push
    }
    public enum Passive_Active
    {
    
    }

    [HideInInspector] public Skill_Active skill_active;
    [HideInInspector] public Passive_Active skill_passive;

    [HideInInspector] public Skill_Active player_skill;

    public float power_smash = 2;
    float height_smash = 3;
    
    private System.Action[] skillFunctions;

    private GameObject HitBox;
    private GameObject AttackBox;
    private P_Attack p_attack;

    void Awake()
    {
        player = GameObject.Find("Player");
        stat = GetComponent<StatController>();
        rigid_player = player.GetComponent<Rigidbody2D>();

        active_langth = System.Enum.GetValues(typeof(Skill_Active)).Length;

        skillFunctions = new System.Action[]
        {
            Slash,
            Smash,
            DashAttack,
            Push
        };

        HitBox = Resources.Load<GameObject>("P_Attack");
    }



    void Update()
    {
        Check_PlayerSkill();
        
        /*if(isSkill == true)
        {
            vel = Vector3.down * power_smash;
        }*/
        
        for (int i = 0; i < active_langth; i++)
        {
            if (isSkill[i] == true)
            {
                skillFunctions[i]();
            }
        }
    }
    void Check_PlayerSkill()
    {
        if (GameObject.FindGameObjectWithTag("Controlled") != null)
        {
            controlled = GameObject.FindGameObjectWithTag("Controlled");

            player_skill = controlled.GetComponent<EnemyController>().CurSkill;
        }
    }

    public void Active()
    {
        for (int i = 0; i < active_langth; i++)
        {
            if ((int)player_skill == i)
            {
                isSkill[i] = true;
            }
        }
    }
    
    void Slash()
    {
        Debug.Log("Slash!");
    }

    void Smash()
    {
        LayerMask mask = 1 << 20 | 1 << 21;
        RaycastHit2D rayHit_smash = Physics2D.Raycast(player.transform.position, Vector2.down, height_smash, mask);

        if (condition == false)
        {
            height_smash = 3;
            if(rayHit_smash.collider == null)
                Check_Condition(3);
        }
        else
        {
            height_smash = 0.5f;
            rigid_player.velocity= Vector3.down * power_smash;
            Debug.Log("Smash!");
            if(rayHit_smash.collider != null)
            {
                End_Skill();
                Create_HitBox(player.transform.position, new Vector2(2, 2), 50, 3);
            }
        }
    }
    void DashAttack()
    {
        Debug.Log("DashAttack!");
    }
    void Push()
    {
        Debug.Log("Push!");
    }

    void Check_Condition(float st)
    {
        if (stat.Player_CurST >= st)
            condition = true;
        else
            condition = false;
    }
    void Check_Condition(float st, float cool)
    {
        if (stat.Player_CurST >= st && cool <= 0)
            condition = true;
        else
            condition = false;
    }
    
    void End_Skill()
    {
        condition = false;
        isSkill[(int)player_skill] = false;
        height_smash = 3;
    }

    void Create_HitBox(Vector3 pos, Vector2 size, float dmg, float fade)
    {
        AttackBox = Instantiate(HitBox, pos, transform.rotation);
        AttackBox.GetComponent<P_Attack>().Attack_Area(pos, size, dmg, fade);
    }
}
