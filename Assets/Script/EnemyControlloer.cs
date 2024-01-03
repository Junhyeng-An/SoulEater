using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using static SoonsoonData;

public class EnemyController : MonoBehaviour
{
    public enum EnemyType
    {
        Enemy_A,
        Enemy_B,
        Enemy_C,
        Boss_A
    }
    public EnemyType enemyType;
    [Header("AI")]
    public float detect_distance = 4.0f;
    public float attack_distance = 2.0f;
    int nextMove;//행동지표를 결정할 변수

    [Header("Bar_Position")]
    public Vector3 XYSpace = new Vector3(0, 1, 0.15f); // Z = between space X, Y

    GameObject Player;
    GameObject Weapon;
    GameObject Attack_area;
    GameObject UI_EnemyStat;
    GameObject bar_EnemyHP;
    GameObject bar_EnemyWP;
    RectTransform bar_PosHP;
    RectTransform bar_PosWP;
    Slider Enemy_HP;
    Slider Enemy_WP;
    Animator animator;
    Rigidbody2D rigid;
    Rigidbody2D rigidPlayer;
    SpriteRenderer spriteRenderer;
    CircleCollider2D collider;
    CircleCollider2D colliderPlayer;

    Sword sword;
    Movement movement;
    TimeScale timeScale;
    Soul_Drop Soul_Drop;
    StatController stat;
    SkillController skill;
    PlayerController playerController;
    Red_Square red_Square;

    public EnemyData EnemyA;
    public EnemyData EnemyB;
    public EnemyData EnemyC;


    [HideInInspector] public float CurHP;
    [HideInInspector] public float MaxHP;
    [HideInInspector] public float CurWP;
    [HideInInspector] public float MaxWP;
    [HideInInspector] public float damage_enemyAttack;
    [HideInInspector] public SkillController.Skill_Active CurSkill;

    [HideInInspector] public float timer;

    [HideInInspector] public bool isHit = false;
    [HideInInspector] public bool isParried = false;
    [HideInInspector] public bool isDamage = false;

    bool isAni = false;
    bool isEnemy = true;
    bool isPlayer = false;
    bool isAttake = false;
    bool issearch = false;
    bool hasDroppedItem = false;

    Vector2 pos;
    Vector2 playerPos;
    string E_filePath;
    string E_filePath1;
    string E_filePath2;
    public class EnemyData
    {
        public float curHP;
        public float maxHP;
        public float CurWP;
        public float MaxWP;
        public float detect_distance;
        public float attack_distance;
        public float damage_enemyAttack;
        public SkillController.Skill_Active CurSkill;


        public EnemyData(float cur, float max, float curWp, float maxWp, float detect, float attack, float damage, SkillController.Skill_Active skill)
        {
            curHP = cur;
            maxHP = max;
            CurWP = curWp;
            MaxWP = maxWp;
            detect_distance = detect;
            attack_distance = attack;
            damage_enemyAttack = damage;

            CurSkill = skill;
        }

        public static EnemyData LoadFromJSON(string filePath)
        {
            if (System.IO.File.Exists(filePath))
            {
                string json = System.IO.File.ReadAllText(filePath);
                return JsonUtility.FromJson<EnemyData>(json);
            }
            else
            {
                Debug.LogError("File not found: " + filePath);
                return null;
            }
        }
    }

    void Awake()
    {
        Enemy_data_save();
        EnemyStat();
        Objects();
        Components();
        Invoke("Think", 1);
    }

    public void EnemyStat() // Setting Status Enemy, If Start Disarmed
    {
        switch (enemyType)
        {
            case EnemyType.Enemy_A:
                LoadEnemyData(E_filePath);
                CurSkill = SkillController.Skill_Active.Smash;
                break;
            case EnemyType.Enemy_B:
                LoadEnemyData(E_filePath1);
                CurSkill = SkillController.Skill_Active.Push;
                break;
            case EnemyType.Enemy_C:
                LoadEnemyData(E_filePath2);
                CurSkill = SkillController.Skill_Active.DashAttack;
                break;
            case EnemyType.Boss_A:
                break;
            default:
                break;
        }
    }
    void Components()       // GetComponent<>()
    {
        rigid = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        collider = GetComponent<CircleCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        skill = Player.GetComponent<SkillController>();
        movement = Player.GetComponent<Movement>();
        rigidPlayer = Player.GetComponent<Rigidbody2D>();
        colliderPlayer = Player.GetComponent<CircleCollider2D>();
        playerController = Player.GetComponent<PlayerController>();

        stat = GameObject.Find("GameManager").GetComponent<StatController>();
        timeScale = GameObject.Find("GameManager").GetComponent<TimeScale>();
        sword = GameObject.Find("Sword").GetComponent<Sword>();
        Soul_Drop = GetComponent<global::Soul_Drop>();

        bar_PosHP = bar_EnemyHP.GetComponent<RectTransform>();
        bar_PosWP = bar_EnemyWP.GetComponent<RectTransform>();
        Enemy_HP = bar_EnemyHP.transform.Find("SliderHP").GetComponent<Slider>();
        Enemy_WP = bar_EnemyWP.transform.Find("SliderWP").GetComponent<Slider>();
    }
    void Objects()          // gameObject
    {
        Player = GameObject.Find("Player");

        UI_EnemyStat = transform.Find("UI_EnemyStat").gameObject;
        bar_EnemyHP = UI_EnemyStat.transform.Find("EnemyHP").gameObject;
        bar_EnemyWP = UI_EnemyStat.transform.Find("EnemyWP").gameObject;

        Attack_area = transform.Find("Attack_area").gameObject;
        Weapon = transform.Find("Root").Find("BodySet").Find("P_Body").Find("ArmSet").gameObject;
    }

