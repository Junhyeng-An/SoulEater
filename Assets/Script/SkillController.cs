using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillController : MonoBehaviour
{
    Sword sword;
    StatController stat;
    EnemyController enemy;

    GameObject player;
    GameObject controlled;

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

    public Skill_Active skill_active;
    public Passive_Active passive_active;

    [HideInInspector] public Skill_Active player_skill;

    void Awake()
    {
        player = GameObject.Find("Player");
        stat = GetComponent<StatController>();
    }
    void Update()
    {
        SkillCheck_Player();
    }
    void SkillCheck_Player()
    {
        controlled = GameObject.FindGameObjectWithTag("Controlled");

        player_skill = controlled.GetComponent<EnemyController>().CurSkill;
    }

    public void Active()
    {
        Debug.Log(player_skill);

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

        }
    }
    public void Active_Smash()
    {
        if (stat.Player_CurST >= 3)
        {

        }
    }
    public void Active_DashAttack()
    {
        if (Input.GetMouseButton(0)) // left
        {
            
        }
    }
    public void Active_Push()
    {

    }
}
