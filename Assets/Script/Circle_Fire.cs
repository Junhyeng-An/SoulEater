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
    public GameObject projectilePrefab;  // 발사체 프리팹
    public float projectileSpeed = 5f;   // 발사 속도
    public float fireRate = 2f;          // 발사 간격
    public float projectileSpread = 15f; // 발사체 사이의 각도
    public float projectileLifetime = 4f; // 발사체 수명 (초)
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
        // 발사 간격을 체크하여 발사체 발사
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
        // 발사체를 360도 방향으로 발사
        for (float angle = 0; angle < 360; angle += projectileSpread)
        {
            // 각도를 라디안으로 변환
            float radians = angle * Mathf.Deg2Rad;

            // 발사체의 방향 계산
            Vector2 projectileDirection = new Vector2(Mathf.Cos(radians), Mathf.Sin(radians));

            // 발사체 생성 및 초기화
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
