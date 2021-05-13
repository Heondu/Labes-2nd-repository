using System.Collections.Generic;
using UnityEngine;

public class ItemAdditional
{
    public static void Additional(Item item, int originRarity)
    {
        int rarity = originRarity;
        string additionalCode = "";

        while (additionalCode == "")
        {
            rarity = Mathf.Max(1, rarity);
            int rand = Random.Range(0, 100);
            if (rand < 25)
            {
                if (FindAdditional(originRarity, out additionalCode)) break;
                else
                {
                    rarity -= 1;
                }
            }
            else
            {
                if (FindAdditional(rarity, out additionalCode)) break;
                else
                {
                    rarity -= 1;
                }
            }
        }

        for (int i = 0; i < item.nameAdd.Length; i++)
        {
            if (item.nameAdd[i] == null)
            {
                Dictionary<string, object> additional = DataManager.additional.FindDic("code", additionalCode);
                item.nameAdd[i] = additional["code"].ToString();
                item.statusAdd[i] = additional["status"].ToString();
                item.statAdd[i] = Random.Range((int)additional["statMin"], (int)additional["statMax"]);
                item.cost += (int)additional["cost"];
                
                return;
            }
        }
    }

    private static bool FindAdditional(int rarity, out string additionalCode)
    {
        List<string> additionalList = new List<string>();

        for (int i = 0; i < DataManager.additional.Count; i++)
        {
            if (rarity == (int)DataManager.additional[i]["rarity"])
            {
                additionalList.Add(DataManager.additional[i]["code"].ToString());
            }
        }

        if (additionalList.Count != 0)
        {
            int rand = Random.Range(0, additionalList.Count);

            additionalCode = additionalList[rand];
            return true;
        }
        else
        {
            additionalCode = "";
            return false;
        }
    }
}
