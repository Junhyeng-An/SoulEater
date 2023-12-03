using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hit_Enemy : MonoBehaviour
{
    EnemyController enemyController;
    // Start is called before the first frame update
    void Start()
    {
        enemyController = GetComponentInParent<EnemyController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(enemyController.timer >= 1)
        {
            gameObject.tag = "closehit";
            GetComponent<SpriteRenderer>().color = Color.red;
        }
        else
        {
            gameObject.tag = "Hit_Ready";
            GetComponent<SpriteRenderer>().color = new Color(1, 0.5f, 0.5f);
        }
    }
    private void OnEnable()
    {
        //GetComponent<Collider>().isTrigger = true;
    }
}
