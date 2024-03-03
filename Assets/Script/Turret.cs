using Com.LuisPedroFonseca.ProCamera2D.TopDownShooter;
using UnityEngine;
using UnityEngine.UI;

public class Turret : MonoBehaviour
{
    public float CurHP;
    public float MaxHP;
    public float fireRate = 0.5f;            // �߻� �ӵ� (�ʴ� �߻� Ƚ��)
    public float maxDistance = 10f;        // �߻� �ִ� �Ÿ�
    public float hp_har_height = 1;
    public GameObject bulletPrefab;        // �Ѿ� ������
    public Transform shootingPoint;        // �߻� ���� ������Ʈ
    public RectTransform my_bar;
    public GameObject Canvas;
    GameObject bullet;
    GameObject player;
    private float nextFireTime;             // ���� �߻� �ð�
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
        // �߻� �ӵ��� ���� �Ѿ� �߻�
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
        // �߻� �������� �Ѿ� ����
        bullet = Instantiate(bulletPrefab, shootingPoint.position, shootingPoint.rotation);

        // �Ѿ˿� Rigidbody2D�� �ִٰ� �����ϰ� �ӵ� ����
        Rigidbody2D bulletRigidbody = bullet.GetComponent<Rigidbody2D>();
        if (bulletRigidbody != null)
        {
            // �Ѿ� �߻� ���� ���� (�Ѿ��� right ����)
            Vector2 shootDirection = shootingPoint.right;
            bulletRigidbody.velocity = shootDirection * 10f;
            bulletRigidbody.velocity = shootDirection * 10f;

            // �Ѿ��� ���� �ð� �Ŀ� �ı�
            Destroy(bullet, maxDistance / 10f);
        }
        else
        {
            Debug.LogError("�Ѿ˿� Rigidbody2D�� �����ϴ�!");
        }
    }
    void AutoShoot()
    {
        // �߻� �������� �Ѿ� ����
        bullet = Instantiate(bulletPrefab, shootingPoint.position, shootingPoint.rotation);

        // �Ѿ˿� Rigidbody2D�� �ִٰ� �����ϰ� �ӵ� ����
        Rigidbody2D bulletRigidbody = bullet.GetComponent<Rigidbody2D>();
        if (bulletRigidbody != null)
        {
            // ���� ��ġ�� �������� �Ѿ� �߻� ���� ����
            Vector2 shootDirection = sword.transform.position - shootingPoint.position;
            bulletRigidbody.velocity = shootDirection.normalized * 10f;

            // �Ѿ��� ���� �ð� �Ŀ� �ı�
            Destroy(bullet, maxDistance / 10f);
        }
        else
        {
            Debug.LogError("�Ѿ˿� Rigidbody2D�� �����ϴ�!");
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
