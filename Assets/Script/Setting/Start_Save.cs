using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Start_Save : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        DataManager.Instance.SaveData();
        Debug.Log("save");
    }

    // Update is called once per frame
 
}
