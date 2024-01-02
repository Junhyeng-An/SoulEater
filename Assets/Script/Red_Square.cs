using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Red_Square : MonoBehaviour
{
    public GameObject redSquarePrefab;  // ������ �׸� ������
    public float timeBeforeFalling = 1.5f;  // �������� �� ��� �ð�
    public float timeToLive = 5f;  // ������ �׸� �����ϴ� �ð�

    private Transform playerTransform;  

    void Start()
    {
        // �÷��̾ ã�Ƽ� Ʈ������ ����
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerTransform = player.transform;
            InvokeRepeating("SpawnRedSquare", 0f, timeBeforeFalling);
        }
        else
        {
            Debug.LogError("�÷��̾ ã�� �� �����ϴ�.");
        }
    }

    void SpawnRedSquare()
    {
        Vector3 playerPosition = playerTransform.position;
        GameObject redSquare = Instantiate(redSquarePrefab, new Vector3(playerPosition.x, playerPosition.y + 8f, playerPosition.z), Quaternion.identity);

        Destroy(redSquare, timeToLive);
    }

}
