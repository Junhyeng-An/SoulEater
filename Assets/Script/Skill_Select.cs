using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Skill_Select : MonoBehaviour
{
    private List<Skill_Card> skillCards = new List<Skill_Card>();  // 스킬 카드 목록
    List<Skill_Card> selectedCards = new List<Skill_Card>(); // 선택된 스킬 카드 목록

    public List<Text> Card;
    public List<Button> Button;
    public List<Image> CardImages; // UI 이미지 표시용 이미지 목록
    bool isShow = false;

    public Sprite blue;
    public Sprite purple;
    public Sprite yellow;
    
    
    class Skill_Card
    {
        public string name; // 스킬 이름
        public int card_level; // 0 = 레어, 1 = 에픽, 2 = 레전더리
        public string skillContent; // 스킬 설명
        public float upgradeCount; // 능력 추가
        public int skill_level; // 스킬 레벨

        public Skill_Card(string N, int CL, string SC, float UC, int SL)
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
        // 전체 카드 목록 초기화
        skillCards.Clear();

        // 레어 카드 85%
        for (int i = 0; i < 85; i++)
        {
                                           //스킬이름 , 등급기준 , 스킬내용 , 강화량, 강화수치
            skillCards.Add(new Skill_Card("Reduce damage", 0, "방어도를 얻어 데미지를 적게있습니다", 10, 1));
            skillCards.Add(new Skill_Card("Absorption", 0, "체력을 흡수합니다", 1, 1));
            skillCards.Add(new Skill_Card("Hermes", 0, "이동속도가 증가합니다", 1, 1));
            skillCards.Add(new Skill_Card("Dash Distance", 0, "대쉬 거리를 늘립니다", 1, 1)); //clear
            skillCards.Add(new Skill_Card("Max Health", 0, "최대 체력을 증가시킵니다", 20, 1));
            skillCards.Add(new Skill_Card("Discount", 0, "상점에서 할인을 받습니다", 10, 1));
            skillCards.Add(new Skill_Card("Double Jump", 0, "더블점프가 가능해집니다", 10, 1));
            skillCards.Add(new Skill_Card("Miss", 0, "공격을 빚 맞습니다", 10, 1));
            skillCards.Add(new Skill_Card("Posion", 0, "독 데미지를 가합니다", 5, 1));
            skillCards.Add(new Skill_Card("Skill_Damage", 0, "액티브 스킬 데미지가 증가합니다", 10, 1));
        }

        // 에픽 카드 13%
        for (int i = 0; i < 13; i++)
        {
            skillCards.Add(new Skill_Card("Reduce damage", 1, "방어도를 얻어 데미지를 적게있습니다", 20, 2));
            skillCards.Add(new Skill_Card("Absorption", 1, "체력을 흡수합니다", 5, 2));
            skillCards.Add(new Skill_Card("Hermes", 1, "이동속도가 증가합니다", 2, 2));
            skillCards.Add(new Skill_Card("Dash Distance", 1, "대쉬 거리를 늘립니다", 2, 2));
            skillCards.Add(new Skill_Card("Max Health", 1, "최대 체력을 증가시킵니다", 50, 2));
            skillCards.Add(new Skill_Card("Discount", 1, "상점에서 할인을 받습니다", 20, 2));
            skillCards.Add(new Skill_Card("Miss", 1, "공격을 빚 맞습니다", 20, 2));
            skillCards.Add(new Skill_Card("Posion", 1, "독 데미지를 가합니다", 10, 2));
            skillCards.Add(new Skill_Card("Skill_Damage", 0, "액티브 스킬 데미지가 증가합니다", 50, 2));
        }

        // 레전더리 카드 2%
        for (int i = 0; i < 2; i++)
        {
            skillCards.Add(new Skill_Card("Reduce damage", 2, "방어도를 얻어 데미지를 적게있습니다", 50, 3));
            skillCards.Add(new Skill_Card("Absorption", 2, "체력을 흡수합니다", 10, 3));
            skillCards.Add(new Skill_Card("Hermes", 2, "이동속도가 증가합니다", 3, 3));
            skillCards.Add(new Skill_Card("Dash Distance", 2, "대쉬 거리를 늘립니다", 3, 3));
            skillCards.Add(new Skill_Card("Max Health", 2, "최대 체력을 증가시킵니다", 100, 3));
            skillCards.Add(new Skill_Card("Discount", 2, "상점에서 할인을 받습니다", 50, 3));
            skillCards.Add(new Skill_Card("Miss", 2, "공격을 빚 맞습니다", 50, 3));
            skillCards.Add(new Skill_Card("Posion", 2, "독 데미지를 가합니다", 20, 3));
            skillCards.Add(new Skill_Card("Skill_Damage", 0, "액티브 스킬 데미지가 증가합니다", 100, 3));
        }

        // 다양한 스킬 추가 및 수정 가능

        // 전체 카드를 섞음
        skillCards = ShuffleList(skillCards);
    }

    void CardShuffle()
    {
        List<int> selectedIndices = new List<int>(); // 선택된 인덱스 목록
        for (int i = 0; i < 3; i++)
        {
            int randomIndex;

            // 중복된 카드가 선택되지 않도록 랜덤한 인덱스 선택
            do
            {
                randomIndex = Random.Range(0, skillCards.Count);
            } while (selectedIndices.Contains(randomIndex) || IsNameAlreadySelected(skillCards[randomIndex].name));

            // 선택된 인덱스를 목록에 추가하고 해당 카드를 선택된 목록에 추가
            selectedIndices.Add(randomIndex);
            selectedCards.Add(skillCards[randomIndex]);
        }
    }

    // 선택된 카드를 화면에 표시하는 함수
    void CardShow()
    {
        for (int i = 0; i < selectedCards.Count; i++)
        {
            Card[i].text = $"{selectedCards[i].name}\n\n{selectedCards[i].skillContent}\n\n능력 강화량: {selectedCards[i].upgradeCount}";

            // 카드 색상 설정
            SetCardColor(i, selectedCards[i].card_level);
        }
    }

    void CardEffect(int selectedIndex)
    {
        Skill_Card selectedSkill = selectedCards[selectedIndex];

        // 선택된 스킬에 따라 효과 적용
        switch (selectedSkill.name)
        {
            case "Reduce damage":
                switch (selectedSkill.card_level)
                {
                    case 0:
                        DataManager.Instance._Player_Skill.Reduce_damage_Level += 1;
                        break;
                    case 1:
                        DataManager.Instance._Player_Skill.Reduce_damage_Level += 2;
                        break;
                    case 2:
                        DataManager.Instance._Player_Skill.Reduce_damage_Level += 3;
                        break;
                }
                break;
            case "Absorption":
                switch (selectedSkill.card_level)
                {
                    case 0:
                        DataManager.Instance._Player_Skill.HP_Drain_Level += 1;
                        break;
                    case 1:
                        DataManager.Instance._Player_Skill.HP_Drain_Level += 2;
                        break;
                    case 2:
                        DataManager.Instance._Player_Skill.HP_Drain_Level += 3;
                        break;
                }
                break;
            case "Hermes":
                switch (selectedSkill.card_level)
                {
                    case 0:
                        DataManager.Instance._Player_Skill.Skill_Speed_Level += 1;
                        break;
                    case 1:
                        DataManager.Instance._Player_Skill.Skill_Speed_Level += 2;
                        break;
                    case 2:
                        DataManager.Instance._Player_Skill.Skill_Speed_Level += 3;
                        break;
                }
                break;
            case "Dash Distance":
                switch (selectedSkill.card_level)
                {
                    case 0:
                        DataManager.Instance._Player_Skill.Dash_Level += 1;
                        break;
                    case 1:
                        DataManager.Instance._Player_Skill.Dash_Level += 2;
                        break;
                    case 2:
                        DataManager.Instance._Player_Skill.Dash_Level += 3;
                        break;
                }
                break;
            case "Max Health":
                switch (selectedSkill.card_level)
                {
                    case 0:
                        DataManager.Instance._Player_Skill.MaxHP_Level += 1;
                        SelectManager.Instance.isHPupadate = true;
                        Debug.Log(SelectManager.Instance.isHPupadate);
                        break;
                    case 1:
                        DataManager.Instance._Player_Skill.MaxHP_Level += 2;
                        SelectManager.Instance.isHPupadate = true;
                        break;
                    case 2:
                        DataManager.Instance._Player_Skill.MaxHP_Level += 3;
                        SelectManager.Instance.isHPupadate = true;
                        break;
                }
                break;
            case "Discount":
                switch (selectedSkill.card_level)
                {
                    case 0:
                        DataManager.Instance._Player_Skill.Discount_Cost_Level += 1;
                        break;
                    case 1:
                        DataManager.Instance._Player_Skill.Discount_Cost_Level += 2;
                        break;
                    case 2:
                        DataManager.Instance._Player_Skill.Discount_Cost_Level += 3;
                        break;
                }
                break;
            case "Double Jump":
                switch (selectedSkill.card_level)
                {
                    case 0:
                        DataManager.Instance._Player_Skill.Discount_Cost_Level += 1;
                        break;
                }
                break;
            case "Miss":
                switch (selectedSkill.card_level)
                {
                    case 0:
                        DataManager.Instance._Player_Skill.Miss_Level += 1;
                        break;
                    case 1:
                        DataManager.Instance._Player_Skill.Miss_Level += 2;
                        break;
                    case 2:
                        DataManager.Instance._Player_Skill.Miss_Level += 3;
                        break;
                }
                break;
            case "Poison":
                switch (selectedSkill.card_level)
                {
                    case 0:
                        DataManager.Instance._Player_Skill.Miss_Level += 1;
                        break;
                    case 1:
                        DataManager.Instance._Player_Skill.Miss_Level += 2;
                        break;
                    case 2:
                        DataManager.Instance._Player_Skill.Miss_Level += 3;
                        break;
                }
                break;
            case "Skill_Damage":
                switch (selectedSkill.card_level)
                {
                    case 0:
                        DataManager.Instance._Active_Skill.Slash_Damage_Level += 1;
                        DataManager.Instance._Active_Skill.Smash_Damage_Level += 1;
                        DataManager.Instance._Active_Skill.Dash_Damage_Level += 1;
                        break;
                    case 1:
                        DataManager.Instance._Active_Skill.Slash_Damage_Level += 2;
                        DataManager.Instance._Active_Skill.Smash_Damage_Level += 2;
                        DataManager.Instance._Active_Skill.Dash_Damage_Level += 2;
                        break;
                    case 2:
                        DataManager.Instance._Active_Skill.Slash_Damage_Level += 2;
                        DataManager.Instance._Active_Skill.Smash_Damage_Level += 2;
                        DataManager.Instance._Active_Skill.Dash_Damage_Level += 2;
                        break;
                }
                break;
        }
    }

    void skill_organize()
    {
        // 받는 피해 감소
        if (DataManager.Instance._Player_Skill.Reduce_damage_Level == 1)
        {
            DataManager.Instance._Player_Skill.Reduce_damage = 10;
        }
        else if (DataManager.Instance._Player_Skill.Reduce_damage_Level == 2)
        {
            DataManager.Instance._Player_Skill.Reduce_damage = 20;
        }
        else if (DataManager.Instance._Player_Skill.Reduce_damage_Level >= 3)
        {
            DataManager.Instance._Player_Skill.Reduce_damage_Level = 3;
            DataManager.Instance._Player_Skill.Reduce_damage = 50;
        }

        // 흡수
        if (DataManager.Instance._Player_Skill.HP_Drain_Level == 1)
        {
            DataManager.Instance._Player_Skill.HP_Drain = 2;
        }
        else if (DataManager.Instance._Player_Skill.HP_Drain_Level == 2)
        {
            DataManager.Instance._Player_Skill.HP_Drain = 5;
        }
        else if (DataManager.Instance._Player_Skill.HP_Drain_Level >= 3)
        {
            DataManager.Instance._Player_Skill.HP_Drain_Level = 3;
            DataManager.Instance._Player_Skill.HP_Drain = 10;
        }

        // 이동속도 증가
        if (DataManager.Instance._Player_Skill.Skill_Speed_Level == 1)
        {
            DataManager.Instance._Player_Skill.Skill_Speed = 1;
        }
        else if (DataManager.Instance._Player_Skill.Skill_Speed_Level == 2)
        {
            DataManager.Instance._Player_Skill.Skill_Speed = 2;
        }
        else if (DataManager.Instance._Player_Skill.Skill_Speed_Level >= 3)
        {
            DataManager.Instance._Player_Skill.Skill_Speed_Level = 3;
            DataManager.Instance._Player_Skill.Skill_Speed = 3;
        }

        // 대쉬
        if (DataManager.Instance._Player_Skill.Dash_Level == 1)
        {
            DataManager.Instance._Player_Skill.Dash = 5;
        }
        else if (DataManager.Instance._Player_Skill.Dash_Level == 2)
        {
            DataManager.Instance._Player_Skill.Dash = 8;
        }
        else if (DataManager.Instance._Player_Skill.Dash_Level >= 3)
        {
            DataManager.Instance._Player_Skill.Dash_Level = 3;
            DataManager.Instance._Player_Skill.Dash = 10;
        }

        // 최대 체력
        if (DataManager.Instance._Player_Skill.MaxHP_Level == 1)
        {
            DataManager.Instance._Player_Skill.MaxHP = 20;
        }
        else if (DataManager.Instance._Player_Skill.MaxHP_Level == 2)
        {
            DataManager.Instance._Player_Skill.MaxHP = 50;
        }
        else if (DataManager.Instance._Player_Skill.MaxHP_Level >= 3)
        {
            DataManager.Instance._Player_Skill.MaxHP_Level = 3;
            DataManager.Instance._Player_Skill.MaxHP = 100;
        }

        // 할인
        if (DataManager.Instance._Player_Skill.Discount_Cost_Level == 1)
        {
            DataManager.Instance._Player_Skill.Discount_Cost = 10;
        }
        else if (DataManager.Instance._Player_Skill.Discount_Cost_Level == 2)
        {
            DataManager.Instance._Player_Skill.Discount_Cost = 20;
        }
        else if (DataManager.Instance._Player_Skill.Discount_Cost_Level >= 3)
        {
            DataManager.Instance._Player_Skill.Discount_Cost_Level = 3;
            DataManager.Instance._Player_Skill.Discount_Cost = 50;
        }

        // 더블점프
        if (DataManager.Instance._Player_Skill.isDouble_Jump_Level >= 1)
        {
            DataManager.Instance._Player_Skill.isDouble_Jump = true;
        }

        //회피율
        if (DataManager.Instance._Player_Skill.Miss_Level == 1)
        {
            DataManager.Instance._Player_Skill.Miss = 10;
        }
        else if (DataManager.Instance._Player_Skill.Miss_Level == 2)
        {
            DataManager.Instance._Player_Skill.Miss = 20;
        }
        else if (DataManager.Instance._Player_Skill.Miss_Level >= 3)
        {
            DataManager.Instance._Player_Skill.Miss_Level = 3;
            DataManager.Instance._Player_Skill.Miss = 50;
        }

        //독
        if (DataManager.Instance._Player_Skill.Poision_Damage_Level == 1)
        {
            DataManager.Instance._Player_Skill.poison_damage = 5;
        }
        else if (DataManager.Instance._Player_Skill.Poision_Damage_Level == 2)
        {
            DataManager.Instance._Player_Skill.poison_damage = 10;
        }
        else if (DataManager.Instance._Player_Skill.Poision_Damage_Level >= 3)
        {
            DataManager.Instance._Player_Skill.Poision_Damage_Level = 3;
            DataManager.Instance._Player_Skill.poison_damage = 20;
        }

        //스킬 데미지
        if (DataManager.Instance._Active_Skill.Slash_Damage == 1)
        {
            DataManager.Instance._Active_Skill.Slash_Damage = DataManager.Instance._Active_Skill.Slash_Damage_default + (DataManager.Instance._Active_Skill.Slash_Damage_default * 0.1f);
            DataManager.Instance._Active_Skill.Smash_Damage = DataManager.Instance._Active_Skill.Smash_Damage_default + (DataManager.Instance._Active_Skill.Smash_Damage_default * 0.1f);
            DataManager.Instance._Active_Skill.Dash_Damage = DataManager.Instance._Active_Skill.Dash_Damage_default + (DataManager.Instance._Active_Skill.Dash_Damage_default * 0.1f);
        }
        else if (DataManager.Instance._Active_Skill.Slash_Damage_Level == 2)
        {
            DataManager.Instance._Active_Skill.Slash_Damage = DataManager.Instance._Active_Skill.Slash_Damage_default + (DataManager.Instance._Active_Skill.Slash_Damage_default * 0.5f);
            DataManager.Instance._Active_Skill.Smash_Damage = DataManager.Instance._Active_Skill.Smash_Damage_default + (DataManager.Instance._Active_Skill.Smash_Damage_default * 0.5f);
            DataManager.Instance._Active_Skill.Dash_Damage = DataManager.Instance._Active_Skill.Dash_Damage_default + (DataManager.Instance._Active_Skill.Dash_Damage_default * 0.5f);
        }
        else if (DataManager.Instance._Active_Skill.Slash_Damage_Level >= 3)
        {
            DataManager.Instance._Active_Skill.Slash_Damage_Level = 3;
            DataManager.Instance._Active_Skill.Smash_Damage_Level = 3;
            DataManager.Instance._Active_Skill.Dash_Damage_Level = 3;
            DataManager.Instance._Active_Skill.Slash_Damage = DataManager.Instance._Active_Skill.Slash_Damage_default + (DataManager.Instance._Active_Skill.Slash_Damage_default * 1.0f);
            DataManager.Instance._Active_Skill.Smash_Damage = DataManager.Instance._Active_Skill.Smash_Damage_default + (DataManager.Instance._Active_Skill.Smash_Damage_default * 1.0f);
            DataManager.Instance._Active_Skill.Dash_Damage = DataManager.Instance._Active_Skill.Dash_Damage_default + (DataManager.Instance._Active_Skill.Dash_Damage_default * 1.0f);
        }
    }
    public void ButtonClick(int buttonIndex) // 버튼 클릭 이벤트, CardEffect(int selectedIndex) 호출
    {
        CardEffect(buttonIndex);
        skill_organize();
        DataManager.Instance._PlayerData.soul_Count -= SelectManager.Instance.upgrade_soul;
        SelectManager.Instance.Destroy_Prefab();
    }

    void SetCardColor(int index, int cardLevel)
    {
        Image cardImage = CardImages[index];

        switch (cardLevel)
        {
            case 0: // 레어
                cardImage.sprite = blue;
                break;
            case 1: // 에픽
                cardImage.sprite = purple;
                break;
            case 2: // 레전더리
                cardImage.sprite = yellow;
                break;
            default:
                break;
        }
    }

    public void Reroll()
    {
        selectedCards.Clear();
        CardShuffle();
        CardShow();
    }

    List<T> ShuffleList<T>(List<T> inputList)
    {
        List<T> randomList = new List<T>();

        System.Random r = new System.Random();
        int randomIndex = 0;
        while (inputList.Count > 0)
        {
            randomIndex = r.Next(0, inputList.Count);
            randomList.Add(inputList[randomIndex]);
            inputList.RemoveAt(randomIndex);
        }

        return randomList;
    }

    bool IsNameAlreadySelected(string cardName)
    {
        // 이미 선택된 카드들 중에서 중복된 이름이 있는지 확인
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
