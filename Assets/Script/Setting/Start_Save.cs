using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Start_Save : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        DataManager.Instance.SaveData();
    }

    // Update is called once per frame
 
}
