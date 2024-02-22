using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Grid_Portal : MonoBehaviour
{
    
    
    
    private void Awake()
    {
   
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
                
                Change_Scene();
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

    private void Change_Scene()
    {
        if (SceneManager.GetActiveScene().name == "Map1_1")
            LoadingScene.LoadScene("Map_Test");
        if (SceneManager.GetActiveScene().name == "Map_test")
            LoadingScene.LoadScene("Dorf");

        
        
        
        
    }

   



}
