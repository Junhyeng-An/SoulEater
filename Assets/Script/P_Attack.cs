using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P_Attack : MonoBehaviour
{
    //[HideInInspector] public Vector3 pos;
    //[HideInInspector] public Vector2 size;
    [HideInInspector] public float damage;

    Rigidbody2D rigid;

    float time = 0;
    private float fadeTime = 100;
    float vel;

    public int isAttack = 1;
    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        time += Time.deltaTime;
        
        if (time >= fadeTime)
            Destroy(gameObject);

        rigid.velocity = transform.up * vel;
    }

    public void Attack_Area(Vector3 pos, Vector2 size, float dmg, float fade)
    {
        transform.position = pos;
        transform.localScale = size;
        damage = dmg;
        fadeTime = fade;
    }
    public void Attack_Area(Vector3 pos, Vector2 size, float dmg, float fade, float velocity)
    {
        transform.position = pos;
        transform.localScale = size;
        damage = dmg;
        fadeTime = fade;
        vel = velocity;
    }
}