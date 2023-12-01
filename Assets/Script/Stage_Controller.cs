using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage_Controller : MonoBehaviour
{


    // Update is called once per frame
    void Update()
    {
        int enemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length; 
        Debug.Log("적의 수: " + enemyCount);
    }
}
