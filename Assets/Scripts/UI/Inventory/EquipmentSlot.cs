using UnityEngine;
using UnityEngine.EventSystems;

public class EquipmentSlot : MonoBehaviour, IBeginDragHandler
{
    private Item item = null;
    private Slot slot;
    [SerializeField]
    private GameObject icon;

    private void Awake()
    {
        InventoryManager.instance.onSlotChangedCallback += EquipCheck;
        InventoryManager.instance.onSlotChangedCallback += DisableIconImage;
        slot = GetComponent<Slot>();
    }

    private void EquipCheck()
    {
        if (slot.item != null || slot.skill != null) slot.isEquip = true;

        if (slot.useType != UseType.weapon && slot.useType != UseType.equipment) return;

        if (item != slot.item)
        {
            if (slot.item != null)
            {
                if (item != null)
                {
                    InventoryManager.instance.onItemUnequipCallback.Invoke(item);
                }
                if (InventoryManager.instance.onItemEquipCallback != null)
                {
                    InventoryManager.instance.onItemEquipCallback.Invoke(slot.item);
                }
            }
            else if (slot.item == null)
            {
                if (item != null)
                {
                    InventoryManager.instance.onItemUnequipCallback.Invoke(item);
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
        else icon.SetActive(true);
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
