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
        public string name; //스킬이름
        public string skill_content; //스킬내용
        public float upgrade_count; //얼마만큼 되는지

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
        //카드스킬 (점프높이,이동속도,칼 길이)등을 추가
        skillCards.Add(new Skill_Card("피흡", "적을 처치시 일정량의 HP를 회복한다", 10));
        skillCards.Add(new Skill_Card("강화된 이동", "이동 속도가 증가합니다", 5));
        skillCards.Add(new Skill_Card("높이 뛰기", "점프 높이가 증가합니다", 8));
        skillCards.Add(new Skill_Card("칼 강화", "칼의 길이가 증가합니다", 12));
        skillCards.Add(new Skill_Card("화염 속성", "적에게 화상 효과를 줍니다", 15));
    }

    // Update is called once per frame
    void Update()
    {
        Card_Show();
    }

    void Card_Show()
    {
        // 3장의 스킬 카드를 무작위로 선택합니다.
        List<Skill_Card> selectedCards = new List<Skill_Card>();
        for (int i = 0; i < 3; i++)
        {
            int randomIndex = Random.Range(0, skillCards.Count);
            selectedCards.Add(skillCards[randomIndex]);
        }

        // 선택된 카드를 UI를 통해 표시합니다 ('cardText'라는 이름의 UI Text 컴포넌트를 사용한다고 가정합니다).
        for (int i = 0; i < selectedCards.Count; i++)
        {
            Card[i].text = $"카드 {i + 1}: {selectedCards[i].name}\n{selectedCards[i].skill_content}\n강화 계수: {selectedCards[i].upgrade_count}";
        }

        // 선택된 카드 중 하나의 효과만을 적용합니다.
        //int selectedCardIndex = ChooseCardIndex(); // 사용자의 선택을 얻는 메서드 호출
        //if (selectedCardIndex != -1)
        //{
        //    Card_Effect(selectedCards[selectedCardIndex]);
        //}
    }

    void Card_Effet() //카드를 선택시 카드가 무엇인지 따라 각 카드의 효과를 적용
    {
        
    }
}
