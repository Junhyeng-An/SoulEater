using Com.LuisPedroFonseca.ProCamera2D.TopDownShooter;
using UnityEngine;
using UnityEngine.UI;

public class Turret : MonoBehaviour
{
    public float CurHP;
    public float MaxHP;
    public float fireRate = 0.5f;            // 발사 속도 (초당 발사 횟수)
    public float maxDistance = 10f;        // 발사 최대 거리
    public float hp_har_height = 1;
    public GameObject bulletPrefab;        // 총알 프리팹
    public Transform shootingPoint;        // 발사 지점 오브젝트
    public RectTransform my_bar;
    public GameObject Canvas;
    GameObject bullet;
    GameObject player;
    private float nextFireTime;             // 다음 발사 시간
    private Sword sword;
    private bool isDamage;
    public bool isAuto = false;
    [SerializeField]
    Slider Turret_HP;

    private void Awake()
    {
        sword = GameObject.Find("Sword").GetComponent<Sword>();
    }
    void Update()
    {
        if (sword.isSwing == false)
        {
            isDamage = false;
        }
        player = GameObject.Find("GameManager");
        // 발사 속도에 따라 총알 발사
        if (Time.time > nextFireTime)
        {
            if (isAuto == false)
            {
                Shoot();
            }
            else if(isAuto == true) 
            {
                AutoShoot();
            }
            nextFireTime = Time.time + 1f / fireRate;
        }
        Turret_HP.value = CurHP / MaxHP;
        Vector3 hpbar_pos = Camera.main.WorldToScreenPoint(new Vector3(transform.position.x, transform.position.y + hp_har_height, 0));
        my_bar.position = hpbar_pos;

        if (CurHP <= 0)
        {
            Destroy(gameObject);
        }
    }

    void Shoot()
    {
        // 발사 지점에서 총알 생성
        bullet = Instantiate(bulletPrefab, shootingPoint.position, shootingPoint.rotation);

        // 총알에 Rigidbody2D가 있다고 가정하고 속도 설정
        Rigidbody2D bulletRigidbody = bullet.GetComponent<Rigidbody2D>();
        if (bulletRigidbody != null)
        {
            // 총알 발사 방향 설정 (총알의 right 방향)
            Vector2 shootDirection = shootingPoint.right;
            bulletRigidbody.velocity = shootDirection * 10f;

            // 총알을 일정 시간 후에 파괴
            Destroy(bullet, maxDistance / 10f);
        }
        else
        {
            Debug.LogError("총알에 Rigidbody2D가 없습니다!");
        }
    }
    void AutoShoot()
    {
        // 발사 지점에서 총알 생성
        bullet = Instantiate(bulletPrefab, shootingPoint.position, shootingPoint.rotation);

        // 총알에 Rigidbody2D가 있다고 가정하고 속도 설정
        Rigidbody2D bulletRigidbody = bullet.GetComponent<Rigidbody2D>();
        if (bulletRigidbody != null)
        {
            // 검의 위치를 기준으로 총알 발사 방향 설정
            Vector2 shootDirection = sword.transform.position - shootingPoint.position;
            bulletRigidbody.velocity = shootDirection.normalized * 10f;

            // 총알을 일정 시간 후에 파괴
            Destroy(bullet, maxDistance / 10f);
        }
        else
        {
            Debug.LogError("총알에 Rigidbody2D가 없습니다!");
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
