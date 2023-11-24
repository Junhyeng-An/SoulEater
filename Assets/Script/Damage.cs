using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
    float closeDamage = 10;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        EnemyController enemyController = GetComponentInParent<EnemyController>();
        if (enemyController != null)
        {
            if (collision.CompareTag("closehit"))
            {
                enemyController.CurHP -= closeDamage;
            }
        }
        else
        {
            Debug.LogError("EnemyController not found in the parent!");
        }

    }
    void damage()
    {

    }
}
