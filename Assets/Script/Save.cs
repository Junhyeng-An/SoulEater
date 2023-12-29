using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Save : MonoBehaviour
{
    void OnTriggerStay2D(Collider2D col)
    {
        if (Input.GetKey(KeyCode.V))
        {
            DataManager.Instance.SaveData();
            Debug.Log("save success");
        }

        if (Input.GetKey(KeyCode.B))
        {
            DataManager.Instance.Delete_Save_File();
        }
    }

}
