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
    public static KeyManager instance;

    private void Awake()
    {
        if (instance == null) instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        if (SaveDataManager.instance.loadKeyPreset)
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
        KeySetting.keys.Add(KeyAction.up, KeyCode.W);
        KeySetting.keys.Add(KeyAction.down, KeyCode.S);
        KeySetting.keys.Add(KeyAction.left, KeyCode.A);
        KeySetting.keys.Add(KeyAction.right, KeyCode.D);
        KeySetting.keys.Add(KeyAction.skill1, KeyCode.Mouse0);
        KeySetting.keys.Add(KeyAction.skill2, KeyCode.Mouse1);
        KeySetting.keys.Add(KeyAction.skill3, KeyCode.Q);
        KeySetting.keys.Add(KeyAction.skill4, KeyCode.E);
        KeySetting.keys.Add(KeyAction.skill5, KeyCode.T);
        KeySetting.keys.Add(KeyAction.item1, KeyCode.Alpha1);
        KeySetting.keys.Add(KeyAction.item2, KeyCode.Alpha2);
        KeySetting.keys.Add(KeyAction.item3, KeyCode.Alpha3);
        KeySetting.keys.Add(KeyAction.item4, KeyCode.Alpha4);
        KeySetting.keys.Add(KeyAction.status, KeyCode.U);
        KeySetting.keys.Add(KeyAction.inventory, KeyCode.I);
        KeySetting.keys.Add(KeyAction.awaken, KeyCode.O);
        KeySetting.keys.Add(KeyAction.quest, KeyCode.P);
        //KeySetting.keys.Add(KeyAction.pause, KeyCode.Escape);
        KeySetting.keys.Add(KeyAction.setting, KeyCode.Escape);
        KeySetting.keys.Add(KeyAction.portal, KeyCode.G);
        KeySetting.keys.Add(KeyAction.minimap, KeyCode.M);
    }

    [System.Serializable]
    private class KeyData
    {
        public List<Key> keys = new List<Key>();
    }

    public void SaveKeyPreset()
    {
        if (!SaveDataManager.instance.saveKeyPreset) return;

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

        if (keyData == null) return;

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
