using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDistory : MonoBehaviour
{
    // Start is called before the first frame update
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.childCount > 0 && transform.GetChild(0).CompareTag("Controlled"))
        {
            // If it is, don't destroy on load
            DontDestroyOnLoad(this.gameObject);
        }
    }
}
