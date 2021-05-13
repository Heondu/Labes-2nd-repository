using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIBGMSlider : MonoBehaviour
{
    [SerializeField]
    private Slider slider;
    [SerializeField]
    private TextMeshProUGUI volumeText;

    private void Awake()
    {
        slider.value = SettingsManager.getBGM;

        slider.onValueChanged.AddListener(UpdateValue);

        volumeText.text = Mathf.RoundToInt(SettingsManager.getBGM * 10).ToString();
    }

    private void UpdateValue(float value)
    {
        volumeText.text = Mathf.RoundToInt(value * 10).ToString();
        SettingsManager.setBGM(value);
    }
}
