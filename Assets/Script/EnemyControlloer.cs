using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build.Content;
using UnityEngine;
public class EnemyController : MonoBehaviour
{
    public GameObject Player;
    public float CurHP; 
    public float MaxHP;

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
                CurHP = 100;
                MaxHP = 100;
                break;
            case EnemyType.Enemy_B:
                CurHP = 80;
                MaxHP = 80;
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
        CheckState();
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
        if (col.gameObject.tag == "Sword" && gameObject.tag == "Disarmed")
        {
            if (col.gameObject.GetComponentInParent<PlayerController>().isThrowing == true)
            {
                col.gameObject.GetComponentInParent<PlayerController>().isThrowing = false;
                isPlayer = true;
                GetComponent<CircleCollider2D>().enabled = false;
                GetComponent<Rigidbody2D>().gravityScale = 0;
                gameObject.tag = "Controlled";
            }
        }
        else if(col.gameObject.tag == "Sword" && gameObject.layer == 12) //collid Sword && Enemy state
        {
            if (gameObject.tag == "Disarmed")
            {
                CurHP -= 10;
            }
        }
    }
    private void OnTriggerExit2D(Collider2D col)
    {
        if (gameObject.tag == "Controlled" && col.gameObject.GetComponentInParent<PlayerController>().isThrowing == true)
        {
            gameObject.tag = "Disarmed"; //Disarm
        }
    }
}