    void Update()
    {
        //Debug.Log(CurSkill);

        pos = transform.position;
        playerPos = Player.transform.position;

        Weapon.SetActive(true);
        UI_EnemyStat.SetActive(true);

        Check_HW();
        Check_Die();

        if (gameObject.tag == "Enemy")
            Tag_Enemy();
        if (gameObject.tag == "Disarmed")
            Tag_Disarmed();
        if (gameObject.tag == "Controlled")
            Tag_Controlled();
        else
            animator.SetFloat("RunState", 0.1f); //Run Animation//

        if (sword.isSwing == false)
        {
            isDamage = false;
        }
    }
    void Tag_Enemy()        // tag == Enemy
    {
        if (issearch == false)
            Idle();
        else
            Enemy_detect();
    }
    void Tag_Disarmed()     // tag == DisArmed
    {
        CurWP = 0;
        Weapon.SetActive(false);

        if (isAni == true)
        {
            animator.SetTrigger("parrying");
            StartCoroutine(StopForSeconds(1f));
        }
        else
        {
            float runSpeed = 1f;

            if (pos.x - playerPos.x < 0)
            {
                transform.rotation = Quaternion.Euler(0f, 0f, 0f);
                transform.Translate((pos - playerPos).normalized * runSpeed * Time.deltaTime);
            }
            else if (pos.x - playerPos.x >= 0)
            {
                transform.rotation = Quaternion.Euler(0f, 180f, 0f);
                transform.Translate((playerPos - pos).normalized * runSpeed * Time.deltaTime);
            }
        }
    }
    void Tag_Controlled()   // tag == Controlled
    {
        Weapon.SetActive(false);
        UI_EnemyStat.SetActive(false);

        if (isPlayer == true)
        {
            transform.position = playerPos - Vector2.up * 0.5f;

            if (playerController.isThrowing == true)
            {
                isPlayer = false;
                collider.enabled = true;
                rigid.gravityScale = 5;
                UI_EnemyStat.SetActive(false);
                Weapon.SetActive(false);
            }
        }
    }
    void Check_HW()         // Check HP, WP
    {
        Enemy_HP.value = CurHP / MaxHP;
        Enemy_WP.value = CurWP / MaxWP;

        Vector3 bar_pos = transform.position + Vector3.right * XYSpace.x + Vector3.up * XYSpace.y;
        bar_PosHP.position = Camera.main.WorldToScreenPoint(bar_pos);
        bar_PosWP.position = Camera.main.WorldToScreenPoint(bar_pos - Vector3.up * XYSpace.z);
    }
    void Check_Die()        // Check Die Player & Enemy
    {
        if (CurHP <= 0)
        {
            if (gameObject.tag == "Controlled")
            {
                CurHP = 0;
                bar_EnemyHP.SetActive(false);
                bar_EnemyWP.SetActive(false);
                Invoke("Die_Player", 1.4f);
            }
            else
            {
                if (!hasDroppedItem)
                {
                    Soul_Drop.DropItem();
                    hasDroppedItem = true;
                    bar_EnemyHP.SetActive(false);
                    bar_EnemyWP.SetActive(false);
                }

                CurHP = 0;
                animator.SetTrigger("Die");
                Invoke("Die_Enemy", 1.4f);
            }
        }
    }
    void Die_Player()
    {
        gameObject.SetActive(false);
        timeScale.SlowMotion(TimeScale.MotionType.die);
        movement.gameover = true;
    }
    void Die_Enemy()
    {
        gameObject.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        Change_Player(col);
        Col_Red_Square(col);
    }
    private void OnTriggerStay2D(Collider2D col)
    {
        Col_Sword(col);
        Col_Enemy(col);
    }
    private void OnTriggerExit2D(Collider2D col)
    {
        Ejection(col);
    }
    void Change_Player(Collider2D col)
    {
        if (col.gameObject.tag == "Sword" && gameObject.tag == "Disarmed")
        {
            if (playerController.isThrowing == true)
            {
                rigidPlayer.velocity *= Vector2.right; // velocity.y = 0
                playerController.isThrowing = false;
                isPlayer = true;
                colliderPlayer.isTrigger = false;
                movement.bounceCount = 2;
                collider.enabled = false;
                rigid.gravityScale = 0;
                gameObject.tag = "Controlled";

                if (Attack_area != null)
                    Attack_area.SetActive(false);
                timer = 0.0f;
            }
        }
    }
    void Col_Sword(Collider2D col)
    {
        if (col.gameObject.layer == LayerMask.NameToLayer("Sword") &&
            gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            if (isDamage == false)
            {
                if (col.gameObject.tag == "Attack" && gameObject.tag != "Controlled")
                {
                    CurHP -= sword.damage_playerAttack;
                    //stat.Stat("ST", 3);

                    isDamage = true;

                    timeScale.SlowMotion(TimeScale.MotionType.attack);
                }
                if (col.gameObject.tag == "Parrying" && gameObject.tag != "Controlled" &&
                    timer > 0 && timer <= 1.2f)
                {
                    CurWP -= sword.damage_playerParrying;
                    stat.Stat("ST", 6);

                    if (CurWP <= 0)
                    {
                        gameObject.tag = "Disarmed";
                    }

                    isDamage = true;

                    timeScale.SlowMotion(TimeScale.MotionType.attack);
                }
            }
        }
    }
    void Col_Enemy(Collider2D col)
    {
        if (col.CompareTag("closehit") && isHit == false)
        {
            CurHP -= damage_enemyAttack; 
            isHit = true;
        }
    }
    void Col_Red_Square(Collider2D col)
    {
        if (col.tag == "red_square") 
        {
            CurHP -= red_Square.Damage;
        }
    }

