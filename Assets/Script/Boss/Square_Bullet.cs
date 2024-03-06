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
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Col_Red_Square(collision);
    }
    void Col_Red_Square(Collider2D col)
    {
        player = GameObject.FindGameObjectWithTag("Controlled");
        enemyController = player.GetComponent<EnemyController>();
        if (col.gameObject.tag == "hit_area")
        {
            SettingManager.Instance.Damage_Calculate(col, Bullet_Damage, enemyController);
            Destroy(gameObject);
        }
    }

}
