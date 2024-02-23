using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HIt_boss_atk : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.parent.GetComponent<Transform>().rotation.y == 180)
            transform.position = new Vector2(transform.parent.position.x - 1.5f, transform.position.y);
        else
            transform.position = new Vector2(transform.parent.position.x + 1.5f, transform.position.y);
    }
}
