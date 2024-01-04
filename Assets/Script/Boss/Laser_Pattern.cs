using System.Collections;
using UnityEngine;

public class Laser_Pattern : MonoBehaviour
{
    public GameObject laserPrefab;
    public GameObject Start_pos;
    private float speed = 5f; // 레이저 이동 속도

    [HideInInspector] public float Layzer_Damage = 10f;

    private void Start()
    {
        StartCoroutine(FireLaserPattern());
    }

    IEnumerator FireLaserPattern()
    {
        while (true)
        {
            // 레이저 생성
            GameObject laser = Instantiate(laserPrefab, Start_pos.transform.position, Quaternion.identity);

            // 레이저 이동 속도 설정
            LaserMovement laserMovement = laser.AddComponent<LaserMovement>();
            laserMovement.speed = speed;

            // 다음 레이저까지 대기
            yield return new WaitForSeconds(2.5f);
        }
    }
}

public class LaserMovement : MonoBehaviour
{
    public float speed;

    void Update()
    {
        // 레이저를 오른쪽으로 이동
        transform.Translate(Vector3.right * speed * Time.deltaTime);

        // 레이저가 화면을 벗어나면 파괴
        if (transform.position.x > 20f)
        {
            Destroy(gameObject);
        }
    }
}

