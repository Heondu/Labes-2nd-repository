using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class LoadingSceneManager : MonoBehaviour
{
    public static string nextScene = "";
    [SerializeField] TextMeshProUGUI progressText;

    private void Start()
    {
        StartCoroutine(LoadScene());
    }

    public static void LoadScene(string sceneName) 
    { 
        nextScene = sceneName;
        SceneManager.LoadScene("LoadingScene"); 
    }

    IEnumerator LoadScene()
    {
        PlayerInput.instance.SetInputMode(InputMode.pause);
        yield return null;
        AsyncOperation op = SceneManager.LoadSceneAsync(nextScene); 
        op.allowSceneActivation = false;
        float timer = 0.0f;
        while (!op.isDone)
        {
            yield return null; 
            timer += Time.deltaTime; 
            if (op.progress < 0.9f) 
            {
                progressText.text = $"{(int)(op.progress * 100)}%";
            } 
            else
            {
                progressText.text = "100%";
                op.allowSceneActivation = true;
                PlayerInput.instance.SetInputMode(InputMode.normal);
                yield break;
            }
        } 
    }
}
