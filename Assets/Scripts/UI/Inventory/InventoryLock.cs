using UnityEngine;

public class InventoryLock : MonoBehaviour
{
    [SerializeField]
    private Slot[] lockSlots;

    private void Start()
    {
        LoadingSceneManager.onLevelWasLoaded.AddListener(CheckForScene);
    }

    private void CheckForScene(string sceneName)
    {
        if (sceneName != SceneData.instance.mainScene)
        {
            Lock(true);
        }
        else
        {
            Lock(false);
        }
    }

    private void Lock(bool value)
    {
        for (int i = 0; i < lockSlots.Length; i++)
        {
            lockSlots[i].isLock = value;
        }
    }
}
