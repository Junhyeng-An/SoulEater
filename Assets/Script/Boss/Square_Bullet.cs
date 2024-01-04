using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Square_Bullet : MonoBehaviour
{
    public float Bullet_Damage;
    GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        player = GameObject.FindGameObjectWithTag("Controlled");
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Col_Red_Square(collision);
    }
    void Col_Red_Square(Collider2D col)
    {
        if (col.gameObject.tag == "hit_area")
        {
            Destroy(gameObject);
            player.GetComponent<EnemyController>().CurHP -= Bullet_Damage;
        }
    }
}
