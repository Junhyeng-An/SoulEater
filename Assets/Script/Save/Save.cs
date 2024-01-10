using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Save : MonoBehaviour
{
    
    public void Save_Button()
    {
        
        DataManager.Instance.SaveData();
        Debug.Log("save success");
    
    }

}
