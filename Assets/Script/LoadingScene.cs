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
    string[] tips; // 여러 팁을 저장할 배열

    private void Start()
    {
        StartCoroutine(LoadScene());
        tips = new string[]
        {
            "칼을 던지는것은 재밌습니다.",
            "TMI : 이 게임은 3개월의 개발기간을 가졌습니다.",
            "TMI : 이 게임은 캡스톤디자인에 의해 개발되었습니다."
            // 필요한 만큼 팁을 추가할 수 있습니다.
        };

        text.text = tips[Random.Range(0, tips.Length)];
    }
    private void Update()
    {

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
        float delayTime = 1.2f; // 로딩 시간을 늘리고 싶은 초 단위의 값

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
                // 여기서 로딩 시간을 늘리기 위해 조건을 추가
                if (timer >= delayTime)
                {
                    progressBar.fillAmount = Mathf.Lerp(progressBar.fillAmount, 1.0f, timer);
                }
                else
                {
                    // 로딩 시간이 아직 끝나지 않았으면 계속 진행
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
