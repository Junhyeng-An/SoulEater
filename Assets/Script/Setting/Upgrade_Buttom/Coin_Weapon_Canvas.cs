using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Coin_Weapon_Canvas : MonoBehaviour
{
    public TextMeshProUGUI upgrade_cost;


    private void Update()
    {
        upgrade_cost.text = DataManager.Instance._PlayerData.coin.ToString() + "coin";
    }

}
