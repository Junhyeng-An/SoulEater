using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Square_Bullet : MonoBehaviour
{
    public float Bullet_Damage;
    GameObject player;
    GameObject hit_area;

    bool isimmune;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        player = GameObject.FindGameObjectWithTag("Controlled");
        hit_area = GameObject.FindGameObjectWithTag("hit_area");
        isimmune = hit_area.GetComponent<Damage>().isImmune;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Col_Red_Square(collision);
    }
    void Col_Red_Square(Collider2D col)
    {
        if (col.gameObject.tag == "Player" && isimmune == false)
        {
            Destroy(gameObject);
            player.GetComponent<EnemyController>().CurHP -= Bullet_Damage;
        }
    }
}
