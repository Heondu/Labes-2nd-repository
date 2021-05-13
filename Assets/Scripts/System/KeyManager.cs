using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum KeyAction
{
    up, down, left, right, esc, skill1, skill2, skill3, skill4, skill5, item1, item2, item3, item4, interaction, portal, evade, attack, status, inventory, awaken, quest, setting 
}

public static class KeySetting { public static Dictionary<KeyAction, KeyCode> keys = new Dictionary<KeyAction, KeyCode>(); }

public class KeyManager : MonoBehaviour
{

    private void Awake()
    {
        KeysetDefault();
        DontDestroyOnLoad(gameObject);
    }

    public void KeysetDefault()
    {
        KeySetting.keys.Add(KeyAction.up, KeyCode.W);
        KeySetting.keys.Add(KeyAction.down, KeyCode.S);
        KeySetting.keys.Add(KeyAction.left, KeyCode.A);
        KeySetting.keys.Add(KeyAction.right, KeyCode.D);
        KeySetting.keys.Add(KeyAction.esc, KeyCode.Escape);
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
        KeySetting.keys.Add(KeyAction.setting, KeyCode.Escape);
    }
}
