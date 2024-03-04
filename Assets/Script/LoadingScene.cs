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
    string[] tips; // ���� ���� ������ �迭

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
                "���� �������� ��Ű�� �ҿ��� ���� �� �ֽ��ϴ�.",
                "���⸦ ��ȭ�ϰ� �ʹٸ� ������ �������̸� ã������.",
                "ĳ���ͺ��� ü�°� ü���� �ٸ��ٴ� ��� �˰� ��Ű���.",
                "��(���)�� ���� ���������� ���� ��Ʈ�� �� �� �ֽ��ϴ�.",
                "�����ϼ��� ���� ���� ���� ���ϸ� ����� �׽��ϴ�.",
                "������ ������ �ڵ� ����Ǹ� ������ ���� �������ε� ���� �� �� �ֽ��ϴ�.",
                "�ִ��� ���� �ҿ��� ���� �մϴ�, ������ �������� ������.",
                "ĳ������ ����� ���� ������ ��ų�� �����մϴ�.",
                "�ҿ��� ���� ���ο� Ư���� ���� �� �ֽ��ϴ�."
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
        float delayTime = 1.2f; // �ε� �ð��� �ø��� ���� �� ������ ��

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
