using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadWhere : MonoBehaviour
{
    [SerializeField]
    private GameObject panel;

    [SerializeField]
    private AudioClip ac;

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        panel.SetActive(true);
        SoundEffectManager.SoundEffect(ac);
    }
}
