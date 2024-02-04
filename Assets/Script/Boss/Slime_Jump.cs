using UnityEngine;
using System.Collections;

public class Slime_Jump : MonoBehaviour
{
    private float jumpForce = 10f;  // 폴짝 뛰기 힘

    private GameObject player;
    private Coroutine jump_pattern;
    public Animator jump_animator;
    Vector2 leftVector;
    Vector2 rightVector;

    Vector2 direction;
    void Start()
    {
       jump_animator = GetComponent<Animator>();
    }

    void Update()
    {
        direction = new Vector2(Mathf.Abs(gameObject.transform.position.x - player.transform.position.x),0f);
        if (direction.x > 4) { direction.x = 4; }
        leftVector = -direction;
        rightVector = direction;
    }

    IEnumerator JumpT()
    {

        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");

        if (rb == null)
        {
            Debug.LogError("Rigidbody2D component is missing.");
            yield break; // 코루틴 종료
        }
        if (player == null)
        {
            Debug.LogError("Player object is missing.");
            yield break; // 코루틴 종료
        }
        // 왼쪽으로 이동할 벡터


        // 특정 조건에서 반복되는 루프를 사용하거나 원하는 횟수만큼 반복
        while (true)
        {
            // 특정 시간 동안 대기
            jump_animator.SetBool("isjump",true);
            Vector2 jumpForceVector;
            if (gameObject.transform.position.x - player.transform.position.x >= 0) { jumpForceVector = leftVector + Vector2.up * jumpForce; }
            else { jumpForceVector = rightVector + Vector2.up * jumpForce; }
            // 점프
            rb.AddForce(jumpForceVector, ForceMode2D.Impulse);
            // 특정 시간 동안 대기
            jump_animator.SetBool("isjump", false);

            yield return new WaitForSecondsRealtime(3f); // 예시: 1초 동안 대기
        }
    }



    void OnEnable()
    {
        // 스크립트가 활성화되어 있을 때만 코루틴 시작
        if (gameObject.activeSelf)
        {
            // 기존 코루틴을 중지하고 새로운 코루틴을 시작
            if (jump_pattern != null)
            {
                StopCoroutine(jump_pattern);
            }
            jump_pattern = StartCoroutine(JumpT());
        }
    }

    void OnDisable()
    {
        // 스크립트가 비활성화되었을 때 코루틴을 중지
        if (jump_pattern != null)
            StopCoroutine(jump_pattern);
    }
}
