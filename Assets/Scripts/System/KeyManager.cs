using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum KeyAction
{
    up, down, left, right,
    skill1, skill2, skill3, skill4, skill5,
    item1, item2, item3, item4,
    portal, evade,
    status, inventory, awaken, quest, pause, setting, minimap
}

public static class KeySetting { public static Dictionary<KeyAction, KeyCode> keys = new Dictionary<KeyAction, KeyCode>(); }

public class KeyManager : MonoBehaviour
{
    private static KeyManager instance;

    private void Awake()
    {
        if (instance == null) instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        if (SaveDataManager.instance == null || SaveDataManager.instance.loadKeyPreset)
        {
            LoadKeyPreset();
        }
        else
        {
            KeysetDefault();
        }
    }

    public void KeysetDefault()
    {
        KeySetting.keys[KeyAction.up] = KeyCode.W;
        KeySetting.keys[KeyAction.down] = KeyCode.S;
        KeySetting.keys[KeyAction.left] = KeyCode.A;
        KeySetting.keys[KeyAction.right] = KeyCode.D;
        KeySetting.keys[KeyAction.skill1] = KeyCode.Mouse0;
        KeySetting.keys[KeyAction.skill2] = KeyCode.Mouse1;
        KeySetting.keys[KeyAction.skill3] = KeyCode.Q;
        KeySetting.keys[KeyAction.skill4] = KeyCode.E;
        KeySetting.keys[KeyAction.skill5] = KeyCode.T;
        KeySetting.keys[KeyAction.item1] = KeyCode.Alpha1;
        KeySetting.keys[KeyAction.item2] = KeyCode.Alpha2;
        KeySetting.keys[KeyAction.item3] = KeyCode.Alpha3;
        KeySetting.keys[KeyAction.item4] = KeyCode.Alpha4;
        KeySetting.keys[KeyAction.status] = KeyCode.U;
        KeySetting.keys[KeyAction.inventory] = KeyCode.I;
        KeySetting.keys[KeyAction.awaken] = KeyCode.O;
        KeySetting.keys[KeyAction.quest] = KeyCode.P;
        KeySetting.keys[KeyAction.pause] = KeyCode.Escape;
        KeySetting.keys[KeyAction.setting] = KeyCode.Escape;
        KeySetting.keys[KeyAction.portal] = KeyCode.G;
        KeySetting.keys[KeyAction.minimap] = KeyCode.M;
    }

    [System.Serializable]
    private class KeyData
    {
        public List<Key> keys = new List<Key>();
    }

    public static void SaveKeyPreset()
    {
        if (!SaveDataManager.instance.saveKeyPreset) return;

        KeyData keyData = new KeyData();

        foreach (KeyAction key in KeySetting.keys.Keys)
        {
            keyData.keys.Add(new Key(key, KeySetting.keys[key]));
        }

        JsonIO.SaveToJson(keyData, SaveDataManager.saveFile[SaveFile.KeyPreset]);
    }

    private static void LoadKeyPreset()
    {
        KeyData keyData = JsonIO.LoadFromJson<KeyData>(SaveDataManager.saveFile[SaveFile.KeyPreset]);

        if (keyData == null)
        {
            instance.KeysetDefault();
            return;
        }

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
