using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Skill_Select : MonoBehaviour
{
    private List<Skill_Card> skillCards = new List<Skill_Card>();  //��ü ��ųī�� ����Ʈ
    List<Skill_Card> selectedCards = new List<Skill_Card>(); //�������� �� ��ųī�� ����Ʈ

    public List<Text> Card;
    public List<Button> Button;
    bool isShow = false;
    class Skill_Card
    {
        public string name; //��ų�̸�
        public string skill_content; //��ų����
        public float upgrade_count; //�󸶸�ŭ �Ǵ���

        public Skill_Card(string N, string SC, float UC)
        {
            name = N;
            skill_content = SC;
            upgrade_count = UC;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        skillCards.Add(new Skill_Card("����", "���� óġ�� �������� HP�� ȸ���Ѵ�", 10));
        skillCards.Add(new Skill_Card("Į ��ȭ", "Į�� ���̰� �����մϴ�", 12));
        skillCards.Add(new Skill_Card("ȭ�� �Ӽ�", "������ ȭ�� ȿ���� �ݴϴ�", 15));
        Card_Suffle();
        Card_Show();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void Card_Suffle()
    {
        List<int> selectedIndices = new List<int>(); //�ߺ����� ����Ʈ
        for (int i = 0; i < 3; i++)
        {
            int randomIndex;

            // �ߺ��� �ε����� ���ϱ� ���� ����
            do
            {
                randomIndex = Random.Range(0, skillCards.Count);
            } while (selectedIndices.Contains(randomIndex));

            // ���õ� �ε����� ����Ʈ�� �߰��ϰ� ī�带 ���õ� ī�� ����Ʈ�� �߰�
            selectedIndices.Add(randomIndex);
            selectedCards.Add(skillCards[randomIndex]);
        }
    }
    void Card_Show()
    {
        for (int i = 0; i < selectedCards.Count; i++)
        {
            Card[i].text = $"ī�� {i + 1}: {selectedCards[i].name}\n\n{selectedCards[i].skill_content}\n\n��ȭ ���: {selectedCards[i].upgrade_count}";
        }
    }

    void Card_Effet(int selectedIndex)
    {
        Skill_Card selectedSkill = selectedCards[selectedIndex];

        // ���õ� ��ų�� ȿ���� �÷��̾� �����Ϳ� ����
        switch (selectedSkill.name)
        {
            case "����":
                DataManager.Instance._Player_Skill.HP_Drain += selectedSkill.upgrade_count;
                break;
            case "Į ��ȭ":
                DataManager.Instance._Player_Skill.sword_Reach += selectedSkill.upgrade_count;
                break;
            case "ȭ�� �Ӽ�":
                DataManager.Instance._Player_Skill.fire_dote += selectedSkill.upgrade_count;
                break;
                // �ٸ� ��ų�� ���� ��� �߰�
        }

        // ������Ʈ�� �÷��̾� �����͸� ����
        DataManager.Instance.SaveData();
    }

    public void Button_Click(int button_index) //��ưŬ���̺�Ʈ , ������ ��ư�� �ε��� ���� ���� Card_Effet(int selectedIndex) �����ϱ�
    {
        Card_Effet(button_index);
        gameObject.SetActive(false);
        isShow = false;
    }
}
