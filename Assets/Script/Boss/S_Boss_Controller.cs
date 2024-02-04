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

    [HideInInspector]public float maxHealth = 1000;  // �ִ� ü��
    private float currentHealth;    // ���� ü��
    [HideInInspector]public float slime_damage = 50f;
    float damage_playerAttack;
    private bool isDamage;

    public Slime_Jump slime_Jump;  // ����1 ��ũ��Ʈ
    public Slime_Super_Jump slime_super_jump;  // ����2 ��ũ��Ʈ

    private Sword sword;
    GameObject player;

    private void Awake()
    {
        currentHealth = maxHealth;  // ���� �� ���� ü���� �ִ� ü������ �ʱ�ȭ
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
        // HP�� ���� ���� ��ȯ
        if (currentHealth <= 0)
        {
            // ���� ��� ó�� �Ǵ� ���� �ܰ�� ����
            Destroy(gameObject);
            Debug.Log(" Boss DIE ");
        }

        pText_hp.text = Mathf.Floor(currentHealth) + " / " + maxHealth.ToString(); // ���� ü���� ǥ���մϴ�.
        Handle();
    }
    IEnumerator ActivatePatterns()
    {
        while (true)
        {
            ActivatePattern(slime_Jump);
            yield return new WaitForSeconds(50f); // 2 ��° ���� ���� �ð�

            yield return new WaitForSeconds(2f);
            ActivatePattern(slime_super_jump);
            yield return new WaitForSeconds(6f); // 2 ��° ���� ���� �ð�

            yield return new WaitForSeconds(1f);
        }
    }

    // ������ Ȱ��ȭ�ϴ� �޼���
    void ActivatePattern(Slime_Jump patternScript)
    {
        // ��� ���� ��Ȱ��ȭ
        DeactivateAllPatterns();
        // ������ ���� Ȱ��ȭ
        patternScript.enabled = true;
    }
    void ActivatePattern(Slime_Super_Jump patternScript)
    {
        DeactivateAllPatterns();
        patternScript.enabled = true;
    }

    // ��� ������ ��Ȱ��ȭ�ϴ� �޼���
    void DeactivateAllPatterns()
    {
        if (slime_Jump != null)
            slime_Jump.enabled = false;
        if (slime_super_jump != null)
            slime_super_jump.enabled = false;
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