    void Ejection(Collider2D col)
    {
        if (gameObject.tag == "Controlled" && playerController.isThrowing == true)
        {
            gameObject.tag = "Disarmed";
        }
    }
    
    /// [ AI ] ///
    void Idle() //Enemy ai Idle
    {
        rigid.velocity = new Vector2(nextMove, rigid.velocity.y);

        float distance = Vector2.Distance(pos, playerPos);

        //Enemy Move 
        Vector2 frontVec = new Vector2(rigid.position.x + nextMove * 1.2f, rigid.position.y);

        RaycastHit2D rayHit = Physics2D.Raycast(frontVec, Vector3.down, 1, LayerMask.GetMask("Floor"));
        if (pos.x - frontVec.x < 0)
        {
            transform.rotation = Quaternion.Euler(0f, 180f, 0f);
        }
        else if (pos.x - frontVec.x > 0)
        {
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        }

        if (rayHit.collider == null)
        {
            Turn();
        }
        if (distance <= detect_distance && distance >= attack_distance)
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

        float distance = Vector2.Distance(pos, playerPos);
        if (isAttake == false && CurHP > 0)
        {
            // flip
            if (pos.x - playerPos.x < 0)
            {
                transform.rotation = Quaternion.Euler(0f, 180f, 0f);
            }
            else if (pos.x - playerPos.x >= 0)
            {
                transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            }

            if (distance <= detect_distance && distance >= attack_distance)
            {
                transform.position = Vector2.Lerp(pos, playerPos, 0.4f * Time.deltaTime);
            }
            else if (distance <= attack_distance) //Attack AI start
            {
                isAttake = true;
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

    private void Enemy_data_save()
    {
        E_filePath = Application.persistentDataPath + "/EnemyA_data.json";
        E_filePath1 = Application.persistentDataPath + "/EnemyB_data.json";
        E_filePath2 = Application.persistentDataPath + "/EnemyC_data.json";
        Debug.Log("File path: " + E_filePath);
        Debug.Log("File path: " + E_filePath1);
        Debug.Log("File path: " + E_filePath2);
         EnemyA = new EnemyData(100, 100,20,20,5,2, 10, SkillController.Skill_Active.Smash);
         EnemyB = new EnemyData(80, 80,20,20,5,2, 10, SkillController.Skill_Active.DashAttack);
         EnemyC = new EnemyData(50, 50,20,20,5,2, 10, SkillController.Skill_Active.Push);
        string jsonA = JsonUtility.ToJson(EnemyA);
        string jsonB = JsonUtility.ToJson(EnemyB);
        string jsonC = JsonUtility.ToJson(EnemyC);
        System.IO.File.WriteAllText(E_filePath , jsonA);
        System.IO.File.WriteAllText(E_filePath1 ,jsonB);
        System.IO.File.WriteAllText(E_filePath2 , jsonC);
    }

    private void LoadEnemyData(string filePath)
    {
        EnemyData enemyData = EnemyData.LoadFromJSON(filePath);

        if (enemyData != null)
        {
            CurHP = enemyData.curHP;
            CurWP = enemyData.CurWP;
            MaxHP = enemyData.maxHP;
            MaxWP = enemyData.MaxWP;
            detect_distance = enemyData.detect_distance;
            attack_distance = enemyData.attack_distance;
            CurSkill = enemyData.CurSkill;
        }
    }
    ////////////////////

    IEnumerator StopForSeconds(float time)
    {
        yield return new WaitForSeconds(time);
        isAni = false;
    }
}