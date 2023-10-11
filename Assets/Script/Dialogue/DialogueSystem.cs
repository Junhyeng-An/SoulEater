using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


public class DialogueSystem : MonoBehaviour
{
    public TextMeshProUGUI textName;
    public TextMeshProUGUI textSentence;

    private Queue<string> sentences = new Queue<string>();
    
    
    public void Begin(Dialogue info)
    {
        sentences.Clear();

        textName.text = info.name;
        
        foreach (var VARIABLE in info.sentences)
        {
            sentences.Enqueue(VARIABLE);
        }
        Next();
        
    }

    private void Next()
    {
        if (sentences.Count == 0)
        {
            End();
        }

        textSentence.text = sentences.Dequeue();
    }

    private void End()
    {
        Debug.Log("end");
    }
    
    
}
