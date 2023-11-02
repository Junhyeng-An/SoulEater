using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build.Content;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class EnemyController : MonoBehaviour
{
    public GameObject Player;
    public RectTransform my_bar;
    public GameObject Canvas;
    public float CurHP; 
    public float MaxHP;
    public float CurWP;
    public float MaxWP;
    float height = 0.8f;
    [SerializeField]
    Slider Enemy_HP;

    StatController stat;

    public bool isDamage = false;
    private Sword sword;
    public enum EnemyType
    {
        Enemy_A,
        Enemy_B,
        Enemy_C,
        Boss_A
    }
    public EnemyType enemyType;

    bool isPlayer = false;
    bool isEnemy = true;

    Rigidbody2D rigidPlayer;
    public void CheckState()
    {
        switch (enemyType)
        {
            case EnemyType.Enemy_A:
                CurHP = 100;
                MaxHP = 100;
                CurWP = 10;
                MaxWP = 10;
                break;
            case EnemyType.Enemy_B:
                CurHP = 80;
                MaxHP = 80;
                CurWP = 10;
                MaxWP = 10;
                break;
            case EnemyType.Enemy_C:
                break;
            case EnemyType.Boss_A:
                break;
            default:
                break;
        }
    }
    // Start is called before the first frame update
    void Awake()
    {
        CheckState();
        stat = GameObject.Find("GameManager").GetComponent<StatController>();
        rigidPlayer = Player.GetComponent<Rigidbody2D>();
        sword = GameObject.Find("Sword").GetComponent<Sword>();
    }
    // Update is called once per frame
    void Update()
    {
        if (gameObject.layer == 12)
        {
            Canvas.SetActive(true);
            Enemy_HP.value = CurHP / MaxHP;
            Vector3 hpbar_pos = Camera.main.WorldToScreenPoint(new Vector3(transform.position.x, transform.position.y + height, 0));
            my_bar.position = hpbar_pos;
        }
        if (gameObject.tag == "Controlled")
        {
            Canvas.SetActive(false);
        }
        if (isPlayer == true)
        {
            transform.position = Player.transform.position;
            if (Player.GetComponent<PlayerController>().isThrowing == true)
            {
                isPlayer = false;
                GetComponent<CircleCollider2D>().enabled = true;
                GetComponent<Rigidbody2D>().gravityScale = 5;
            }
        }
        HP_Check();
        if(sword.swingForce <= 1.5f)
        {
            isDamage = false;
        }
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Sword" && gameObject.tag == "Disarmed")
        {
            if (col.gameObject.GetComponentInParent<PlayerController>().isThrowing == true)
            {
                rigidPlayer.velocity = new Vector2(rigidPlayer.velocity.x, 0);
                col.gameObject.GetComponentInParent<PlayerController>().isThrowing = false;
                isPlayer = true;
                Player.GetComponent<CircleCollider2D> ().isTrigger = false;
                Player.GetComponent<Movement>().bounceCount = 2;
                GetComponent<CircleCollider2D>().enabled = false;
                GetComponent<Rigidbody2D>().gravityScale = 0;
                gameObject.tag = "Controlled";
            }
        }
    }
    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.layer == 11 && gameObject.layer == 12) //collid Sword && Enemy state
        {
            if (isDamage == false)
            {
                if (col.gameObject.tag == "Attack" && gameObject.tag != "Controlled")
                {
                    CurHP -= 10;
                    stat.Stat("ST", 3);

                    isDamage = true;
                }
                if (col.gameObject.tag == "Parrying" && gameObject.tag != "Controlled")
                {
                    CurWP -= 10;
                    stat.Stat("ST", 6);

                    if (CurWP <= 0)
                    {
                        gameObject.tag = "Disarmed";
                    }

                    isDamage = true;
                }
            }
        }
    }
    private void OnTriggerExit2D(Collider2D col)
    {
        if (gameObject.tag == "Controlled" && col.gameObject.GetComponentInParent<PlayerController>().isThrowing == true)
        {
            gameObject.tag = "Disarmed"; //Disarm
        }
    }
    public void HP_Check()
    {
        if(CurHP <= 0)
        {
            this.gameObject.active = false;
        }
    }
}
