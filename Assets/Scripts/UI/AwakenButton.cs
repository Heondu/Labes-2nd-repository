using UnityEngine;
using UnityEngine.UI;

public class AwakenButton : MonoBehaviour
{
    [SerializeField]
    private string id;
    [SerializeField]
    private GameObject icon;
    [SerializeField]
    private GameObject line;

    private void Awake()
    {
        GetComponent<Button>().onClick.AddListener(Active);
    }

    private void Start()
    {
        if (AwakenManager.instance.IsActive(id))
        {
            ActiveGameObject();
        }
    }

    public void Active()
    {
        if (AwakenManager.instance.CanActive(id))
        {
            ActiveGameObject();
        }
    }

    private void ActiveGameObject()
    {
        if (icon != null)
            icon.SetActive(true);
        if (line != null)
            line.SetActive(true);
    }
}
