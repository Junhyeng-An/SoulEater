using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Dungeon_Stage_Controller : MonoBehaviour
{
    private string Scene_Name;
    private Map_Create _mapCreate;

    private void Awake()
    {
        if (SceneManager.GetActiveScene().name == "Map1_1")
            Scene_Name = "Dorf";
        
        
        
        
        
        
        
        
        
        
        
        
        GameObject obj = GameObject.Find("Map_Manager");
        _mapCreate = obj.GetComponent<Map_Create>();
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
        return enemies.Length-_mapCreate.Type_Enemy == 0;
    }

    private void LoadNextScene()
    {
        // Load the scene named "main2"
        SceneManager.LoadScene(Scene_Name);
    }
    
    
    
    
}