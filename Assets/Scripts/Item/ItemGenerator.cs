using System.Collections.Generic;
using UnityEngine;

public class ItemGenerator : MonoBehaviour
{
    public static ItemGenerator instance;
    [SerializeField]
    private Item item;
    private Dictionary<string, object> rarityDic;
    private Dictionary<string, object> rarityAddDic;
    private List<Item> itemList = new List<Item>();
    private string rarityType = "Normal";
    [SerializeField]
    private GameObject itemPrefab;
    [SerializeField]
    private GameObject goldPrefab;
    [SerializeField]
    private GameObject orePrefab;
    [SerializeField]
    private GameObject leafPrefab;
    private Transform itemHolder;

    private void Awake()
    {
        if (instance != null) Destroy(gameObject);
        else instance = this;
    }

    private void Update()
    {
        if (itemHolder == null)
            itemHolder = new GameObject("ItemHolder").transform;
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
        Filtering(rarityMin, rarityMax, "allEquipment");
        RandomRarity(classType);
        ItemInit();
        Additional();
        GameObject clone = ObjectPooler.instance.ObjectPool(itemHolder, itemPrefab, pos);
        clone.GetComponent<ItemScript>().Init(item);
    }

    private void DropConsumeItem(int rarityMin, int rarityMax, Vector3 pos)
    {
        Filtering(rarityMin, rarityMax, "consume");
        if (itemList.Count == 0) return;
        item = itemList[Random.Range(0, itemList.Count)];
        GameObject clone = Resources.Load<GameObject>("Prefabs/Items/" + item.name);
        clone = ObjectPooler.instance.ObjectPool(itemHolder, clone, pos);
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
            GameObject clone = ObjectPooler.instance.ObjectPool(itemHolder, goldPrefab, pos);
            Gold g = clone.GetComponent<Gold>();
            g.SetGold(gold1000);
            g.Diffusion(10);
        }
        if (gold100 != 0)
        {
            GameObject clone = ObjectPooler.instance.ObjectPool(itemHolder, goldPrefab, pos);
            Gold g = clone.GetComponent<Gold>();
            g.SetGold(gold100);
            g.Diffusion(5);
        }
        if (gold10 != 0)
        {
            GameObject clone = ObjectPooler.instance.ObjectPool(itemHolder, goldPrefab, pos);
            Gold g = clone.GetComponent<Gold>();
            g.SetGold(gold10);
            g.Diffusion(1);
        }
    }

    public void DropOre(int amount, Vector3 pos)
    {
        GameObject clone = ObjectPooler.instance.ObjectPool(itemHolder, orePrefab, pos);
        Ore ore = clone.GetComponent<Ore>();
        ore.SetAmount(amount);
        ore.Diffusion(3);
    }

    public void DropLeaf(int amount, Vector3 pos)
    {
        GameObject clone = ObjectPooler.instance.ObjectPool(itemHolder, leafPrefab, pos);
        Leaf leaf = clone.GetComponent<Leaf>();
        leaf.SetAmount(amount);
        leaf.Diffusion(3);
    }

    private Dictionary<string, Item> FindItemDB(string type)
    {
        Dictionary<string, Item> itemDB = new Dictionary<string, Item>();

        if (type == "allEquipment")
        {
            foreach (string key in DataManager.itemWeaponDB.Keys)
            {
                itemDB.Add(key, DataManager.itemWeaponDB[key]);
            }
            foreach (string key in DataManager.itemArmorDB.Keys)
            {
                itemDB.Add(key, DataManager.itemArmorDB[key]);
            }
            foreach (string key in DataManager.itemAccessoriesDB.Keys)
            {
                itemDB.Add(key, DataManager.itemAccessoriesDB[key]);
            }
        }
        else if (type == "weapon")
        {
            itemDB = DataManager.itemWeaponDB;
        }
        else if (type == "armor")
        {
            itemDB = DataManager.itemArmorDB;
        }
        else if (type == "accessories")
        {
            itemDB = DataManager.itemAccessoriesDB;
        }
        else if (type == "consume")
        {
            itemDB = DataManager.itemConsumeDB;
        }

        return itemDB;
    }

    private void Filtering(int rarityMin, int rarityMax, string type)
    {
        itemList.Clear();

        Dictionary<string, Item> itemDB = FindItemDB(type);

        do
        {
            foreach (string key in itemDB.Keys)
            {
                if (rarityMin <= itemDB[key].rarity && rarityMax >= itemDB[key].rarity)
                {
                    Item item = new Item();
                    item.DeepCopy(itemDB[key]);
                    itemList.Add(item);
                }
            }

            if (itemList.Count == 0 && rarityMin == 1)
            {
                Debug.LogError("레어리티에 해당하는 아이템 없음");
                break;
            }
            else if (itemList.Count == 0)
            {
                rarityMin = Mathf.Max(1, rarityMin - 1);
            }

        } while (itemList.Count == 0);
    }

    private void RandomRarity(string classType)
    {
        rarityDic = DataManager.rarity.FindDic("Function", classType);
        int[] sort = { (int)rarityDic["Legendary"], (int)rarityDic["Unique"], (int)rarityDic["Rare"], (int)rarityDic["Magic"], (int)rarityDic["HiQuality"], (int)rarityDic["Normal"] };
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
        rarityAddDic = DataManager.rarity.FindDic("Function", "rarityAdd");
        int rarityAll = item.rarity + (int)rarityAddDic[rarityType];

        for (int i = 0; i < 3; i++)
        {
            ItemAdditional.Additional(item, rarityAll);
        }
    }

    public Item GenerateItem(string type, string rarityType)
    {
        rarityAddDic = DataManager.rarity.FindDic("Function", "rarityAdd");
        int rarity = (int)rarityAddDic[rarityType];
        this.rarityType = rarityType;
        Filtering(Mathf.Max(1, rarity), Mathf.Max(1, rarity), type);
        ItemInit();
        Additional();
        return item;
    }
}
