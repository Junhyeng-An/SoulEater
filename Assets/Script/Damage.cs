using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{

    Sword sword;

    private void OnTriggerStay2D(Collider2D collision)
    {
        EnemyController enemyController = GetComponentInParent<EnemyController>();
        if (enemyController != null)
        {
            if (collision.CompareTag("closehit") && enemyController.isHit == false)
            {
                Debug.Log("적 공격력 : "+collision.GetComponentInParent<EnemyController>().damage_enemyAttack);
                 enemyController.CurHP -= collision.GetComponentInParent<EnemyController>().damage_enemyAttack;
                 enemyController.CurHP += (collision.GetComponentInParent<EnemyController>().damage_enemyAttack * (DataManager.Instance._Player_Skill.Reduce_damage / 100));
                
                 enemyController.isHit = true;
            }
        }
        else
        {
            Debug.LogError("EnemyController not found in the parent!");
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
    }
    void damage()
    {

    }
}
