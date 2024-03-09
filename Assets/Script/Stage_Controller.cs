using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Stage_Controller : MonoBehaviour
{
    public string Scene_Name;

    private void Awake()
    {
        if (SceneManager.GetActiveScene().name == "Map_test")
            Scene_Name = "Main";
        

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
                while(true)
                {
                    if (controlledObjects != null)
                    {
                        DontDestroyOnLoad(controlledObjects);
                        break;
                    }
                    else
                    {
                        controlledObjects = GameObject.FindGameObjectWithTag("Controlled");
                    }

                }
                
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
