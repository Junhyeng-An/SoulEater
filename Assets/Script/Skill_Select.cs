using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Skill_Select : MonoBehaviour
{
    private List<Skill_Card> skillCards = new List<Skill_Card>();
    public List<TextMeshProUGUI> Card;
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
        //ī�彺ų (��������,�̵��ӵ�,Į ����)���� �߰�
        skillCards.Add(new Skill_Card("����", "���� óġ�� �������� HP�� ȸ���Ѵ�", 10));
        skillCards.Add(new Skill_Card("��ȭ�� �̵�", "�̵� �ӵ��� �����մϴ�", 5));
        skillCards.Add(new Skill_Card("���� �ٱ�", "���� ���̰� �����մϴ�", 8));
        skillCards.Add(new Skill_Card("Į ��ȭ", "Į�� ���̰� �����մϴ�", 12));
        skillCards.Add(new Skill_Card("ȭ�� �Ӽ�", "������ ȭ�� ȿ���� �ݴϴ�", 15));
    }

    // Update is called once per frame
    void Update()
    {
        Card_Show();
    }

    void Card_Show()
    {
        // 3���� ��ų ī�带 �������� �����մϴ�.
        List<Skill_Card> selectedCards = new List<Skill_Card>();
        for (int i = 0; i < 3; i++)
        {
            int randomIndex = Random.Range(0, skillCards.Count);
            selectedCards.Add(skillCards[randomIndex]);
        }

        // ���õ� ī�带 UI�� ���� ǥ���մϴ� ('cardText'��� �̸��� UI Text ������Ʈ�� ����Ѵٰ� �����մϴ�).
        for (int i = 0; i < selectedCards.Count; i++)
        {
            Card[i].text = $"ī�� {i + 1}: {selectedCards[i].name}\n{selectedCards[i].skill_content}\n��ȭ ���: {selectedCards[i].upgrade_count}";
        }

        // ���õ� ī�� �� �ϳ��� ȿ������ �����մϴ�.
        //int selectedCardIndex = ChooseCardIndex(); // ������� ������ ��� �޼��� ȣ��
        //if (selectedCardIndex != -1)
        //{
        //    Card_Effect(selectedCards[selectedCardIndex]);
        //}
    }

    void Card_Effet() //ī�带 ���ý� ī�尡 �������� ���� �� ī���� ȿ���� ����
    {
        
    }
}
