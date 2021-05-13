using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UISESlider : MonoBehaviour
{
    [SerializeField]
    private Slider slider;
    [SerializeField]
    private TextMeshProUGUI volumeText;

    private void Awake()
    {
        slider.value = SettingsManager.getSE;

        slider.onValueChanged.AddListener(UpdateValue);

        volumeText.text = Mathf.RoundToInt(SettingsManager.getSE * 10).ToString();
    }

    private void UpdateValue(float value)
    {
        volumeText.text = Mathf.RoundToInt(value * 10).ToString();
        SettingsManager.setSE(value);
    }
}
