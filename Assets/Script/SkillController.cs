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

    public float damage;
    public bool onSkill;

    public float coolTime;

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

    public KeyCode skill_key;

    [HideInInspector] public Skill_Active skill_active;
    [HideInInspector] public Passive_Active skill_passive;

    [HideInInspector] public Skill_Active player_skill;

    public float power_smash = 2;
    float height_smash = 3;
    float height;
    float size_smash;
    
    private System.Action[] skillFunctions;

    private GameObject HitBox;
    private GameObject AttackBox;
    private P_Attack p_attack;

    void Awake()
    {
        player = GameObject.Find("Player");
        stat = GetComponent<StatController>();
        rigid_player = player.GetComponent<Rigidbody2D>();
        sword = player.GetComponentInChildren<Sword>();

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

        for (int i = 0; i < active_langth; i++)
        {
            if (isSkill[i] == true)
            {
                skillFunctions[i]();
            }
        }

        if (coolTime > 0)
            coolTime -= Time.deltaTime;
        else
            coolTime = 0;
    }
    void Check_PlayerSkill()
    {
        if (GameObject.FindGameObjectWithTag("Controlled") != null)
        {
            controlled = GameObject.FindGameObjectWithTag("Controlled");

            player_skill = controlled.GetComponent<EnemyController>().CurSkill;
            Check_SkillKey();
        }
    }
    void Check_SkillKey()
    {
        if (player_skill == Skill_Active.DashAttack)
        {
            if (Input.GetKey(KeyCode.LeftShift) )
            {
                Active();
            }
        }
        if (player_skill == Skill_Active.Slash)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                Active();
            }
        }
        if (player_skill == Skill_Active.Smash)
        {
            if (Input.GetKey(KeyCode.F))
            {
                Active();
            }
        }
    }
    public void Active()
    {
        for (int i = 0; i < active_langth; i++)
        {
            if ((int)player_skill == i && onSkill ==false)
            {
                isSkill[i] = true;
                onSkill = true;
            }
        }
    }
    
    void Slash()
    {
        Vector2 skillSize = new Vector2(2, 2);
        if(condition == false)
        {
            Check_Condition(3, coolTime);
            damage = 10;
        }
        else
        {
            Create_HitBox(
                sword.transform.position, 
                skillSize, 
                damage, 
                5, 
                Quaternion.AngleAxis(sword.angle * Mathf.Rad2Deg - 90, Vector3.forward),
                10
                );
            coolTime = 3;
            End_Skill();
        }
    }

    void Smash()
    {
        LayerMask mask = 1 << 20 | 1 << 21;
        RaycastHit2D rayHit_smash = Physics2D.Raycast(player.transform.position, Vector2.down, Mathf.Infinity, mask);

        height = rayHit_smash.distance;
        Vector3 colPos = rayHit_smash.point;

        Vector2 skillSize = new Vector2(size_smash, size_smash);

        if (condition == false)
        {
            if (height >= height_smash)
            {
                Check_Condition(3);
                size_smash = height / 2;
                if (size_smash >= 4)
                    size_smash = 4;
                damage = height * 3;
            }
            else
                End_Skill();
        }
        else
        {
            sword.transform.position = player.transform.position - Vector3.up * sword.stretch_Min;
            sword.transform.rotation = Quaternion.Euler(0, 0, -90);

            rigid_player.velocity= Vector3.down * power_smash;
            if(height <= player.GetComponent<CircleCollider2D>().radius + 0.1f)
            {
                End_Skill();
                Create_HitBox(colPos + Vector3.up * skillSize.y / 2, skillSize, damage, 1);
            }
        }
    }
    void DashAttack()
    {
        Vector2 skillSize = new Vector2(player.GetComponent<Movement>().dashForce, 1);
        if (condition == false && stat.Player_CurST >= 3)
        {
            condition = true;
            damage = 10;
        }
        else
        {
            Create_HitBox(
                new Vector2(player.transform.position.x, player.transform.position.y) - player.GetComponent<Movement>().posMid / 2, 
                skillSize, 
                damage, 
                1,
                Quaternion.AngleAxis(sword.angle * Mathf.Rad2Deg, Vector3.forward)
                );
            End_Skill();
        }
    }
    void Push()
    {
        Debug.Log("Push!");
    }

    void Check_Condition(float st)
    {
        if (stat.Player_CurST >= st)
        {
            condition = true;
            stat.Player_CurST -= st;
        }
        else
        {
            End_Skill();
        }
    }
    void Check_Condition(float st, float cool)
    {
        if (stat.Player_CurST >= st && cool <= 0)
        {
            condition = true;
            stat.Player_CurST -= st;
        }
        else
        {
            End_Skill();
        }
    }
    
    void End_Skill()
    {
        condition = false;
        isSkill[(int)player_skill] = false;
        height_smash = 3;
        onSkill = false;
    }

    void Create_HitBox(Vector3 pos, Vector2 size, float dmg, float fade)
    {
        AttackBox = Instantiate(HitBox, pos, transform.rotation);
        AttackBox.GetComponent<P_Attack>().Attack_Area(pos, size, dmg, fade);
    }
    void Create_HitBox(Vector3 pos, Vector2 size, float dmg, float fade, Quaternion rotation)
    {
        AttackBox = Instantiate(HitBox, pos, rotation);
        AttackBox.GetComponent<P_Attack>().Attack_Area(pos, size, dmg, fade);
    }
    void Create_HitBox(Vector3 pos, Vector2 size, float dmg, float fade, Quaternion rotation , float speed)
    {
        AttackBox = Instantiate(HitBox, pos, rotation);
        AttackBox.GetComponent<P_Attack>().Attack_Area(pos, size, dmg, fade, speed);
    }
}
