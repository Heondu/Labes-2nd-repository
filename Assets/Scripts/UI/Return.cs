using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Return : MonoBehaviour
{
    private TextMeshProUGUI keycodeText;

    [SerializeField]
    private UIKeyChanger keyChanger;

    private void Start()
    {
        keycodeText = transform.Find("TextKeycode").GetComponent<TextMeshProUGUI>();
        UpdateKeyCode();

        keyChanger.onKeyChanged.AddListener(UpdateKeyCode);
    }

    private void UpdateKeyCode()
    {
        keycodeText.text = KeySetting.keys[KeyAction.portal].ToString();
    }

    private void Update()
    {
        if (IsReturnKeyInput())
        {
            if (SceneManager.GetActiveScene().name == SceneData.instance.mainScene) return;
            if (SceneManager.GetActiveScene().name == SceneData.instance.loadingScene) return;
            {
                LoadingSceneManager.LoadScene(SceneData.instance.mainScene);
            }
        }
    }

    private bool IsReturnKeyInput()
    {
        if (PlayerInput.instance.GetInputMode() != InputMode.normal) return false;
        if (Input.GetKeyDown(KeySetting.keys[KeyAction.portal])) return true;
        return false;
    }
}