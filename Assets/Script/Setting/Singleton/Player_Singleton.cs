using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Singleton : MonoBehaviour
{
 
     private static Player_Singleton instance = null;
     public static Player_Singleton Instance
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
