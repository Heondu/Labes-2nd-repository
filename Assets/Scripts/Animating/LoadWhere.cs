using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class LoadWhere : MonoBehaviour
{
    private static LoadWhere instance;

    [SerializeField]
    private GameObject panel;

    [SerializeField]
    private AudioClip ac;

    [SerializeField]
    private TextMeshProUGUI mapName;

    void Awake()
    {
        if (instance == null) instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == SceneData.loadingScene) return;

        StartCoroutine("OnSceneLoadedCo");
    }

    private IEnumerator OnSceneLoadedCo()
    {
        while (LazyCamera.instance.GetMapData() == null)
        {
            yield return null;
        }

        if (panel != null)
        {
            panel.SetActive(true);
        }
        SoundEffectManager.SoundEffect(ac);
        mapName.text = DataManager.Localization(LazyCamera.instance.GetMapData().GetMapId());
    }
}
