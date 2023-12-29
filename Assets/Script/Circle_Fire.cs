using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Circle_Fire : MonoBehaviour
{
    public float CurHP;
    public float MaxHP;
    public float hp_har_height = 1;
    //public RectTransform my_bar;
    //public GameObject Canvas;
    public GameObject projectilePrefab;  // �߻�ü ������
    public float projectileSpeed = 5f;   // �߻� �ӵ�
    public float fireRate = 2f;          // �߻� ����
    public float projectileSpread = 15f; // �߻�ü ������ ����
    public float projectileLifetime = 4f; // �߻�ü ���� (��)
    private float timeSinceLastFire = 0f;
    //[SerializeField]
    //Slider Boss_HP;

    private Sword sword;
    private bool isDamage;
    private GameObject player;
    private void Start()
    {
        sword = GameObject.Find("Sword").GetComponent<Sword>();
    }
    void Update()
    {
        //if (sword.isSwing == false)
        //{
        //    isDamage = false;
        //}
        //player = GameObject.Find("GameManager");
        //Turret_HP.value = CurHP / MaxHP;
        //Vector3 hpbar_pos = Camera.main.WorldToScreenPoint(new Vector3(transform.position.x, transform.position.y + hp_har_height, 0));
        //my_bar.position = hpbar_pos;
        // �߻� ������ üũ�Ͽ� �߻�ü �߻�
        timeSinceLastFire += Time.deltaTime;
        if (timeSinceLastFire >= 1f / fireRate)
        {
            FireProjectile();
            timeSinceLastFire = 0f;
        }

        if (CurHP <= 0)
        {
            Destroy(gameObject);
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
            GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
            Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
            rb.velocity = projectileDirection * projectileSpeed;

            Destroy(projectile, projectileLifetime);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (isDamage == false)
        {
            if (collision.gameObject.tag == "Attack" && gameObject.tag != "Controlled")
            {
                CurHP -= 10;
                player.GetComponent<StatController>().Stat("ST", 3);

                isDamage = true;
            }
        }
    }

}
