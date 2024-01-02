using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Black_Smith : MonoBehaviour
{
    public GameObject weapon_Upgrade;
    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            if (Input.GetKeyDown(KeyCode.V))
            {
                weapon_Upgrade.SetActive(true);
            }
        }
        
    }
}

