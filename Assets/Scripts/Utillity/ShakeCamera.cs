using System.Collections;
using UnityEngine;

public class ShakeCamera : MonoBehaviour
{
    public static ShakeCamera instance;

    private bool isShake = false;

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    public IEnumerator Shake(float amount, float duration)
    {
        if (SettingsManager.GetVFX() == false) yield break;
        if (isShake) yield break;

        if (isShake == false)
        {
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
}
