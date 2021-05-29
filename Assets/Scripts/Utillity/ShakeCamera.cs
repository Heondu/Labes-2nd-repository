using System.Collections;
using UnityEngine;

public class ShakeCamera : MonoBehaviour
{
    public static ShakeCamera instance;

    [SerializeField]
    private OnOffToggle toggle;

    private bool isOn = true;
    private bool isShake = false;

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        toggle.onValueChanged.AddListener(OnValueChnaged);
        isOn = toggle.IsOn;
    }

    public IEnumerator Shake(float amount, float duration)
    {
        if (isOn == false) yield break;
        if (isShake) yield break;

        if (isShake == false)
        {
            Debug.Log("A");
            isShake = true;
            float time = 0;
            while (time < duration)
            {
                transform.position += (Vector3)Random.insideUnitCircle * amount;
                time += Time.deltaTime;
                yield return null;
            }
        }
        isShake = false;
    }

    private void OnValueChnaged(bool value)
    {
        isOn = value;
    }
}
