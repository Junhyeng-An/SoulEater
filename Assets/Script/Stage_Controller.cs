using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Stage_Controller : MonoBehaviour
{
    public string Scene_Name;

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the object entering the portal is the player
        if (other.CompareTag("Player"))
        {
            // Check if there are no enemies in the scene
            if (NoEnemiesInScene())
            {
                // Load the next scene
                LoadingScene.LoadScene("Stage_2");
                //LoadNextScene();
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
        Debug.Log(enemies);
        return enemies.Length == 0;
    }

    private void LoadNextScene()
    {
        // Load the scene named "main2"
        SceneManager.LoadScene(Scene_Name);
    }
}
