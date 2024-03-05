using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Stat_UI : MonoBehaviour
{
    public TextMeshProUGUI HP_Drain_Text;
    public TextMeshProUGUI Reduce_Damage_Text;
    public TextMeshProUGUI Skill_Speed_Text;
    public TextMeshProUGUI Poison_Damage_Text;
    public TextMeshProUGUI Dash_Text;
    public TextMeshProUGUI Discount_Cost_Text;
    public TextMeshProUGUI Miss_Text;
    public TextMeshProUGUI isDouble_Jump_Text;
    
    private void Update()
    {
        SetHPDrainText();
        SetReduceDamageText();
        SetSkillSpeedText();
        SetPoisonDamageText();
        SetDashText();
        SetDiscountCostText();
        SetIsDoubleJumpText();
        SetMissText();
    }
    
    
    // HP Drain 스킬 정보 설정
    public void SetHPDrainText()
    {
        SetSkillLevelText(HP_Drain_Text, DataManager.Instance._Player_Skill.HP_Drain_Level, "HP Drain", "체력 흡수");
    }
    public void SetReduceDamageText()
    {
        SetSkillLevelText(Reduce_Damage_Text, DataManager.Instance._Player_Skill.Reduce_damage_Level, "Reduce Damage", "받는 데미지 감소");
    }
    
    // Skill Speed 스킬 정보 설정
    public void SetSkillSpeedText()
    {
        SetSkillLevelText(Skill_Speed_Text, DataManager.Instance._Player_Skill.Skill_Speed_Level, "Player Speed", "이동속도 증가");
    }
    
    // Poison Damage 스킬 정보 설정
    public void SetPoisonDamageText()
    {
        SetSkillLevelText(Poison_Damage_Text, DataManager.Instance._Player_Skill.Poision_Damage_Level, "Poison Damage", "독 피해");
    }
    
    // Dash 스킬 정보 설정
    public void SetDashText()
    {
        SetSkillLevelText(Dash_Text, DataManager.Instance._Player_Skill.Dash_Level, "Dash", "대시");
    }
   
    
    // Discount Cost 스킬 정보 설정
    public void SetDiscountCostText()
    {
        SetSkillLevelText(Discount_Cost_Text, DataManager.Instance._Player_Skill.Discount_Cost_Level, "Discount Cost", "무기 강화 할인");
    }
    
    // isDouble Jump 스킬 정보 설정
    public void SetIsDoubleJumpText()
    {
        SetSkillLevelText(isDouble_Jump_Text, DataManager.Instance._Player_Skill.isDouble_Jump_Level, "Is Double Jump", "이중 점프");
    }
    
    // Miss 스킬 정보 설정
    public void SetMissText()
    {
        SetSkillLevelText(Miss_Text, DataManager.Instance._Player_Skill.Miss_Level, "Miss", "회피 확률");
    }
        private void SetSkillLevelText(TextMeshProUGUI textComponent, int level, string skillNameEnglish, string skillNameKorean)
        {
            if (DataManager.Instance._Sound_Volume.Language == 0)
            {
                if (level == 0)
                {
                    textComponent.SetText($"{skillNameEnglish} LV: {level}");
                    textComponent.color = Color.gray;
                }
                else
                {
                    textComponent.SetText($"{skillNameEnglish} LV: {level}");
                    textComponent.color = Color.white;
                }
            }
            else if (DataManager.Instance._Sound_Volume.Language == 1)
            {
                if (level == 0)
                {
                    textComponent.SetText($"{skillNameKorean} LV: {level}");
                    textComponent.color = Color.gray;
                }
                else
                {
                    textComponent.SetText($"{skillNameKorean} LV: {level}");
                    textComponent.color = Color.white;
                }
            }
        }
    }
