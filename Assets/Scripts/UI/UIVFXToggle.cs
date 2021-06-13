using UnityEngine;

public class UIVFXToggle : MonoBehaviour
{
    private OnOffToggle toggle;

    private void Awake()
    {
        toggle = GetComponent<OnOffToggle>();

        toggle.onValueChanged.AddListener(SetVFX);

        toggle.IsOn = SettingsManager.GetVFX();
    }

    private void SetVFX(bool value)
    {
        SettingsManager.SetVFX(value);
    }
}
