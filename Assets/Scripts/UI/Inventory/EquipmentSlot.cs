using UnityEngine;
using UnityEngine.EventSystems;

public class EquipmentSlot : MonoBehaviour, IBeginDragHandler
{
    private Item item = null;
    private Slot slot;
    [SerializeField]
    private GameObject icon;

    private void Start()
    {
        InventoryManager.instance.onSlotChanged.AddListener(EquipCheck);
        slot = GetComponent<Slot>();
    }

    private void EquipCheck()
    {
        if (slot.item != null || slot.skill != null) slot.isEquip = true;
        DisableIconImage();

        if (slot.useType != UseType.weapon && slot.useType != UseType.equipment) return;

        if (item != slot.item)
        {
            if (slot.item != null)
            {
                if (item != null)
                {
                    InventoryManager.instance.onItemUnequip.Invoke(item);
                }
                if (InventoryManager.instance.onItemEquip != null)
                {
                    InventoryManager.instance.onItemEquip.Invoke(slot.item);
                }
            }
            else if (slot.item == null)
            {
                if (item != null)
                {
                    InventoryManager.instance.onItemUnequip.Invoke(item);
                }
            }

            item = slot.item;
        }
    }

    private void DisableIconImage()
    {
        if (slot.item != null || slot.skill != null)
        {
            icon.SetActive(false);
        }
        else
        {
            icon.SetActive(true);
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (slot.isLock) return;
        if (slot.item != null || slot.skill != null)
        {
            icon.SetActive(true);
        }
    }
}
