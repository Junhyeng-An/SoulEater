using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Circle_Fire : MonoBehaviour
{
    public GameObject projectilePrefab;  // 발사체 프리팹
    public GameObject Start_Pos;
    public float projectileSpeed = 5f;   // 발사 속도
    public float fireRate = 2f;          // 발사 간격
    public float projectileSpread = 15f; // 발사체 사이의 각도
    public float projectileLifetime = 4f; // 발사체 수명 (초)
    private float timeSinceLastFire = 0f;


    private Sword sword;
    private bool isDamage;
    private GameObject player;
    private void Start()
    {
        sword = GameObject.Find("Sword").GetComponent<Sword>();
    }
    void Update()
    {
        timeSinceLastFire += Time.deltaTime;
        if (timeSinceLastFire >= 1f / fireRate)
        {
            FireProjectile();
            timeSinceLastFire = 0f;
        }
    }

    void FireProjectile()
    {
        // 발사체를 360도 방향으로 발사
        for (float angle = 0; angle < 360; angle += projectileSpread)
        {
            // 각도를 라디안으로 변환
            float radians = angle * Mathf.Deg2Rad;

            // 발사체의 방향 계산
            Vector2 projectileDirection = new Vector2(Mathf.Cos(radians), Mathf.Sin(radians));

            // 발사체 생성 및 초기화
            GameObject projectile = Instantiate(projectilePrefab, Start_Pos.transform.position, Quaternion.identity);
            Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
            rb.velocity = projectileDirection * projectileSpeed;

            Destroy(projectile, projectileLifetime);
        }
    }
}
