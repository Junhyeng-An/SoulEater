using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime_Super_Jump : MonoBehaviour
{
    private float s_jumpForce = 15f;  // 슈퍼 점프 힘
    private float jumpCooldown = 3.1f;  // 슈퍼 점프 쿨다운
    public GameObject spikePrefab;  // 가시 프리팹
    public Transform spikeSpawnPoint;  // 가시 생성 위치
    [HideInInspector]public float spike_damage= 10f;

    private bool isJumping = false;
    private Coroutine Super_pattern;
    void Update()
    {
    }

    IEnumerator SuperJump()
    {
        isJumping = true;

        // 높게 뛰어올라가기
        GetComponent<Rigidbody2D>().AddForce(Vector2.up * s_jumpForce, ForceMode2D.Impulse);

        yield return new WaitForSeconds(jumpCooldown);

        // 땅에 찍고 가시 생성
        SoundManager.Instance.Playsfx(SoundManager.SFX.Slime_spike);
        CreateSpike();
        yield return new WaitForSeconds(0.5f); // 가시가 생성된 후 0.5초 대기

        // 가시 삭제
        DestroySpike();
        isJumping = false;
    }

    void CreateSpike()
    {
        Instantiate(spikePrefab, spikeSpawnPoint.position, Quaternion.identity);
    }

    void DestroySpike()
    {
        GameObject spike = GameObject.FindGameObjectWithTag("Spike");
        if (spike != null)
        {
            Destroy(spike);
        }
    }
    void OnEnable()
    {
        // 스크립트가 활성화되어 있을 때만 코루틴 시작
        if (gameObject.activeSelf)
        {
            // 기존 코루틴을 중지하고 새로운 코루틴을 시작
            if (Super_pattern != null)
            {
                StopCoroutine(Super_pattern);
            }
            Super_pattern = StartCoroutine(SuperJump());
        }
    }

    void OnDisable()
    {
        // 스크립트가 비활성화되었을 때 코루틴을 중지
        if (Super_pattern != null)
            StopCoroutine(Super_pattern);
    }
}