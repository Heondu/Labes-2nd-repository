using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public bool isUIActive = false;

    private void Start()
    {
        FindObjectOfType<UIManager>().onUIActive.AddListener(IsUIActive);
    }

    private void IsUIActive(bool value)
    {
        isUIActive = value;
    }

    public int GetSkillIndex()
    {
        if (isUIActive) return -1;
        if (Input.GetKey(KeySetting.keys[KeyAction.skill1])) return 0;
        if (Input.GetKey(KeySetting.keys[KeyAction.skill2])) return 1;
        if (Input.GetKey(KeySetting.keys[KeyAction.skill3])) return 2;
        if (Input.GetKey(KeySetting.keys[KeyAction.skill4])) return 3;
        return -1;
    }

    public int GetItemIndex()
    {
        if (isUIActive) return -1;
        if (Input.GetKey(KeySetting.keys[KeyAction.item1])) return 4;
        if (Input.GetKey(KeySetting.keys[KeyAction.item2])) return 5;
        if (Input.GetKey(KeySetting.keys[KeyAction.item3])) return 6;
        if (Input.GetKey(KeySetting.keys[KeyAction.item4])) return 7;
        return -1;
    }

    public Vector3 GetAxis()
    {
        float horizontal = 0;
        float vertical = 0;

        if (!Input.GetKey(KeySetting.keys[KeyAction.up]) || !Input.GetKey(KeySetting.keys[KeyAction.down]))
        {
            if (Input.GetKey(KeySetting.keys[KeyAction.up])) vertical = 1;
            else if (Input.GetKey(KeySetting.keys[KeyAction.down])) vertical = -1;
        }
        if (!Input.GetKey(KeySetting.keys[KeyAction.left]) || !Input.GetKey(KeySetting.keys[KeyAction.right]))
        {
            if (Input.GetKey(KeySetting.keys[KeyAction.left])) horizontal = -1;
            else if (Input.GetKey(KeySetting.keys[KeyAction.right])) horizontal = 1;
        }

        return new Vector3(horizontal, vertical, 0).normalized;
    }
}
