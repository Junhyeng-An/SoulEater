using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Prologue_manager : MonoBehaviour
{
    // 텍스트를 담을 리스트
    public List<TextMeshProUGUI> textObjects = new List<TextMeshProUGUI>();

    //이미지
    public GameObject S_Image;

    // 현재 보여지는 텍스트 인덱스
    private int currentIndex = 0;

    // 마지막으로 마우스를 클릭한 시간
    private float lastClickTime = 0f;

    // 클릭 간격(쿨타임)
    public float clickInterval = 1f;

    // 자동으로 텍스트를 보여줄 간격
    public float autoShowInterval = 5f;

    // 코루틴 참조 변수
    private Coroutine autoShowCoroutine;

    // 페이드 인 속도
    public float fadeInSpeed = 5f;

    private void Start()
    {
        // 텍스트 초기화
        foreach (var textObject in textObjects)
        {
            textObject.color = new Color(textObject.color.r, textObject.color.g, textObject.color.b, 0f);
        }
        // 시작 시 자동으로 첫 번째 텍스트를 보여줌
        autoShowCoroutine = StartCoroutine(AutoShowNextText());
    }

    void Update()
    {
        // 현재 시간
        float currentTime = Time.time;
        if (Input.GetMouseButtonDown(0) && currentIndex >= 7) { SceneManager.LoadScene("Main"); }
        if (currentIndex >= 5) { S_Image.SetActive(true); }
    }

    // 다음 텍스트를 보여주는 함수
    void ShowNextText()
    {
        // 현재 인덱스가 리스트의 범위 내에 있는지 확인
        if (currentIndex < textObjects.Count)
        {
            // 현재 인덱스에 해당하는 텍스트 오브젝트를 활성화
            StartCoroutine(FadeInText(textObjects[currentIndex]));
            // 현재 인덱스를 증가시킴으로써 다음 텍스트를 준비
            currentIndex++;
        }
        else
        {
            // 리스트의 마지막 텍스트까지 모두 보여준 경우
            Debug.Log("모든 텍스트를 보여주었습니다.");
            // 코루틴을 멈춤
            if (autoShowCoroutine != null)
            {
                StopCoroutine(autoShowCoroutine);
            }
        }
    }

    // 자동으로 다음 텍스트를 보여주는 코루틴
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
                // 마우스 클릭 간격 동안 자동으로 텍스트를 보여주지 않음
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


    // 텍스트 페이드 인 효과를 위한 코루틴
    IEnumerator FadeInText(TextMeshProUGUI text)
    {
        textObjects[currentIndex].gameObject.SetActive(true);
        // 현재 알파값
        float alpha = 0f;

        while (alpha < 1f)
        {
            alpha += Time.deltaTime * fadeInSpeed;
            text.color = new Color(text.color.r, text.color.g, text.color.b, alpha);
            yield return null;
        }
    }
}
