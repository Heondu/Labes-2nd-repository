using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AwakenManager : MonoBehaviour
{
    public static AwakenManager instance;

    [SerializeField]
    private Player player;
    [SerializeField]
    private TextMeshProUGUI textUsedPoint;
    [SerializeField]
    private TextMeshProUGUI textPoint;

    private List<Awaken> awakenList = new List<Awaken>();
    private Dictionary<string, int> status = new Dictionary<string, int>();

    [SerializeField]
    private int point = 0;
    private int usedPoint = 0;

    private int[] minActiveNum = { 12, 6, 3, 3, 2, 1 };
    private int[] neededPoint = { 5, 10, 20, 40, 50, 100 };

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);

        player = FindObjectOfType<Player>();
        player.onLevelUp.AddListener(AddPoint);
        //player.onLevelUp.AddListener(ApplyStatus);


        if (Load())
        {
            for (int i = 0; i < awakenList.Count; i++)
            {
                if (awakenList[i].id.Contains("confe"))
                {
                    awakenList[i].Init(DataManager.confession.FindDic("name", awakenList[i].id));
                }
                else if (awakenList[i].id.Contains("karma"))
                {
                    awakenList[i].Init(DataManager.karma.FindDic("name", awakenList[i].id));
                }
                else if (awakenList[i].id.Contains("trans"))
                {
                    awakenList[i].Init(DataManager.transcendence.FindDic("name", awakenList[i].id));
                }
            }
        }
        else
        {
            for (int i = 0; i < DataManager.confession.Count; i++)
            {
                awakenList.Add(new Awaken(DataManager.confession.FindDic("name", DataManager.confession[i]["name"])));
            }

            for (int i = 0; i < DataManager.karma.Count; i++)
            {
                awakenList.Add(new Awaken(DataManager.karma.FindDic("name", DataManager.karma[i]["name"])));
            }

            for (int i = 0; i < DataManager.transcendence.Count; i++)
            {
                awakenList.Add(new Awaken(DataManager.transcendence.FindDic("name", DataManager.transcendence[i]["name"])));
            }
        }

        textUsedPoint.text = usedPoint.ToString();
        textPoint.text = point.ToString();

        Save();
    }

    public bool CanActive(string id)
    {
        foreach (Awaken awaken in awakenList)
        {
            if (awaken.id != id) continue;
            if (awaken.isActive) continue;

            if (IsActive(awaken.GetReqAwaken()) == false) return false;

            //int num = GetCurrentNeededPoint();
            //if (num == -1) break;
            int num = awaken.GetConsume();
            if (num <= point)
            {
                awaken.isActive = true;
                point -= num;
                usedPoint += num;
                textUsedPoint.text = usedPoint.ToString();
                textPoint.text = point.ToString();

                Save();
                //status[awaken.GetStatus()] = awaken.GetAmount();
                ApplyStatus(awaken.GetStatus(), awaken.GetAmount());

                return true;
            }
        }
        return false;
    }
    public bool IsActive(string id)
    {
        if (id == "") return true;

        foreach (Awaken awaken in awakenList)
        {
            if (awaken.id == id)
            {
                return awaken.isActive;
            }
        }

        return false;
    }

    private int GetActiveNum()
    {
        int count = 0;
        foreach (Awaken awaken in awakenList)
        {
            if (awaken.isActive) count++;
        }

        return count;
    }

    private int GetCurrentNeededPoint()
    {
        int num = GetActiveNum();
        int sumOfNum = 0;
        for (int i = 0; i < minActiveNum.Length; i++)
        {
            sumOfNum += minActiveNum[i];

            if (num < sumOfNum)
            {
                return neededPoint[i];
            }    
        }
        return -1;
    }

    private void AddPoint()
    {
        point += 10;
        textPoint.text = point.ToString();
    }

    private void ApplyStatus(string status, int amount)
    {
        //foreach (string key in status.Keys)
        //{
        //    //player.GetStatus(key).RemoveAllModifiersFromSource(key);
        //    //player.GetStatus(key).AddModifier(new StatusModifier(status[key] * player.status.level, StatusModType.Flat, key));
        //}

        player.GetStatus(status).AddModifier(new StatusModifier(amount, StatusModType.Flat, status));

        player.status.CalculateDerivedStatus();
    }

    [System.Serializable]
    public class AwakenData
    {
        public List<Awaken> awakenList = new List<Awaken>();
        public int point = 0;
        public int usedPoint = 0;
    }

    private void Save()
    {
        if (SaveDataManager.instance.saveAwaken == false) return;

        AwakenData awakenData = new AwakenData();
        awakenData.awakenList = awakenList;
        awakenData.point = point;
        awakenData.usedPoint = usedPoint;
        JsonIO.SaveToJson(awakenData, SaveDataManager.saveFile[SaveFile.AwakenData]);
    }

    private bool Load()
    {
        if (SaveDataManager.instance.loadAwaken == false) return false;

        AwakenData awakenData = JsonIO.LoadFromJson<AwakenData>(SaveDataManager.saveFile[SaveFile.AwakenData]);
        if (awakenData != null)
        {
            awakenList = awakenData.awakenList;
            point = awakenData.point;
            usedPoint = awakenData.usedPoint;
            return true;
        }
        else return false;
    }

    private void OnApplicationQuit()
    {
        Save();
    }
}

[System.Serializable]
public class Awaken
{
    public string id;
    public bool isActive = false;

    private string status;
    private int amount;
    private string prefab;
    private int consume;
    private string reqAwaken;

    public Awaken(Dictionary<string, object> awakenDic)
    {
        id = awakenDic["name"].ToString();
        Init(awakenDic);
    }

    public void Init(Dictionary<string, object> awakenDic)
    {
        status = awakenDic["status"].ToString();
        amount = (int)awakenDic["amount"];
        prefab = awakenDic["prefab"].ToString();
        consume = (int)awakenDic["consume"];
        reqAwaken = awakenDic["reqAwaken"].ToString();
    }

    public string GetReqAwaken()
    {
        return reqAwaken;
    }

    public string GetStatus()
    {
        return status;
    }

    public int GetAmount()
    {
        return amount;
    }

    public int GetConsume()
    {
        return consume;
    }
}
