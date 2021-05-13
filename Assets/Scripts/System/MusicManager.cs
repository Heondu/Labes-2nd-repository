using UnityEngine;

public class MusicManager : MonoBehaviour
{
    static MusicManager _musicManager;
    private AudioSource soundBar;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    public static MusicManager musicManager
    {
        get
        {
            if (!_musicManager)
            {
                GameObject go;
                if (GameObject.Find("Managers") == null)
                {
                    go = new GameObject("Managers");
                }
                else
                {
                    go = GameObject.Find("Managers");
                }
                _musicManager = go.AddComponent<MusicManager>();
                _musicManager.soundBar = go.AddComponent<AudioSource>();
            }
            return _musicManager;
        }
    }

    /// <summary>
    /// BGM을 재생한다.(현재 재생되는 갯수와 상관 없이 세팅매니저에 따라 음량을 조절한다.)
    /// </summary>
    /// <param name="ac">재생할 오디오 클립</param>
    public static void MusicPlay(AudioClip ac)
    {
        musicManager.soundBar.clip = ac;
        musicManager.soundBar.volume = SettingsManager.getBGM;
        musicManager.soundBar.Play();
        musicManager.soundBar.loop = true;
    }

    public static void SetVolume()
    {
        musicManager.soundBar.volume = SettingsManager.getBGM;
    }

}
