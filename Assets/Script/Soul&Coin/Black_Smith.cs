using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Black_Smith : MonoBehaviour
{
    public GameObject weapon_Upgrade;
    
    private bool isInTrigger = false;
 
    private void OnTriggerEnter2D(Collider2D other)
    {
        // 플레이어가 트리거에 들어갔을 때 호출되는 함수
        if (other.CompareTag("Player"))
        {
            // 트리거 영역에 들어갔음을 표시
            isInTrigger = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        // 플레이어가 트리거에서 나갔을 때 호출되는 함수
        if (other.CompareTag("Player"))
        {
            // 트리거 영역에서 나갔음을 표시
            isInTrigger = false;
        }
    }

    void Update()
    {
        if (isInTrigger && Input.GetKeyDown(KeyCode.V))
        {
            // 화면을 활성화
            if (weapon_Upgrade != null)
            {
                weapon_Upgrade.SetActive(true);
            }
        }
    }
    
    
}

