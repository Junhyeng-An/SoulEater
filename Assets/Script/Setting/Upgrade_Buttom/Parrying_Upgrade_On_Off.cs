using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Parrying_Upgrade_On_Off : MonoBehaviour
{
    public GameObject Yes_No_Button;
    public GameObject Not_Enough_Coin;
    public TextMeshProUGUI upgrade_cost;

    float parrying_cost_final;
    private void Update()
    {
        parrying_cost_final = DataManager.Instance._SwordData.Upgrade_parrying_Cost - Mathf.RoundToInt(DataManager.Instance._SwordData.Upgrade_parrying_Cost * DataManager.Instance._Player_Skill.Discount_Cost / 100);
        upgrade_cost.text = parrying_cost_final.ToString()+ "coin";
    }

    public void Upgrade_Click()
    {
        if(DataManager.Instance._PlayerData.coin >= DataManager.Instance._SwordData.Upgrade_parrying_Cost)
            Yes_No_Button.gameObject.SetActive(true);
        else
            Not_Enough_Coin.gameObject.SetActive(true);
        
        
    }
}