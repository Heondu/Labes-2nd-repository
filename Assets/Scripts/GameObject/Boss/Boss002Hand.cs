using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss002Hand : MonoBehaviour
{
    [SerializeField]
    private float smoothTime = 0.3f;
    private float xVelocity = 0.0f;

    public void MoveTo(float xPos)
    {
        float x = Mathf.SmoothDamp(transform.position.x, xPos, ref xVelocity, smoothTime);
        transform.position = new Vector3(x, transform.position.y, transform.position.z);
    }
}
