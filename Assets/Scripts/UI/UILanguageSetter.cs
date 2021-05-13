using UnityEngine;
using TMPro;

public class UILanguageSetter : MonoBehaviour
{
    private TMP_Dropdown dropdown;

    private void Awake()
    {
        dropdown = GetComponent<TMP_Dropdown>();

        dropdown.onValueChanged.AddListener(delegate {
            SetLanguage(dropdown);
        });

        dropdown.value = (int)SettingsManager.GetLanguage();
    }

    private void SetLanguage(TMP_Dropdown option)
    {
        SettingsManager.SetLanguage(option.value);
    }
}
