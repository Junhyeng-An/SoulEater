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

    [HideInInspector]
    public float Player_MaxHP;
    [HideInInspector]
    public float Player_CurHP;
    [HideInInspector]
    public float Player_MaxST = 10;
    [HideInInspector]
    public float Player_CurST = 10;

    GameObject player;


    void Start()
    {
        Player_CurST = 10;
    }


    void Update()
    {
        player = GameObject.FindGameObjectWithTag("Controlled");
        if (player != null)
        {
            //MaxHP, CurHP of "Controlled".
            Player_MaxHP = player.GetComponent<EnemyController>().MaxHP;
            Player_CurHP = player.GetComponent<EnemyController>().CurHP;

            Player_HP.value = Player_CurHP / Player_MaxHP;
            Player_ST.value = Player_CurST / Player_MaxST;
          

            //if(hp, st <= 0)
            if (Player_CurHP <= 0)
                Player_CurHP = 0;
            if (Player_CurST <= 0)
                Player_CurST = 0;
            //if(hp, st >= max)
            if (Player_CurHP > Player_MaxHP)
                Player_CurHP = Player_MaxHP;
            if (Player_CurST > Player_MaxST)
                Player_CurST = Player_MaxST;

            if (Player_CurST < Player_MaxST)
            {
                float stRecoveryRate = 1f; // 0.1�ʴ� ȸ����
                Player_CurST += stRecoveryRate * Time.deltaTime;
                // ���� ST�� �ִ� ST�� ���� �ʵ��� �����մϴ�.
                //Player_CurST = Mathf.Min(Player_CurST, Player_MaxST);
            }
            pText_hp.text = Mathf.Floor(Player_CurHP) + " / " + Player_MaxHP.ToString(); // ���� ü���� ǥ���մϴ�.
            pText_ST.text = Mathf.Floor(Player_CurST) + " / " + Player_MaxST.ToString(); // ���� ���׹̳��� ǥ���մϴ�.
            Handle();
        }
    }

    void Handle() //hp , st �� ��� �ִϸ��̼�
    {
        Player_HP.value = Mathf.Lerp(Player_HP.value, (float)Player_CurHP / (float)Player_MaxHP, Time.deltaTime * 10);
        Player_ST.value = Mathf.Lerp(Player_ST.value, (float)Player_CurST / (float)Player_MaxST, Time.deltaTime * 10);
    }

    public void Stat(string stat, float value)
    {
        if (stat == "HP")
            Player_CurHP += value;
        if (stat == "ST")
            Player_CurST += value;
    }
}
