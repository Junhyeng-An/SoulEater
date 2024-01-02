using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Red_Square : MonoBehaviour
{
    public GameObject redSquarePrefab;  // 빨간색 네모 프리팹
    public float timeBeforeFalling = 1.5f;  // 떨어지기 전 대기 시간
    public float timeToLive = 5f;  // 빨간색 네모가 존재하는 시간
    public float Damage;

    private Transform playerTransform;
    private float timeElapsed = 0f; // 경과 시간을 저장하는 변수

    void Start()
    {
        // 플레이어를 찾아서 트랜스폼 저장
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerTransform = player.transform;
        }
        else
        {
            Debug.LogError("플레이어를 찾을 수 없습니다.");
        }
    }

    private void Update()
    {
        // 경과 시간을 누적
        timeElapsed += Time.deltaTime;

        // 일정 시간마다 SpawnRedSquare 함수 호출
        if (timeElapsed >= timeBeforeFalling)
        {
            SpawnRedSquare();
            timeElapsed = 0f; // 경과 시간 초기화
        }
    }

    void SpawnRedSquare()
    {
        if (playerTransform != null) // playerTransform이 null이 아닌 경우에만 실행
        {
            Vector3 playerPosition = playerTransform.position;
            GameObject redSquare = Instantiate(redSquarePrefab, new Vector3(playerPosition.x, playerPosition.y + 8f, playerPosition.z), Quaternion.identity);

            Destroy(redSquare, timeToLive);
        }
    }
}
