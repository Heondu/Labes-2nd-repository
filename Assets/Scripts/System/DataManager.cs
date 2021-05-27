using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static List<Dictionary<string, object>> weapon = new List<Dictionary<string, object>>();
    public static List<Dictionary<string, object>> armor = new List<Dictionary<string, object>>();
    public static List<Dictionary<string, object>> accessories = new List<Dictionary<string, object>>();
    public static List<Dictionary<string, object>> additional = new List<Dictionary<string, object>>();
    public static List<Dictionary<string, object>> experience = new List<Dictionary<string, object>>();
    public static List<Dictionary<string, object>> monlvl = new List<Dictionary<string, object>>();
    public static List<Dictionary<string, object>> monster = new List<Dictionary<string, object>>();
    public static List<Dictionary<string, object>> rarity = new List<Dictionary<string, object>>();
    public static List<Dictionary<string, object>> droptable = new List<Dictionary<string, object>>();
    public static List<Dictionary<string, object>> localization_KOR = new List<Dictionary<string, object>>();
    public static List<Dictionary<string, object>> ore = new List<Dictionary<string, object>>();
    public static List<Dictionary<string, object>> runes = new List<Dictionary<string, object>>();
    public static List<Dictionary<string, object>> skills = new List<Dictionary<string, object>>();
    public static List<Dictionary<string, object>> skillexp = new List<Dictionary<string, object>>();
    public static List<Dictionary<string, object>> item = new List<Dictionary<string, object>>();
    public static List<Dictionary<string, object>> confession = new List<Dictionary<string, object>>();
    public static List<Dictionary<string, object>> karma = new List<Dictionary<string, object>>();
    public static List<Dictionary<string, object>> transcendence = new List<Dictionary<string, object>>();
    public static List<Dictionary<string, object>> dialogue = new List<Dictionary<string, object>>();
    public static List<Dictionary<string, object>> quest = new List<Dictionary<string, object>>();

    public static Dictionary<string, Item> itemEquipmentDB = new Dictionary<string, Item>();
    public static Dictionary<string, Item> itemConsumeDB = new Dictionary<string, Item>();
    public static Dictionary<string, Skill> skillDB = new Dictionary<string, Skill>();

    private void Awake()
    {
        weapon = CSVReader.Read("weapon");
        armor = CSVReader.Read("armor");
        accessories = CSVReader.Read("accessories");
        additional = CSVReader.Read("additional");
        experience = CSVReader.Read("experience");
        monlvl = CSVReader.Read("monlvl");
        monster = CSVReader.Read("monster");
        rarity = CSVReader.Read("rarity");
        droptable = CSVReader.Read("droptable");
        localization_KOR = CSVReader.Read("localization_KOR");
        ore = CSVReader.Read("ore");
        runes = CSVReader.Read("runes");
        skills = CSVReader.Read("skills");
        skillexp = CSVReader.Read("skillexp");
        item = CSVReader.Read("item");
        confession = CSVReader.Read("confession");
        karma = CSVReader.Read("karma");
        transcendence = CSVReader.Read("transcendence");
        dialogue = CSVReader.Read("dialogue");
        quest = CSVReader.Read("quest");

        ListToDict(weapon);
        ListToDict(armor);
        ListToDict(accessories);
        ListToDict(item);
        ListToDict(skills);
    }

    private void ListToDict(List<Dictionary<string, object>> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            if (list == weapon || list == armor || list == accessories)
            {
                string name = list[i]["name"].ToString();
                itemEquipmentDB[name] = new Item
                {
                    name = name,
                    spawnable = (int)list[i]["spawnable"],
                    useType = list[i]["useType"].ToString(),
                    type = list[i]["type"].ToString(),
                    status = list[i]["status"].ToString(),
                    rarity = (int)list[i]["rarity"],
                    statMin = (int)list[i]["statMin"],
                    statMax = (int)list[i]["statMax"],
                    cost = (int)list[i]["cost"],
                    itemImage = list[i]["itemImage"].ToString(),
                    inventoryImage = list[i]["inventoryImage"].ToString()
                };
            }
            else if (list == item)
            {
                string name = list[i]["name"].ToString();
                itemConsumeDB[name] = new Item
                {
                    name = name,
                    spawnable = (int)list[i]["spawnable"],
                    useType = list[i]["useType"].ToString(),
                    type = list[i]["type"].ToString(),
                    itemImage = list[i]["itemImage"].ToString(),
                    inventoryImage = list[i]["inventoryImage"].ToString(),
                    rarity = (int)list[i]["rarity"]
                };
            }
            else if (list == skills)
            {
                string name = list[i]["name"].ToString();
                skillDB[name] = new Skill();
                skillDB[name].skill = list[i]["skill"].ToString();
                skillDB[name].name = list[i]["name"].ToString();
                skillDB[name].rarity = (int)list[i]["rarity"];
                skillDB[name].exp = (int)list[i]["exp"];
                skillDB[name].material = list[i]["material"].ToString();
                skillDB[name].weaponClass = list[i]["weaponClass"].ToString();
                skillDB[name].classBonus = list[i]["classBonus"].ToString();
                skillDB[name].bonusAmount = (int)list[i]["bonusAmount%"];
                skillDB[name].position = list[i]["position"].ToString();
                skillDB[name].element = list[i]["element"].ToString();
                skillDB[name].cooltime = float.Parse(list[i]["cooltime"].ToString());
                skillDB[name].amount = (int)list[i]["amount"];
                skillDB[name].perlvl = (int)list[i]["perlvl"];
                skillDB[name].isPositive = (int)list[i]["isPositive"];
                skillDB[name].repeat = (int)list[i]["repeat"];
                skillDB[name].speed = (int)list[i]["speed"];
                skillDB[name].size = (int)list[i]["size"];
                skillDB[name].lifetime = (int)list[i]["lifetime"];
                skillDB[name].guide = float.Parse(list[i]["guide"].ToString());
                skillDB[name].penetration = (int)list[i]["penetration"];
                skillDB[name].consume = (int)list[i]["consume"];
            }
        }
    }

    public static bool Exists(List<Dictionary<string, object>> list, string key, object value)
    {
        for (int i = 0; i < list.Count; i++)
        {
            if (list[i][key].Equals(value)) return true;
        }
        return false;
    }

    public static string Localization(string str)
    {
        if (str == null) return "";
        string percent = "";
        if (str.Contains("%"))
        {
            str = str.Substring(0, str.Length - 1);
            percent = "%";
        }

        if (SettingsManager.GetLanguage() == Language.korean)
        {
            for (int i = 0; i < localization_KOR.Count; i++)
            {
                if (localization_KOR[i]["name"].ToString() == str) return localization_KOR[i]["localization"].ToString() + percent;
            }
        }
        else
        {
            return str;
        }
        return "";
    }

    public static Dictionary<string, DialogueCollection> GetDialogueDB()
    {
        Dictionary<string, DialogueCollection> dialogueDB = new Dictionary<string, DialogueCollection>();
        DialogueCollection dialogueCollection = new DialogueCollection();

        string code;
        for (int i = 0; i < dialogue.Count; i++)
        {
            code = dialogue[i]["code"].ToString();
            if (!code.Equals(""))
            {
                Dialogue dial = new Dialogue();

                string spritePath = dialogue[i]["image"].ToString();
                if (!spritePath.Equals(""))
                {
                    if (spritePath.Contains("_"))
                    {
                        string[] split = spritePath.Split('_');
                        dial.face = Resources.LoadAll<Sprite>(split[0])[int.Parse(split[1])];
                    }
                    else dial.face = Resources.Load<Sprite>(spritePath);
                }
                dial.name = dialogue[i]["name"].ToString();
                dial.content = dialogue[i]["content"].ToString();
                dial.interval = (float)dialogue[i]["interval"];

                dialogueCollection.dialogues.Add(dial);

                if (i + 1 >= dialogue.Count || !dialogue[i + 1]["code"].ToString().Equals(code))
                {
                    dialogueDB.Add(code, dialogueCollection);
                    dialogueCollection = new DialogueCollection();
                }
            }
        }
        return dialogueDB;
    }

    public static List<Quest> GetQuestDB()
    {
        List<Quest> questList = new List<Quest>();

        for (int i = 0; i < quest.Count; i++)
        {
            if (quest[i]["name"].ToString().Contains("quest"))
            {
                Quest q = new Quest();

                q.name = quest[i]["name"].ToString();
                q.type = quest[i]["type"].ToString();
                q.amount = (int)quest[i]["amount"];
                for (int j = 0; j < 3; j++)
                {
                    QuestReward qr = new QuestReward();

                    if (quest[i]["reward" + (j + 1)].ToString() == "") continue;
                    
                    qr.reward = quest[i]["reward" + (j + 1)].ToString();

                    if (quest[i]["rarity" + (j + 1)].ToString() != "")
                        qr.rarity = quest[i]["rarity" + (j + 1)].ToString();

                    qr.amount = (int)quest[i]["amount" + (j + 1)];

                    q.rewards.Add(qr);
                }
                q.npc = quest[i]["npc"].ToString();
                q.minlvl = (int)quest[i]["minlvl"];
                q.maxlvl = (int)quest[i]["maxlvl"];
                q.chance = (int)quest[i]["chance"];

                questList.Add(q);
            }
        }

        return questList;
    }
}

public static class Data
{
    public static object Find(this List<Dictionary<string, object>> list, string key, object value, string key2)
    {
        for (int i = 0; i < list.Count; i++)
        {
            if (list[i][key].Equals(value)) return list[i][key2];
        }
        return null;
    }

    public static List<object> FindAll(this List<Dictionary<string, object>> list, string key, object value, string key2)
    {
        List<object> objects = new List<object>();
        for (int i = 0; i < list.Count; i++)
        {
            if (list[i][key].Equals(value)) objects.Add(list[i][key2]);
        }
        return objects;
    }

    public static Dictionary<string, object> FindDic(this List<Dictionary<string, object>> list, string key, object value)
    {
        for (int i = 0; i < list.Count; i++)
        {
            if (list[i][key].Equals(value)) return list[i];
        }
        return null;
    }
}
