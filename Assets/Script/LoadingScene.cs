using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
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
        
        if(DataManager.Instance._Sound_Volume.Language == 0)
            tips = new string[]
            {
                "Disarming enemies drop souls.",
                "If you want to enhance your weapons, find the BlackSmith in Village.",
                "Do you know that characters have different health and endurance?",
                "You can control disarmed enemies by throwing your sword at them.",
                "Be cautious, if you fail to enter an enemy's body, you will die.",
                "Upon reaching the village, your progress will be automatically saved, and you can also save manually through statues.",
                "You must gather as many souls as possible, despite the risks involved.",
                "Characters possess unique skills based on their appearance.",
                "You can acquire new abilities through souls.",
            };
        
        
        
        
        
        if(DataManager.Instance._Sound_Volume.Language == 1 ) 
            tips = new string[]
            {
                "적을 무장해제 시키면 소울을 얻을 수 있습니다.",
                "무기를 강화하고 싶다면 마을의 대장장이를 찾으세요.",
                "캐릭터별로 체력과 체간이 다르다는 사실 알고 계신가요.",
                "검(당신)을 던져 무장해제된 적을 컨트롤 할 수 있습니다.",
                "주의하세요 적의 몸에 들어가지 못하면 당신은 죽습니다.",
                "마을에 도착시 자동 저장되며 석상을 통해 수동으로도 저장 할 수 있습니다.",
                "최대한 많은 소울을 얻어야 합니다, 위험이 따르지만 말이죠.",
                "캐릭터의 모습에 따라 고유한 스킬이 존재합니다.",
                "소울을 통해 새로운 특성을 얻을 수 있습니다."
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
