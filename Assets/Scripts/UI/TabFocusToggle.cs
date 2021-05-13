using UnityEngine;
using UnityEngine.UI;

public class TabFocusToggle : MonoBehaviour
{
    [SerializeField]
    private TabFocusSlider tabFocusSlider;
    private Toggle toggle;
    [SerializeField]
    private bool x;
    [SerializeField]
    private bool y;

    private void Awake()
    {
        toggle = GetComponent<Toggle>();
        toggle.onValueChanged.AddListener(TabFocusMove);
    }

    private void TabFocusMove(bool isOn)
    {
        if (isOn) tabFocusSlider.TabFocusMove(transform, x, y);
    }
}
