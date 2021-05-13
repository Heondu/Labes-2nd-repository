using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class OnOffToggle : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    private GameObject onObject;
    [SerializeField]
    private GameObject offObject;
    private bool isOn;
    public bool IsOn
    {
        get
        {
            return isOn;
        }

        set
        {
            isOn = value;
            onValueChanged.Invoke(value);
        }
    }
    public UnityEvent<bool> onValueChanged;

    private void Awake()
    {
        onValueChanged.AddListener(ObjectSetActive);
    }

    private void Start()
    {
        ObjectSetActive(isOn);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        isOn = !isOn;

        onValueChanged.Invoke(isOn);
    }

    private void ObjectSetActive(bool value)
    {
        onObject.SetActive(value);
        offObject.SetActive(!value);
    }
}
