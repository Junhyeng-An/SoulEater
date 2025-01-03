using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectManager : MonoBehaviour
{
    public GameObject settingWindowPrefab; // 세팅 창 프리팹
    private GameObject settingWindowInstance; // 생성된 세팅 창 인스턴스

    public Transform Setting_Canvas;

    public int upgrade_soul = 2;
    [HideInInspector]public bool isChange_C;

    private static SelectManager instance = null;

    public static SelectManager Instance
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
          if (DataManager.Instance._PlayerData.soul_Count >=upgrade_soul && settingWindowInstance == null )
          {
              Time.timeScale = 0;
              SettingManager.Instance.Setting_Active = true;
              settingWindowInstance = Instantiate(settingWindowPrefab, Setting_Canvas);
              SoundManager.Instance.Playsfx(SoundManager.SFX.SKill_Select);
          }
    }

    
    
    
    
    public void Destroy_Prefab()
    {
        SettingManager.Instance.Setting_Active = false;
        Time.timeScale = 1;
        Destroy(settingWindowInstance);
        Debug.Log("destroy");
    }

}
