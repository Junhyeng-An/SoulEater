using UnityEngine;

public class Square_Bullet : MonoBehaviour
{
    float Bullet_Damage =10;
    // TODO: Bat Falling Damage (Damage to be adjusted later)
    GameObject player;
    GameObject hit_area;
    EnemyController enemyController;
    bool isimmune;
    // Start is called before the first frame update
    void Start()
    {
        SoundManager.Instance.Playsfx(SoundManager.SFX.Bat_Cry);
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Col_Red_Square(collision);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Floor")
        {
            SoundManager.Instance.Playsfx(SoundManager.SFX.Bat_Drop);
            Destroy(gameObject);
        }
    }
    void Col_Red_Square(Collider2D col)
    {
        player = GameObject.FindGameObjectWithTag("Controlled");
        if (player != null)
        {
            enemyController = player.GetComponent<EnemyController>();
            if (enemyController != null)
            {
                if (col.gameObject.tag == "hit_area")
                {
                    SoundManager.Instance.Playsfx(SoundManager.SFX.Bat_Drop);
                    SettingManager.Instance.Damage_Calculate(col, Bullet_Damage, enemyController);
                    Destroy(gameObject);
                }
            }
        }
    }
}
