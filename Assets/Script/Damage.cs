using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{

    Sword sword;
    float Miss_const;
    bool isDamage;
    bool isImmume = false;

    public GameObject root;
    public GameObject Body;
    Knight_Controller knight;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Miss_const = Random.Range(0f, 100f);
        if (collision.tag == "Slime_Boss")
        {
            Debug.Log("���������� ����");
            EnemyController enemyController = GetComponentInParent<EnemyController>();
            S_Boss_Controller s_Boss = GameObject.Find("Slime_boss").GetComponent<S_Boss_Controller>();
            SettingManager.Instance.Damage_Calculate(collision, s_Boss.slime_damage, enemyController);
        }

        if (collision.tag == "Spike")
        {
            Debug.Log("���ÿ� ��");
            EnemyController enemyController = GetComponentInParent<EnemyController>();
            Slime_Super_Jump slime_Super = GameObject.Find("Slime_boss").GetComponent<Slime_Super_Jump>();
            SettingManager.Instance.Damage_Calculate(collision, slime_Super.spike_damage, enemyController);
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {

        EnemyController enemyController = GetComponentInParent<EnemyController>();
        
       
        if (enemyController != null)
        {
            if (collision.CompareTag("closehit") && enemyController.isHit == false && root.tag == "Controlled" && !isImmume)
            {
                enemyController.isHit = true;
                SettingManager.Instance.Damage_Calculate(collision, collision.GetComponentInParent<EnemyController>().damage_enemyAttack, enemyController);
                
                
                StartCoroutine(ResetImmume());
            }
            if (collision.CompareTag("Bosshit") && enemyController.isHit == false && root.tag == "Controlled") 
            {
                enemyController.isHit = true;
                SettingManager.Instance.Damage_Calculate(collision,  knight.Boss_Attack_Damage, enemyController);
               
            }
        }
        else
        {
            Debug.LogError("�θ𿡼� EnemyController�� ã�� �� �����ϴ�!");
        }
    }

    IEnumerator ResetImmume()
    {
        isImmume = true;
        Body.GetComponent<Renderer>().material.color = Color.yellow;
        yield return new WaitForSeconds(1f);
        Body.GetComponent<Renderer>().material.color = Color.white;
        isImmume = false;
    }
}
