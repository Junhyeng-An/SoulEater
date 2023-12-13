using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class EnemyController : MonoBehaviour
{
    public GameObject Player;
    public GameObject Attack_area;
    public GameObject attackPrefab;
    public GameObject Hit_area;
    public GameObject[] Weapon;
    public RectTransform my_bar;
    public RectTransform my_bar_WP;
    public GameObject my_hp_bar;
    public GameObject my_WP_bar;
    public GameObject Canvas;
    public Soul_Drop Soul_Drop;
    public float CurHP;
    public float MaxHP;
    public float CurWP;
    public float MaxWP;
    public int nextMove;//행동지표를 결정할 변수
    public float detect_meter = 4.0f;
    public float attack_meter = 2.0f;
    public bool issearch = false;
    public float hp_har_height = 1;
    public float timer;
    public bool isHit = false;
    public bool isParried = false;

    bool isAttake = false;
    bool isAni = false;
    bool hasDroppedItem = false;
    private Animator animator;
    [SerializeField]
    Slider Enemy_HP;
    [SerializeField]
    Slider Enemy_WP;

    Rigidbody2D rigid;
    SpriteRenderer spriteRenderer;
    StatController stat;
    Movement movement;
    public bool isDamage = false;
    private Sword sword;
    private GameObject attackAreaInstance;
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
                CurWP = 20;
                MaxWP = 20;
                break;
            case EnemyType.Enemy_B:
                CurHP = 80;
                MaxHP = 80;
                CurWP = 20;
                MaxWP = 20;
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
        area_setting();
        CheckState();
        stat = GameObject.Find("GameManager").GetComponent<StatController>();
        rigidPlayer = Player.GetComponent<Rigidbody2D>();
        sword = GameObject.Find("Sword").GetComponent<Sword>();
        Soul_Drop = GetComponent<global::Soul_Drop>();
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        Invoke("Think", 1);
    }
    void Update()
    {
        if (gameObject.layer == 12)
        {
            for (int i = 0; i < Weapon.Length; i++)
            { Weapon[i].SetActive(true); }
            Canvas.SetActive(true);

            if (gameObject.tag == "Disarmed")
            {
                for (int i = 0; i < Weapon.Length; i++)
                {
                    Weapon[i].SetActive(false);
                }
                CurWP = 0;
            }

            if (gameObject.CompareTag("Controlled") != true)
            {
                animator.SetFloat("RunState", 0.1f); //Run Animation//
            }

            Enemy_HP.value = CurHP / MaxHP;
            Enemy_WP.value = CurWP / MaxWP;
            Vector3 hpbar_pos = Camera.main.WorldToScreenPoint(new Vector3(transform.position.x, transform.position.y + hp_har_height, 0));
            float slider_scale = 0.15f;
            Vector3 wpbar_pos = Camera.main.WorldToScreenPoint(new Vector3(transform.position.x, transform.position.y + hp_har_height - slider_scale, 0));
            my_bar.position = hpbar_pos;
            my_bar_WP.position = wpbar_pos;
        }
        if (gameObject.tag == "Controlled")
        {

            area_setting();
            for (int i = 0; i < Weapon.Length; i++)
            {
                Weapon[i].SetActive(false);
            }

            Canvas.SetActive(false);
        }
        else if (gameObject.tag != "Controlled")
        {
            area_setting();
        }

        if (isPlayer == true)
        {

            transform.position = new Vector2(Player.transform.position.x, Player.transform.position.y - 0.5f);
            if (Player.GetComponent<PlayerController>().isThrowing == true)
            {
                isPlayer = false;
                GetComponent<CircleCollider2D>().enabled = true;
                GetComponent<Rigidbody2D>().gravityScale = 5;
            }
        }
        HP_Check();
        if (sword.isSwing == false)
        {
            isDamage = false;
        }
        if (gameObject.tag == "Enemy" || gameObject.tag == "Disarmed")
        {
            if (issearch == false)
            {
                Idle();
            }
            else if (issearch == true)
            {
                if (gameObject.tag == "Enemy")
                    Enemy_detect();
                else
                    Enemy_Disarmed();
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
                Player.GetComponent<CircleCollider2D>().isTrigger = false;
                Player.GetComponent<Movement>().bounceCount = 2;
                GetComponent<CircleCollider2D>().enabled = false;
                GetComponent<Rigidbody2D>().gravityScale = 0;
                gameObject.tag = "Controlled";

                if (Attack_area != null)
                    Attack_area.SetActive(false);
                timer = 0.0f;
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
                if (col.gameObject.tag == "Parrying" && gameObject.tag != "Controlled" &&
                    timer > 0 && timer <= 1.2f)
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

    void area_setting()
    {
        if (gameObject.tag == "Controlled")
        {
            Hit_area.SetActive(true);
        }
        else
        {
            Hit_area.SetActive(false);
        }
    }

    public void HP_Check()
    {
        if (CurHP <= 0)
        {
            if (gameObject.tag == "Controlled")
            {
                CurHP = 0;
                //animator.SetTrigger("Die");
                my_hp_bar.SetActive(false);
                my_WP_bar.SetActive(false);
                Invoke("Die_me", 1.4f);
            }
            else
            {
                if (!hasDroppedItem)
                {
                    Soul_Drop.DropItem();
                    hasDroppedItem = true;
                    my_hp_bar.SetActive(false);
                    my_WP_bar.SetActive(false);
                }

                CurHP = 0;
                animator.SetTrigger("Die");
                Invoke("Die_enemy", 1.4f);
            }
        }
    }
    void Die_me()
    {
        gameObject.SetActive(false);
        Time.timeScale = 0;
    }
    void Die_enemy()
    {

        gameObject.SetActive(false);
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
        //Debug.DrawRay(frontVec, Vector3.down, new Color(0, 1, 0)); //fall_area check Ray

        RaycastHit2D rayHit = Physics2D.Raycast(frontVec, Vector3.down, 1, LayerMask.GetMask("Floor"));
        if (Mypos.x - frontVec.x < 0)
        {
            transform.rotation = Quaternion.Euler(0f, 180f, 0f);
        }
        else if (Mypos.x - frontVec.x > 0)
        {
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        }


        if (rayHit.collider == null)
        {
            Turn();
        }
        if (distance <= detect_meter && distance >= attack_meter)
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
        if (isAttake == false && CurHP > 0)
        {
            if (Mypos.x - conPos.x < 0)
            {
                transform.rotation = Quaternion.Euler(0f, 180f, 0f);
            }
            else if (Mypos.x - conPos.x >= 0)
            {
                transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            }
            if (distance <= detect_meter && distance >= attack_meter)
            {
                //Debug.Log("인식됨");
                transform.position = Vector2.Lerp(transform.position, conPos, 0.4f * Time.deltaTime);
            }
            else if (distance <= attack_meter) //Attack AI start
            {
                isAttake = true;
                //Debug.Log("플레이어를 향해 공격중");
            } //Attack AI End
            else
            {
                issearch = false;
                Attack_area.SetActive(false);
            }
        }
        else if (isAttake == true)
        {
            timer += Time.deltaTime;
            if (timer >= 0.0f && timer < 1.2f) //Attack in a 1second
            {
                if (isParried == false)
                {
                    Attack_area.SetActive(true);
                    if (isAni == false)
                    {
                        animator.SetTrigger("Attack");//<-------- Attack Animation Here
                        isAni = true;
                    }
                }
                else
                {
                    if (isAni == true)
                    {
                        animator.SetTrigger("parrying");
                        isAni = false;
                    }

                }
            }
            else if (timer >= 2.0f)
            {
                Attack_area.SetActive(false);
                animator.SetFloat("RunState", 0.1f);
                if (GameObject.FindGameObjectWithTag("Controlled") != null)
                    GameObject.FindGameObjectWithTag("Controlled").GetComponentInChildren<EnemyController>().isHit = false;
                timer = 0.0f;

                isAttake = false;
                isParried = false;
                isAni = false;
            }
        }
    }

    void Enemy_Disarmed()
    {
        if (isAni == true)
        {
            animator.SetTrigger("parrying");
            StartCoroutine(StopForSeconds(1f));
        }
        else
        {
            float runSpeed = 1f;

            Vector2 Mypos = transform.position;

            GameObject con = GameObject.FindGameObjectWithTag("Player");

            Vector2 conPos = con.transform.position;

            if (Mypos.x - conPos.x < 0)
            {
                transform.rotation = Quaternion.Euler(0f, 0f, 0f);
                transform.Translate((Mypos - conPos).normalized * runSpeed * Time.deltaTime);
            }
            else if (Mypos.x - conPos.x >= 0)
            {
                transform.rotation = Quaternion.Euler(0f, 180f, 0f);
                transform.Translate((conPos - Mypos).normalized * runSpeed * Time.deltaTime);
            }
        }
    }
    IEnumerator StopForSeconds(float time)
    {
        yield return new WaitForSeconds(time);
        isAni = false;
    }
}
