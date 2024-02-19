using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Prologue_manager : MonoBehaviour
{
    // �ؽ�Ʈ�� ���� ����Ʈ
    public List<TextMeshProUGUI> textObjects = new List<TextMeshProUGUI>();

    //�̹���
    public GameObject S_Image;

    // ���� �������� �ؽ�Ʈ �ε���
    private int currentIndex = 0;

    // ���������� ���콺�� Ŭ���� �ð�
    private float lastClickTime = 0f;

    // Ŭ�� ����(��Ÿ��)
    public float clickInterval = 1f;

    // �ڵ����� �ؽ�Ʈ�� ������ ����
    public float autoShowInterval = 5f;

    // �ڷ�ƾ ���� ����
    private Coroutine autoShowCoroutine;

    // ���̵� �� �ӵ�
    public float fadeInSpeed = 5f;

    private void Start()
    {
        // �ؽ�Ʈ �ʱ�ȭ
        foreach (var textObject in textObjects)
        {
            textObject.color = new Color(textObject.color.r, textObject.color.g, textObject.color.b, 0f);
        }
        // ���� �� �ڵ����� ù ��° �ؽ�Ʈ�� ������
        autoShowCoroutine = StartCoroutine(AutoShowNextText());
    }

    void Update()
    {
        // ���� �ð�
        float currentTime = Time.time;

        // ���콺 �Է��� �ް� ��Ÿ���� Ȯ��
        if (Input.GetMouseButtonDown(0) && (currentTime - lastClickTime) > clickInterval)
        {
            ShowNextText();
            // ���콺 Ŭ���� �ð� ������Ʈ
            lastClickTime = currentTime;
        }
        if (Input.GetMouseButtonDown(0) && currentIndex >= 7) { SceneManager.LoadScene("Dorf"); }
        if (currentIndex >= 5) { S_Image.SetActive(true); }
    }

    // ���� �ؽ�Ʈ�� �����ִ� �Լ�
    void ShowNextText()
    {
        // ���� �ε����� ����Ʈ�� ���� ���� �ִ��� Ȯ��
        if (currentIndex < textObjects.Count)
        {
            // ���� �ε����� �ش��ϴ� �ؽ�Ʈ ������Ʈ�� Ȱ��ȭ
            StartCoroutine(FadeInText(textObjects[currentIndex]));
            // ���� �ε����� ������Ŵ���ν� ���� �ؽ�Ʈ�� �غ�
            currentIndex++;
        }
        else
        {
            // ����Ʈ�� ������ �ؽ�Ʈ���� ��� ������ ���
            Debug.Log("��� �ؽ�Ʈ�� �����־����ϴ�.");
            // �ڷ�ƾ�� ����
            if (autoShowCoroutine != null)
            {
                StopCoroutine(autoShowCoroutine);
            }
        }
    }

    // �ڵ����� ���� �ؽ�Ʈ�� �����ִ� �ڷ�ƾ
    IEnumerator AutoShowNextText()
    {
        while (true)
        {
            if (currentIndex == 0)
            {
                ShowNextText();
            }
            if (currentIndex != 0)
            {
                yield return new WaitForSeconds(autoShowInterval);
                // ���콺 Ŭ�� ���� ���� �ڵ����� �ؽ�Ʈ�� �������� ����
                if (Time.time - lastClickTime > clickInterval && currentIndex < 4)
                {
                    ShowNextText();
                }
                if (Time.time - lastClickTime > clickInterval && currentIndex == 4)
                {
                    yield return new WaitForSeconds(4f); // Wait for 2 seconds
                    for (int i = 0; i < 4; i++)
                    {
                        textObjects[i].gameObject.SetActive(false);
                    }
                    ShowNextText();
                }
                if (Time.time - lastClickTime > clickInterval && currentIndex > 4)
                {
                    ShowNextText();
                }
            }
        }
    }


    // �ؽ�Ʈ ���̵� �� ȿ���� ���� �ڷ�ƾ
    IEnumerator FadeInText(TextMeshProUGUI text)
    {
        textObjects[currentIndex].gameObject.SetActive(true);
        // ���� ���İ�
        float alpha = 0f;

        while (alpha < 1f)
        {
            alpha += Time.deltaTime * fadeInSpeed;
            text.color = new Color(text.color.r, text.color.g, text.color.b, alpha);
            yield return null;
        }
    }
}
