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
    /// ������Ʈ Ǯ�� �޼ҵ�
    /// </summary>
    /// <param name="holder">������Ʈ�� ã�ų� ���� �� �θ�� ����� ������Ʈ�� Ʈ������</param>
    /// <param name="obj">ã�ų� ���� �� ������ ������Ʈ</param>
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
