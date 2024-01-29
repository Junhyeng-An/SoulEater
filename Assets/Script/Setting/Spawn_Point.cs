using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Spawn_Point : MonoBehaviour
{
    
    public void Start()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        if(SceneManager.GetActiveScene().name == "Main"  )
            CharacterManager.Instance.PlayerPosition(transform);
        else if(SceneManager.GetActiveScene().name == "Dorf")
            CharacterManager.Instance.PlayerPosition(transform);
        else if (SceneManager.GetActiveScene().name == "Boss")
            CharacterManager.Instance.PlayerPosition(transform);
        else if (SceneManager.GetActiveScene().name == "Boss2")
            CharacterManager.Instance.PlayerPosition(transform);
        else
            CharacterManager.Instance.PlayerPosition(CharacterManager.Instance.spawnPosition);
        
        
    }
}
