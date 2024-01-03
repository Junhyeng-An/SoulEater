using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P_Attack : MonoBehaviour
{
    //[HideInInspector] public Vector3 pos;
    //[HideInInspector] public Vector2 size;
    [HideInInspector] public float damage;

    float time = 0;
    private float fadeTime = 100;
    private bool isActive = false;
    void Awake()
    {
        
    }

    void Update()
    {
        time += Time.deltaTime;
        
        Debug.Log("fadetime: " + fadeTime);
        
        if (time >= fadeTime)
            Destroy(gameObject);
    }

    public void Attack_Area(Vector3 pos, Vector2 size, float dmg, float fade)
    {
        transform.position = pos;
        transform.localScale = size;
        damage = dmg;
        fadeTime = fade;
    }
}