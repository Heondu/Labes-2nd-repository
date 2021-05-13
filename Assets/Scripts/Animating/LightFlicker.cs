using System.Collections;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class LightFlicker : MonoBehaviour
{
    private Light2D L2d;
    [SerializeField] private float flickInterval = 1.0f;
    [SerializeField] private float flickIntensity = 2f;
    private float maxIntensity;
    private float minIntensity;
    private float step;


    // Start is called before the first frame update
    void Start()
    {
        L2d = GetComponent<Light2D>();
        maxIntensity = L2d.intensity;
        minIntensity = L2d.intensity / flickIntensity;
        step = (maxIntensity - minIntensity) / 20;
        StartCoroutine(Flicker());
    }

    IEnumerator Flicker()
    {
        while (true)
        {
            for(int i = 0; i < 10; i++)
            {
                L2d.intensity -= step;
                yield return new WaitForSeconds(flickInterval / 10);
            }
            L2d.intensity = minIntensity;

            for (int i = 0; i < 10; i++)
            {
                L2d.intensity += step;
                yield return new WaitForSeconds(flickInterval / 10);
            }
            L2d.intensity = maxIntensity;
        }
    }
}
