using System.Collections;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
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

    #region Localization

    private string Reduce_Damage;
    private string Absortion;
    private string Hermes;
    private string Dash_Distance;
    private string Discout;
    private string Double_Jump;
    private string Miss;
    private string Poision;
    private string Skill_Damage;
    private string Get_Coin;
    

    #endregion
    
    
    
    
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
        Language();
        GenerateSkillCards();
        CardShuffle();
        CardShow();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void Language()
    {
        if (DataManager.Instance._Sound_Volume.Language == 0)
        {
            Reduce_Damage = "You gain a shield to reduce incoming damage.";
            Absortion = "You absorb health.";
            Hermes = "Movement speed increased.";
            Dash_Distance = "Increasing dash distance.";
            Discout = "Receiving a discount at the blacksmith.";
            Double_Jump = "You can now double jump.";
            Miss = "Attacks occasionally miss.";
            Poision = "Inflicts poison damage.";
            Skill_Damage = "Active skill damage increased.";
            Get_Coin = "Get Coin";

        }


        else if (DataManager.Instance._Sound_Volume.Language == 1)
        {
            Reduce_Damage = "방어도를 얻어 데미지를 적게있습니다.";
            Absortion = "체력을 흡수합니다.";
            Hermes = "이동속도가 증가합니다.";
            Dash_Distance = "대쉬 거리를 늘립니다.";
            Discout = "대장간에서 할인을 받습니다";
            Double_Jump = "더블점프가 가능해집니다";
            Miss = "일정 확률 공격이 빚나갑니다";
            Poision = "독 데미지를 가합니다";
            Skill_Damage = "액티브 스킬 데미지가 증가합니다";
            Get_Coin = "코인을 얻습니다";
            
        }
        
        
        
        
        
    }
    
    
    void GenerateSkillCards()
    {
        // 전체 카드 목록 초기화
        skillCards.Clear();

        // 레어 카드 85%
        for (int i = 0; i < 85; i++)
        {
            //스킬이름 , 등급기준 , 스킬내용 , 강화량, 강화수치
            if (DataManager.Instance._Player_Skill.Reduce_damage_Level<6)
            {
                skillCards.Add(new Skill_Card("Reduce damage", 0, Reduce_Damage, 1, 1));
            }
            if (DataManager.Instance._Player_Skill.HP_Drain_Level<6)
            {
                skillCards.Add(new Skill_Card("Absorption", 0, Absortion, 1, 1));
            }
            if (DataManager.Instance._Player_Skill.Skill_Speed_Level<6)
            {
                skillCards.Add(new Skill_Card("Hermes", 0, Hermes, 1, 1));
            }
            if (DataManager.Instance._Player_Skill.Dash_Level<6)
            {
                skillCards.Add(new Skill_Card("Dash Distance", 0, Dash_Distance, 1, 1));
            }
            if (DataManager.Instance._Player_Skill.Discount_Cost_Level<6)
            {
                skillCards.Add(new Skill_Card("Discount", 0, Discout, 1, 1));
            }
            if (DataManager.Instance._Player_Skill.isDouble_Jump_Level<1)
            {
                skillCards.Add(new Skill_Card("Double_Jump", 0, Double_Jump, 1, 1));
            }
            if (DataManager.Instance._Player_Skill.Miss_Level<6)
            {
                skillCards.Add(new Skill_Card("Miss", 0, Miss, 1, 1));
            }
            if (DataManager.Instance._Player_Skill.Poision_Damage_Level<6)
            {
                skillCards.Add(new Skill_Card("Poison", 0, Poision, 1, 1));
            }
            if (DataManager.Instance._Active_Skill.Skill_Level<6)
            {
                skillCards.Add(new Skill_Card("Skill_Damage", 0, Skill_Damage, 1, 1));
            }
            //Get Coin
            {
                skillCards.Add(new Skill_Card("Get_Coin", 0, Get_Coin, 1, 1));
            }
        }

        // 에픽 카드 13%
        for (int i = 0; i < 13; i++)
        {
            if (DataManager.Instance._Player_Skill.Reduce_damage_Level < 5)
            {
                skillCards.Add(new Skill_Card("Reduce damage", 1, Reduce_Damage, 2, 2));
            }
            if (DataManager.Instance._Player_Skill.HP_Drain_Level < 5)
            {
                skillCards.Add(new Skill_Card("Absorption", 1, Absortion, 2, 2));
            }
            if (DataManager.Instance._Player_Skill.Skill_Speed_Level < 5)
            {
                skillCards.Add(new Skill_Card("Hermes", 1, Hermes, 2, 2));
            }
            if (DataManager.Instance._Player_Skill.Dash_Level < 5)
            {
                skillCards.Add(new Skill_Card("Dash Distance", 1, Dash_Distance, 2, 2));
            }
            if (DataManager.Instance._Player_Skill.Discount_Cost_Level < 5)
            {
                skillCards.Add(new Skill_Card("Discount", 1, Discout, 2, 2));
            }
            if (DataManager.Instance._Player_Skill.Miss_Level < 5)
            {
                skillCards.Add(new Skill_Card("Miss", 1, Miss, 2, 2));
            }
            if (DataManager.Instance._Player_Skill.Poision_Damage_Level < 5)
            {
                skillCards.Add(new Skill_Card("Poison", 1, Poision, 2, 2));
            }
            if (DataManager.Instance._Active_Skill.Skill_Level < 5)
            {
                skillCards.Add(new Skill_Card("Skill_Damage", 1, Skill_Damage, 2, 2));
            }
                //Get Coin
            {
                skillCards.Add(new Skill_Card("Get_Coin", 1, Get_Coin, 2, 2));
            }
        }

        

        // 레전더리 카드 2%
        for (int i = 0; i < 2; i++)
        {
            if (DataManager.Instance._Player_Skill.Reduce_damage_Level < 4)
            {
                skillCards.Add(new Skill_Card("Reduce damage", 2, Reduce_Damage, 3, 3));
            }
            if (DataManager.Instance._Player_Skill.HP_Drain_Level < 4)
            {
                skillCards.Add(new Skill_Card("Absorption", 2, Absortion, 3, 3));
            }
            if (DataManager.Instance._Player_Skill.Skill_Speed_Level < 4)
            {
                skillCards.Add(new Skill_Card("Hermes", 2, Hermes, 3, 3));
            }
            if (DataManager.Instance._Player_Skill.Dash_Level < 4)
            {
                skillCards.Add(new Skill_Card("Dash Distance", 2, Dash_Distance, 3, 3));
            }
            if (DataManager.Instance._Player_Skill.Discount_Cost_Level < 4)
            {
                skillCards.Add(new Skill_Card("Discount", 2, Discout, 3, 3));
            }
            if (DataManager.Instance._Player_Skill.Miss_Level < 4)
            {
                skillCards.Add(new Skill_Card("Miss", 2, Miss, 3, 3));
            }
            if (DataManager.Instance._Player_Skill.Poision_Damage_Level < 4)
            {
                skillCards.Add(new Skill_Card("Poison", 2, Poision, 3, 3));
            }
            if (DataManager.Instance._Active_Skill.Skill_Level < 4 )
            {
                skillCards.Add(new Skill_Card("Skill_Damage", 2, Skill_Damage, 3, 3));
            }
            //Get Coin
            {
                skillCards.Add(new Skill_Card("Get_Coin", 2, Get_Coin, 3, 3));
            }

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
            if(DataManager.Instance._Sound_Volume.Language ==0)
                Card[i].text = $"{selectedCards[i].name}\n\n{selectedCards[i].skillContent}\n\nEnhancement level of abilities: {selectedCards[i].upgradeCount}";
            else if(DataManager.Instance._Sound_Volume.Language == 1)
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
                case "Get_Coin":
                switch (selectedSkill.card_level)
                {
                    case 0:
                        DataManager.Instance._PlayerData.coin += 10;
                        break;
                    case 1:
                        DataManager.Instance._PlayerData.coin += 15;
                        break;
                    case 2:
                        DataManager.Instance._PlayerData.coin += 20;
                        break;
                }
                break;


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
            case "Double_Jump":
                switch (selectedSkill.card_level)
                {
                    case 0:
                        DataManager.Instance._Player_Skill.isDouble_Jump_Level += 1;
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
                        DataManager.Instance._Player_Skill.Poision_Damage_Level += 1;
                        break;
                    case 1:
                        DataManager.Instance._Player_Skill.Poision_Damage_Level += 2;
                        break;
                    case 2:
                        DataManager.Instance._Player_Skill.Poision_Damage_Level += 3;
                        break;
                }
                break;
            case "Skill_Damage":
                switch (selectedSkill.card_level)
                {
                    case 0:
                        DataManager.Instance._Active_Skill.Skill_Level += 1;
                        break;
                    case 1:
                        DataManager.Instance._Active_Skill.Skill_Level += 2;
                        break;
                    case 2:
                        DataManager.Instance._Active_Skill.Skill_Level += 3;
                        break;
                }
                break;
        }
    }

    void skill_organize()
    {

        #region Reduce_Damage
        
        if (DataManager.Instance._Player_Skill.Reduce_damage_Level >= 6)
            DataManager.Instance._Player_Skill.Reduce_damage_Level = 6;
        
        switch(DataManager.Instance._Player_Skill.Reduce_damage_Level)
        {
            case 1:
                DataManager.Instance._Player_Skill.Reduce_damage = 5;
                break;
            case 2:
                DataManager.Instance._Player_Skill.Reduce_damage = 8;
                break;
            case 3:
                DataManager.Instance._Player_Skill.Reduce_damage = 11; 
                break;
            case 4:
                DataManager.Instance._Player_Skill.Reduce_damage = 14;
                break;
            case 5:
                DataManager.Instance._Player_Skill.Reduce_damage = 17;
                break;
            case 6:
                DataManager.Instance._Player_Skill.Reduce_damage = 20;
                break;
            
        }
    
        
        

        #endregion
        
        #region HP_Drain
        if (DataManager.Instance._Player_Skill.HP_Drain_Level >= 6)
            DataManager.Instance._Player_Skill.HP_Drain_Level = 6;
        
        switch(DataManager.Instance._Player_Skill.HP_Drain_Level)
        {
            case 1:
                DataManager.Instance._Player_Skill.HP_Drain = 0.5f;
                break;
            case 2:
                DataManager.Instance._Player_Skill.HP_Drain = 1.0f;
                break;
            case 3:
                DataManager.Instance._Player_Skill.HP_Drain = 1.5f;
                break;
            case 4:
                DataManager.Instance._Player_Skill.HP_Drain = 2.0f;
                break;
            case 5:
                DataManager.Instance._Player_Skill.HP_Drain = 2.5f;
                break;
            case 6:
                DataManager.Instance._Player_Skill.HP_Drain = 3.0f;
                break;
            
        }

        

        #endregion

        #region Speed_Level
        if (DataManager.Instance._Player_Skill.Skill_Speed_Level >= 6)
            DataManager.Instance._Player_Skill.Skill_Speed_Level = 6;
        
        
        switch(DataManager.Instance._Player_Skill.Skill_Speed_Level)
        {
            case 1:
                DataManager.Instance._Player_Skill.Skill_Speed = 0.5f;
                break;
            case 2:
                DataManager.Instance._Player_Skill.Skill_Speed = 1.0f;
                break;
            case 3:
                DataManager.Instance._Player_Skill.Skill_Speed = 1.5f;
                break;
            case 4:
                DataManager.Instance._Player_Skill.Skill_Speed = 2.0f;
                break;
            case 5:
                DataManager.Instance._Player_Skill.Skill_Speed = 2.5f;
                break;
            case 6:
                DataManager.Instance._Player_Skill.Skill_Speed = 3.0f;
                break;
            
        }
        

        #endregion

        #region Dash_Level
        
        if (DataManager.Instance._Player_Skill.Dash_Level >= 6)
            DataManager.Instance._Player_Skill.Dash_Level = 6;
        
        
        switch(DataManager.Instance._Player_Skill.Dash_Level)
        {
            case 1:
                DataManager.Instance._Player_Skill.Dash = 2.0f;
                break;
            case 2:
                DataManager.Instance._Player_Skill.Dash = 4.0f;
                break;
            case 3:
                DataManager.Instance._Player_Skill.Dash = 6.0f;
                break;
            case 4:
                DataManager.Instance._Player_Skill.Dash = 8.0f;
                break;
            case 5:
                DataManager.Instance._Player_Skill.Dash = 10.0f;
                break;
            case 6:
                DataManager.Instance._Player_Skill.Dash = 12.0f;
                break;
            
        }

        

        #endregion
        
        #region Discount
        
        if (DataManager.Instance._Player_Skill.Discount_Cost_Level >= 6)
            DataManager.Instance._Player_Skill.Discount_Cost_Level = 6;
        
        switch(DataManager.Instance._Player_Skill.Discount_Cost_Level)
        {
            case 1:
                DataManager.Instance._Player_Skill.Discount_Cost = 10.0f;
                break;
            case 2:
                DataManager.Instance._Player_Skill.Discount_Cost = 15.0f;
                break;
            case 3:
                DataManager.Instance._Player_Skill.Discount_Cost = 20.0f;
                break;
            case 4:
                DataManager.Instance._Player_Skill.Discount_Cost = 25.0f;
                break;
            case 5:
                DataManager.Instance._Player_Skill.Discount_Cost = 30.0f;
                break;
            case 6:
                DataManager.Instance._Player_Skill.Discount_Cost = 35.0f;
                break;
            
        }
        
        #endregion

        #region Double_Jump
        if (DataManager.Instance._Player_Skill.isDouble_Jump_Level == 1)
        {
            DataManager.Instance._Player_Skill.isDouble_Jump_Level = 1;
            DataManager.Instance._Player_Skill.isDouble_Jump = true;
        }
        #endregion

        #region Miss
    
        if (DataManager.Instance._Player_Skill.Miss_Level >= 6)
            DataManager.Instance._Player_Skill.Miss_Level = 6;
        
        switch(DataManager.Instance._Player_Skill.Miss_Level)
        {
            case 1:
                DataManager.Instance._Player_Skill.Miss = 5.0f;
                break;
            case 2:
                DataManager.Instance._Player_Skill.Miss = 8.0f;
                break;
            case 3:
                DataManager.Instance._Player_Skill.Miss = 11.0f;
                break;
            case 4:
                DataManager.Instance._Player_Skill.Miss = 14.0f;
                break;
            case 5:
                DataManager.Instance._Player_Skill.Miss = 17.0f;
                break;
            case 6:
                DataManager.Instance._Player_Skill.Miss = 20.0f;
                break;
            
        }

        #endregion

        #region Poison

        if (DataManager.Instance._Player_Skill.Poision_Damage_Level >= 6)
            DataManager.Instance._Player_Skill.Poision_Damage_Level = 0;
        
        switch(DataManager.Instance._Player_Skill.Poision_Damage_Level)
        {
            case 1:
                DataManager.Instance._Player_Skill.poison_damage = 5.0f;
                DataManager.Instance._Player_Skill.poison_per = 15.0f;
                break;
            case 2:
                DataManager.Instance._Player_Skill.poison_damage = 8.0f;
                DataManager.Instance._Player_Skill.poison_per = 16.0f;
                break;
            case 3:
                DataManager.Instance._Player_Skill.poison_damage = 11.0f;
                DataManager.Instance._Player_Skill.poison_per = 17.0f;
                break;
            case 4:
                DataManager.Instance._Player_Skill.poison_damage = 14.0f;
                DataManager.Instance._Player_Skill.poison_per = 18.0f;
                break;
            case 5:
                DataManager.Instance._Player_Skill.poison_damage = 17.0f;
                DataManager.Instance._Player_Skill.poison_per = 19.0f;
                break;
            case 6:
                DataManager.Instance._Player_Skill.poison_damage = 20.0f;
                DataManager.Instance._Player_Skill.poison_per = 20.0f;
                break;
            
        }

        
        
        
        #endregion

        #region Skill_Damage

        if (DataManager.Instance._Active_Skill.Skill_Level >= 6)
            DataManager.Instance._Active_Skill.Skill_Level = 6;
        
        switch(DataManager.Instance._Active_Skill.Skill_Level)
        {
            case 1:
                DataManager.Instance._Active_Skill.Slash_Damage = DataManager.Instance._Active_Skill.Slash_Damage_default *1.1f;
                DataManager.Instance._Active_Skill.Smash_Damage = DataManager.Instance._Active_Skill.Slash_Damage_default *1.1f;
                DataManager.Instance._Active_Skill.Dash_Damage  = DataManager.Instance._Active_Skill.Slash_Damage_default *1.1f;
                break;
            case 2:
                DataManager.Instance._Active_Skill.Slash_Damage = DataManager.Instance._Active_Skill.Slash_Damage_default *1.3f;
                DataManager.Instance._Active_Skill.Smash_Damage = DataManager.Instance._Active_Skill.Slash_Damage_default *1.3f;
                DataManager.Instance._Active_Skill.Dash_Damage  = DataManager.Instance._Active_Skill.Slash_Damage_default *1.3f;
                break;
            case 3:
                DataManager.Instance._Active_Skill.Slash_Damage = DataManager.Instance._Active_Skill.Slash_Damage_default *1.5f;
                DataManager.Instance._Active_Skill.Smash_Damage = DataManager.Instance._Active_Skill.Slash_Damage_default *1.5f;
                DataManager.Instance._Active_Skill.Dash_Damage  = DataManager.Instance._Active_Skill.Slash_Damage_default *1.5f;
                break;
            case 4:
                DataManager.Instance._Active_Skill.Slash_Damage = DataManager.Instance._Active_Skill.Slash_Damage_default *1.7f;
                DataManager.Instance._Active_Skill.Smash_Damage = DataManager.Instance._Active_Skill.Slash_Damage_default *1.7f;
                DataManager.Instance._Active_Skill.Dash_Damage  = DataManager.Instance._Active_Skill.Slash_Damage_default *1.7f;
                break;
            case 5:
                DataManager.Instance._Active_Skill.Slash_Damage = DataManager.Instance._Active_Skill.Slash_Damage_default *1.9f;
                DataManager.Instance._Active_Skill.Smash_Damage = DataManager.Instance._Active_Skill.Slash_Damage_default *1.9f;
                DataManager.Instance._Active_Skill.Dash_Damage  = DataManager.Instance._Active_Skill.Slash_Damage_default *1.9f;
                break;
            case 6:
                DataManager.Instance._Active_Skill.Slash_Damage = DataManager.Instance._Active_Skill.Slash_Damage_default *2.0f;
                DataManager.Instance._Active_Skill.Smash_Damage = DataManager.Instance._Active_Skill.Slash_Damage_default *2.0f;
                DataManager.Instance._Active_Skill.Dash_Damage  = DataManager.Instance._Active_Skill.Slash_Damage_default *2.0f;
                break;
            
        }
        
        
        
        
        
        

        #endregion
       
    }
    
    
    
    
    
    
    public void ButtonClick(int buttonIndex) // 버튼 클릭 이벤트, CardEffect(int selectedIndex) 호출
    {
        CardEffect(buttonIndex);
        skill_organize();
        DataManager.Instance._PlayerData.soul_Count -= SelectManager.Instance.upgrade_soul;
        SoundManager.Instance.Playsfx(SoundManager.SFX.Skill_Canvas_On);
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
