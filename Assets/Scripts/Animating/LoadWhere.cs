using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class LoadWhere : MonoBehaviour
{
    [SerializeField]
    private GameObject panel;

    [SerializeField]
    private AudioClip ac;

    [SerializeField]
    private TextMeshProUGUI mapName;

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == SceneData.loadingScene) return;

        if (panel != null)
        {
            panel.SetActive(true);
        }
        SoundEffectManager.SoundEffect(ac);
        MapData mapData = FindObjectOfType<MapData>();
        if (mapData != null)
        {
            mapName.text = DataManager.Localization(mapData.GetMapId());
        }
    }
}
