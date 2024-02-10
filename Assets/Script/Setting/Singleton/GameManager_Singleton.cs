using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager_Singleton : MonoBehaviour
{
    private static GameManager_Singleton instance = null;
    public static GameManager_Singleton Instance
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
     
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
    }
}
