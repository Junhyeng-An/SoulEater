using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using static SoonsoonData;
using Com.LuisPedroFonseca.ProCamera2D;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;

public class EnemyController : MonoBehaviour
{
    public enum EnemyType
    {
        Enemy_A,
        Enemy_B,
        Enemy_C,
        Boss_A
    }
    public enum EnemyState
    {
        Idle,
        Posion
    }
    public EnemyType enemyType;
    public EnemyState enemyState;
    [Header("AI")]
    public float detect_distance = 4.0f;
    public float attack_distance = 2.0f;
    int nextMove;//행동지표를 결정할 변수
    float skill_MaxHP;
    float result_MaxHP;
    private System.Threading.Timer poisonTimer;  // 독 상태 지속 타이머

    [Header("Bar_Position")]
    public Vector3 XYSpace = new Vector3(0, 1, 0.15f); // Z = between space X, Y

    GameObject Player;
    public GameObject Hit_Area;
    //GameObject Weapon;
    GameObject Attack_area;
    GameObject UI_EnemyStat;
    GameObject bar_EnemyHP;
    GameObject bar_EnemyWP;
    RectTransform bar_PosHP;
    RectTransform bar_PosWP;
    Slider Enemy_HP;
    Slider Enemy_WP;
    //Animator animator;
    Rigidbody2D rigid;
    Rigidbody2D rigidPlayer;
    SpriteRenderer spriteRenderer;
    public SpriteRenderer Posion_Sprite;
    CircleCollider2D collider;
    CircleCollider2D colliderPlayer;

    Sword sword;
    Movement movement;
    TimeScale timeScale;
    StatController stat;
    SkillController skill;
    PlayerController playerController;
    Red_Square red_Square;
    Laser_Pattern laser_Pattern;

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
    [HideInInspector] public bool isDamage_skill = false;

    bool isAni = false;
    bool isEnemy = true;
    bool isPlayer = false;
    bool isAttake = false;
    bool issearch = false;
    bool hasDroppedItem = false;
    bool ispoison = false;

    Vector2 pos;
    Vector2 playerPos;
    string E_filePath;
    string E_filePath1;
    string E_filePath2;

    /// <summary>
    /// Enemy Stat
    /// </summary>
 
    /// <summary>
    /// Enemy Stat
    /// </summary>
    float elapsedTime = 0f;
    float poisonDuration = 5f; // 수정: 독 지속 시간을 5초로 변경

