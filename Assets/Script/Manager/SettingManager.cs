using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingManager : MonoBehaviour
{
  
    public GameObject settingWindowPrefab; // 세팅 창 프리팹
    public GameObject PlayerUI; // 세팅 창 프리팹
    private GameObject settingWindowInstance; // 생성된 세팅 창 인스턴스

    public Transform Setting_Canvas;
    public Transform Game_Over_Panel;   
 
    
    
    
    
    private static SettingManager instance = null;

    public static SettingManager Instance
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

    private void Update()
    {
        if (SceneManager.GetActiveScene().name != "Start_Page")
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                // 세팅 창이 생성되지 않은 경우에만 생성
                if (settingWindowInstance == null)
                {
                    settingWindowInstance = Instantiate(settingWindowPrefab, Setting_Canvas);
                }
                // 세팅 창이 생성된 경우에는 파괴
                else
                {
                    Destroy(settingWindowInstance);
                }
            }
        }
        if (SceneManager.GetActiveScene().name != "Prologue") 
        {
            PlayerUI.SetActive(true);
        }
        else
        {
            PlayerUI.SetActive(false);
        }
    }

    public void Destroy_Prefab()
    {
        Destroy(settingWindowInstance);
    }

    public void Game_Over_Panel_Active()
    {
        Game_Over_Panel.gameObject.SetActive(true);
    }

    public void Main_Menu_Button()
    {
        LoadingScene.LoadScene("Start_Page");
        Game_Over_Panel.gameObject.SetActive(false);
    }
    
    
 

    
    
    
    
}
