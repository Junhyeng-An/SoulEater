using Com.LuisPedroFonseca.ProCamera2D.TopDownShooter;
using UnityEngine;
using UnityEngine.UI;

public class Turret : MonoBehaviour
{
    public float CurHP;
    public float MaxHP;
    private float fireRate = 0.5f;
    private float maxDistance = 10f;
    private float hp_har_height = 2.5f;
    public GameObject bulletPrefab;       
    public Transform shootingPoint;        
    public RectTransform my_bar;
    public GameObject Canvas;
    GameObject bullet;
    GameObject player;
    private float nextFireTime;
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
        bullet = Instantiate(bulletPrefab, shootingPoint.position, shootingPoint.rotation);

        Rigidbody2D bulletRigidbody = bullet.GetComponent<Rigidbody2D>();
        if (bulletRigidbody != null)
        {
            Vector2 shootDirection = shootingPoint.right;
            bulletRigidbody.velocity = shootDirection * 10f;
            bulletRigidbody.velocity = shootDirection * 10f;

            Destroy(bullet, maxDistance / 10f);
        }
        else
        {
            Debug.LogError("Rigidbody2D NULL");
        }
    }
    void AutoShoot()
    {
        bullet = Instantiate(bulletPrefab, shootingPoint.position, shootingPoint.rotation);

        Rigidbody2D bulletRigidbody = bullet.GetComponent<Rigidbody2D>();
        if (bulletRigidbody != null)
        {
            Vector2 shootDirection = sword.transform.position - shootingPoint.position;
            bulletRigidbody.velocity = shootDirection.normalized * 10f;

            Destroy(bullet, maxDistance / 10f);
        }
        else
        {
            Debug.LogError("Rigidbody2D NULL");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Attack" && gameObject.tag != "Controlled")
        {
            CurHP -= DataManager.Instance._SwordData.player_damage_attack;
            player.GetComponent<StatController>().Stat("ST", 3);
        }
    }
}
