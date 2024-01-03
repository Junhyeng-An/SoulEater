using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Save : MonoBehaviour
{
    
    public void Save_Button()
    {
        if (Input.GetKey(KeyCode.V))
        {
            DataManager.Instance.SaveData();
            Debug.Log("save success");
        }
    }

}
