using UnityEngine;
using TMPro;

public class MinimapController : MonoBehaviour
{
    [SerializeField]
    private Camera minimapCam;
    [SerializeField]
    private RectTransform minimap;

    [SerializeField]
    private float maximizeSize = 2;

    [SerializeField]
    private TextMeshProUGUI mapName;

    private float originCamSize;
    private Vector2 originMinimapSize;

    private bool isMaximize = false;

    private void Start()
    {
        originCamSize = minimapCam.orthographicSize;
        originMinimapSize = minimap.rect.size;

        LazyCamera.instance.onMapDataChanged.AddListener(UpdateMapName);
    }
    
    private void Update()
    {
        if (IsMinimapKeyInput())
        {
            isMaximize = !isMaximize;

            Maximize(isMaximize);
        }
    }

    private bool IsMinimapKeyInput()
    {
        if (PlayerInput.instance.GetInputMode() != InputMode.normal) return false;
        if (Input.GetKeyDown(KeySetting.keys[KeyAction.minimap])) return true;
        return false;
    }

    private void Maximize(bool value)
    {
        if (value)
        {
            minimapCam.orthographicSize = originCamSize * maximizeSize;
            minimap.sizeDelta = originMinimapSize * maximizeSize;
        }
        else
        {
            minimapCam.orthographicSize = originCamSize;
            minimap.sizeDelta = originMinimapSize;
        }
    }

    private void UpdateMapName()
    {
        mapName.text = DataManager.Localization(LazyCamera.instance.GetMapData().GetMapId());
    }
}
