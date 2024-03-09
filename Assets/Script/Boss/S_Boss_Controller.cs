using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class S_Boss_Controller : MonoBehaviour
{
    [SerializeField]
    Slider Boss_HP;
    [SerializeField]
    TextMeshProUGUI pText_hp;

    [HideInInspector]private float maxHealth = 1000;  // 최대 체력
    private float currentHealth;    // 현재 체력
    public float slime_damage = 50f;
    float damage_playerAttack;
    private bool isDamage;

    public Slime_Jump slime_Jump;  // 패턴1 스크립트
    public Slime_Super_Jump slime_super_jump;  // 패턴2 스크립트

    private Sword sword;
    GameObject player;
    public GameObject Portal;

    private void Awake()
    {
        currentHealth = maxHealth;  // 시작 시 현재 체력을 최대 체력으로 초기화
        sword = GameObject.Find("Sword").GetComponent<Sword>();
        slime_Jump = GetComponent<Slime_Jump>();
    }
    void Start()
    {
        damage_playerAttack = DataManager.Instance._SwordData.player_damage_attack;
        DeactivateAllPatterns();
        StartCoroutine(ActivatePatterns());
    }

    void Update()
    {
        player = GameObject.Find("GameManager");
        if (sword.isSwing == false)
        {
            isDamage = false;
        }
        // HP에 따라 패턴 전환
        if (currentHealth <= 0)
        {
            // 보스 사망 처리 또는 다음 단계로 진행
            DataManager.Instance._PlayerData.clear_stage = 1;
            DataManager.Instance._PlayerData.Boss_Stage = false;
            Portal.SetActive(true);
            Destroy(gameObject);
            
            Debug.Log(" Boss DIE ");
        }

        pText_hp.text = Mathf.Floor(currentHealth) + " / " + maxHealth.ToString(); // 현재 체력을 표시합니다.
        Handle();
    }
    IEnumerator ActivatePatterns()
    {
        while (true)
        {
            ActivatePattern(slime_Jump);
            yield return new WaitForSeconds(6.2f); // 2 번째 패턴 유지 시간

            yield return new WaitForSeconds(2f);
            ActivatePattern(slime_super_jump);
            yield return new WaitForSeconds(6f); // 2 번째 패턴 유지 시간

            yield return new WaitForSeconds(1f);
        }
    }

    // 패턴을 활성화하는 메서드
    void ActivatePattern(Slime_Jump patternScript)
    {
        // 모든 패턴 비활성화
        DeactivateAllPatterns();
        // 선택한 패턴 활성화
        patternScript.enabled = true;
    }
    void ActivatePattern(Slime_Super_Jump patternScript)
    {
        DeactivateAllPatterns();
        patternScript.enabled = true;
    }

    // 모든 패턴을 비활성화하는 메서드
    void DeactivateAllPatterns()
    {
        if (slime_Jump != null)
            slime_Jump.enabled = false;
        if (slime_super_jump != null)
            slime_super_jump.enabled = false;
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
                Debug.Log("damage");
            }
        }
    }
}
