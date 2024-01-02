using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillController : MonoBehaviour
{
    Sword sword;
    StatController stat;
    EnemyController enemy;
    //[HideInInspector] public 

    public enum Skill_Active
    {
        Slash,
        Thrust,
        Smash,
        DashAttack
    }
    public enum Passive_Active
    {

    }

    public Skill_Active skill_active;
    public Passive_Active passive_active;

    void Awake()
    {
        
    }
    void Update()
    {
        
    }

    public void Active(Skill_Active skill)
    {
        if (skill == Skill_Active.Slash)
        {

        }
        if (skill == Skill_Active.Thrust)
        {

        }
        if (skill == Skill_Active.Smash)
        {
            Active_Smash();
        }
        if (skill == Skill_Active.DashAttack)
        {
            Active_DashAttack();
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
}
