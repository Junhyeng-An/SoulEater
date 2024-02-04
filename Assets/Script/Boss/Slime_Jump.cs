using UnityEngine;
using System.Collections;

public class Slime_Jump : MonoBehaviour
{
    private float jumpForce = 10f;  // ��¦ �ٱ� ��

    private GameObject player;
    private Coroutine jump_pattern;
    public Animator jump_animator;
    Vector2 leftVector;
    Vector2 rightVector;

    Vector2 direction;
    void Start()
    {
       jump_animator = GetComponent<Animator>();
    }

    void Update()
    {
        direction = new Vector2(Mathf.Abs(gameObject.transform.position.x - player.transform.position.x),0f);
        if (direction.x > 4) { direction.x = 4; }
        leftVector = -direction;
        rightVector = direction;
    }

    IEnumerator JumpT()
    {

        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");

        if (rb == null)
        {
            Debug.LogError("Rigidbody2D component is missing.");
            yield break; // �ڷ�ƾ ����
        }
        if (player == null)
        {
            Debug.LogError("Player object is missing.");
            yield break; // �ڷ�ƾ ����
        }
        // �������� �̵��� ����


        // Ư�� ���ǿ��� �ݺ��Ǵ� ������ ����ϰų� ���ϴ� Ƚ����ŭ �ݺ�
        while (true)
        {
            // Ư�� �ð� ���� ���
            jump_animator.SetBool("isjump",true);
            Vector2 jumpForceVector;
            if (gameObject.transform.position.x - player.transform.position.x >= 0) { jumpForceVector = leftVector + Vector2.up * jumpForce; }
            else { jumpForceVector = rightVector + Vector2.up * jumpForce; }
            // ����
            rb.AddForce(jumpForceVector, ForceMode2D.Impulse);
            // Ư�� �ð� ���� ���
            jump_animator.SetBool("isjump", false);

            yield return new WaitForSecondsRealtime(3f); // ����: 1�� ���� ���
        }
    }



    void OnEnable()
    {
        // ��ũ��Ʈ�� Ȱ��ȭ�Ǿ� ���� ���� �ڷ�ƾ ����
        if (gameObject.activeSelf)
        {
            // ���� �ڷ�ƾ�� �����ϰ� ���ο� �ڷ�ƾ�� ����
            if (jump_pattern != null)
            {
                StopCoroutine(jump_pattern);
            }
            jump_pattern = StartCoroutine(JumpT());
        }
    }

    void OnDisable()
    {
        // ��ũ��Ʈ�� ��Ȱ��ȭ�Ǿ��� �� �ڷ�ƾ�� ����
        if (jump_pattern != null)
            StopCoroutine(jump_pattern);
    }
}
