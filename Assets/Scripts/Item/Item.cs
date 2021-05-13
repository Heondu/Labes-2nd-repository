public enum ItemType { equipment = 0, consume }

public class Item
{
    private const int additionalMax = 3;

    public string name;
    public int spawnable;
    public string useType;
    public string type;
    public string itemImage;
    public string inventoryImage;
    public string status;
    public int rarity;
    public int statMin;
    public int statMax;
    public int cost;
    public int stat;
    public string rarityType;
    public string[] nameAdd = new string[additionalMax];
    public string[] statusAdd = new string[additionalMax];
    public int[] statAdd = new int[additionalMax];
    public int quality = 0;
    public bool isNew = true;
    public Skill skill;

    public void DeepCopy(Item item)
    {
        name = item.name;
        spawnable = item.spawnable;
        useType = item.useType;
        type = item.type;
        status = item.status;
        rarity = item.rarity;
        statMin = item.statMin;
        statMax = item.statMax;
        cost = item.cost;
        itemImage = item.itemImage;
        inventoryImage = item.inventoryImage;
    }
}

[System.Serializable]
public class ItemSaveData
{
    public string name;
    public int spawnable;
    public string useType;
    public string type;
    public string itemImage;
    public string inventoryImage;
    public string status;
    public int rarity;
    public int statMin;
    public int statMax;
    public int cost;
    public int stat;
    public string rarityType;
    public string[] nameAdd;
    public string[] statusAdd;
    public int[] statAdd;
    public int quality = 0;
    public bool isNew = true;
    public SkillSaveData skill;

    public ItemSaveData(Item item)
    {
        if (item == null) return;

        name = item.name;
        spawnable = item.spawnable;
        useType = item.useType;
        type = item.type;
        itemImage = item.itemImage;
        inventoryImage = item.inventoryImage;
        status = item.status;
        rarity = item.rarity;
        statMin = item.statMin;
        statMax = item.statMax;
        cost = item.cost;
        stat = item.stat;
        rarityType = item.rarityType;
        nameAdd = item.nameAdd;
        statusAdd = item.statusAdd;
        statAdd = item.statAdd;
        quality = item.quality;
        isNew = item.isNew;
        skill = new SkillSaveData(item.skill);
    }

    public Item DeepCopy()
    {
        Item item = new Item();

        item.name = name;
        item.spawnable = spawnable;
        item.useType = useType;
        item.type = type;
        item.itemImage = itemImage;
        item.inventoryImage = inventoryImage;
        item.status = status;
        item.rarity = rarity;
        item.statMin = statMin;
        item.statMax = statMax;
        item.cost = cost;
        item.stat = stat;
        item.rarityType = rarityType;
        item.nameAdd = nameAdd;
        item.statusAdd = statusAdd;
        item.statAdd = statAdd;
        item.quality = quality;
        item.isNew = isNew;
        item.skill = skill.DeepCopy();

        return item;
    }
}
