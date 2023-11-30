using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    public float Bullet_Damage;
    GameObject player;
    GameObject hit_area;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        hit_area = GameObject.FindGameObjectWithTag("hit_area");
        player = GameObject.FindGameObjectWithTag("Controlled");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == hit_area)
        {
            Destroy(gameObject);
            player.GetComponent<EnemyController>().CurHP -= Bullet_Damage;

        }
    }
}