    private bool Drop_Soul = false;
    
    
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
                LoadEnemyData(EnemyA);
                break;
            case EnemyType.Enemy_B:
                LoadEnemyData(EnemyB);
                break;
            case EnemyType.Enemy_C:
                LoadEnemyData(EnemyC);
                break;
            case EnemyType.Boss_A:
                break;
            default:
                break;
        }
        enemyState = EnemyState.Idle;
    }
    void Components()       // GetComponent<>()
    {
        rigid = GetComponent<Rigidbody2D>();
        //animator = GetComponent<Animator>();
        collider = GetComponent<CircleCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        movement = Player.GetComponent<Movement>();
        rigidPlayer = Player.GetComponent<Rigidbody2D>();
        colliderPlayer = Player.GetComponent<CircleCollider2D>();
        playerController = Player.GetComponent<PlayerController>();

        skill = GameObject.Find("GameManager").GetComponent<SkillController>();
        stat = GameObject.Find("GameManager").GetComponent<StatController>();
        timeScale = GameObject.Find("GameManager").GetComponent<TimeScale>();
        sword = GameObject.Find("Sword").GetComponent<Sword>();

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
        //Weapon = transform.Find("Root").Find("BodySet").Find("P_Body").Find("ArmSet").gameObject;
    }
    void Update()
    {
        //Debug.Log(CurSkill);
        //최대체력 증가
        if ((gameObject.tag == "Controlled") || SelectManager.Instance.isChange_C)
        {
            SelectManager.Instance.isChange_C = false;
            skill_MaxHP = DataManager.Instance._Player_Skill.MaxHP;
            switch (enemyType)
            {
                case EnemyType.Enemy_A:
                    result_MaxHP = EnemyA.maxHP + skill_MaxHP;
                    MaxHP = result_MaxHP;
                    if (CurHP == MaxHP )
                    {
                        CurHP = MaxHP;
                    }
                    SelectManager.Instance.isHPupadate = false;
                    break;
                case EnemyType.Enemy_B:
                    result_MaxHP =  EnemyB.maxHP + skill_MaxHP;
                    MaxHP = result_MaxHP;
                    if (CurHP == MaxHP)
                    {
                        CurHP = MaxHP;
                    }
                    SelectManager.Instance.isHPupadate = false;
                    break;
                case EnemyType.Enemy_C:
                    result_MaxHP =  EnemyC.maxHP + skill_MaxHP;
                    MaxHP = result_MaxHP;
                    if (CurHP == MaxHP)
                    {
                        CurHP = MaxHP;
                    }
                    SelectManager.Instance.isHPupadate = false;
                    break;
            }
        }
        else if (gameObject.tag != "Controlled")
        {
            SelectManager.Instance.isHPupadate = false;
            switch (enemyType)
            {
                case EnemyType.Enemy_A:
                    MaxHP =  EnemyA.maxHP;
                    break;
                case EnemyType.Enemy_B:
                    MaxHP =  EnemyB.maxHP;
                    break;
                case EnemyType.Enemy_C:
                    MaxHP =  EnemyC.maxHP;
                    break;
            }
        }

        pos = transform.position;
        playerPos = Player.transform.position;

        //Weapon.SetActive(true);
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
            //animator.SetFloat("RunState", 0.1f); //Run Animation//

        if (sword.isSwing == false)
        {
            isDamage = false;
        }
        
        
        string currentSceneName = SceneManager.GetActiveScene().name;
        if (currentSceneName == "Dorf")
            CurHP = MaxHP;



    }

    private void LateUpdate()
    {
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
        //Weapon.SetActive(false);

        if (isAni == true)
        {
            //animator.SetTrigger("parrying");
            StartCoroutine(StopForSeconds(1f));
        }
        else
        {
            float runSpeed = 1f;

            if (pos.x - playerPos.x < 0)
            {
                transform.Translate((pos - playerPos).normalized * runSpeed * Time.deltaTime);
            }
            else if (pos.x - playerPos.x >= 0)
            {
                transform.Translate((playerPos - pos).normalized * runSpeed * Time.deltaTime);
            }
        }
    }
    void Tag_Controlled()   // tag == Controlled
    {
        //Weapon.SetActive(false);
        UI_EnemyStat.SetActive(false);

        if (isPlayer == true)
        {
            transform.position = playerPos;

            if (playerController.isThrowing == true)
            {
                isPlayer = false;
                collider.enabled = true;
                rigid.gravityScale = 5;
                UI_EnemyStat.SetActive(false);
                //Weapon.SetActive(false);
            }
        }
    }
    void Check_HW()         // Check HP, WP
    {
        Enemy_HP.value = Mathf.Clamp01(CurHP / MaxHP);
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
                    Coin_Soul_Manager.Instance.Drop_Coin(transform);
                    hasDroppedItem = true;
                    bar_EnemyHP.SetActive(false);
                    bar_EnemyWP.SetActive(false);
                }

                CurHP = 0;
                //animator.SetTrigger("Die");
                Invoke("Die_Enemy", 1.4f);
            }
        }
    }
    void Die_Player()
    {
        gameObject.SetActive(false);
        timeScale.SlowMotion(TimeScale.MotionType.die);
        movement.gameover = true;
        
        Debug.Log("Game over");
    }
    void Die_Enemy()
    {
        gameObject.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        Change_Player(col);
    }
    private void OnTriggerStay2D(Collider2D col)
    {
        Col_Sword(col);
        Col_Skill(col);
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
                SelectManager.Instance.isChange_C = true;
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
                    HP_Drain();
                    if (DataManager.Instance._Player_Skill.Poision_Damage_Level > 0 && !ispoison)
                    {
                        Posion(70);
                    }
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
                        if (Drop_Soul == false)
                        {
                            Coin_Soul_Manager.Instance.Drop_Soul(transform);
                            Drop_Soul = true;
                        }

                        gameObject.tag = "Disarmed";
                    }

                    isDamage = true;

                    timeScale.SlowMotion(TimeScale.MotionType.attack);
                }
            }
        }
    }
    void Col_Skill(Collider2D col)
    {
        if(skill.onSkill == true)
            isDamage_skill = false;
        if (col.gameObject.layer == LayerMask.NameToLayer("P_Attack") && isDamage_skill == false)
        {
            CurHP -= skill.damage;
            isDamage_skill = true;
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

    void Ejection(Collider2D col)
    {
        if (gameObject.tag == "Controlled" && playerController.isThrowing == true)
        {
            gameObject.tag = "Disarmed";
        }
    }

    void HP_Drain()
    {
        GameObject controlledObject = GameObject.FindWithTag("Controlled");

        if (controlledObject != null)
        {
            EnemyController enemyController = controlledObject.GetComponent<EnemyController>();

            if (enemyController != null)
            {
                enemyController.CurHP += sword.damage_playerAttack * DataManager.Instance._Player_Skill.HP_Drain/100;
            }
        }
    }
    IEnumerator UpdatePoison()
    {
        float elapsedTime = 0f;
        float poisonDuration = 0.047f; //독 유지시간 약 10초(9.4초)

        while (elapsedTime < poisonDuration)
        {
            // 1초마다 10의 독 데미지
            CurHP -= DataManager.Instance._Player_Skill.poison_damage;

            // 초록색으로 변경
            Posion_Sprite.color = Color.green;

            elapsedTime += Time.deltaTime;

            yield return new WaitForSeconds(1f);
            Debug.Log(elapsedTime);
            if (elapsedTime > poisonDuration ) { ispoison = false; break; }
            // 독 상태가 종료되면 코루틴 중단
            if (!ispoison)
                yield break;

        }

        // 독 상태가 지속된 후에 실행될 부분
        enemyState = EnemyState.Idle;
        Posion_Sprite.color = Color.white; // 원래의 색상으로 변경
        ispoison = false;
    }

    public void Posion(int Posion_per)
    {
        // 1부터 100까지의 무작위 숫자를 생성합니다.
        int randomNum = Random.Range(1, 101);

        // 생성된 숫자가 Posion_per보다 작거나 같은지 확인합니다.
        if (randomNum <= Posion_per)
        {
            // 독 상태가 아니라면 독 상태로 변경
            if (!ispoison)
            {
                // 적의 상태를 독으로 변경합니다.
                enemyState = EnemyState.Posion;
                StartCoroutine(UpdatePoison());
                ispoison = true;
            }
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
            if (nextMove == -1)
            {
                //spriteRenderer.flipX = false;
                //transform.rotation = Quaternion.Euler(0, 180, 0);
            }
            else if (nextMove == 1)
            {
                //transform.rotation = Quaternion.Euler(0, 0, 0);
                //spriteRenderer.flipX = (nextMove == 1);
            }
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
        //spriteRenderer.flipX = (nextMove == 1);
        CancelInvoke();
        Invoke("Think", 2);
    }
    void Enemy_detect()
    {

        float distance = Vector2.Distance(pos, playerPos);
        if (playerPos.x - pos.x >= 0)
        {
            //transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else if (playerPos.x - pos.x < 0)
        {
            //transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        if (isAttake == false && CurHP > 0)
        {
            if (distance <= detect_distance && distance >= attack_distance)
            {
                Vector3 direction = (Player.transform.position - transform.position).normalized;
                direction.y = 0;
                rigid.velocity = direction;
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
                        //animator.SetTrigger("Attack");//<-------- Attack Animation Here
                        isAni = true;
                    }
                }
                else
                {
                    if (isAni == true)
                    {
                        //animator.SetTrigger("parrying");
                        isAni = false;
                    }

                }
            }
            else if (timer >= 2.0f)
            {
                Attack_area.SetActive(false);
                //animator.SetFloat("RunState", 0.1f);
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
        //////////////////////////////////////Enemy Stat Change _ 
        
        
        
        float EnemyA_MaxHP = 200;
        float EnemyB_MaxHP = 200;
        float EnemyC_MaxHP = 200;   
        
        float EnemyA_MaxHP_2 = 400;
        float EnemyB_MaxHP_2 = 400;
        float EnemyC_MaxHP_2 = 400;
        
        
        
            
         

         if (DataManager.Instance._PlayerData.clear_stage == (int)stage.Main)
         {
             EnemyA = new EnemyData(EnemyA_MaxHP, EnemyA_MaxHP, 20, 20, 5, 2, 10, SkillController.Skill_Active.Smash);
             EnemyB = new EnemyData(EnemyB_MaxHP, EnemyB_MaxHP, 20, 20, 5, 2, 20, SkillController.Skill_Active.DashAttack);
             EnemyC = new EnemyData(EnemyC_MaxHP, EnemyC_MaxHP, 20, 20, 5, 2, 30, SkillController.Skill_Active.Slash);
         }

         else if(DataManager.Instance._PlayerData.clear_stage == (int)stage.stage1)
         {
             EnemyA = new EnemyData(EnemyA_MaxHP_2, EnemyA_MaxHP_2, 20, 20, 5, 2, 10, SkillController.Skill_Active.Smash);
             EnemyB = new EnemyData(EnemyB_MaxHP_2, EnemyB_MaxHP_2, 20, 20, 5, 2, 20, SkillController.Skill_Active.DashAttack);
             EnemyC = new EnemyData(EnemyC_MaxHP_2, EnemyC_MaxHP_2, 20, 20, 5, 2, 30, SkillController.Skill_Active.Slash);
         }
    }

    private void LoadEnemyData(EnemyData enemyData)
    {

        if (enemyData != null)
        {
            CurHP = enemyData.curHP;
            CurWP = enemyData.CurWP;
            MaxHP = enemyData.maxHP;
            MaxWP = enemyData.MaxWP;
            detect_distance = enemyData.detect_distance;
            attack_distance = enemyData.attack_distance;
            damage_enemyAttack = enemyData.damage_enemyAttack;
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