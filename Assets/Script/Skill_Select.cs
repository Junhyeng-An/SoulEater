using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Skill_Select : MonoBehaviour
{
    private List<Skill_Card> skillCards = new List<Skill_Card>();  //전체 스킬카드 리스트
    List<Skill_Card> selectedCards = new List<Skill_Card>(); //선택지에 뜰 스킬카드 리스트

    public List<Text> Card;
    public List<Button> Button;
    bool isShow = false;
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
        skillCards.Add(new Skill_Card("피흡", "적을 처치시 일정량의 HP를 회복한다", 10));
        skillCards.Add(new Skill_Card("칼 강화", "칼의 길이가 증가합니다", 12));
        skillCards.Add(new Skill_Card("화염 속성", "적에게 화상 효과를 줍니다", 15));
        Card_Suffle();
        Card_Show();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void Card_Suffle()
    {
        List<int> selectedIndices = new List<int>(); //중복방지 리스트
        for (int i = 0; i < 3; i++)
        {
            int randomIndex;

            // 중복된 인덱스를 피하기 위한 루프
            do
            {
                randomIndex = Random.Range(0, skillCards.Count);
            } while (selectedIndices.Contains(randomIndex));

            // 선택된 인덱스를 리스트에 추가하고 카드를 선택된 카드 리스트에 추가
            selectedIndices.Add(randomIndex);
            selectedCards.Add(skillCards[randomIndex]);
        }
    }
    void Card_Show()
    {
        for (int i = 0; i < selectedCards.Count; i++)
        {
            Card[i].text = $"카드 {i + 1}: {selectedCards[i].name}\n\n{selectedCards[i].skill_content}\n\n강화 계수: {selectedCards[i].upgrade_count}";
        }
    }

    void Card_Effet(int selectedIndex)
    {
        Skill_Card selectedSkill = selectedCards[selectedIndex];

        // 선택된 스킬의 효과를 플레이어 데이터에 적용
        switch (selectedSkill.name)
        {
            case "피흡":
                DataManager.Instance._PlayerData.Hp_Drain += selectedSkill.upgrade_count;
                break;
            case "칼 강화":
                DataManager.Instance._PlayerData.sword_Reach += selectedSkill.upgrade_count;
                break;
            case "화염 속성":
                DataManager.Instance._PlayerData.fire_dote += selectedSkill.upgrade_count;
                break;
                // 다른 스킬에 대한 경우 추가
        }

        // 업데이트된 플레이어 데이터를 저장
        DataManager.Instance.SaveData();
    }

    public void Button_Click(int button_index) //버튼클릭이벤트 , 눌려진 버튼의 인덱스 값에 따라 Card_Effet(int selectedIndex) 실행하기
    {
        Card_Effet(button_index);
        gameObject.SetActive(false);
        isShow = false;
    }
}
