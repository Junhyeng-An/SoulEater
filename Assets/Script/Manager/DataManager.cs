using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using PixelCrushers.DialogueSystem.UnityGUI.Wrappers;
using UnityEngine.Experimental.GlobalIllumination;

public class Player_Data
{
    public int coin;
    public int soul_Count;
    public float speed;
    public float jump;
    public string controll_enemy;
}

public class Sword_Data
{
    public float player_damage_attack;
    public int player_attack_level;

    public float player_sword_reach;
    public int player_sword_level;
    
    public float player_parrying_attack;
    public int player_parrying_level;
}

public class Player_Skill
{
    public float HP_Drain;
    public int HP_Drain_Level;

    public float Reduce_damage;
    public int Reduce_damage_Level;

    public float poison_damage;
    public int Poision_Damage_Level;

    public float Dash = 5;
    public int Dash_Level;

    public float Discount;
    public int Discount_Level;

    public float MaxHP;
    public int MaxHP_Level;

}

public class Sound_Volume
{
    public float BGM_Volume;
    public float SFX_Volume;
    public bool Mute;
}





public class DataManager : MonoBehaviour
{
    private string path;
    private string Player_Data_filename = "PlayerData";
    private string Sword_Data_filename = "SwordData";
    private string Player_Skill_filename = "PlayerSkill";
    private string Sound_Volume_filename = "Sound_Volume";
    private bool SAVE_FILE_EXIST = false;

    
    public Player_Data _PlayerData = new Player_Data();
    public Sword_Data _SwordData = new Sword_Data();
    public Player_Skill _Player_Skill = new Player_Skill();
    public Sound_Volume _Sound_Volume = new Sound_Volume();



    public bool Load = false;
    
    
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
            Debug.Log(path+Player_Data_filename);
            string load_Sound_Volume_Data = File.ReadAllText(path + Sound_Volume_filename);
            _Sound_Volume = JsonUtility.FromJson<Sound_Volume>(load_Sound_Volume_Data);
            
            
        }
        else
        {
            _Sound_Volume.SFX_Volume = 0.2f;
            _Sound_Volume.BGM_Volume = 0.2f;
            _Sound_Volume.Mute = false; // true : mute , false : Sound On
            
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
        
        string json_Volume_Sound = JsonUtility.ToJson(_Sound_Volume);
        
        File.WriteAllText(path + Sound_Volume_filename,json_Volume_Sound);
        
    }

    public void LoadData()
    {
        string load_player_Data = File.ReadAllText(path + Player_Data_filename);
        _PlayerData = JsonUtility.FromJson<Player_Data>(load_player_Data);
        
        string load_sword_Data = File.ReadAllText(path + Sword_Data_filename);
        _SwordData = JsonUtility.FromJson<Sword_Data>(load_sword_Data);
        
        string load_skill_Data = File.ReadAllText(path + Player_Skill_filename);
        _Player_Skill = JsonUtility.FromJson<Player_Skill>(load_skill_Data);
        
        //Sound is load when game was start 
        
        // string load_Sound_Volume_Data = File.ReadAllText(path + Sound_Volume_filename);
        // _Sound_Volume = JsonUtility.FromJson<Sound_Volume>(load_Sound_Volume_Data);
        
    }

    public bool Save_File_Exist()
    {
        Debug.Log("SAVE_FILE_EXIST");
        return SAVE_FILE_EXIST;
    }

    //Sound need independent save system
    public void Save_Sound()
    {
        string json_Volume_Sound = JsonUtility.ToJson(_Sound_Volume);
        
        File.WriteAllText(path + Sound_Volume_filename,json_Volume_Sound);

    }
    
    

    public void Delete_Save_File()
    {
        File.Delete(path + Player_Data_filename);
        File.Delete(path + Sword_Data_filename);
        File.Delete(path + Player_Skill_filename);
    }


}