using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static System.Net.Mime.MediaTypeNames;

public class StatController : MonoBehaviour
{
    //GameManger���� ������
    [SerializeField]
    Slider Player_HP;
    [SerializeField]
    TextMeshProUGUI pText_hp;
    [SerializeField]
    Slider Player_ST;
    [SerializeField]
    TextMeshProUGUI pText_ST;

    float Player_MaxHP;
    float Player_CurHP;

    float Player_MaxST = 10;
    float Player_CurST = 10;
    GameObject player;


    void Start()
    {

    }


    void Update()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            //Player ������Ʈ���� Player_MaxHP�� Player_CurHP ���� �����ɴϴ�.
            Player_MaxHP = player.GetComponent<HP_ST>().MaxHP;
            Player_CurHP = player.GetComponent<HP_ST>().CurHP;

            Player_HP.value = Player_CurHP / Player_MaxHP;
            Player_ST.value = Player_CurST / Player_MaxST;
            if (Input.GetKeyDown("p")) //hp ������
            {
                if (Player_CurHP > 0)
                {
                    player.GetComponent<HP_ST>().CurHP -= 10;
                }
            }
            if (Input.GetMouseButtonDown(0)) //st �Ҹ�
            {
                if (Player_CurST > 0)
                {
                    Player_CurST -= 3;
                }
            }
            if (Player_CurHP <= 0 || Player_CurST <= 0) //hp, ���׹̳� ���� ����
            {
                Player_CurST = 0;
                Player_CurHP = 0;
            }

            if (Player_CurST < Player_MaxST)
            {
                float stRecoveryRate = 0.1f; // 0.1�ʴ� ȸ����
                Player_CurST += stRecoveryRate * Time.deltaTime;

                // ���� ST�� �ִ� ST�� ���� �ʵ��� �����մϴ�.
                Player_CurST = Mathf.Min(Player_CurST, Player_MaxST);
            }
            pText_hp.text = Player_CurHP.ToString() + " / " + Player_MaxHP.ToString(); // ���� ü���� ǥ���մϴ�.
            pText_ST.text = Player_CurST.ToString() + " / " + Player_MaxST.ToString(); // ���� ���׹̳��� ǥ���մϴ�.
            Handle();

        }
        
    }

    void Handle() //hp , st �� ��� �ִϸ��̼�
    {
        Player_HP.value = Mathf.Lerp(Player_HP.value, (float)Player_CurHP / (float)Player_MaxHP, Time.deltaTime * 10);
        Player_ST.value = Mathf.Lerp(Player_ST.value, (float)Player_CurST / (float)Player_MaxST, Time.deltaTime * 10);
    }


}
