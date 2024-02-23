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
            // �÷��̾���� �Ÿ� ���
            float distanceToPlayer = Vector3.Distance(transform.position, player.position);

            // �Ÿ��� 2 �����̸� �÷��̾ ���� �̵��ϰ� hit_area�� Ȱ��ȭ
            if (distanceToPlayer <= 2f)
            {
                // �÷��̾� ���� �ٶ󺸵��� ����
                LookAtPlayer();

                // hit_area Ȱ��ȭ
                hit_area.SetActive(true);

                // 1�� ���
                yield return new WaitForSeconds(1f);

                // hit_area ��Ȱ��ȭ
                hit_area.SetActive(false);
            }
            else
            {

                // hit_area ��Ȱ��ȭ
                hit_area.SetActive(false);

                // �÷��̾� ������ �̵�
                transform.position = Vector3.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
            }

            // 1�� ��� �� ���� ������ �Ѿ
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
