using UnityEngine;
using UnityEngine.Events;

public class SkillLifetime : MonoBehaviour
{
    private float currentTime = 0;
    private float lifetime;
    public float GetCurrentTime => currentTime;
    public float GetLifetime => lifetime;
    public UnityEvent onDestroy = new UnityEvent();

    private void OnEnable()
    {
        currentTime = 0;
    }

    private void Start()
    {
        lifetime = GetComponent<SkillData>().lifetime;
    }

    private void Update()
    {
        currentTime += Time.deltaTime;
        if (currentTime >= lifetime)
        {
            Destroy(gameObject);
        }
    }

    public void ResetTime()
    {
        currentTime = 0;
    }

    private void OnDestroy()
    {
        onDestroy.Invoke();
    }
}
