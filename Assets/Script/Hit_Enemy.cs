using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hit_Enemy : MonoBehaviour
{
    EnemyController enemyController;

    bool isFlip = false;

    void Start()
    {
        enemyController = GetComponentInParent<EnemyController>();
    }

    void Update()
    {
        if (transform.parent.Find("Root").Find("Body").GetComponent<SpriteRenderer>().flipX == true)
            transform.position = new Vector2(transform.parent.position.x - 1.5f, transform.position.y);
        else
            transform.position = new Vector2(transform.parent.position.x + 1.5f, transform.position.y);

        if (enemyController.timer >= 1.0 && enemyController.timer <= 1.2)
        {
            gameObject.tag = "closehit";
            GetComponent<SpriteRenderer>().color = new Color(1, 0, 0, 0.5f);
        }
        else if(enemyController.timer < 1.0)
        {
            gameObject.tag = "Hit_Ready";
            GetComponent<SpriteRenderer>().color = new Color(1, 0.5f, 0.5f, 0.5f);
        }
        else
        {
            gameObject.tag = "Hit_Ready";
            GetComponent<SpriteRenderer>().color = new Color(0.5f, 0.5f, 0.5f, 0.5f);
        }
    }
    private void OnEnable()
    {
        //GetComponent<Collider>().isTrigger = true;
    }
}
