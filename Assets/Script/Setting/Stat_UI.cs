using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Stat_UI : MonoBehaviour
{
    public TextMeshProUGUI Attack_damage;
    public TextMeshProUGUI Parrying_damage;
    public TextMeshProUGUI Sword_reach;

    private void Update()
    {
        if (DataManager.Instance._Sound_Volume.Language == 0)
        {
            Attack_damage.text = "Attack Damage: " + DataManager.Instance._SwordData.player_damage_attack.ToString();
            Parrying_damage.text = "Parrying Damage: " + DataManager.Instance._SwordData.player_parrying_attack.ToString();
            Sword_reach.text = "Sword Size: " + DataManager.Instance._SwordData.player_sword_reach.ToString();


        }
        else if (DataManager.Instance._Sound_Volume.Language == 1)
        {
            Attack_damage.text = "무기 데미지: " + DataManager.Instance._SwordData.player_damage_attack.ToString();
            Parrying_damage.text = "페링 데미지: " + DataManager.Instance._SwordData.player_parrying_attack.ToString();
            Sword_reach.text = "검 크기: " + DataManager.Instance._SwordData.player_sword_reach.ToString();
        }
    }
}
