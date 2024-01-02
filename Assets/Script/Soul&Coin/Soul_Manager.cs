 using System;
 using System.Collections;
using System.Collections.Generic;
 using TMPro;
 using UnityEngine;

public class Soul_Manager : MonoBehaviour
{
    private int Soul_Count;
    public TextMeshProUGUI textName;

    private void Awake()
    {
        Soul_Count = DataManager.Instance._PlayerData.soul_Count;
    }


    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Soul"))
        {
            Soul_Count = ++DataManager.Instance._PlayerData.soul_Count;
        }
    }

    private void Update()
    {
        Show_Count();
    }

    void Show_Count()
    {
        Soul_Count = DataManager.Instance._PlayerData.soul_Count;
        textName.text = Soul_Count.ToString();
    }
    
    
    
}
