using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharaterStats : MonoBehaviour
{
    public float HP;
    public float ST;
    GameObject P; //플레이어
    GameObject E; //적

    // Start is called before the first frame update
    void Start()
    {
        P = GameObject.FindWithTag("Player"); //플레이어를 찾음
        E = GameObject.FindWithTag("Enemy"); //적을 찾음
        HP = 10;
        ST = 10;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.tag == "Player")
        {
            if (Input.GetMouseButtonDown(0))
            {
                P.GetComponent<CharaterStats>().HP -= 1; //플레이어의 피를 깎음
                Debug.Log("내가 맞아서 피가 닳음");
            }
        }
        else if (gameObject.tag == "Enemy")
        {
            if (Input.GetMouseButtonDown(1))
            {
                E.GetComponent<CharaterStats>().HP -= 1; //적의 피를 깎음
                Debug.Log("적이 맞아서 피가 닳음");
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
    }
}
