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

    public float maxHealth = 1000;  // �ִ� ü��
    private float currentHealth;    // ���� ü��

    float damage_playerAttack;
    private bool isPattern1Active = false;  // ����1 Ȱ��ȭ ����
    private bool isPattern2Active = false;  // ����2 Ȱ��ȭ ����
    private bool isDamage;

    public Circle_Fire pattern_circle;  // ����1 ��ũ��Ʈ
    public Red_Square pattern_Square;  // ����2 ��ũ��Ʈ
    public Laser_Pattern laserPattern;  // ������ ���� ��ũ��Ʈ
    public Animator wing_animator;
    public GameObject[] bloods;
    public GameObject Portal;
    private Sword sword;
    GameObject player;
    GameObject hit_area;
    Damage damage;

    private void Awake()
    {
        currentHealth = maxHealth;  // ���� �� ���� ü���� �ִ� ü������ �ʱ�ȭ
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
        // HP�� ���� ���� ��ȯ
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            // ���� ��� ó�� �Ǵ� ���� �ܰ�� ����
            Destroy(gameObject, 3.6f); // 3�� �ڿ� ���� ����
            StartCoroutine(ActivateBloodsAndDeactivate());
        }

        IEnumerator ActivateBloodsAndDeactivate()
        {
            // Blood ��ü���� Ȱ��ȭ
            foreach (GameObject blood in bloods)
            {
                blood.SetActive(true);
                yield return new WaitForSeconds(0.2f); // 0.4�� ���
            }

            // ���� �ð��� ���� �Ŀ� Blood ��ü�� ��Ȱ��ȭ
            yield return new WaitForSeconds(2.0f); // 2�� ���
            foreach (GameObject blood in bloods)
            {
                blood.SetActive(false);
                yield return new WaitForSeconds(0.2f); // 0.4�� ���
            }
            DataManager.Instance._PlayerData.clear_stage++;
            DataManager.Instance._PlayerData.Boss_Stage = false;
            Portal.SetActive(true);
        }

        pText_hp.text = Mathf.Floor(currentHealth) + " / " + maxHealth.ToString(); // ���� ü���� ǥ���մϴ�.
        Handle();
    }
    IEnumerator ActivatePatterns()
    {
        while (true)
        {
            ActivatePattern(laserPattern);
            yield return new WaitForSeconds(3.5f); // 1 ��° ���� ���� �ð�

            // ��� �ð�
            yield return new WaitForSeconds(2f);

            ActivatePattern(pattern_circle);
            yield return new WaitForSeconds(5f); // 2 ��° ���� ���� �ð�

            // ��� �ð�
            yield return new WaitForSeconds(2f);

            // �� ��° ���� Ȱ��ȭ
            ActivatePattern(pattern_Square);
            yield return new WaitForSeconds(10f); // 3 ��° ���� ���� �ð�

            // ��� �ð�
            yield return new WaitForSeconds(1.5f);
        }
    }

    // ������ Ȱ��ȭ�ϴ� �޼���
    void ActivatePattern(Circle_Fire patternScript)
    {
        // ��� ���� ��Ȱ��ȭ
        DeactivateAllPatterns();
        // ������ ���� Ȱ��ȭ
        patternScript.enabled = true;
    }
    void ActivatePattern(Red_Square patternScript)
    {
        DeactivateAllPatterns();
        patternScript.enabled = true;
    }
    void ActivatePattern(Laser_Pattern patternScript)
    {
        // ��� ���� ��Ȱ��ȭ
        DeactivateAllPatterns();
        // ������ ���� Ȱ��ȭ
        patternScript.enabled = true;
    }

    // ��� ������ ��Ȱ��ȭ�ϴ� �޼���
    void DeactivateAllPatterns()
    {
        if (pattern_circle != null)
            pattern_circle.enabled = false;
        if (pattern_Square != null)
            pattern_Square.enabled = false;
        if (laserPattern != null)
            laserPattern.enabled = false;
    }

    // �������� �޾��� �� ȣ��Ǵ� �޼���
    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
    }

    void Handle() //hp , st �� ��� �ִϸ��̼�
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
