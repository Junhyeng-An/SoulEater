using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{

    Sword sword;
    float Miss_per;
    float Miss_const;
    bool isDamage;

    private void Update()
    {
        Miss_per = DataManager.Instance._Player_Skill.Miss;
        //Debug.Log("회피율 : " + Miss_per);
        //Debug.Log("회피상수 : " + Miss_const);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Miss_const = Random.Range(0f, 100f);
        if (collision.tag == "Boss")
        {
            Debug.Log("슬라임한테 맞음");
            EnemyController enemyController = GetComponentInParent<EnemyController>();
            S_Boss_Controller s_Boss = GameObject.Find("Slime_boss").GetComponent<S_Boss_Controller>();
            enemyController.CurHP -= s_Boss.slime_damage;
        }

        if (collision.tag == "Spike")
        {
            Debug.Log("가시에 찔림");
            EnemyController enemyController = GetComponentInParent<EnemyController>();
            Slime_Super_Jump slime_Super = GameObject.Find("Slime_boss").GetComponent<Slime_Super_Jump>();
            enemyController.CurHP -= slime_Super.spike_damage;
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {

        EnemyController enemyController = GetComponentInParent<EnemyController>();
        if (enemyController.isHit == true && collision.tag == "Attack") {
            Miss_const = Random.Range(0f, 100f);

        }
        if (Miss_const <= Miss_per)
        {
            // 공격이 빗나간 경우, 아무 동작도 하지 않음
            //Debug.Log("빗나감");
            return;
        }
        else
        {
            isDamage = true;
        }
       
        if (enemyController != null && isDamage == true)
        {
            if (collision.CompareTag("closehit") && enemyController.isHit == false)
            {
                enemyController.CurHP -= collision.GetComponentInParent<EnemyController>().damage_enemyAttack;
                enemyController.CurHP += (collision.GetComponentInParent<EnemyController>().damage_enemyAttack * (DataManager.Instance._Player_Skill.Reduce_damage / 100));

                enemyController.isHit = true;
            }
        }
        else
        {
            Debug.LogError("부모에서 EnemyController를 찾을 수 없습니다!");
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
    }
}
