using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class Boss_Controller : MonoBehaviour
{
    [SerializeField]
    Slider Boss_HP;
    [SerializeField]
    TextMeshProUGUI pText_hp;

    public float maxHealth = 100;  // 최대 체력
    private float currentHealth;    // 현재 체력

    float damage_playerAttack;
    private bool isPattern1Active = false;  // 패턴1 활성화 여부
    private bool isPattern2Active = false;  // 패턴2 활성화 여부

    public Circle_Fire pattern_circle;  // 패턴1 스크립트
    public Red_Square pattern_Square;  // 패턴2 스크립트

    private void Awake()
    {
        currentHealth = maxHealth;  // 시작 시 현재 체력을 최대 체력으로 초기화
    }
    void Start()
    {
        //damage_playerAttack = DataManager.Instance._SwordData.player_damage_attack;
        
        DeactivateAllPatterns();
        StartCoroutine(ActivatePatterns());
    }

    void Update()
    {
        if (Input.GetKeyDown("p")) //hp 데미지
        {
            //currentHealth -= damage_playerAttack;
            currentHealth -= 10;
        }
        // HP에 따라 패턴 전환
        if (currentHealth <= 0)
        {
            // 보스 사망 처리 또는 다음 단계로 진행
            Destroy(gameObject);
            Debug.Log(" Boss DIE ");
        }
        //else if (currentHealth <= 80 && !isPattern1Active)
        //{
        //    isPattern2Active = false;
        //    isPattern1Active = true;
        //    ActivatePattern(patternScript1);
        //}
        //else if (currentHealth < maxHealth / 2 && !isPattern2Active)
        //{
        //    // HP가 최대 체력의 절반 미만이면서 현재 패턴이 활성화되어 있지 않은 경우
        //    // 패턴 전환 및 해당 패턴 활성화
        //    isPattern2Active = true;
        //    isPattern1Active = false;
        //    ActivatePattern(patternScript2);
        //}

        pText_hp.text = Mathf.Floor(currentHealth) + " / " + maxHealth.ToString(); // 현재 체력을 표시합니다.
        Handle();
    }
    IEnumerator ActivatePatterns()
    {
        while (true)
        {
            // 첫 번째 패턴 활성화
            ActivatePattern(pattern_circle);
            yield return new WaitForSeconds(5f); // 첫 번째 패턴 유지 시간

            // 대기 시간
            yield return new WaitForSeconds(1f);

            // 두 번째 패턴 활성화
            ActivatePattern(pattern_Square);
            yield return new WaitForSeconds(10f); // 두 번째 패턴 유지 시간
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

    // 모든 패턴을 비활성화하는 메서드
    void DeactivateAllPatterns()
    {
        if (pattern_circle != null)
            pattern_circle.enabled = false;

        if (pattern_Square != null)
            pattern_Square.enabled = false;
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


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Sword")
        {
            TakeDamage(damage_playerAttack);
        }
    }
}
