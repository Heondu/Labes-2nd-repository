using UnityEngine;

public class TitleSceneManager : MonoBehaviour
{
    public static bool isNewGame = true;

    public void NewGame()
    {
        isNewGame = true;

        LoadingSceneManager.LoadScene(SceneData.mainScene);
    }

    public void LoadGame()
    {
        isNewGame = false;

        LoadingSceneManager.LoadScene(SceneData.mainScene);
    }
}
