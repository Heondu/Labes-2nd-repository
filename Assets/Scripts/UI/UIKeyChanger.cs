using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class UIKeyChanger : MonoBehaviour, IPointerClickHandler
{
    private TextMeshProUGUI keyCodeText;
    [SerializeField]
    private KeyAction keyAction;
    private bool isEditMode = false;

    private void Awake()
    {
        keyCodeText = GetComponent<TextMeshProUGUI>();

        LoadKeyPreset();
    }

    private void OnEnable()
    {
        UpdateKey();
    }

    private void OnDisable()
    {
        UpdateKey();
    }

    private void UpdateKey()
    {
        keyCodeText.text = KeySetting.keys[keyAction].ToString();

        isEditMode = false;

        UIManager.IsStopKeyInput = false;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (isEditMode) return;

        keyCodeText.text = "";

        isEditMode = true;

        UIManager.IsStopKeyInput = true;
    }

    private void Update()
    {
        if (isEditMode)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                UpdateKey();
            }
        }
    }

    private void OnGUI()
    {
        if (isEditMode)
        {
            Event e = Event.current;

            KeyCode keyCode = KeyCode.None;
            if (e.isKey)
            {
                keyCode = e.keyCode;
            }
            if (e.isMouse)
            {
                switch (e.button)
                {
                    case 0: keyCode = KeyCode.Mouse0; break;
                    case 1: keyCode = KeyCode.Mouse1; break;
                }
            }

            if (keyCode != KeyCode.None)
            {
                bool flag = false;
                foreach (KeyAction key in KeySetting.keys.Keys)
                {
                    if (key != keyAction && KeySetting.keys[key] == keyCode)
                    {
                        flag = true;
                    }
                }
                if (flag == false)
                {
                    KeySetting.keys[keyAction] = keyCode;

                    SaveKeyPreset();

                    UpdateKey();
                }
                else
                {
                    StartCoroutine(DisplayMessage("사용 중인 키입니다.", 1f));
                }
            }
        }
    }

    private IEnumerator DisplayMessage(string msg, float duration)
    {
        keyCodeText.text = msg;

        yield return new WaitForSeconds(duration);

        keyCodeText.text = "";
    }

    [System.Serializable]
    private class KeyData
    {
        public List<Key> keys = new List<Key>();
    }

    private void SaveKeyPreset()
    {
        KeyData keyData = new KeyData();

        foreach (KeyAction key in KeySetting.keys.Keys)
        {
            keyData.keys.Add(new Key(key, KeySetting.keys[key]));
        }

        JsonIO.SaveToJson(keyData, SaveDataManager.saveFile[SaveFile.KeyPreset]);
    }

    private void LoadKeyPreset()
    {
        KeyData keyData = JsonIO.LoadFromJson<KeyData>(SaveDataManager.saveFile[SaveFile.KeyPreset]);

        for (int i = 0; i < keyData.keys.Count; i++)
        {
            KeySetting.keys[keyData.keys[i].keyAction] = keyData.keys[i].keyCode;
        }
    }
}

[System.Serializable]
public class Key
{
    public KeyAction keyAction;
    public KeyCode keyCode;

    public Key(KeyAction keyAction, KeyCode keyCode)
    {
        this.keyAction = keyAction;
        this.keyCode = keyCode;
    }
}
