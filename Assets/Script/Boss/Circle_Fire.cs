using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Circle_Fire : MonoBehaviour
{
    public GameObject projectilePrefab;  // �߻�ü ������
    public GameObject Start_Pos;
    public float projectileSpeed = 5f;   // �߻� �ӵ�
    public float fireRate = 2f;          // �߻� ����
    public float projectileSpread = 15f; // �߻�ü ������ ����
    public float projectileLifetime = 4f; // �߻�ü ���� (��)
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
        // �߻�ü�� 360�� �������� �߻�
        for (float angle = 0; angle < 360; angle += projectileSpread)
        {
            // ������ �������� ��ȯ
            float radians = angle * Mathf.Deg2Rad;

            // �߻�ü�� ���� ���
            Vector2 projectileDirection = new Vector2(Mathf.Cos(radians), Mathf.Sin(radians));

            // �߻�ü ���� �� �ʱ�ȭ
            GameObject projectile = Instantiate(projectilePrefab, Start_Pos.transform.position, Quaternion.identity);
            Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
            rb.velocity = projectileDirection * projectileSpeed;

            Destroy(projectile, projectileLifetime);
        }
    }
}
