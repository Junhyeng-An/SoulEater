using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharaterStats : MonoBehaviour
{
    public float HP;
    public float ST;
    GameObject P; //�÷��̾�
    GameObject E; //��

    // Start is called before the first frame update
    void Start()
    {
        P = GameObject.FindWithTag("Player"); //�÷��̾ ã��
        E = GameObject.FindWithTag("Enemy"); //���� ã��
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
                P.GetComponent<CharaterStats>().HP -= 1; //�÷��̾��� �Ǹ� ����
                Debug.Log("���� �¾Ƽ� �ǰ� ����");
            }
        }
        else if (gameObject.tag == "Enemy")
        {
            if (Input.GetMouseButtonDown(1))
            {
                E.GetComponent<CharaterStats>().HP -= 1; //���� �Ǹ� ����
                Debug.Log("���� �¾Ƽ� �ǰ� ����");
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
    }
}
