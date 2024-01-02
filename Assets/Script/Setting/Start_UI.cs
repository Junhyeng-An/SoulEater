using System;
using System.Collections;
using System.Collections.Generic;
using PixelCrushers;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Start_UI : MonoBehaviour
{
    public Button Load_Button;

    private void Start()
    {
        if (DataManager.Instance.Save_File_Exist())
            Load_Button.gameObject.SetActive(true);
        else
            Load_Button.gameObject.SetActive(false);
    }

    public void load_Game()
    {
        DataManager.Instance.LoadData();
        SceneManager.LoadScene("Dorf");
    }

    public void new_Game()
    {
        #region Player_Data
        DataManager.Instance._PlayerData.soul_Count = 0;
        DataManager.Instance._PlayerData.jump = 12.0f;
        DataManager.Instance._PlayerData.coin = 0;
        DataManager.Instance._PlayerData.speed = 5.0f;
        #endregion


        #region Sword_Data
        
        DataManager.Instance._SwordData.player_damage_attack = 10.0f;
        DataManager.Instance._SwordData.player_attack_level = 1;

        DataManager.Instance._SwordData.player_sword_reach = 1.0f;
        DataManager.Instance._SwordData.player_sword_level = 1;
        
        DataManager.Instance._SwordData.player_parrying_attack = 1.0f;
        DataManager.Instance._SwordData.player_parrying_level = 1;
        #endregion
        
        
        
        
        
        
        
        DataManager.Instance._Player_Skill.HP_Drain = 0.0f;
        DataManager.Instance._Player_Skill.sword_Reach = 0.0f;
        DataManager.Instance._Player_Skill.fire_dote = 0.0f;
        
        
        SceneManager.LoadScene("Main");
    }


    



    public void Quit()
    {
        Application.Quit();
    }


}
