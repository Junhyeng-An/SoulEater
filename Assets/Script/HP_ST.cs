using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HP_ST : MonoBehaviour
{
    public float CurHP;
    public float MaxHP;
    void Start()
    {
        
    }


    void Update()
    {
        //if(gameObject.tag == "Enemy")
        //{
        //   HP_Bar.transform.position = transform.root.position + new Vector3(0,2,0);
        //   HP_Bar.SetActive(true);
        //   HP_Slider.value = CurHP / MaxHP;
        //    hp_text.text = CurHP.ToString() + " / " + MaxHP.ToString(); // 현재 체력을 표시합니다.
        //}
        //else
        //{
        //    HP_Bar.SetActive(false);
        //}
    }
}
