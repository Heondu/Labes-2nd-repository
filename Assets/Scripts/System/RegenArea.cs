using UnityEngine;

public class RegenArea : MonoBehaviour
{
    public int maxRegenNum;

    public Vector2 area = Vector2.zero;

    public bool autoArea = true;

    public GameObject[] monsters;
    public int[] prob;

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireCube(transform.position, area);
    }
}
