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

    public float maxHealth = 100;  // �ִ� ü��
    private float currentHealth;    // ���� ü��

    float damage_playerAttack;
    
    private bool isPattern1Active = false;  // ����1 Ȱ��ȭ ����
    private bool isPattern2Active = false;  // ����2 Ȱ��ȭ ����
    private bool isDamage;
    private bool isAlive = true;
    private float interval = 0.4f; // �� ������Ʈ�� Ȱ��ȭ�Ǵ� ����
    private int currentIndex = 0; // ���� Ȱ��ȭ�� ������Ʈ�� �ε���
    private float timer = 0f; // Ÿ�̸� ����
    private float BloodTime = 0f; // Ÿ�̸� ����

    public Circle_Fire pattern_circle;  // ����1 ��ũ��Ʈ
    public Red_Square pattern_Square;  // ����2 ��ũ��Ʈ
    public Laser_Pattern laserPattern;  // ������ ���� ��ũ��Ʈ
    public Animator wing_animator;
    public GameObject[] bloods;
    public GameObject Portal;
    private Sword sword;
    GameObject player;

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
        if (Input.GetKeyDown("p")) //hp ������
        {
            currentHealth -= 500;
        }
        if (currentHealth <= 0 && isAlive == true)
        {
            // ���� ��� ó�� �Ǵ� ���� �ܰ�� ����
            DeactivateAllPatterns();
            Destroy(gameObject, 3.5f); // 3�� �ڿ� ���� ����
            Debug.Log(" Boss DIE ");

            // Blood ��ü���� ���� �ð� �������� Ȱ��ȭ�ϰ�, �� �Ŀ� ��Ȱ��ȭ�մϴ�.
            StartCoroutine(ActivateBloodsAndDeactivate());
        }

        IEnumerator ActivateBloodsAndDeactivate()
        {
            isAlive = false;
            foreach (GameObject blood in bloods)
            {
                blood.SetActive(true); // blood ��ü Ȱ��ȭ
                yield return new WaitForSeconds(0.2f); // 0.4�� ���
            }

            // ���� �ð��� ���� �Ŀ� Blood ��ü�� ��Ȱ��ȭ
            yield return new WaitForSeconds(2.2f); // 1�� ���
            foreach (GameObject blood in bloods)
            {
                blood.SetActive(false); // blood ��ü ��Ȱ��ȭ
                yield return new WaitForSeconds(0.2f); // 0.4�� ���
            }
            Portal.SetActive(true);
        }


        //else if (currentHealth <= 80 && !isPattern1Active)
        //{
        //    isPattern2Active = false;
        //    isPattern1Active = true;
        //    ActivatePattern(patternScript1);
        //}
        //else if (currentHealth < maxHealth / 2 && !isPattern2Active)
        //{
        //    // HP�� �ִ� ü���� ���� �̸��̸鼭 ���� ������ Ȱ��ȭ�Ǿ� ���� ���� ���
        //    // ���� ��ȯ �� �ش� ���� Ȱ��ȭ
        //    isPattern2Active = true;
        //    isPattern1Active = false;
        //    ActivatePattern(patternScript2);
        //}

        pText_hp.text = Mathf.Floor(currentHealth) + " / " + maxHealth.ToString(); // ���� ü���� ǥ���մϴ�.
        Handle();
    }
    IEnumerator ActivatePatterns()
    {
        while (true)
        {
            ActivatePattern(laserPattern);
            yield return new WaitForSeconds(5f); // 1 ��° ���� ���� �ð�

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
    }
}
