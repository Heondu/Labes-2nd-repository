using UnityEngine;

public class Timer
{
    private float time = 0;
    public float GetTime => time;

    public bool IsTimeOut(float limitTime)
    {
        time += Time.deltaTime;
        if (time >= limitTime)
        {
            time = 0;
            return true;
        }
        return false;
    }
}
