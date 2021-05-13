using UnityEngine;

public class UIVFXToggle : MonoBehaviour
{
    private OnOffToggle toggle;
    [SerializeField]
    private GameObject[] vfxs;

    private void Awake()
    {
        toggle = GetComponent<OnOffToggle>();

        toggle.onValueChanged.AddListener(SetVFX);

        toggle.IsOn = SettingsManager.GetVFX();
    }

    private void SetVFX(bool value)
    {
        SettingsManager.SetVFX(value);

        for (int i = 0; i < vfxs.Length; i++)
        {
            vfxs[i].SetActive(value);
        }
    }
}
