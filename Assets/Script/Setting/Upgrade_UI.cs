using System;
using System.Collections;
using System.Collections.Generic;
using PixelCrushers;
using UnityEngine;
using TMPro;

public class Upgrade_UI : MonoBehaviour
{
    public TextMeshProUGUI Weapon_Attack_Level;
    public TextMeshProUGUI Weapon_Parrying_Level;


    private void Update()
    {
        Weapon_Attack_Level.text = "LV"+DataManager.Instance._SwordData.player_attack_level.ToString() + "->"
            + (DataManager.Instance._SwordData.player_attack_level + 1).ToString();
        Weapon_Parrying_Level.text = "LV"+DataManager.Instance._SwordData.player_parrying_level.ToString() + "->"
            + (DataManager.Instance._SwordData.player_parrying_level + 1).ToString();
    
    }

  
    
    
    
    
}
