using System.Collections;
using UnityEngine;

public class TabFocusSlider : MonoBehaviour
{
    [SerializeField]
    private float moveTime = 0.3f;
    private Transform target;
    private bool x;
    private bool y;

    public void TabFocusMove(Transform target, bool x, bool y)
    {
        this.target = target;
        this.x = x;
        this.y = y;
        StopCoroutine("Move");
        StartCoroutine("Move", target.position);
    }

    private IEnumerator Move(Vector3 newPos)
    {
        float startTime = Time.realtimeSinceStartup;
        float percent = 0;
        while (percent < 1)
        {
            if (x)
            {
                float xPos = Mathf.Lerp(transform.position.x, newPos.x, percent);
                transform.position = new Vector3(xPos, transform.position.y, transform.position.z);
            }
            else if (y)
            {
                float yPos = Mathf.Lerp(transform.position.y, newPos.y, percent);
                transform.position = new Vector3(transform.position.x, yPos, transform.position.z);
            }
            percent += (Time.realtimeSinceStartup - startTime) / moveTime;
            yield return null;
        }
        if (x) transform.position = new Vector3(newPos.x, transform.position.y, transform.position.z);
        else if (y) transform.position = new Vector3(transform.position.x, newPos.y, transform.position.z);
    }

    private void OnDisable()
    {
        StopCoroutine("Move");
        if (target != null)
        {
            if (x) transform.position = new Vector3(target.position.x, transform.position.y, transform.position.z);
            else if (y) transform.position = new Vector3(transform.position.x, target.position.y, transform.position.z);
        }    
    }
}
