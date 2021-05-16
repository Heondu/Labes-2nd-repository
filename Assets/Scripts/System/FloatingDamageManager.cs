using System.Collections.Generic;
using UnityEngine;

public enum DamageType { normal = 0, critical, heal, miss }

public class FloatingDamageManager : MonoBehaviour
{
    public static FloatingDamageManager instance;
    [SerializeField]
    private GameObject[] damagePrefab;
    private Dictionary<GameObject, List<FloatingDamage>> damageList = new Dictionary<GameObject, List<FloatingDamage>>();

    private void Awake()
    {
        if (instance != null) Destroy(this);
        else instance = this;
    }

    public void FloatingDamage(GameObject executor, string damage, Vector3 position, DamageType damageType)
    {
        GameObject clone = Instantiate(damagePrefab[(int)damageType], position, Quaternion.identity, transform);
        clone.GetComponent<FloatingDamage>().Init(executor, damage, position);

        if (damageList.ContainsKey(executor) == false)
        {
            damageList.Add(executor, new List<FloatingDamage>());
        }
        damageList[executor].Add(clone.GetComponent<FloatingDamage>());

        for (int i = damageList[executor].Count - 1; i >= 0; i--)
        {
            if (i > 0 && damageList[executor][i - 1] != null && damageList[executor][i] != null)
            {
                float distance = Mathf.Abs(damageList[executor][i - 1].transform.localPosition.y - damageList[executor][i].transform.localPosition.y);
                float sizeY = damageList[executor][i].GetComponent<RectTransform>().sizeDelta.y;
                float newPos = (sizeY - distance) / damageList[executor].Count;
                newPos = Mathf.Max(0, newPos);
                damageList[executor][i - 1].SetPos(newPos);
            }
        }
    }

    public void RemoveDamage(GameObject executor, FloatingDamage floatingDamage)
    {
        damageList[executor].Remove(floatingDamage);
    }
}
