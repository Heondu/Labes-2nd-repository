using UnityEngine;

public class MagicCircle : MonoBehaviour
{
    [SerializeField]
    private float rotationSpeed;
    private float angle = 0;
    [SerializeField]
    private bool left;
    private int frameCount = 0;
    private int frameRate = 0;
    private float period = 0.5f;
    private float nextPeriod = 0;

    private void Awake()
    {
        nextPeriod = Time.realtimeSinceStartup + period;
    }

    private void Update()
    {
        frameCount++;
        if (Time.realtimeSinceStartup > nextPeriod)
        {
            frameRate = (int)(frameCount / period);
            frameCount = 0;
            nextPeriod += period;
        }

        if (left) angle -= rotationSpeed / frameRate;
        else angle += rotationSpeed / frameRate;
        if (angle < 0) angle = 360;
        if (angle > 360) angle = 0;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
}
