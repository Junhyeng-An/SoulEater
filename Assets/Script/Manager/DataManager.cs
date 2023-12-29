using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Player_Data
{
    public int soul_Count;
    public float sword_Reach;
}





public class DataManager : MonoBehaviour
{
    private string path;
    private string filename = "PlayerData";
    private bool SAVE_FILE_EXIST = false;

    public Player_Data _PlayerData = new Player_Data();
       
    
    
    
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

        if (File.Exists(path + filename))
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
        
        File.WriteAllText(path + filename,json_playerdata);

        Debug.Log("save");
    }

    public void LoadData()
    {
        string load_Data = File.ReadAllText(path + filename);
        _PlayerData = JsonUtility.FromJson<Player_Data>(load_Data);
    }

    public bool Save_File_Exist()
    {
        Debug.Log(SAVE_FILE_EXIST);
        return SAVE_FILE_EXIST;
    }

    public void Delete_Save_File()
    {
        File.Delete(path + filename);
    }


}