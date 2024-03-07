using System;
using System.Collections;
using System.Collections.Generic;
using PixelCrushers;
using PixelCrushers.DialogueSystem;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Start_UI : MonoBehaviour
{
    public Button Load_Button;


    private void Awake()
    {
        #region Player_Data
        DataManager.Instance._PlayerData.soul_Count = 0;
        DataManager.Instance._PlayerData.jump = 12.0f;
        DataManager.Instance._PlayerData.coin = 0;
        DataManager.Instance._PlayerData.speed = 5.0f;
        DataManager.Instance._PlayerData.clear_stage = (int)stage.Main;
        DataManager.Instance._PlayerData.Boss_Stage = false;
        
        #endregion


        #region Sword_Data

        DataManager.Instance._SwordData.player_damage_attack = 10.0f;
        DataManager.Instance._SwordData.player_attack_level = 1;
        DataManager.Instance._SwordData.Upgrade_attack_Cost = 2;

        DataManager.Instance._SwordData.player_sword_reach = 1.0f;
        DataManager.Instance._SwordData.player_sword_level = 1;
        DataManager.Instance._SwordData.Upgrade_reach_Cost = 10;

        DataManager.Instance._SwordData.player_parrying_attack = 55.0f;
        DataManager.Instance._SwordData.player_parrying_level = 1;
        DataManager.Instance._SwordData.Upgrade_parrying_Cost = 2;
        #endregion


        #region Player_Skill;
        DataManager.Instance._Player_Skill.HP_Drain = 0.0f;
        DataManager.Instance._Player_Skill.HP_Drain_Level = 0;

        DataManager.Instance._Player_Skill.Reduce_damage = 0.0f;
        DataManager.Instance._Player_Skill.Reduce_damage_Level = 0;

        DataManager.Instance._Player_Skill.poison_damage = 0.0f;
        DataManager.Instance._Player_Skill.Poision_Damage_Level = 0;

    

        DataManager.Instance._Player_Skill.Skill_Speed = 0.0f;
        DataManager.Instance._Player_Skill.Skill_Speed_Level = 0;

        DataManager.Instance._Player_Skill.Discount_Cost = 0.0f;
        DataManager.Instance._Player_Skill.Discount_Cost_Level = 0;

        DataManager.Instance._Player_Skill.isDouble_Jump = false;
        DataManager.Instance._Player_Skill.isDouble_Jump_Level = 0;

        DataManager.Instance._Player_Skill.Miss = 0.0f;
        DataManager.Instance._Player_Skill.Miss_Level = 0;


        DataManager.Instance._Player_Skill.Dash = 5.0f;
        DataManager.Instance._Player_Skill.Dash_Level = 0;

        #endregion

        #region Active_SKill

        DataManager.Instance._Active_Skill.Dash_Damage = 10;
        DataManager.Instance._Active_Skill.Dash_Damage_default = 10;


        DataManager.Instance._Active_Skill.Slash_Damage = 10;
        DataManager.Instance._Active_Skill.Slash_Damage_default = 10;


        DataManager.Instance._Active_Skill.Smash_Damage = 5;
        DataManager.Instance._Active_Skill.Smash_Damage_default = 5;

        DataManager.Instance._Active_Skill.Skill_Level = 0;

        #endregion


    }


    private void Start()
    {
        if (DataManager.Instance.Save_File_Exist())
            Load_Button.gameObject.SetActive(true);
        else
            Load_Button.gameObject.SetActive(false);
    }

    public void load_Game()
    {
        DataManager.Instance.Load = true;
        DataManager.Instance.LoadData();
        SceneManager.LoadScene("Dorf");
    }

    public void new_Game()
    {
        //SceneManager.LoadScene("Main");
        SceneManager.LoadScene("Prologue");
    }

    public void Boss_room()
    {
        SceneManager.LoadScene("Boss3");
    }

    public void Test_room()
    {
        SceneManager.LoadScene("Main");
    }
    public void Tutorial()
    {
        SceneManager.LoadScene("Home");
    }
    public void Test_Clear()
    {
        SceneManager.LoadScene("Clear_Scene");
    }
    public void Quit()
    {
        Application.Quit();
    }





}
