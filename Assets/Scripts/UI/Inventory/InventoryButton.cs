using UnityEngine;
using UnityEngine.UI;

public class InventoryButton : MonoBehaviour
{
    [SerializeField]
    private ScrollRect scrollRect;
    [SerializeField]
    private GameObject inventory;

    public void SetInventory(bool isOn)
    {
        inventory.SetActive(isOn);
        if (isOn) scrollRect.content = inventory.GetComponent<RectTransform>();
    }
}
