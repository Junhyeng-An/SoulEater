using System.Collections;
using UnityEngine;

public class Laser_Pattern : MonoBehaviour
{
    public GameObject laserPrefab;
    public GameObject Start_pos;
    private float speed = 5f; // 레이저 이동 속도
    [HideInInspector] public float Layzer_Damage = 10f;

    private Coroutine laserCoroutine;  // 레이저 생성 코루틴을 저장할 변수

    void OnEnable()
    {
        // 스크립트가 활성화되어 있을 때만 코루틴 시작
        if (gameObject.activeSelf)
        {
            // 기존 코루틴을 중지하고 새로운 코루틴을 시작
            if (laserCoroutine != null)
                StopCoroutine(laserCoroutine);

            laserCoroutine = StartCoroutine(FireLaserPattern());
        }
    }

    void OnDisable()
    {
        // 스크립트가 비활성화되었을 때 코루틴을 중지
        if (laserCoroutine != null)
            StopCoroutine(laserCoroutine);
    }

    IEnumerator FireLaserPattern()
    {
        while (true)
        {
            GameObject laser = Instantiate(laserPrefab, Start_pos.transform.position, Quaternion.identity);
            LaserMovement laserMovement = laser.AddComponent<LaserMovement>();
            laserMovement.speed = speed;

            yield return new WaitForSeconds(2.5f);

            Destroy(laser); // 레이저를 생성한 후 일정 시간이 지나면 파괴
        }
    }
}

public class LaserMovement : MonoBehaviour
{
    public float speed;

    void Update()
    {
        transform.Translate(Vector3.right * speed * Time.deltaTime);

        if (transform.position.x > 40f)
        {
            Destroy(gameObject);
        }
    }
}

