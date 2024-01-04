using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Skill_Select : MonoBehaviour
{
    private List<Skill_Card> skillCards = new List<Skill_Card>();  // List of skill cards
    List<Skill_Card> selectedCards = new List<Skill_Card>(); // List of selected skill cards

    public List<Text> Card;
    public List<Button> Button;
    public List<Image> CardImages; // UI �̹��� ������Ʈ ����Ʈ
    bool isShow = false;

    class Skill_Card
    {
        public string name; // Skill name
        public int card_level; //0 = rare 1 = epic 2 = legendary
        public string skillContent; // Skill description
        public float upgradeCount; // Add ability
        public int skill_level; //skill_level


        public Skill_Card(string N, int CL ,string SC, float UC, int SL)
        {
            name = N;
            card_level = CL;
            skillContent = SC;
            upgradeCount = UC;
            skill_level = SL;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        GenerateSkillCards();
        CardShuffle();
        CardShow();
    }

    // Update is called once per frame
    void Update()
    {

    }
    void GenerateSkillCards()
    {
        // ���ο� ī�带 �����ϱ� ���� ������ ī�� ����Ʈ�� ���
        skillCards.Clear();

        // ���� ī�带 75% Ȯ��
        for (int i = 0; i < 85; i++)
        {
            skillCards.Add(new Skill_Card("ġ��", 0, "��� �� HP�� ȸ���մϴ�", 10, 1));
            skillCards.Add(new Skill_Card("���", 0, "���� �� HP�� ȸ���մϴ�", 10, 1));
            skillCards.Add(new Skill_Card("��", 0, "���� �� ������ �� �������� �����ϴ�", 10, 1));
            skillCards.Add(new Skill_Card("�뽬�Ÿ�", 0, "�뽬�Ÿ��� �����մϴ�", 10, 1));
            skillCards.Add(new Skill_Card("�ִ�ü��", 0, "Į ���� �����մϴ�", 10, 1));
            skillCards.Add(new Skill_Card("����", 0, "���� ��ȭ����� �����մϴ�", 10, 1));
        }

        // ���� ī�带 20% Ȯ��
        for (int i = 0; i < 13; i++)
        {
            skillCards.Add(new Skill_Card("ġ��", 1, "��� �� HP�� ȸ���մϴ�", 20, 2));
            skillCards.Add(new Skill_Card("���", 1, "���� �� HP�� ȸ���մϴ�", 20, 2));
            skillCards.Add(new Skill_Card("��", 1, "���� �� ������ �� �������� �����ϴ�", 20, 2));
            skillCards.Add(new Skill_Card("�뽬�Ÿ�", 1, "�뽬�Ÿ��� �����մϴ�", 20, 2));
            skillCards.Add(new Skill_Card("�ִ�ü��", 1, "Į ���� �����մϴ�", 20, 2));
            skillCards.Add(new Skill_Card("����", 1, "���� ��ȭ����� �����մϴ�", 20, 2));
        }

        // �������� ī�带 2% Ȯ��
        for (int i = 0; i < 2; i++)
        {
            skillCards.Add(new Skill_Card("ġ��", 2, "��� �� HP�� ȸ���մϴ�", 30, 3));
            skillCards.Add(new Skill_Card("���", 2, "���� �� HP�� ȸ���մϴ�", 30, 3));
            skillCards.Add(new Skill_Card("��", 2, "���� �� ������ �� �������� �����ϴ�", 30, 3));
            skillCards.Add(new Skill_Card("�뽬�Ÿ�", 2, "�뽬�Ÿ��� �����մϴ�", 30, 3));
            skillCards.Add(new Skill_Card("�ִ�ü��", 2, "Į ���� �����մϴ�", 30, 3));
            skillCards.Add(new Skill_Card("����", 2, "���� ��ȭ����� �����մϴ�", 30, 3));
        }

        // �ٸ� ��ų�� ���� ��ϵ� �����ϰ� �߰� ����

        // ��� ī�带 ������ �Ŀ� ī�� ����Ʈ�� ����
        skillCards = ShuffleList(skillCards);
    }

    void CardShuffle()
    {
        List<int> selectedIndices = new List<int>(); // ���õ� �ε��� ����Ʈ
        for (int i = 0; i < 3; i++)
        {
            int randomIndex;

            // ������ �ε��� �Ǵ� ������ �̸��� ī�尡 �ٽ� ���õ��� �ʵ��� ����
            do
            {
                randomIndex = Random.Range(0, skillCards.Count);
            } while (selectedIndices.Contains(randomIndex) || IsNameAlreadySelected(skillCards[randomIndex].name));

            // ���õ� �ε����� ����Ʈ�� �߰��ϰ� �ش��ϴ� ī�带 selectedCards ����Ʈ�� �߰�
            selectedIndices.Add(randomIndex);
            selectedCards.Add(skillCards[randomIndex]);
        }
    }

    // ���õ� ī�� ������ ȭ�鿡 ǥ���ϴ� �Լ�
    void CardShow()
    {
        for (int i = 0; i < selectedCards.Count; i++)
        {
            Card[i].text = $"ī�� {i + 1}: {selectedCards[i].name}\n\n{selectedCards[i].skillContent}\n\n���׷��̵� Ƚ��: {selectedCards[i].upgradeCount}";

            // ��޿� ���� �ٸ� ������ ����
            SetCardColor(i, selectedCards[i].card_level);
        }
    }

    void CardEffect(int selectedIndex)
    {
        Skill_Card selectedSkill = selectedCards[selectedIndex];

        // Apply the effects based on the selected skill
        switch (selectedSkill.name)
        {
            case "ġ��":
                DataManager.Instance._Player_Skill.HP_Drain += selectedSkill.upgradeCount;
                DataManager.Instance._Player_Skill.HP_Drain_Level += 1;
                break;
            case "���":
                DataManager.Instance._Player_Skill.Hp_Up += selectedSkill.upgradeCount;
                DataManager.Instance._Player_Skill.HP_Up_Level += 1;
                break;
            case "��":
                DataManager.Instance._Player_Skill.poison_damage += selectedSkill.upgradeCount;
                DataManager.Instance._Player_Skill.Poision_Damage_Level +=1;
                break;
            case "�뽬":
                DataManager.Instance._Player_Skill.Dash += selectedSkill.upgradeCount;
                DataManager.Instance._Player_Skill.Dash += 1;
                break;
            case "�ִ�ü��":
                DataManager.Instance._Player_Skill.MaxHP += selectedSkill.upgradeCount;
                DataManager.Instance._Player_Skill.MaxHP_Level += 1;
                break;
            case "����":
                DataManager.Instance._Player_Skill.Discount += selectedSkill.upgradeCount;
                DataManager.Instance._Player_Skill.Discount_Level += 1;
                break;
        }

        // Save the data after applying the effects
        DataManager.Instance.SaveData();
    }

    public void ButtonClick(int buttonIndex) // Button event, calls CardEffect(int selectedIndex)
    {
        CardEffect(buttonIndex);
        DataManager.Instance._PlayerData.soul_Count -= SelectManager.Instance.upgrade_soul;
        SelectManager.Instance.Destroy_Prefab();
    }

    void SetCardColor(int index, int cardLevel)
    {
        Image cardImage = CardImages[index];

        switch (cardLevel)
        {
            case 0: // ����
                cardImage.color = Color.blue;
                break;
            case 1: // ����
                cardImage.color = Color.magenta;
                break;
            case 2: // ��������
                cardImage.color = Color.yellow;
                break;
            default:
                break;
        }
    }

    List<T> ShuffleList<T>(List<T> inputList)
    {
        List<T> randomList = new List<T>();

        System.Random r = new System.Random();
        int randomIndex = 0;
        while (inputList.Count > 0)
        {
            randomIndex = r.Next(0, inputList.Count); // ����Ʈ���� ������ ��ü ����
            randomList.Add(inputList[randomIndex]); // ���ο� ���� ����Ʈ�� �߰�
            inputList.RemoveAt(randomIndex); // �ߺ��� ���ϱ� ���� ����
        }

        return randomList; // ���ο� ���� ����Ʈ ��ȯ
    }
    bool IsNameAlreadySelected(string cardName)
    {
        // �̹� ���õ� ī��� �߿� ������ �̸��� ī�尡 �ִ��� Ȯ��
        foreach (Skill_Card selectedCard in selectedCards)
        {
            if (selectedCard.name == cardName)
            {
                return true;
            }
        }
        return false;
    }
}
