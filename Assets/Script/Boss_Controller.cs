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

    public Circle_Fire pattern_circle;  // ����1 ��ũ��Ʈ
    public Red_Square pattern_Square;  // ����2 ��ũ��Ʈ

    private void Awake()
    {
        currentHealth = maxHealth;  // ���� �� ���� ü���� �ִ� ü������ �ʱ�ȭ
    }
    void Start()
    {
        //damage_playerAttack = DataManager.Instance._SwordData.player_damage_attack;
        
        DeactivateAllPatterns();
        StartCoroutine(ActivatePatterns());
    }

    void Update()
    {
        if (Input.GetKeyDown("p")) //hp ������
        {
            //currentHealth -= damage_playerAttack;
            currentHealth -= 10;
        }
        // HP�� ���� ���� ��ȯ
        if (currentHealth <= 0)
        {
            // ���� ��� ó�� �Ǵ� ���� �ܰ�� ����
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
            // ù ��° ���� Ȱ��ȭ
            ActivatePattern(pattern_circle);
            yield return new WaitForSeconds(5f); // ù ��° ���� ���� �ð�

            // ��� �ð�
            yield return new WaitForSeconds(1f);

            // �� ��° ���� Ȱ��ȭ
            ActivatePattern(pattern_Square);
            yield return new WaitForSeconds(10f); // �� ��° ���� ���� �ð�
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

    // ��� ������ ��Ȱ��ȭ�ϴ� �޼���
    void DeactivateAllPatterns()
    {
        if (pattern_circle != null)
            pattern_circle.enabled = false;

        if (pattern_Square != null)
            pattern_Square.enabled = false;
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


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Sword")
        {
            TakeDamage(damage_playerAttack);
        }
    }
}
