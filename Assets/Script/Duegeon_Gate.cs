using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Duegeon_Gate : MonoBehaviour
{
    private string Scene_Name;
    private void Awake()
    {
        if(DataManager.Instance._PlayerData.clear_stage == (int)stage.Main && DataManager.Instance._PlayerData.Boss_Stage == false)
            Scene_Name = "Map1_3";
        if (DataManager.Instance._PlayerData.clear_stage == (int)stage.Main && DataManager.Instance._PlayerData.Boss_Stage == true)
        {
            Scene_Name = "Boss2";
            DataManager.Instance._PlayerData.Boss_Stage = false;
        }

        if(DataManager.Instance._PlayerData.clear_stage == (int)stage.stage1 && DataManager.Instance._PlayerData.Boss_Stage == false)
            Scene_Name = "Map2_1";
        if (DataManager.Instance._PlayerData.clear_stage == (int)stage.stage1 && DataManager.Instance._PlayerData.Boss_Stage == true)
        {
            Scene_Name = "Boss";
            DataManager.Instance._PlayerData.Boss_Stage = false;
        }


        if(DataManager.Instance._PlayerData.clear_stage == (int)stage.stage2 && DataManager.Instance._PlayerData.Boss_Stage == false)
            Scene_Name = "Map3_1";
        if(DataManager.Instance._PlayerData.clear_stage == (int)stage.stage2 && DataManager.Instance._PlayerData.Boss_Stage == true)
            Scene_Name = "Boss3";


        
        









    }
    
    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D other)
    {
  
        GameObject controlledObjects = GameObject.FindGameObjectWithTag("Controlled");

   
        
        
        // Check if the object entering the portal is the player
        if (other.CompareTag("Player"))
        {
            // Check if there are no enemies in the scene
            if (NoEnemiesInScene())
            {
                DontDestroyOnLoad(controlledObjects);
                
                LoadingScene.LoadScene(Scene_Name);
            }
            else
            {
                Debug.Log("Cannot enter portal with enemies in the scene!");
            }
        }
    }

    private bool NoEnemiesInScene()
    {
        // Check if there are no game objects with the "Enemy" tag in the scene
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        return enemies.Length == 0;
    }

    private void LoadNextScene()
    {
        // Load the scene named "main2"
        SceneManager.LoadScene(Scene_Name);
    }
    
    
    
    
}


