using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingManager : MonoBehaviour
{
    public GameObject StatWindowPrefab;
    private GameObject StatWindowInstance;
    public GameObject settingWindowPrefab; // 세팅 창 프리팹
    public GameObject PlayerUI; // 세팅 창 프리팹
    private GameObject settingWindowInstance; // 생성된 세팅 창 인스턴스

    public Transform Setting_Canvas;
    public Transform Game_Over_Panel;   
 
    public bool gameover = false;
    
    private static SettingManager instance = null;

    public bool Setting_Active = false;

    
    float Miss_const;
    public float Damage;



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
        if (Active_Condition())
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                // 세팅 창이 생성되지 않은 경우에만 생성
                if (settingWindowInstance == null)
                {
                    Setting_Active = true;
                    settingWindowInstance = Instantiate(settingWindowPrefab, Setting_Canvas);
                    Time.timeScale = 0;

                }
                // 세팅 창이 생성된 경우에는 파괴
                else
                {
                    Setting_Active = false;
                    Time.timeScale = 1;
                    Destroy(settingWindowInstance);
                }
            }
            
            
            
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                // 세팅 창이 생성되지 않은 경우에만 생성
                if (StatWindowInstance == null)
                {
                    StatWindowInstance = Instantiate(StatWindowPrefab, Setting_Canvas);
                }
                // 세팅 창이 생성된 경우에는 파괴
                else
                {
                    Destroy(StatWindowInstance);
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
        Time.timeScale = 1;
        Destroy(settingWindowInstance);
        Setting_Active = false;
    }

    public void Game_Over_Panel_Active()
    {
        Game_Over_Panel.gameObject.SetActive(true);
    }

    public void Main_Menu_Button()
    {
        LoadingScene.LoadScene("Start_Page");
        Game_Over_Panel.gameObject.SetActive(false);
        SettingManager.Instance.gameover = false;
        Time.timeScale = 1.0f;
    }


    public bool Active_Condition()
    {
        string activeSceneName = SceneManager.GetActiveScene().name;
        return activeSceneName != "Start_Page" && activeSceneName != "Prologue" && activeSceneName != "Loading";

    }


    public void Damage_Calculate(Collider2D collision, float Damage, EnemyController enemyController)
    {
        Miss_const = UnityEngine.Random.Range(0f, 100f);
        Debug.Log(Miss_const);
        Debug.Log(DataManager.Instance._Player_Skill.Miss);

        if (Miss_const <= DataManager.Instance._Player_Skill.Miss)
        {
            return;
        }
        Debug.Log(Damage * (1.0f - DataManager.Instance._Player_Skill.Reduce_damage * 0.01f));
        enemyController.CurHP -= Damage * (1.0f - (DataManager.Instance._Player_Skill.Reduce_damage * 0.01f));


    }

    public float smash_Damage()
    {
        return Damage;
    }

}
