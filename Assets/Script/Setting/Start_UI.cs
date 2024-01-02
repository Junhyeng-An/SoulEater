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
        DataManager.Instance._PlayerData.soul_Count = 1;
        DataManager.Instance._PlayerData.jump = 12.0f;
        DataManager.Instance._PlayerData.gold = 0;
        DataManager.Instance._PlayerData.speed = 5.0f;

        DataManager.Instance._SwordData.player_damage_attack = 10.0f;
        DataManager.Instance._SwordData.player_parrying_attack = 1.0f;

        DataManager.Instance._Player_Skill.HP_Drain = 0.0f;
        DataManager.Instance._Player_Skill.sword_Reach = 0.0f;
        DataManager.Instance._Player_Skill.fire_dote = 0.0f;
        
        
        SceneManager.LoadScene("Boss_cinema_test");
    }


    



    public void Quit()
    {
        Application.Quit();
    }


}
