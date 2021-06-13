using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Language { english, korean }

public class SettingsManager : MonoBehaviour
{
    static SettingsManager _settingsManager;

    private Language language = Language.korean;
    private float BGM_volume;
    private float SE_volume;
    private bool VFX_toggle = true;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    public static SettingsManager settingsManager
    {
        get
        {
            if (!_settingsManager)
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
                _settingsManager = go.AddComponent<SettingsManager>();
                _settingsManager.Resetting();
            }
            return _settingsManager;
        }
    }

    public void Resetting()
    {
        if (PlayerPrefs.HasKey("Language"))
        {
            language = (Language)PlayerPrefs.GetInt("Language");
        }
        else
        {
            language = Language.korean;
        }

        if (PlayerPrefs.HasKey("BGM"))
        {
            BGM_volume = PlayerPrefs.GetFloat("BGM");//PlayerPref에 옵션 정보가 있으면 로드하고 없으면 0.7로 설정
        }
        else
        {
            BGM_volume = 0.7f;
        }

        if (PlayerPrefs.HasKey("SE"))
        {
            SE_volume = PlayerPrefs.GetFloat("SE");
        }
        else
        {
            SE_volume = 0.7f;
        }

        if (PlayerPrefs.HasKey("VFX"))
        {
            VFX_toggle = System.Convert.ToBoolean(PlayerPrefs.GetInt("VFX"));
        }
        else
        {
            VFX_toggle = true;
        }
    }

    public static void SaveSettings()
    {
        PlayerPrefs.SetInt("Language", (int)settingsManager.language);
        PlayerPrefs.SetFloat("BGM", settingsManager.BGM_volume);
        PlayerPrefs.SetFloat("SE", settingsManager.SE_volume);
        PlayerPrefs.SetInt("VFX", System.Convert.ToInt32(settingsManager.VFX_toggle));
    }


    public static void Reset_now()
    {
        settingsManager.Resetting();
    }

    public static float getBGM
    {
        get
        {
            return settingsManager.BGM_volume;
        }
    }
    public static void setBGM(float value)
    {
        settingsManager.BGM_volume = value;
        SaveSettings();
    }
    public static float getSE
    {
        get
        {
            return settingsManager.SE_volume;
        }
    }
    public static void setSE(float value)
    {
        settingsManager.SE_volume = value;
        SaveSettings();
    }

    public static Language GetLanguage()
    {
        return settingsManager.language;
    }
    public static void SetLanguage(int value)
    {
        settingsManager.language = (Language)value;
        SaveSettings();
    }

    public static bool GetVFX()
    {
        return settingsManager.VFX_toggle;
    }

    public static void SetVFX(bool value)
    {
        settingsManager.VFX_toggle = value;
        SaveSettings();
    }
}
