using UnityEngine;

public class UIClose : MonoBehaviour
{
    private void Update()
    {
        if (PlayerInput.GetInputMode() == InputMode.UI)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                gameObject.SetActive(false);
            }
        }
    }
}
