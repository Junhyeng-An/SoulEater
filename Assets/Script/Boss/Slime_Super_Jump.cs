using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime_Super_Jump : MonoBehaviour
{
    private float s_jumpForce = 15f;  // ���� ���� ��
    private float jumpCooldown = 3.1f;  // ���� ���� ��ٿ�
    public GameObject spikePrefab;  // ���� ������
    public Transform spikeSpawnPoint;  // ���� ���� ��ġ
    [HideInInspector]public float spike_damage= 10f;

    private bool isJumping = false;
    private Coroutine Super_pattern;
    void Update()
    {
    }

    IEnumerator SuperJump()
    {
        isJumping = true;

        // ���� �پ�ö󰡱�
        GetComponent<Rigidbody2D>().AddForce(Vector2.up * s_jumpForce, ForceMode2D.Impulse);

        yield return new WaitForSeconds(jumpCooldown);

        // ���� ��� ���� ����
        SoundManager.Instance.Playsfx(SoundManager.SFX.Slime_spike);
        CreateSpike();
        yield return new WaitForSeconds(0.5f); // ���ð� ������ �� 0.5�� ���

        // ���� ����
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
        // ��ũ��Ʈ�� Ȱ��ȭ�Ǿ� ���� ���� �ڷ�ƾ ����
        if (gameObject.activeSelf)
        {
            // ���� �ڷ�ƾ�� �����ϰ� ���ο� �ڷ�ƾ�� ����
            if (Super_pattern != null)
            {
                StopCoroutine(Super_pattern);
            }
            Super_pattern = StartCoroutine(SuperJump());
        }
    }

    void OnDisable()
    {
        // ��ũ��Ʈ�� ��Ȱ��ȭ�Ǿ��� �� �ڷ�ƾ�� ����
        if (Super_pattern != null)
            StopCoroutine(Super_pattern);
    }
}