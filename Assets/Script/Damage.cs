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
        //Debug.Log("ȸ���� : " + Miss_per);
        //Debug.Log("ȸ�ǻ�� : " + Miss_const);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Miss_const = Random.Range(0f, 100f);
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        EnemyController enemyController = GetComponentInParent<EnemyController>();
        if (enemyController.isHit == true && collision.tag == "Attack") {
            Miss_const = Random.Range(0f, 100f);

        }
        if (Miss_const <= Miss_per)
        {
            // ������ ������ ���, �ƹ� ���۵� ���� ����
            //Debug.Log("������");
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
                //Debug.Log("�� ���ݷ� : "+collision.GetComponentInParent<EnemyController>().damage_enemyAttack);
                enemyController.CurHP -= collision.GetComponentInParent<EnemyController>().damage_enemyAttack;
                enemyController.CurHP += (collision.GetComponentInParent<EnemyController>().damage_enemyAttack * (DataManager.Instance._Player_Skill.Reduce_damage / 100));

                enemyController.isHit = true;
            }
        }
        else
        {
            Debug.LogError("�θ𿡼� EnemyController�� ã�� �� �����ϴ�!");
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
    }
}
