using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight_Controller : MonoBehaviour
{
    public Transform player; // 플레이어의 위치를 저장할 변수
    public Animator animator; // 애니메이터 컴포넌트를 저장할 변수
    public GameObject attack_area; // 공격 영역을 나타내는 게임 오브젝트
    private float moveSpeed = 2.5f; // 이동 속도
    public bool isFlipped = false; // 좌우 방향을 나타내는 변수
    private bool isAttacking = false; // 공격 중인지 여부를 나타내는 변수

    // Update is called once per frame
    void Update()
    {
        // 플레이어와의 거리를 확인
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        // 플레이어가 공격 범위 내에 있고 기사가 아직 공격 중이 아닌 경우
        if (distanceToPlayer <= 2f && !isAttacking)
        {
            isAttacking = true;
            AttackPlayer(); // 플레이어를 공격
        }
        else
        {
            isAttacking = false;
            // 플레이어가 공격 범위 내에 없는 경우 플레이어 쪽으로 이동
            if (player.position.x > transform.position.x)
            {
                isFlipped = false;
                transform.localScale = new Vector3(1, 1, 1);
            }
            else
            {
                isFlipped = true;
                transform.localScale = new Vector3(-1, 1, 1);
            }
            LookAtPlayer(); // 플레이어를 바라봄
            RunBoss(); // 플레이어 쪽으로 이동
        }
    }

    // 플레이어를 바라보도록 회전시키는 함수
    public void LookAtPlayer()
    {
        Vector3 direction = player.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, moveSpeed * Time.deltaTime);
    }

    // 플레이어를 공격하는 함수
    public void AttackPlayer()
    {
        // 공격 영역 활성화
        attack_area.SetActive(true);
        // 공격 애니메이션 시작
        animator.SetTrigger("Attack");
        // 애니메이션 완료 후 일정 시간 후에 공격 영역 비활성화
        StartCoroutine(DisableAttackArea());
    }

    // 공격 영역을 비활성화하는 코루틴 함수
    IEnumerator DisableAttackArea()
    {
        yield return new WaitForSeconds(0.5f); // 짧은 시간 동안 대기
        // 공격 영역 비활성화
        attack_area.SetActive(false);
    }

    // 플레이어 쪽으로 이동하는 함수
    public void RunBoss()
    {
        // 플레이어 쪽으로 일정 속도로 이동
        transform.position = Vector2.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
    }
}

