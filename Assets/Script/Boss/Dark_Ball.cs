using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Dark_Ball : MonoBehaviour
{
    float Dark_Ball_Damage = 10;
    // TODO: knight Boss Dark Damage (Damage to be adjusted later)
    GameObject player;
    EnemyController enemyController;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Col_Dark_Ball(collision);
    }
    void Col_Dark_Ball(Collider2D col)
    {
        player = GameObject.FindGameObjectWithTag("Controlled");
        if (player != null)
        {
            enemyController = player.GetComponent<EnemyController>();
            if (enemyController != null)
            {
                if (col.tag == "hit_area")
                {
                    SettingManager.Instance.Damage_Calculate(col, Dark_Ball_Damage, enemyController);
                }
            }
        }
    }
}
