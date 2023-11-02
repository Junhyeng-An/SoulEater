 using System;
 using System.Collections;
using System.Collections.Generic;
 using TMPro;
 using UnityEngine;

public class Soul_Manager : MonoBehaviour
{
    public int Soul_Count = 0;
    public TextMeshProUGUI textName;
    
    
    
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Soul"))
        {
            Soul_Count++;
        }
    }

    private void Update()
    {
        Show_Count();
    }

    void Show_Count()
    {
        textName.text = Soul_Count.ToString();
    }
    
    
    
}
