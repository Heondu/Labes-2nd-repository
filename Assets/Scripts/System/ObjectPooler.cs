using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    public static ObjectPooler instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public GameObject ObjectPool(Transform holder, GameObject obj)
    {
        return ObjectPool(holder, obj, Vector3.zero, Quaternion.identity);
    }

    public GameObject ObjectPool(Transform holder, GameObject obj, Vector3 position)
    {
        return ObjectPool(holder, obj, position, Quaternion.identity);
    }

    public GameObject ObjectPool(Transform holder, GameObject obj, Quaternion rotation)
    {
        return ObjectPool(holder, obj, Vector3.zero, rotation);
    }

    /// <summary>
    /// 오브젝트 풀링 메소드
    /// </summary>
    /// <param name="holder">오브젝트를 찾거나 생성 시 부모로 등록할 오브젝트의 트랜스폼</param>
    /// <param name="obj">찾거나 없을 시 생성할 오브젝트</param>
    /// <returns></returns>
    public GameObject ObjectPool(Transform holder, GameObject obj, Vector3 position, Quaternion rotation)
    {
        string name = obj.name + "(Clone)";

        GameObject clone = null;
        if (holder.childCount != 0)
        {
            for (int i = 0; i < holder.childCount; i++)
            {
                if (holder.GetChild(i).gameObject.activeSelf == false && holder.GetChild(i).gameObject.name == name)
                {
                    clone = holder.GetChild(i).gameObject;
                    clone.transform.SetPositionAndRotation(position, rotation);
                    clone.SetActive(true);
                    return clone;
                }
            }
        }
        if (clone == null)
        {
            clone = Instantiate(obj, position, rotation, holder);
        }

        return clone;
    }
}
