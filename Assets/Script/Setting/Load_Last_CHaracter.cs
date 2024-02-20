using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Load_Last_CHaracter : MonoBehaviour
{
    public GameObject enemy_Prefab_A;
    public GameObject enemy_Prefab_B;
    public GameObject enemy_Prefab_C;
    

    
    
    private void Awake()
    {

        if (DataManager.Instance.Load)
        {
            if(DataManager.Instance._PlayerData.controll_enemy == 0)
               enemy_Prefab_A.SetActive(true);
            if(DataManager.Instance._PlayerData.controll_enemy == 1)
                enemy_Prefab_B.SetActive(true);
            if(DataManager.Instance._PlayerData.controll_enemy == 2)
                enemy_Prefab_C.SetActive(true);
      

            DataManager.Instance.Load = false;
        }
    }
}
