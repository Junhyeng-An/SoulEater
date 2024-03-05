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


    private void OnTriggerEnter2D(Collider2D collision)
    {
        Miss_const = Random.Range(0f, 100f);
        if (collision.tag == "Boss")
        {
            Debug.Log("슬라임한테 맞음");
            EnemyController enemyController = GetComponentInParent<EnemyController>();
            S_Boss_Controller s_Boss = GameObject.Find("Slime_boss").GetComponent<S_Boss_Controller>();
            Damage_Calculate(collision, s_Boss.slime_damage, enemyController);
        }

        if (collision.tag == "Spike")
        {
            Debug.Log("가시에 찔림");
            EnemyController enemyController = GetComponentInParent<EnemyController>();
            Slime_Super_Jump slime_Super = GameObject.Find("Slime_boss").GetComponent<Slime_Super_Jump>();
            Damage_Calculate(collision, slime_Super.spike_damage, enemyController);
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {

        EnemyController enemyController = GetComponentInParent<EnemyController>();
     
       
      
        isDamage = true;
        
        
        
        
        
        
        
       
        if (enemyController != null && isDamage == true)
        {
            if (collision.CompareTag("closehit") && enemyController.isHit == false && root.tag == "Controlled" && !isImmume)
            {
                enemyController.isHit = true;
                Damage_Calculate(collision, 20, enemyController);
                
                
                StartCoroutine(ResetImmume());
            }
            if (collision.CompareTag("Bosshit") && enemyController.isHit == false && root.tag == "Controlled") 
            {
                enemyController.isHit = true;
                Damage_Calculate(collision,  30, enemyController);
               
            }
        }
        else
        {
            Debug.LogError("부모에서 EnemyController를 찾을 수 없습니다!");
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
   
    void Damage_Calculate(Collider2D collision, float Damage,EnemyController enemyController)
    {
        
        Debug.Log("Damage Calculate");
        
        if (enemyController.isHit == true && collision.tag == "Attack") {
            Miss_const = Random.Range(0f, 100f);
            Debug.Log(Miss_const);
        }
        if (Miss_const <= DataManager.Instance._Player_Skill.Miss)
        {
            Debug.Log("빗나감");
            return;
        }
        else
        {
            Debug.Log("맞음");
            isDamage = true;
        }
         Debug.Log( Damage * (1.0f - DataManager.Instance._Player_Skill.Reduce_damage * 0.1f));
        enemyController.CurHP -= Damage * (1.0f - (DataManager.Instance._Player_Skill.Reduce_damage * 0.01f));
        

    }
}
