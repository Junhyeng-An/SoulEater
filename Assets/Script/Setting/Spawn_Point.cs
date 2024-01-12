using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn_Point : MonoBehaviour
{
    
    public void Start()
    {
        CharacterManager.Instance.PlayerPosition(transform);
    }
}
