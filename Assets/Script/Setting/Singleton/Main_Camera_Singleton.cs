using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Com.LuisPedroFonseca.ProCamera2D;
using UnityEngine.SceneManagement;

public class Main_Camera_Singleton : MonoBehaviour
{
    private static Main_Camera_Singleton instance = null;
    public static Main_Camera_Singleton Instance
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
