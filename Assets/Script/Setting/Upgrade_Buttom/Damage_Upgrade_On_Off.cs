using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class Damage_Upgrade_On_Off : MonoBehaviour
{
    public GameObject Yes_No_Button;
    public GameObject Not_Enough_Coin;
    public TextMeshProUGUI upgrade_cost;

    float attack_cost_final;
    private void Update()
    {
        attack_cost_final = DataManager.Instance._SwordData.Upgrade_attack_Cost - Mathf.RoundToInt(DataManager.Instance._SwordData.Upgrade_attack_Cost * DataManager.Instance._Player_Skill.Discount_Cost / 100);
        if(DataManager.Instance._Sound_Volume.Language == 0)
            upgrade_cost.text = attack_cost_final.ToString() + "coin";
        if(DataManager.Instance._Sound_Volume.Language == 1)
            upgrade_cost.text = attack_cost_final.ToString() + "코인";
    }
    
    
    public void Upgrade_Click()
    {
        if(DataManager.Instance._PlayerData.coin >= DataManager.Instance._SwordData.Upgrade_attack_Cost)
            Yes_No_Button.gameObject.SetActive(true);
        else
            Not_Enough_Coin.gameObject.SetActive(true);
        
        
    }
}
