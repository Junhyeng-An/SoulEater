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


    private void Update()
    {
        upgrade_cost.text = DataManager.Instance._SwordData.Upgrade_attack_Cost.ToString() + "coin";
    }
    
    
    public void Upgrade_Click()
    {
        if(DataManager.Instance._PlayerData.coin >= DataManager.Instance._SwordData.Upgrade_attack_Cost)
            Yes_No_Button.gameObject.SetActive(true);
        else
            Not_Enough_Coin.gameObject.SetActive(true);
        
        
    }
}
