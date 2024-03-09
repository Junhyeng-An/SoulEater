using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static EnemyController;

public class bullet : MonoBehaviour
{
    public float Bullet_Damage;
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

    private void OnTriggerEnter2D(Collider2D col)
    {
        player = GameObject.FindGameObjectWithTag("Controlled");
        if (player != null )
        {
            enemyController = player.GetComponent<EnemyController>();
            if (enemyController != null )
            {
                if (col.gameObject.tag == "Player" && isimmune == false)
                {
                    SoundManager.Instance.Playsfx(SoundManager.SFX.Bat_Drop); 
                    SettingManager.Instance.Damage_Calculate(col, Bullet_Damage, enemyController);
                    Destroy(gameObject);
                }
            }
        }
    }
}
