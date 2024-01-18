using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Load_Last_CHaracter : MonoBehaviour
{
    public GameObject[] enemy_Prefab = new GameObject[9];
    string[] object_name = new string[9];
    
   readonly string character_name0 ="Enemy_1";
   readonly string character_name1 ="Enemy_2";
   readonly string character_name2 ="Enemy_3";
   readonly string character_name3 ="Enemy_4";
   readonly string character_name4 ="Enemy_5";
   readonly string character_name5 ="Enemy_6";
   readonly string character_name6 ="Enemy_7";
   readonly string character_name7 ="Enemy_8";
   readonly string character_name8 ="Enemy_9";

    
    
    private void Awake()
    {

        if (DataManager.Instance.Load)
        {
            if (DataManager.Instance._PlayerData.controll_enemy == character_name0)
            {
                Debug.Log("asdasdasdasd");
                enemy_Prefab[0].SetActive(true);
            }
            else if (DataManager.Instance._PlayerData.controll_enemy == character_name1)
                enemy_Prefab[1].SetActive(true);
            else if (DataManager.Instance._PlayerData.controll_enemy == character_name2)
                enemy_Prefab[2].SetActive(true);
            else if (DataManager.Instance._PlayerData.controll_enemy == character_name3)
                enemy_Prefab[3].SetActive(true);
            else if (DataManager.Instance._PlayerData.controll_enemy == character_name4)
                enemy_Prefab[4].SetActive(true);
            else if (DataManager.Instance._PlayerData.controll_enemy == character_name5)
                enemy_Prefab[5].SetActive(true);
            else if (DataManager.Instance._PlayerData.controll_enemy == character_name6)
                enemy_Prefab[6].SetActive(true);
            else if (DataManager.Instance._PlayerData.controll_enemy == character_name7)
                enemy_Prefab[7].SetActive(true);
            else if (DataManager.Instance._PlayerData.controll_enemy == character_name8)
                enemy_Prefab[8].SetActive(true);


            DataManager.Instance.Load = true;
        }
    }
}
