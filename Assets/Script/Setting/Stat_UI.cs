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
        Attack_damage.text = DataManager.Instance._SwordData.player_damage_attack.ToString();
        Parrying_damage.text = DataManager.Instance._SwordData.player_parrying_attack.ToString();
        Attack_damage.text = DataManager.Instance._SwordData.player_sword_reach.ToString();
    }
}
