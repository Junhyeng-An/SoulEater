using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Spum_Animation : MonoBehaviour
{
    private Animator animator;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");

        if (gameObject.CompareTag("Controlled"))
        {

            // 움직임이 감지되면 애니메이션을 재생하고, 회전도 수행
            if (moveX != 0 || moveY != 0)
            {
                // 애니메이션 재생
                animator.SetFloat("RunState", 0.5f);

                // 오브젝트 회전
                if (moveX > 0)
                {
                    // moveX가 양수일 때 (오른쪽으로 이동 중)
                    transform.rotation = Quaternion.Euler(0f, 180f, 0f);
                }
                else if (moveX < 0)
                {
                    // moveX가 음수일 때 (왼쪽으로 이동 중)
                    transform.rotation = Quaternion.Euler(0f, 0f, 0f);
                }
            }
            else
            {
                // 움직임이 감지되지 않으면 애니메이션을 멈추고 회전을 초기화
                animator.SetFloat("RunState", 0f);
            }
        }
    }
}