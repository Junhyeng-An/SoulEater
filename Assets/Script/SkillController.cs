using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillController : MonoBehaviour
{
    Sword sword;
    StatController stat;
    EnemyController enemy;
    Rigidbody2D rigid_player;

    GameObject player;
    GameObject controlled;

    Vector3 vel;

    public enum Skill_Active
    {
        Slash,
        Thrust,
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

    [HideInInspector] public bool isSkill = false;

    public float power_smash = 2;

    void Awake()
    {
        player = GameObject.Find("Player");
        stat = GetComponent<StatController>();
        rigid_player = player.GetComponent<Rigidbody2D>();
        vel = rigid_player.velocity;
    }
    void Update()
    {
        SkillCheck_Player();

        if(isSkill == true)
        {
            vel = Vector3.down * power_smash;
        }
    }
    void SkillCheck_Player()
    {
        if (GameObject.FindGameObjectWithTag("Controlled") != null)
        {
            controlled = GameObject.FindGameObjectWithTag("Controlled");

            player_skill = controlled.GetComponent<EnemyController>().CurSkill;
        }
    }

    public void Active()
    {
        if (player_skill == Skill_Active.Slash)
        {
            
        }
        if (player_skill == Skill_Active.Thrust)
        {

        }
        if (player_skill == Skill_Active.Smash)
        {
            Active_Smash();
        }
        if (player_skill == Skill_Active.DashAttack)
        {
            Active_DashAttack();
        }
        if (player_skill == Skill_Active.Push)
        {
            Active_Push();
        }
    }
    public void Active_Slash ()
    {
        if(stat.Player_CurST >= 3)
        {

        }
    }
    public void Active_Thrust()
    {
        if (stat.Player_CurST >= 3)
        {
            isSkill = true;
        }
    }
    public void Active_Smash()
    {
        LayerMask mask = 1 << 20;
        LayerMask mask2 = 1 << 21;
        RaycastHit2D rayHit_Jump = Physics2D.Raycast(player.transform.position, Vector2.down, 2, mask | mask2);
        if (stat.Player_CurST >= 3 && rayHit_Jump.collider == null)
        {
            Debug.Log(player_skill);
            //vel = Vector3.zero;

            //vel = Vector3.down * power_smash;
            isSkill = true;
        }
    }
    public void Active_DashAttack()
    {
        if (Input.GetMouseButton(0)) // left
        {
            isSkill = true;
        }
    }
    public void Active_Push()
    {
        isSkill = true;
    }
}
