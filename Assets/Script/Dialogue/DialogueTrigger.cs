using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;


public class DialogueTrigger : MonoBehaviour
{
    public Dialogue info;
    public GameObject dialogue;
    private bool isTrigger = false;
    public void Trigger()
    {
        var system = FindObjectOfType<DialogueSystem>();
        system.Begin(info);
        
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isTrigger = true;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C) && isTrigger == true)
        {
            dialogue.SetActive(true);
            Trigger();
            
        }
    }


    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            dialogue.SetActive(false);
            isTrigger = false;
        }
    }
}
