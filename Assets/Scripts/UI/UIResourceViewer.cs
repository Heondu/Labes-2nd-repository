using UnityEngine;
using TMPro;

public class UIResourceViewer : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI gold;
    [SerializeField]
    private TextMeshProUGUI ore;
    [SerializeField]
    private TextMeshProUGUI leaf;

    private void Update()
    {
        gold.text = InventoryManager.instance.GetGold().ToString();
        ore.text = InventoryManager.instance.GetOre().ToString();
        leaf.text = InventoryManager.instance.GetLeaf().ToString();
    }
}
