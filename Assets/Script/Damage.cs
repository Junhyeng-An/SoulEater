using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{

    Sword sword;
    float Miss_per;
    float Miss_const;
    bool isDamage;
    bool isImmume = false;

    public GameObject root;
    public GameObject Body;

    private void Update()
    {
        Miss_per = DataManager.Instance._Player_Skill.Miss;
        //Debug.Log("ȸ���� : " + Miss_per);
        //Debug.Log("ȸ�ǻ�� : " + Miss_const);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Miss_const = Random.Range(0f, 100f);
        if (collision.tag == "Boss")
        {
            Debug.Log("���������� ����");
            EnemyController enemyController = GetComponentInParent<EnemyController>();
            S_Boss_Controller s_Boss = GameObject.Find("Slime_boss").GetComponent<S_Boss_Controller>();
            enemyController.CurHP -= s_Boss.slime_damage;
        }

        if (collision.tag == "Spike")
        {
            Debug.Log("���ÿ� ��");
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
            if (collision.CompareTag("closehit") && enemyController.isHit == false && root.tag == "Controlled" && !isImmume)
            {
                enemyController.CurHP -= collision.GetComponentInParent<EnemyController>().damage_enemyAttack;
                enemyController.CurHP += (collision.GetComponentInParent<EnemyController>().damage_enemyAttack * (DataManager.Instance._Player_Skill.Reduce_damage / 100));

                enemyController.isHit = true;
                StartCoroutine(ResetImmume());
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
    private void OnTriggerExit2D(Collider2D collision)
    {
    }
}
