using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class LoadingScene : MonoBehaviour
{
    public static string nextScene;
    [SerializeField] Image progressBar;
    public TextMeshProUGUI text;

    private void Start()
    {
        StartCoroutine(LoadScene());
    }
    private void Update()
    {
        text.text = ("*This game is the next game for capstone design");
    }
    public static void LoadScene(string sceneName)
    {
        nextScene = sceneName;
        SceneManager.LoadScene("Loading");
    }

    IEnumerator LoadScene()
    {
        yield return null;
        AsyncOperation op = SceneManager.LoadSceneAsync(nextScene);
        op.allowSceneActivation = false;
        float timer = 0.0f;
        float delayTime = 2.0f; // �ε� �ð��� �ø��� ���� �� ������ ��

        while (!op.isDone)
        {
            yield return null;
            timer += Time.deltaTime;
            if (op.progress < 0.9f)
            {
                progressBar.fillAmount = Mathf.Lerp(progressBar.fillAmount, op.progress, timer);
                if (progressBar.fillAmount >= op.progress)
                {
                    timer = 0f;
                }
            }
            else
            {
                // ���⼭ �ε� �ð��� �ø��� ���� ������ �߰�
                if (timer >= delayTime)
                {
                    progressBar.fillAmount = Mathf.Lerp(progressBar.fillAmount, 1.0f, timer);
                }
                else
                {
                    // �ε� �ð��� ���� ������ �ʾ����� ��� ����
                    continue;
                }

                if (progressBar.fillAmount == 1.0f)
                {
                    op.allowSceneActivation = true;
                    yield break;
                }
            }
        }
    }
}
