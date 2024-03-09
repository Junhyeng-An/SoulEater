using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;
public class Boss_Controller : MonoBehaviour
{
    [SerializeField]
    Slider Boss_HP;
    [SerializeField]
    TextMeshProUGUI pText_hp;

    public float maxHealth = 1000;  // 최대 체력
    private float currentHealth;    // 현재 체력

    float damage_playerAttack;
    private bool isPattern1Active = false;  // 패턴1 활성화 여부
    private bool isPattern2Active = false;  // 패턴2 활성화 여부
    private bool isDamage;

    public Circle_Fire pattern_circle;  // 패턴1 스크립트
    public Red_Square pattern_Square;  // 패턴2 스크립트
    public Laser_Pattern laserPattern;  // 레이저 패턴 스크립트
    public Animator wing_animator;
    public GameObject[] bloods;
    public GameObject Portal;
    private Sword sword;
    GameObject player;
    GameObject hit_area;
    Damage damage;

    private void Awake()
    {
        currentHealth = maxHealth;  // 시작 시 현재 체력을 최대 체력으로 초기화
        sword = GameObject.Find("Sword").GetComponent<Sword>();
        wing_animator = GetComponent<Animator>();
    }
    void Start()
    {
        damage_playerAttack = DataManager.Instance._SwordData.player_damage_attack;
        laserPattern = GetComponent<Laser_Pattern>();
        DeactivateAllPatterns();
        StartCoroutine(ActivatePatterns());
    }

    void Update()
    {
        player = GameObject.Find("GameManager");
        wing_animator.Play("Idle");
        if (sword.isSwing == false)
        {
            isDamage = false;
        }
        // HP에 따라 패턴 전환
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            // 보스 사망 처리 또는 다음 단계로 진행
            Destroy(gameObject, 3.6f); // 3초 뒤에 보스 삭제
            StartCoroutine(ActivateBloodsAndDeactivate());
        }

        IEnumerator ActivateBloodsAndDeactivate()
        {
            // Blood 객체들을 활성화
            foreach (GameObject blood in bloods)
            {
                blood.SetActive(true);
                yield return new WaitForSeconds(0.2f); // 0.4초 대기
            }

            // 일정 시간이 지난 후에 Blood 객체들 비활성화
            yield return new WaitForSeconds(2.0f); // 2초 대기
            foreach (GameObject blood in bloods)
            {
                blood.SetActive(false);
                yield return new WaitForSeconds(0.2f); // 0.4초 대기
            }
            DataManager.Instance._PlayerData.clear_stage++;
            DataManager.Instance._PlayerData.Boss_Stage = false;
            Portal.SetActive(true);
        }

        pText_hp.text = Mathf.Floor(currentHealth) + " / " + maxHealth.ToString(); // 현재 체력을 표시합니다.
        Handle();
    }
    IEnumerator ActivatePatterns()
    {
        while (true)
        {
            ActivatePattern(laserPattern);
            yield return new WaitForSeconds(3.5f); // 1 번째 패턴 유지 시간

            // 대기 시간
            yield return new WaitForSeconds(2f);

            ActivatePattern(pattern_circle);
            yield return new WaitForSeconds(5f); // 2 번째 패턴 유지 시간

            // 대기 시간
            yield return new WaitForSeconds(2f);

            // 두 번째 패턴 활성화
            ActivatePattern(pattern_Square);
            yield return new WaitForSeconds(10f); // 3 번째 패턴 유지 시간

            // 대기 시간
            yield return new WaitForSeconds(1.5f);
        }
    }

    // 패턴을 활성화하는 메서드
    void ActivatePattern(Circle_Fire patternScript)
    {
        // 모든 패턴 비활성화
        DeactivateAllPatterns();
        // 선택한 패턴 활성화
        patternScript.enabled = true;
    }
    void ActivatePattern(Red_Square patternScript)
    {
        DeactivateAllPatterns();
        patternScript.enabled = true;
    }
    void ActivatePattern(Laser_Pattern patternScript)
    {
        // 모든 패턴 비활성화
        DeactivateAllPatterns();
        // 선택한 패턴 활성화
        patternScript.enabled = true;
    }

    // 모든 패턴을 비활성화하는 메서드
    void DeactivateAllPatterns()
    {
        if (pattern_circle != null)
            pattern_circle.enabled = false;
        if (pattern_Square != null)
            pattern_Square.enabled = false;
        if (laserPattern != null)
            laserPattern.enabled = false;
    }

    // 데미지를 받았을 때 호출되는 메서드
    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
    }

    void Handle() //hp , st 가 닳는 애니메이션
    {
        Boss_HP.value = Mathf.Lerp(Boss_HP.value, (float)currentHealth / (float)maxHealth, Time.deltaTime * 10);
    }


    private void OnTriggerStay2D(Collider2D collision)
    {
        if (isDamage == false)
        {
            if (collision.gameObject.tag == "Attack" && gameObject.tag != "Controlled")
            {
                TakeDamage(damage_playerAttack);
                player.GetComponent<StatController>().Stat("ST", 3);

                isDamage = true;
            }
        }
        if (collision.gameObject.tag == "Sword")
        {
            isDamage = false;
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
            else if (collision.gameObject.tag == "Skill_Dash")
            {
                TakeDamage(DataManager.Instance._Active_Skill.Dash_Damage);
            }
            else if (collision.gameObject.tag == "Skill_Smash")
            {
                TakeDamage(SettingManager.Instance.smash_Damage());
            }
        }
    }
}
