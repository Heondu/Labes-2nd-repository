using UnityEngine;

public class RegenArea : MonoBehaviour
{
    public int maxRegenNum;
    public int maxEnemySwarmNum;

    public Vector2 area = Vector2.zero;

    public bool autoArea = true;

    public GameObject[] monsters;
    public int[] prob;
    [Range(0, 100)]
    public int eliteProb;

    [SerializeField]
    private RegenManager regenManager;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            regenManager.SetActiveRegenArea(this);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireCube(transform.position, area);
    }
}
