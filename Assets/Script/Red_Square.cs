using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Red_Square : MonoBehaviour
{
    public GameObject redSquarePrefab;  // ������ �׸� ������
    public float timeBeforeFalling = 1.5f;  // �������� �� ��� �ð�
    public float timeToLive = 5f;  // ������ �׸� �����ϴ� �ð�
    public float Damage;

    private Transform playerTransform;
    private float timeElapsed = 0f; // ��� �ð��� �����ϴ� ����

    void Start()
    {
        // �÷��̾ ã�Ƽ� Ʈ������ ����
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerTransform = player.transform;
        }
        else
        {
            Debug.LogError("�÷��̾ ã�� �� �����ϴ�.");
        }
    }

    private void Update()
    {
        // ��� �ð��� ����
        timeElapsed += Time.deltaTime;

        // ���� �ð����� SpawnRedSquare �Լ� ȣ��
        if (timeElapsed >= timeBeforeFalling)
        {
            SpawnRedSquare();
            timeElapsed = 0f; // ��� �ð� �ʱ�ȭ
        }
    }

    void SpawnRedSquare()
    {
        if (playerTransform != null) // playerTransform�� null�� �ƴ� ��쿡�� ����
        {
            Vector3 playerPosition = playerTransform.position;
            GameObject redSquare = Instantiate(redSquarePrefab, new Vector3(playerPosition.x, playerPosition.y + 8f, playerPosition.z), Quaternion.identity);

            Destroy(redSquare, timeToLive);
        }
    }
}
