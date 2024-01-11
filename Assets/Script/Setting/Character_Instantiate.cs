using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character_Instantiate : MonoBehaviour
{
    public  GameObject Instantiate_Position;
    
    
    public void Awake()
    {
        CharacterManager.Instance.Instantiate_Enemy(Instantiate_Position.transform);
    }
}
