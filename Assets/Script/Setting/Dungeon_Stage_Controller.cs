using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Dungeon_Stage_Controller : MonoBehaviour
{
    private string Scene_Name;

    private void Awake()
    {
        if (SceneManager.GetActiveScene().name == "Map1_1")
            Scene_Name = "Map1_2";

        if (SceneManager.GetActiveScene().name == "Map1_2")
            Scene_Name = "Map1_3";

        if (SceneManager.GetActiveScene().name == "Map1_3")
            Scene_Name = "Map1_4";
        
        if (SceneManager.GetActiveScene().name == "Map1_4")
        {
            Scene_Name = "Dorf";
            DataManager.Instance._PlayerData.Boss_Stage = true;
        }

        if (SceneManager.GetActiveScene().name == "Map2_1")
            Scene_Name = "Map2_2";

        if (SceneManager.GetActiveScene().name == "Map2_2")
            Scene_Name = "Map2_3";

        if (SceneManager.GetActiveScene().name == "Map2_3")
            Scene_Name = "Map2_4";
        
        if (SceneManager.GetActiveScene().name == "Map2_4")
        {
            Scene_Name = "Dorf";
            DataManager.Instance._PlayerData.Boss_Stage = true;
        }
  
        
        
        
        if (SceneManager.GetActiveScene().name == "Map3_1")
            Scene_Name = "Map3_2";

        if (SceneManager.GetActiveScene().name == "Map3_2")
            Scene_Name = "Map3_3";

        if (SceneManager.GetActiveScene().name == "Map3_3")
            Scene_Name = "Map3_4";
        
        if (SceneManager.GetActiveScene().name == "Map3_4")
        {
            Scene_Name = "Dorf";
            DataManager.Instance._PlayerData.Boss_Stage = true;
        }

        
        
        
        
        
        
        
        
        
        GameObject obj = GameObject.Find("Map_Manager");
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