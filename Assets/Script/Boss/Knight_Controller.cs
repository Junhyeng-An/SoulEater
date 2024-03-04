using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;
public class Knight_Controller : MonoBehaviour
{
    [SerializeField]
    Slider Boss_HP;
    [SerializeField]
    TextMeshProUGUI pText_hp;

    float Boss_MaxHP = 1100;
    float Boss_CurHP;
    public float Boss_Attack_Damage = 15;
    public Transform player_T; // 플레이어의 위치를 저장할 변수
    public Animator animator; // 애니메이터 컴포넌트를 저장할 변수
    public GameObject attack_area; // 공격 영역을 나타내는 게임 오브젝트
    private Sword sword;
    private GameObject player;
    public GameObject Portal;
    public GameObject paze2_massage;
    public GameObject[] Layzers;
    private float moveSpeed = 2.5f; // 이동 속도
    public bool isFlipped = false; // 좌우 방향을 나타내는 변수
    private bool isAttacking = false; // 공격 중인지 여부를 나타내는 변수
    private bool isCoolingDown = false; // 쿨다운 중인지 여부를 나타내는 변수
    private float cooldownTime = 2.0f; // 쿨다운 시간
    private float damage_playerAttack;
    private bool isDamage;
    private bool isPaze;
    private bool isLayzing=false;
    private bool isImmume=false;

    private void Awake()
    {
        Boss_CurHP = Boss_MaxHP;
        sword = GameObject.Find("Sword").GetComponent<Sword>();
        damage_playerAttack = DataManager.Instance._SwordData.player_damage_attack;
    }
    // Update is called once per frame
    void Update()
    {
        player = GameObject.Find("GameManager");
        if (sword.isSwing == false)
        {
            isDamage = false;
        }
        if (Boss_CurHP > 1000) //1페이즈
        {
            // 플레이어와의 거리를 확인
            float distanceToPlayer = Vector3.Distance(transform.position, player_T.position);

            // 플레이어가 공격 범위 내에 있고 기사가 아직 공격 중이 아닌 경우
            if (distanceToPlayer <= 2f && !isAttacking && !isCoolingDown)
            {
                isAttacking = true;
                LookAtPlayer();
                AttackPlayer(); // 플레이어를 공격
                StartCoroutine(StartCooldown()); // 쿨다운 시작
            }
            else
            {
                isAttacking = false;
                if (!isCoolingDown)
                {
                    LookAtPlayer(); // 플레이어를 바라봄
                    RunBoss(); // 플레이어 쪽으로 이동
                }
            }
        }
        if (Boss_CurHP <= 1000 && Boss_CurHP > 0) //2페이즈
        {
            if(isPaze == false)
            {
                move_2paze();
            }
            if (isPaze == true) 
            {
                if(!isLayzing)
                {
                    Layzer();
                }
                
            }
        }
        // HP에 따라 패턴 전환
        if (Boss_CurHP <= 0)
        {
            // 보스 사망 처리 또는 다음 단계로 진행
            DataManager.Instance._PlayerData.clear_stage++;

            Portal.SetActive(true);
            Destroy(gameObject);

            Debug.Log(" Boss DIE ");
        }

        pText_hp.text = Mathf.Floor(Boss_CurHP) + " / " + Boss_MaxHP.ToString(); // 현재 체력을 표시합니다.
        Handle();


    }
    IEnumerator StartCooldown()
    {
        isCoolingDown = true;
        yield return new WaitForSeconds(cooldownTime);
        isCoolingDown = false;
    }
    // 플레이어를 바라보도록 회전시키는 함수
    public void LookAtPlayer()
    {
        Vector2 direction = player_T.position - transform.position;
        float x = direction.x;
        if (x < 0)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }


    // 플레이어를 공격하는 함수
    public void AttackPlayer()
    {
        // 애니메이션 완료 후 일정 시간 후에 공격 영역 비활성화
        StartCoroutine(DisableAttackArea());
    }

    // 공격 영역을 비활성화하는 코루틴 함수
    IEnumerator DisableAttackArea()
    {
        // 공격 애니메이션 시작
        animator.SetTrigger("Attack");
        yield return new WaitForSeconds(0.5f); // 짧은 시간 동안 대기
        // 공격 영역 활성화
        attack_area.SetActive(true);
        yield return new WaitForSeconds(0.5f); // 짧은 시간 동안 대기
        // 공격 영역 비활성화
        attack_area.SetActive(false);

    }

    // 플레이어 쪽으로 이동하는 함수
    public void RunBoss()
    {
        // 플레이어 쪽으로 일정 속도로 이동
        float targetX = player_T.position.x;
        float currentX = transform.position.x;
        float newX = Mathf.MoveTowards(currentX, targetX, moveSpeed * Time.deltaTime);
        transform.position = new Vector2(newX, transform.position.y);
    }

    void move_2paze()
    {
        isImmume = true;
        // 게임 오브젝트를 향해 이동하는 코드
        float step = moveSpeed * Time.deltaTime;
        float newX = Mathf.MoveTowards(transform.position.x, 0f, step);
        transform.position = new Vector2(newX, transform.position.y);
        if (transform.position.x == 0)
        {
            isPaze = true;
        }
        StartCoroutine(ActivateMessage());
    }



    void Layzer()
    {
        isLayzing = true;
        isImmume = true;
        StartCoroutine(ActivateLayzers());

    }

    IEnumerator ActivateLayzers()
    {
        // 0부터 5까지의 Layzers 배열을 순회하며 작업
        for (int i = 0; i < Layzers.Length; i++)
        {
            Layzers[i].SetActive(true);
            yield return new WaitForSeconds(0.2f);
            Layzers[i].SetActive(false);
            yield return new WaitForSeconds(0.2f);
        }
        isImmume = false;
        yield return new WaitForSeconds(4.0f);
        isLayzing = false;
    }
    IEnumerator ActivateMessage()
    {
        paze2_massage.SetActive(true);
        yield return new WaitForSeconds(2.0f);
        paze2_massage.SetActive(false);
    }

    public void TakeDamage(float damage)
    {
        Boss_CurHP -= damage;
    }

    void Handle() //hp , st 가 닳는 애니메이션
    {
        Boss_HP.value = Mathf.Lerp(Boss_HP.value, (float)Boss_CurHP / (float)Boss_MaxHP, Time.deltaTime * 10);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (isDamage == false && !isImmume)
        {
            if (collision.gameObject.tag == "Attack" && gameObject.tag != "Controlled")
            {
                TakeDamage(damage_playerAttack);
                player.GetComponent<StatController>().Stat("ST", 3);

                isDamage = true;
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isDamage)
        {
            if (collision.gameObject.tag == "Skill")
            {
                TakeDamage(DataManager.Instance._Active_Skill.Slash_Damage);
            }
        }
    }
}

