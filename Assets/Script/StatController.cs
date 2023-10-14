using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static System.Net.Mime.MediaTypeNames;

public class StatController : MonoBehaviour
{
    //GameManger에서 관리중
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
        player = GameObject.FindGameObjectWithTag("Controlled");
        if (player != null)
        {
            //MaxHP, CurHP of "Controlled".
            Player_MaxHP = player.GetComponent<EnemyController>().MaxHP;
            Player_CurHP = player.GetComponent<EnemyController>().CurHP;

            Player_HP.value = Player_CurHP / Player_MaxHP;
            Player_ST.value = Player_CurST / Player_MaxST;
            if (Input.GetKeyDown("p")) //hp 데미지
            {
                if (Player_CurHP > 0)
                {
                    player.GetComponent<EnemyController>().CurHP -= 10;
                }
            }
            if (Input.GetMouseButtonDown(0)) //st 소모
            {
                if (Player_CurST > 0)
                {
                    Player_CurST -= 3;
                }
            }

            //hp, 스테미나 음수 방지
            if (Player_CurHP <= 0)
                Player_CurHP = 0;
            if (Player_CurST <= 0)
                Player_CurST = 0;

            if (Player_CurST < Player_MaxST)
            {
                float stRecoveryRate = 1f; // 0.1초당 회복량
                Player_CurST += stRecoveryRate * Time.deltaTime;
                // 현재 ST가 최대 ST를 넘지 않도록 제한합니다.
                //Player_CurST = Mathf.Min(Player_CurST, Player_MaxST);
            }
            pText_hp.text = Mathf.Floor(Player_CurHP) + " / " + Player_MaxHP.ToString(); // 현재 체력을 표시합니다.
            pText_ST.text = Mathf.Floor(Player_CurST) + " / " + Player_MaxST.ToString(); // 현재 스테미나를 표시합니다.
            Handle();
        }
    }

    void Handle() //hp , st 가 닳는 애니메이션
    {
        Player_HP.value = Mathf.Lerp(Player_HP.value, (float)Player_CurHP / (float)Player_MaxHP, Time.deltaTime * 10);
        Player_ST.value = Mathf.Lerp(Player_ST.value, (float)Player_CurST / (float)Player_MaxST, Time.deltaTime * 10);
    }
}
