using System.Collections;
using UnityEngine;

public class Laser_Pattern : MonoBehaviour
{
    public GameObject laserPrefab;
    public GameObject Start_pos;
    private float speed = 5f; // ������ �̵� �ӵ�

    [HideInInspector] public float Layzer_Damage = 10f;

    private void Start()
    {
        StartCoroutine(FireLaserPattern());
    }

    IEnumerator FireLaserPattern()
    {
        while (true)
        {
            // ������ ����
            GameObject laser = Instantiate(laserPrefab, Start_pos.transform.position, Quaternion.identity);

            // ������ �̵� �ӵ� ����
            LaserMovement laserMovement = laser.AddComponent<LaserMovement>();
            laserMovement.speed = speed;

            // ���� ���������� ���
            yield return new WaitForSeconds(2.5f);
        }
    }
}

public class LaserMovement : MonoBehaviour
{
    public float speed;

    void Update()
    {
        // �������� ���������� �̵�
        transform.Translate(Vector3.right * speed * Time.deltaTime);

        // �������� ȭ���� ����� �ı�
        if (transform.position.x > 40f)
        {
            Destroy(gameObject);
        }
    }
}

