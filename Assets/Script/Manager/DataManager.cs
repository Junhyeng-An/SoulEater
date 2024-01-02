using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Player_Data
{
    public int gold;
    public int soul_Count;
    public float speed;
    public float jump;
}

public class Sword_Data
{
    public float player_damage_attack;
    public float player_parrying_attack;
}

public class Player_Skill
{
    public float HP_Drain;
    public float sword_Reach;
    public float fire_dote;

}



public class DataManager : MonoBehaviour
{
    private string path;
    private string Player_Data_filename = "PlayerData";
    private string Sword_Data_filename = "SwordData";
    private string Player_Skill_filename = "PlayerSkill";
    private bool SAVE_FILE_EXIST = false;

    
    public Player_Data _PlayerData = new Player_Data();
    public Sword_Data _SwordData = new Sword_Data();
    public Player_Skill _Player_Skill = new Player_Skill();
       
    
    
    
    private static DataManager instance = null;
    public static DataManager Instance
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
            
        path = Application.persistentDataPath + "/";

        if (File.Exists(path + Player_Data_filename))
        {
            SAVE_FILE_EXIST = true;
            Debug.Log(_PlayerData.soul_Count);
        }
        else
        {
            SAVE_FILE_EXIST = false;
        }
        
        
        
        
        
    }


    public void SaveData()
    {
        string json_playerdata = JsonUtility.ToJson(_PlayerData);
        
        File.WriteAllText(path + Player_Data_filename,json_playerdata);

        string json_sworddata = JsonUtility.ToJson(_SwordData);
        
        File.WriteAllText(path + Sword_Data_filename,json_sworddata);
        
        string json_skilldata = JsonUtility.ToJson(_Player_Skill);
        
        File.WriteAllText(path + Player_Skill_filename,json_skilldata);
        
        
    }

    public void LoadData()
    {
        string load_player_Data = File.ReadAllText(path + Player_Data_filename);
        _PlayerData = JsonUtility.FromJson<Player_Data>(load_player_Data);
        
        string load_sword_Data = File.ReadAllText(path + Sword_Data_filename);
        _SwordData = JsonUtility.FromJson<Sword_Data>(load_sword_Data);
        
        string load_skill_Data = File.ReadAllText(path + Player_Skill_filename);
        _Player_Skill = JsonUtility.FromJson<Player_Skill>(load_skill_Data);
    }

    public bool Save_File_Exist()
    {
        Debug.Log(SAVE_FILE_EXIST);
        return SAVE_FILE_EXIST;
    }

    public void Delete_Save_File()
    {
        File.Delete(path + Player_Data_filename);
        File.Delete(path + Sword_Data_filename);
        File.Delete(path + Player_Skill_filename);
    }


}