using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build.Content;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEditor.Searcher;
using UnityEditor.SearchService;
using Unity.VisualScripting;

public class EnemyController : MonoBehaviour
{
    public GameObject Player;
    public RectTransform my_bar;
    public GameObject Canvas;
    public Soul_Drop Soul_Drop;
    public float CurHP; 
    public float MaxHP;
    public float CurWP;
    public float MaxWP;
    public int nextMove;//행동지표를 결정할 변수
    public float detect_meter = 4.0f;
    public float attack_meter = 1.2f;
    public bool issearch = false;
    float height = 0.8f;

    [SerializeField]
    Slider Enemy_HP;

    Rigidbody2D rigid;
    SpriteRenderer spriteRenderer;
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
        Soul_Drop = GetComponent<global::Soul_Drop>();
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        Invoke("Think", 1);
    }
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
        if(gameObject.tag == "Enemy" || gameObject.tag == "Disarmed")
        {
            if(issearch == false)
            {
                Idle();
            }
            else if(issearch == true) 
            {
                Enemy_detect();
            }
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
            Soul_Drop.DropItem();
            this.gameObject.active = false;
            
        }
    }

    void Idle() //Enemy ai Idle
    {
        rigid.velocity = new Vector2(nextMove, rigid.velocity.y);

        Vector2 Mypos = transform.position;

        GameObject con = GameObject.FindGameObjectWithTag("Player");

        Vector2 conPos = con.transform.position;

        float distance = Vector2.Distance(Mypos, conPos);

        //Enemy Move 
        Vector2 frontVec = new Vector2(rigid.position.x + nextMove * 1.2f, rigid.position.y);
        Debug.DrawRay(frontVec, Vector3.down, new Color(0, 1, 0)); //fall_area check Ray

        RaycastHit2D rayHit = Physics2D.Raycast(frontVec, Vector3.down, 1, LayerMask.GetMask("Floor"));


        if (rayHit.collider == null)
        {
            Turn();
        }
        if(distance <= detect_meter && distance >= attack_meter)
        {
            issearch = true;    
        }


    }

    void Think()
    {

        //set next active
        nextMove = Random.Range(-1, 2); //-1 = left, 0 = stop ,1 = right

        //change direct
        if (nextMove != 0)
        {
            spriteRenderer.flipX = (nextMove == 1);
        }
        if (nextMove == 0)
        {
            Invoke("Think", 1.0f);
        }
        else
        {

            float nextThinkTime = Random.Range(2f, 5f);//thinktime cooltime

            Invoke("Think", nextThinkTime);
        }
    }

    void Turn() // if Enemyfront == fall_area
    {
        nextMove = nextMove * (-1);
        spriteRenderer.flipX = (nextMove == 1);
        CancelInvoke();
        Invoke("Think", 2);
    }
    void Enemy_detect()
    {
        Vector2 Mypos = transform.position;

        GameObject con = GameObject.FindGameObjectWithTag("Player");

        Vector2 conPos = con.transform.position;

        float distance = Vector2.Distance(Mypos, conPos);


        if (distance <= detect_meter && distance >= attack_meter)
        {
            Debug.Log("인식됨");
            transform.position = Vector2.Lerp(transform.position, conPos, 0.4f * Time.deltaTime);
        }
        else if (distance <= attack_meter)
        {
            //Attack();
            Debug.Log("플레이어를 향해 공격중");
        }
        else
        {
            issearch = false;
        }
    }
}
