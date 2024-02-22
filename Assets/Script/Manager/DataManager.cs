using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using PixelCrushers.DialogueSystem.UnityGUI.Wrappers;
using UnityEngine.Experimental.GlobalIllumination;

enum stage
{
    Main,
    stage1,
    stage2,
    stage3
}



public class Player_Data
{
    public int coin;
    public int soul_Count;
    public float speed;
    public float jump;
    public int controll_enemy;
    public int clear_stage;
}

public class Sword_Data
{
    public float player_damage_attack;
    public int player_attack_level;
    public int Upgrade_attack_Cost;
    
    
    public float player_sword_reach;
    public int player_sword_level;
    public int Upgrade_reach_Cost;
    
    
    public float player_parrying_attack;
    public int player_parrying_level;
    public int Upgrade_parrying_Cost;


}



public class Active_Skill
{
    public float Slash_Damage;
    public int Slash_Damage_Level;
    public int Slash_Damage_default;

    public float Smash_Damage;
    public int Smash_Damage_Level;
    public int Smash_Damage_default;

    public float Dash_Damage;
    public int Dash_Damage_Level;
    public float Dash_Damage_default;
}


public class Player_Skill
{
    public float HP_Drain;
    public int HP_Drain_Level;

    public float Reduce_damage;
    public int Reduce_damage_Level;

    public float Skill_Speed;
    public int Skill_Speed_Level;


    public float poison_damage;
    public int Poision_Damage_Level;

    public float Dash = 5;
    public int Dash_Level;

    public float MaxHP;
    public int MaxHP_Level;

    public float Discount_Cost;
    public int Discount_Cost_Level;

    public bool isDouble_Jump;
    public int isDouble_Jump_Level;

    public float Miss;
    public int Miss_Level;
}

public class Sound_Volume
{
    public float BGM_Volume;
    public float SFX_Volume;
    public bool Mute;
    public int Language;
}






public class DataManager : MonoBehaviour
{
    private string path;
    private string Player_Data_filename = "PlayerData";
    private string Sword_Data_filename = "SwordData";
    private string Player_Skill_filename = "PlayerSkill";
    private string Sound_Volume_filename = "Sound_Volume";
    private string Player_ASkill_filename = "Player_Active_Skill";
    private bool SAVE_FILE_EXIST = false;

    
    public Player_Data _PlayerData = new Player_Data();
    public Sword_Data _SwordData = new Sword_Data();
    public Player_Skill _Player_Skill = new Player_Skill();
    public Sound_Volume _Sound_Volume = new Sound_Volume();
    public Active_Skill _Active_Skill = new Active_Skill();


    private string key = "qweqweqweqwe";

    [SerializeField]public bool Load = false;
    
    
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
            _Sound_Volume = JsonUtility.FromJson<Sound_Volume>(EncryptAndDecrypt(load_Sound_Volume_Data));
            
            
        }
        else
        {
            _Sound_Volume.SFX_Volume = 0.2f;
            _Sound_Volume.BGM_Volume = 0.2f;
            _Sound_Volume.Mute = false; // true : mute , false : Sound On
            _Sound_Volume.Language = 0; // 0 : English 1 : Korea
            SAVE_FILE_EXIST = false;
        }
        
        
        
        
        
    }


    public void SaveData()
    {
        string json_playerdata = JsonUtility.ToJson(_PlayerData);
        
        File.WriteAllText(path + Player_Data_filename,EncryptAndDecrypt(json_playerdata));

        string json_sworddata = JsonUtility.ToJson(_SwordData);
        
        File.WriteAllText(path + Sword_Data_filename,EncryptAndDecrypt(json_sworddata));
        
        string json_skilldata = JsonUtility.ToJson(_Player_Skill);
        
        File.WriteAllText(path + Player_Skill_filename,EncryptAndDecrypt(json_skilldata));
        
        string json_Volume_Sound = JsonUtility.ToJson(_Sound_Volume);
        
        File.WriteAllText(path + Sound_Volume_filename,EncryptAndDecrypt(json_Volume_Sound));
        
        string json_active_Skill = JsonUtility.ToJson(_Active_Skill);
        
        File.WriteAllText(path + Player_ASkill_filename,EncryptAndDecrypt(json_active_Skill));
        
        //Delete 
        Debug.Log(_PlayerData.controll_enemy);
    }

    public void LoadData()
    {
        string load_player_Data = File.ReadAllText(path + Player_Data_filename);
        _PlayerData = JsonUtility.FromJson<Player_Data>(EncryptAndDecrypt(load_player_Data));
        
        string load_sword_Data = File.ReadAllText(path + Sword_Data_filename);
        _SwordData = JsonUtility.FromJson<Sword_Data>(EncryptAndDecrypt(load_sword_Data));
        
        string load_skill_Data = File.ReadAllText(path + Player_Skill_filename);
        _Player_Skill = JsonUtility.FromJson<Player_Skill>(EncryptAndDecrypt(load_skill_Data));
        
        string load_active_Data = File.ReadAllText(path + Player_ASkill_filename);
        _Active_Skill = JsonUtility.FromJson<Active_Skill>(EncryptAndDecrypt(load_active_Data));

        
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
        
        File.WriteAllText(path + Sound_Volume_filename,EncryptAndDecrypt(json_Volume_Sound));

    }
    
    

    public void Delete_Save_File()
    {
        File.Delete(path + Player_Data_filename);
        File.Delete(path + Sword_Data_filename);
        File.Delete(path + Player_Skill_filename);
        File.Delete(path + Player_ASkill_filename);
    }

    private string EncryptAndDecrypt(string data)
    {
        string result = "";

        for (int i = 0; i < data.Length; i++)
        {
            result += (char)(data[i] ^ key[i % key.Length]);

        }

        return result;
    }
    

}