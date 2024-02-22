using System;
using System.Collections;
using System.Collections.Generic;
using PixelCrushers;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Stat_Page : MonoBehaviour
{
    public TextMeshProUGUI HP_Drain_Text;
    public TextMeshProUGUI Reduce_Damage_Text;
    public TextMeshProUGUI Hermes_Text;
    
    
    
    private void Update()
    {
        HP_Drain();
    }


    public void HP_Drain()
    {
        if (DataManager.Instance._Sound_Volume.Language == 0)
        {

            if (DataManager.Instance._Player_Skill.HP_Drain_Level == 0)
            {
                HP_Drain_Text.SetText("HP Darin LV: " + DataManager.Instance._Player_Skill.HP_Drain_Level); 
                HP_Drain_Text.color = Color.gray;
            }

            else
            {
                HP_Drain_Text.SetText("HP Darin LV: " + DataManager.Instance._Player_Skill.HP_Drain_Level)
                    ;
                HP_Drain_Text.color = Color.white;
            }
        }
        else if (DataManager.Instance._Sound_Volume.Language == 1)
        {

            if (DataManager.Instance._Player_Skill.HP_Drain_Level == 0)
            {
                HP_Drain_Text.SetText("체력 흡수 LV: " + DataManager.Instance._Player_Skill.HP_Drain_Level);
                HP_Drain_Text.color = Color.gray;
            }

            else
            {
                HP_Drain_Text.SetText("체력 흡수 LV: " + DataManager.Instance._Player_Skill.HP_Drain_Level);
                HP_Drain_Text.color = Color.white;
            }
        }
    }

    public void Reduce_Damage()
    {
        if (DataManager.Instance._Sound_Volume.Language == 0)
        {

            if (DataManager.Instance._Player_Skill.Reduce_damage_Level == 0)
            {
                Reduce_Damage_Text.SetText("Reduce Damage LV: " + DataManager.Instance._Player_Skill.Reduce_damage_Level); 
                Reduce_Damage_Text.color = Color.gray;
            }

            else
            {
                Reduce_Damage_Text.SetText("Reduce Damage LV: " + DataManager.Instance._Player_Skill.Reduce_damage_Level);
                Reduce_Damage_Text.color = Color.white;
            }
        }
        else if (DataManager.Instance._Sound_Volume.Language == 1)
        {

            if (DataManager.Instance._Player_Skill.Reduce_damage_Level == 0)
            {
                Reduce_Damage_Text.SetText("받는 데미지 감소 LV: " + DataManager.Instance._Player_Skill.Reduce_damage_Level);
                Reduce_Damage_Text.color = Color.gray;
            }

            else
            {
                Reduce_Damage_Text.SetText("받는 데미지 감소 LV: " + DataManager.Instance._Player_Skill.Reduce_damage_Level);
                Reduce_Damage_Text.color = Color.white;
            }
        }
    }
    
    public void Hermes()
    {
        if (DataManager.Instance._Sound_Volume.Language == 0)
        {

            if (DataManager.Instance._Player_Skill.Skill_Speed_Level == 0)
            {
                Reduce_Damage_Text.SetText("Hermes LV: " + DataManager.Instance._Player_Skill.Skill_Speed_Level); 
                Reduce_Damage_Text.color = Color.gray;
            }

            else
            {
                Reduce_Damage_Text.SetText("Hermes LV: " + DataManager.Instance._Player_Skill.Skill_Speed_Level);
                Reduce_Damage_Text.color = Color.white;
            }
        }
        else if (DataManager.Instance._Sound_Volume.Language == 1)
        {

            if (DataManager.Instance._Player_Skill.Skill_Speed_Level == 0)
            {
                Reduce_Damage_Text.SetText("이동속도 증가 LV: " + DataManager.Instance._Player_Skill.Skill_Speed_Level);
                Reduce_Damage_Text.color = Color.gray;
            }

            else
            {
                Reduce_Damage_Text.SetText("이동속도 증가 LV: " + DataManager.Instance._Player_Skill.Skill_Speed_Level);
                Reduce_Damage_Text.color = Color.white;
            }
        }
    }


    
    
    
   
}
