using JetBrains.Annotations;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Clear_Canvas : MonoBehaviour
{
    public TextMeshProUGUI skill_text;
    string[] skill_level;
    // Start is called before the first frame update
    void Start()
    {
        UpdateSkillText();
    }

    // Update is called once per frame
    void Update()
    {
    }
    public void new_Game()
    {
        DataManager.Instance.Delete_Save_File();
        DataManager.Instance.SAVE_FILE_EXIST = false;
        SceneManager.LoadScene("Start_page");
    }
    void UpdateSkillText()
    {
        
        string displayedText = "";
        if (DataManager.Instance._Sound_Volume.Language == 0)
        {
            if (DataManager.Instance._Player_Skill.HP_Drain_Level >= 1)
                displayedText += "HP Drain Level: " + DataManager.Instance._Player_Skill.HP_Drain_Level + "\n";

            if (DataManager.Instance._Player_Skill.Reduce_damage_Level >= 1)
                displayedText += "Reduce Damage Level: " + DataManager.Instance._Player_Skill.Reduce_damage_Level +
                                 "\n";

            if (DataManager.Instance._Player_Skill.Skill_Speed_Level >= 1)
                displayedText += "Skill Speed Level: " + DataManager.Instance._Player_Skill.Skill_Speed_Level + "\n";

            if (DataManager.Instance._Player_Skill.Poision_Damage_Level >= 1)
                displayedText += "Poison Damage Level: " + DataManager.Instance._Player_Skill.Poision_Damage_Level +
                                 "\n";

            if (DataManager.Instance._Player_Skill.Dash_Level >= 1)
                displayedText += "Dash Level: " + DataManager.Instance._Player_Skill.Dash_Level + "\n";

            if (DataManager.Instance._Player_Skill.Discount_Cost_Level >= 1)
                displayedText += "Discount Cost Level: " + DataManager.Instance._Player_Skill.Discount_Cost_Level +
                                 "\n";

            if (DataManager.Instance._Player_Skill.isDouble_Jump_Level >= 1)
                displayedText += "Double Jump Level: " + DataManager.Instance._Player_Skill.isDouble_Jump_Level + "\n";

            if (DataManager.Instance._Player_Skill.Miss_Level >= 1)
                displayedText += "Miss Level: " + DataManager.Instance._Player_Skill.Miss_Level + "\n";

        }
        
        
        
        
        if (DataManager.Instance._Sound_Volume.Language == 1)
        {
            if (DataManager.Instance._Player_Skill.HP_Drain_Level >= 1)
                displayedText += "체력 흡수: " + DataManager.Instance._Player_Skill.HP_Drain_Level + "\n";

            if (DataManager.Instance._Player_Skill.Reduce_damage_Level >= 1)
                displayedText += "데미지 감소: " + DataManager.Instance._Player_Skill.Reduce_damage_Level +
                                 "\n";

            if (DataManager.Instance._Player_Skill.Skill_Speed_Level >= 1)
                displayedText += "이동속도 증가: " + DataManager.Instance._Player_Skill.Skill_Speed_Level + "\n";

            if (DataManager.Instance._Player_Skill.Poision_Damage_Level >= 1)
                displayedText += "독 피해: " + DataManager.Instance._Player_Skill.Poision_Damage_Level +
                                 "\n";

            if (DataManager.Instance._Player_Skill.Dash_Level >= 1)
                displayedText += "대시: " + DataManager.Instance._Player_Skill.Dash_Level + "\n";

            if (DataManager.Instance._Player_Skill.Discount_Cost_Level >= 1)
                displayedText += "무기 강화 할인: " + DataManager.Instance._Player_Skill.Discount_Cost_Level +
                                 "\n";

            if (DataManager.Instance._Player_Skill.isDouble_Jump_Level >= 1)
                displayedText += "이중 점프: " + DataManager.Instance._Player_Skill.isDouble_Jump_Level + "\n";

            if (DataManager.Instance._Player_Skill.Miss_Level >= 1)
                displayedText += "회피 확률: " + DataManager.Instance._Player_Skill.Miss_Level + "\n";

        }












        skill_text.text = displayedText;
    }
}