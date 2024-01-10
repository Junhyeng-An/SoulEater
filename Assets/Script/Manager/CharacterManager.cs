using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;



public class CharacterManager : MonoBehaviour
{
    private const int SIZE = 9;

    [SerializeField] public int Cur_Hp;
    
    
    public GameObject[] enemy_Prefab = new GameObject[SIZE];
    private string[] enemy_Name = new string[SIZE];

    
    
    
    
    private void Awake()
    {
        #region Initialize_enemy_name

        for (int i = 0; i < SIZE; i++)
        {
            enemy_Name[i] = enemy_Prefab[i].ToString();
        }
        

        #endregion
    }
    
    


    #region SingleTon
    
    private static CharacterManager instance = null;
    public static CharacterManager Instance
    {
        get
        {
            if (null == instance)
            {
                return null;
            }
    
            return instance;
        }
    }
    #endregion

    
}
