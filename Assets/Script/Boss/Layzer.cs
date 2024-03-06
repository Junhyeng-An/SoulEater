using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Layzer : MonoBehaviour
{
    float Bullet_Damage = 10;
    // TODO: Bat Boss Laser Damage (Damage to be adjusted later)
    float Boss_Bullet_Damage = 100;
    // TODO: Night Boss Laser Damage (Damage to be adjusted later)
    GameObject player;
    GameObject hit_area;
    EnemyController enemyController;
    bool isimmune;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Col_Layzer(collision);
    }
    void Col_Layzer(Collider2D col)
    {
        player = GameObject.FindGameObjectWithTag("Controlled");
        enemyController = player.GetComponent<EnemyController>();
        if (col.gameObject.tag == "Player")
        {
            SettingManager.Instance.Damage_Calculate(col, Bullet_Damage, enemyController);
        }
        if (col.gameObject.tag == "Player" && SceneManager.GetActiveScene().name == "Boss3")
        {
            SettingManager.Instance.Damage_Calculate(col, Boss_Bullet_Damage, enemyController);
        }

    }
}
