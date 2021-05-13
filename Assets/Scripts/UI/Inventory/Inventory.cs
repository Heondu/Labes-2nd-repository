using UnityEngine;
using UnityEngine.Events;

public class Inventory : MonoBehaviour
{
    [SerializeField]
    protected Notification notification;
    public UnityEvent<bool> onDisable = new UnityEvent<bool>();
    [SerializeField]
    protected SaveFile saveFileName;

    private void OnEnable()
    {
        if (notification == null) return;

        notification.Notify(false);
    }

    private void OnDisable()
    {
        onDisable.Invoke(false);
    }

    protected void OnNotify()
    {
        if (notification == null) return;

        notification.Notify(true);
    }

    public virtual void ChangeSlot(Slot selectedSlotm, Slot targetSlot)
    {

    }
    
    public virtual void UpdateInventory()
    {

    }
}
