using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;



public class CharacterManager : MonoBehaviour
{
    private const int SIZE = 9;

    [SerializeField] public int Cur_Hp = 10;
    private string Enemy_Name = "";
    
    public GameObject[] enemy_Prefab = new GameObject[SIZE];
    private string[] enemy_Name = new string[SIZE];

    
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
    
    
    
    private void Awake()
    {
        
        #region Singleton
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
        #endregion
        
        #region Initialize_enemy_name

        for (int i = 0; i < SIZE; i++)
        {
            enemy_Name[i] = enemy_Prefab[i].ToString();
        }
        
        for (int i = 0; i < SIZE; i++)
        {
            if (enemy_Name[0] == enemy_Name[i]) 
                Debug.Log("true");
            else
            {
            }
        }
        
        
        
        #endregion
        
    }

    public void Spawn_Enemy_Type()
    {
    }


    public void set_enemy(string enemy_name)
    {
        Enemy_Name = enemy_name;
    }


    
    
    
    
    



    
}
