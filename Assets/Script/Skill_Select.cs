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
    public List<Image> CardImages; // UI 이미지 컴포넌트 리스트
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
        // 새로운 카드를 생성하기 전에 기존의 카드 리스트를 비움
        skillCards.Clear();

        // 레어 카드를 75% 확률
        for (int i = 0; i < 85; i++)
        {
            skillCards.Add(new Skill_Card("치유", 0, "사용 시 HP를 회복합니다", 10, 1));
            skillCards.Add(new Skill_Card("흡수", 0, "공격 시 HP를 회복합니다", 10, 1));
            skillCards.Add(new Skill_Card("독", 0, "공격 시 적에게 독 데미지를 입힙니다", 10, 1));
            skillCards.Add(new Skill_Card("대쉬거리", 0, "대쉬거리가 증가합니다", 10, 1));
            skillCards.Add(new Skill_Card("최대체력", 0, "칼 길이 증가합니다", 10, 1));
            skillCards.Add(new Skill_Card("할인", 0, "무기 강화비용이 감소합니다", 10, 1));
        }

        // 에픽 카드를 20% 확률
        for (int i = 0; i < 13; i++)
        {
            skillCards.Add(new Skill_Card("치유", 1, "사용 시 HP를 회복합니다", 20, 2));
            skillCards.Add(new Skill_Card("흡수", 1, "공격 시 HP를 회복합니다", 20, 2));
            skillCards.Add(new Skill_Card("독", 1, "공격 시 적에게 독 데미지를 입힙니다", 20, 2));
            skillCards.Add(new Skill_Card("대쉬거리", 1, "대쉬거리가 증가합니다", 20, 2));
            skillCards.Add(new Skill_Card("최대체력", 1, "칼 길이 증가합니다", 20, 2));
            skillCards.Add(new Skill_Card("할인", 1, "무기 강화비용이 감소합니다", 20, 2));
        }

        // 레전더리 카드를 2% 확률
        for (int i = 0; i < 2; i++)
        {
            skillCards.Add(new Skill_Card("치유", 2, "사용 시 HP를 회복합니다", 30, 3));
            skillCards.Add(new Skill_Card("흡수", 2, "공격 시 HP를 회복합니다", 30, 3));
            skillCards.Add(new Skill_Card("독", 2, "공격 시 적에게 독 데미지를 입힙니다", 30, 3));
            skillCards.Add(new Skill_Card("대쉬거리", 2, "대쉬거리가 증가합니다", 30, 3));
            skillCards.Add(new Skill_Card("최대체력", 2, "칼 길이 증가합니다", 30, 3));
            skillCards.Add(new Skill_Card("할인", 2, "무기 강화비용이 감소합니다", 30, 3));
        }

        // 다른 스킬에 대한 블록도 유사하게 추가 가능

        // 모든 카드를 생성한 후에 카드 리스트를 섞음
        skillCards = ShuffleList(skillCards);
    }

    void CardShuffle()
    {
        List<int> selectedIndices = new List<int>(); // 선택된 인덱스 리스트
        for (int i = 0; i < 3; i++)
        {
            int randomIndex;

            // 동일한 인덱스 또는 동일한 이름의 카드가 다시 선택되지 않도록 보장
            do
            {
                randomIndex = Random.Range(0, skillCards.Count);
            } while (selectedIndices.Contains(randomIndex) || IsNameAlreadySelected(skillCards[randomIndex].name));

            // 선택된 인덱스를 리스트에 추가하고 해당하는 카드를 selectedCards 리스트에 추가
            selectedIndices.Add(randomIndex);
            selectedCards.Add(skillCards[randomIndex]);
        }
    }

    // 선택된 카드 정보를 화면에 표시하는 함수
    void CardShow()
    {
        for (int i = 0; i < selectedCards.Count; i++)
        {
            Card[i].text = $"카드 {i + 1}: {selectedCards[i].name}\n\n{selectedCards[i].skillContent}\n\n업그레이드 횟수: {selectedCards[i].upgradeCount}";

            // 등급에 따라 다른 색상을 설정
            SetCardColor(i, selectedCards[i].card_level);
        }
    }

    void CardEffect(int selectedIndex)
    {
        Skill_Card selectedSkill = selectedCards[selectedIndex];

        // Apply the effects based on the selected skill
        switch (selectedSkill.name)
        {
            case "치유":
                DataManager.Instance._Player_Skill.HP_Drain += selectedSkill.upgradeCount;
                DataManager.Instance._Player_Skill.HP_Drain_Level += 1;
                break;
            case "흡수":
                DataManager.Instance._Player_Skill.Hp_Up += selectedSkill.upgradeCount;
                DataManager.Instance._Player_Skill.HP_Up_Level += 1;
                break;
            case "독":
                DataManager.Instance._Player_Skill.poison_damage += selectedSkill.upgradeCount;
                DataManager.Instance._Player_Skill.Poision_Damage_Level +=1;
                break;
            case "대쉬":
                DataManager.Instance._Player_Skill.Dash += selectedSkill.upgradeCount;
                DataManager.Instance._Player_Skill.Dash += 1;
                break;
            case "최대체력":
                DataManager.Instance._Player_Skill.MaxHP += selectedSkill.upgradeCount;
                DataManager.Instance._Player_Skill.MaxHP_Level += 1;
                break;
            case "할인":
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
            case 0: // 레어
                cardImage.color = Color.blue;
                break;
            case 1: // 에픽
                cardImage.color = Color.magenta;
                break;
            case 2: // 레전더리
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
            randomIndex = r.Next(0, inputList.Count); // 리스트에서 랜덤한 객체 선택
            randomList.Add(inputList[randomIndex]); // 새로운 랜덤 리스트에 추가
            inputList.RemoveAt(randomIndex); // 중복을 피하기 위해 제거
        }

        return randomList; // 새로운 랜덤 리스트 반환
    }
    bool IsNameAlreadySelected(string cardName)
    {
        // 이미 선택된 카드들 중에 동일한 이름의 카드가 있는지 확인
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
