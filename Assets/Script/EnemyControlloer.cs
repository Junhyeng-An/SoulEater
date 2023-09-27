using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class EnemyController : MonoBehaviour
{
    public GameObject Player;
    public enum EnemyType
    {
        Enemy_A,
        Enemy_B,
        Enemy_C,
        Boss_A
    }
    public EnemyType enemyType;

    bool isPlayer = false;
    bool isEnemy = true;
    public void CheckState()
    {
        switch (enemyType)
        {
            case EnemyType.Enemy_A:
                break;
            case EnemyType.Enemy_B:
                break;
            case EnemyType.Enemy_C:
                break;
            case EnemyType.Boss_A:
                break;
            default:
                break;
        }
    }
    // Start is called before the first frame update
    void Awake()
    {

    }
    // Update is called once per frame
    void Update()
    {
        if (isPlayer == true)
        {
            transform.position = Player.transform.position;
            if (Player.GetComponent<PlayerController>().isThrowing == true)
            {
                isPlayer = false;
                GetComponent<CircleCollider2D>().enabled = true;
                GetComponent<Rigidbody2D>().gravityScale = 5;
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.layer == 11 && gameObject.layer == 14) //collid Sword && Disarm state
        {
            if (col.gameObject.GetComponentInParent<PlayerController>().isThrowing == true)
            {
                col.gameObject.GetComponentInParent<PlayerController>().isThrowing = false;
                isPlayer = true;
                GetComponent<CircleCollider2D>().enabled = false;
                GetComponent<Rigidbody2D>().gravityScale = 0;
                gameObject.layer = 13; //Controlled
            }
        }
        else if(col.gameObject.layer == 11 && gameObject.layer == 12) //collid Sword && Enemy state
        {
            //need damege scirpt
        }
    }
    private void OnTriggerExit2D(Collider2D col)
    {
        if (gameObject.layer == 13 && col.gameObject.GetComponentInParent<PlayerController>().isThrowing == true) //Controlled
        {
            gameObject.layer = 14; //Disarm
        }
    }
}
