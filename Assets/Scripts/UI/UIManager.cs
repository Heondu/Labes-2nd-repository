using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private GameObject inventoryPanel;
    [SerializeField]
    private GameObject[] inventorys;
    [SerializeField]
    private Toggle[] menuToggle;
    [SerializeField]
    private GameObject pause;
    private bool isUIActive = false;

    private void Awake()
    {
        inventoryPanel.SetActive(true);
        for (int i = 0; i < inventorys.Length; i++)
        {
            inventorys[i].gameObject.SetActive(true);
        }
    }

    private void Start()
    {
        for (int i = 1; i < inventorys.Length; i++)
        {
            inventorys[i].gameObject.SetActive(false);
        }
        inventoryPanel.SetActive(false);
    }

    private void Update()
    {
        if (PlayerInput.GetInputMode() == InputMode.normal || PlayerInput.GetInputMode() == InputMode.pause)
        {
            if (Input.GetKeyDown(KeySetting.keys[KeyAction.setting]))
            {
                if (pause.activeSelf == false)
                {
                    PlayerInput.SetInputMode(InputMode.pause);
                    pause.SetActive(true);
                    Time.timeScale = 0;
                }
                else
                {
                    PlayerInput.SetInputMode(InputMode.normal);
                    pause.SetActive(false);
                    Time.timeScale = 1;
                }
                return;
            }
        }
        
        if (PlayerInput.GetInputMode() == InputMode.normal || PlayerInput.GetInputMode() == InputMode.UI)
        {
            if (Input.GetKeyDown(KeySetting.keys[KeyAction.status])) menuToggle[0].isOn = !menuToggle[0].isOn;
            if (Input.GetKeyDown(KeySetting.keys[KeyAction.inventory])) menuToggle[1].isOn = !menuToggle[1].isOn;
            if (Input.GetKeyDown(KeySetting.keys[KeyAction.awaken])) menuToggle[2].isOn = !menuToggle[2].isOn;
            if (Input.GetKeyDown(KeySetting.keys[KeyAction.quest])) menuToggle[3].isOn = !menuToggle[3].isOn;
            if (Input.GetKeyDown(KeySetting.keys[KeyAction.setting]))
            {
                int currentOnToggleIndex = -1;
                for (int i = 0; i < menuToggle.Length; i++)
                {
                    if (menuToggle[i].isOn) currentOnToggleIndex = i;
                }
                if (currentOnToggleIndex == -1) menuToggle[4].isOn = !menuToggle[4].isOn;
                else menuToggle[currentOnToggleIndex].isOn = false;
            }

            IsUIOpen();
        }
    }

    private void IsUIOpen()
    {
        bool flag = false;
        for (int i = 0; i < menuToggle.Length; i++)
        {
            if (menuToggle[i].isOn)
            {
                flag = true;
            }
        }

        if (isUIActive == false && flag == true)
        {
            isUIActive = true;
            PlayerInput.SetInputMode(InputMode.UI);
        }
        else if (isUIActive == true && flag == false)
        {
            isUIActive = false;
            PlayerInput.SetInputMode(InputMode.normal);
        }
    }
    
    public void TimePuase(bool isPause)
    {
        if (isPause) Time.timeScale = 0;
        else Time.timeScale = 1;
    }

    public void OpenSettingUI()
    {
        PlayerInput.SetInputMode(InputMode.UI);
        pause.SetActive(false);
        menuToggle[4].isOn = true;
    }
}
