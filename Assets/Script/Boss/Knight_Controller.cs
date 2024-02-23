using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight_Controller : MonoBehaviour
{
    public Transform player;
    public Animator animator;
    public GameObject hit_area;
    private float moveSpeed = 2.5f;
    public bool isFlipped = false;
    private bool isattack = false;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Boss());
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator Boss()
    {
        while (true)
        {
            // 플레이어와의 거리 계산
            float distanceToPlayer = Vector3.Distance(transform.position, player.position);

            // 거리가 2 이하이면 플레이어를 향해 이동하고 hit_area를 활성화
            if (distanceToPlayer <= 2f)
            {
                // 플레이어 쪽을 바라보도록 설정
                LookAtPlayer();

                // hit_area 활성화
                hit_area.SetActive(true);

                // 1초 대기
                yield return new WaitForSeconds(1f);

                // hit_area 비활성화
                hit_area.SetActive(false);
            }
            else
            {

                // hit_area 비활성화
                hit_area.SetActive(false);

                // 플레이어 쪽으로 이동
                transform.position = Vector3.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
            }

            // 1초 대기 후 다음 턴으로 넘어감
            yield return new WaitForSeconds(1f);
        }
    }
    public void LookAtPlayer()
    {
        Vector3 flipped = transform.localScale;
        flipped.z *= -1;

        if (transform.position.x > player.position.x && isFlipped)
        {
            transform.localScale = flipped;
            transform.Rotate(0f, 180f, 0f);
            isFlipped = false;
        }
        if (transform.position.x < player.position.x && !isFlipped)
        {
            transform.localScale = flipped;
            transform.Rotate(0f, 180f, 0f);
            isFlipped = true;
        }
    }
}
