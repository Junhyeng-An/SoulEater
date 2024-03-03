using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Weapon_Canvas_Stat : MonoBehaviour
{
    public TextMeshProUGUI swordStat;
    public TextMeshProUGUI parryingStat;

    void Update()
    {
        if (DataManager.Instance._Sound_Volume.Language == 0)
        {
            swordStat.text = "Attack Damage: " +DataManager.Instance._SwordData.player_damage_attack.ToString();
            parryingStat.text = "Parrying Damage: " + DataManager.Instance._SwordData.player_parrying_attack.ToString();
        }
        
        if (DataManager.Instance._Sound_Volume.Language == 1)
        {
            swordStat.text = "공격 데미지: " +DataManager.Instance._SwordData.player_damage_attack.ToString();
            parryingStat.text = "페링 데미지: " + DataManager.Instance._SwordData.player_parrying_attack.ToString();
        }
        
    }
}
