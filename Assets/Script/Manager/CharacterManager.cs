using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;



public class CharacterManager : MonoBehaviour
{
    public GameObject Player;

    public Vector2 spawnPosition;
    
    

    
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

        
        
    }

    public void PlayerPosition(Transform transform)
    {
        Player.transform.position = transform.position;
    }

    public void PlayerPosition(Vector3 pos)
    {
        Player.transform.position = pos;
    }
 
    
    
    
    
    



    
}
