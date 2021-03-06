using System.Collections.Generic;
using UnityEngine;

public class ItemGenerator : MonoBehaviour
{
    public static ItemGenerator instance;
    [SerializeField]
    private Item item;
    private Dictionary<string, object> rarity;
    private Dictionary<string, object> rarityAdd;
    private List<Item> itemList = new List<Item>();
    private List<string> additionalList = new List<string>();
    private string rarityType = "Normal";
    [SerializeField]
    private GameObject itemPrefab;
    [SerializeField]
    private GameObject goldPrefab;

    private void Awake()
    {
        if (instance != null) Destroy(gameObject);
        else instance = this;
    }

    public void DropItem(Dictionary<string, object> monlvl, string classType, Vector3 pos)
    {
        Dictionary<string, object> droptable = DataManager.droptable.FindDic("class", classType);
        List<int> probList = new List<int>();

        for (int i = 1; i <= 6; i++)
        {
            if (droptable["prob" + i].ToString() == "") continue;

            probList.Add((int)droptable["prob" + i]);
        }

        int quantity = Random.Range((int)droptable["minquantity"], (int)droptable["maxquantity"] + 1);
        for (int i = 0; i < quantity; i++)
        {
            int sumOfProb = 0;
            for (int j = 0; j < probList.Count; j++)
            {
                sumOfProb += probList[j];
            }

            int rand = Random.Range(0, sumOfProb);
            int sum = 0;
            int index = 0;
            for (int j = 0; j < probList.Count; j++)
            {
                sum += probList[j];
                if (rand < sum)
                {
                    index = j + 1;
                    probList[j] = 0;
                    break;
                }
            }

            switch (droptable["item" + index].ToString())
            {
                case "equip": DropEquipItem((int)monlvl["raritymin"], (int)monlvl["raritymax"], classType, pos); break;
                case "monster": break;
                case "gold": DropGold((int)monlvl["goldmin"], (int)monlvl["goldmax"], 1, pos); break;
                case "gold/2": DropGold((int)monlvl["goldmin"], (int)monlvl["goldmax"], 2, pos); break;
                case "gold/10": DropGold((int)monlvl["goldmin"], (int)monlvl["goldmax"], 10, pos); break;
                case "consume": DropConsumeItem((int)monlvl["raritymin"], (int)monlvl["raritymax"], pos); break;
            }
        }
    }

    private void DropEquipItem(int rarityMin, int rarityMax, string classType, Vector3 pos)
    {
        Filtering(rarityMin, rarityMax, DataManager.itemEquipmentDB);
        RandomRarity(classType);
        ItemInit();
        Additional();
        GameObject clone = Instantiate(itemPrefab, pos, Quaternion.identity);
        clone.GetComponent<ItemScript>().Init(item);
    }

    private void DropConsumeItem(int rarityMin, int rarityMax, Vector3 pos)
    {
        Filtering(rarityMin, rarityMax, DataManager.itemConsumeDB);
        if (itemList.Count == 0) return;
        item = itemList[Random.Range(0, itemList.Count)];
        GameObject clone = Resources.Load<GameObject>("Prefabs/Items/" + item.name);
        clone = Instantiate(clone, pos, Quaternion.identity);
        clone.GetComponent<ItemScript>().Init(item);
    }

    private void DropGold(int goldmin, int goldmax, int divisionNum , Vector3 pos)
    {
        int gold = Random.Range(goldmin, goldmax + 1) * 10;

        gold = Mathf.Max(10, gold / divisionNum);

        int gold1000 = gold / 1000 * 1000;
        int gold100 = gold % 1000 / 100 * 100;
        int gold10 = gold % 1000 % 100 / 10 * 10;

        if (gold1000 != 0)
        {
            GameObject clone = Instantiate(goldPrefab, pos, Quaternion.identity);
            clone.GetComponent<Gold>().SetGold(gold1000);
            clone.GetComponent<Gold>().Diffusion(10);
        }
        if (gold100 != 0)
        {
            GameObject clone = Instantiate(goldPrefab, pos, Quaternion.identity);
            clone.GetComponent<Gold>().SetGold(gold100);
            clone.GetComponent<Gold>().Diffusion(5);
        }
        if (gold10 != 0)
        {
            GameObject clone = Instantiate(goldPrefab, pos, Quaternion.identity);
            clone.GetComponent<Gold>().SetGold(gold10);
            clone.GetComponent<Gold>().Diffusion(1);
        }
    }

    private void Filtering(int rarityMin, int rarityMax, Dictionary<string, Item> itemDB)
    {
        itemList.Clear();
        foreach(string key in itemDB.Keys)
        {
            if (rarityMin <= itemDB[key].rarity && rarityMax >= itemDB[key].rarity)
            {
                Item item = new Item();
                item.DeepCopy(itemDB[key]);
                itemList.Add(item);
            }
        }
    }

    private void RandomRarity(string classType)
    {
        rarity = DataManager.rarity.FindDic("Function", classType);
        int[] sort = { (int)rarity["Legendary"], (int)rarity["Unique"], (int)rarity["Rare"], (int)rarity["Magic"], (int)rarity["HiQuality"], (int)rarity["Normal"] };
        string[] types = { "Legendary", "Unique", "Rare", "Magic", "HiQuality", "Normal" };
        for (int i = 0; i < sort.Length - 1; i++) {
            for (int j = i + 1; j < sort.Length; j++) {
                if (sort[i] > sort[j]) {
                    int tempInt = sort[i];
                    sort[i] = sort[j];
                    sort[j] = tempInt;
                    string tempString = types[i];
                    types[i] = types[j];
                    types[j] = tempString;
                }
            }
        }
        int rand = Random.Range(0, sort[sort.Length - 1]);
        if (rand <= sort[0]) rarityType = types[0];
        else if (rand <= sort[1]) rarityType = types[1];
        else if (rand <= sort[2]) rarityType = types[2];
        else if (rand <= sort[3]) rarityType = types[3];
        else if (rand <= sort[4]) rarityType = types[4];
        else if (rand <= sort[5]) rarityType = types[5];
    }

    private void ItemInit()
    {
        item = itemList[Random.Range(0, itemList.Count)];
        item.rarityType = rarityType;
        item.stat = Random.Range(item.statMin, item.statMax);
    }

    private void Additional()
    {
        additionalList.Clear();
        rarityAdd = DataManager.rarity.FindDic("Function", "rarityAdd");
        int rand = Random.Range(0, 100);
        int rarityAll = item.rarity + (int)rarityAdd[rarityType];
        if (rand > 25) Mathf.Max(1, rarityAll - 1);
        for (int i = 0; i < DataManager.additional.Count; i++)
        {
            if (rarityAll == (int)DataManager.additional[i]["rarity"])
                additionalList.Add(DataManager.additional[i]["code"].ToString());
        }
        for (int i = 0; i < 3; i++)
        {
            rand = Random.Range(0, additionalList.Count);
            Dictionary<string, object> additional = DataManager.additional.FindDic("code", additionalList[rand]);
            item.nameAdd[i] = additional["code"].ToString();
            item.statusAdd[i] = additional["status"].ToString();
            item.statAdd[i] = Random.Range((int)additional["statMin"], (int)additional["statMax"]);
            item.cost += (int)additional["cost"];
        }
    }
}
