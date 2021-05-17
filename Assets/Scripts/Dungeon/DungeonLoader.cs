using UnityEngine;
using UnityEngine.SceneManagement;

public class DungeonLoader : MonoBehaviour
{
    [SerializeField]
    private string sceneName;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PortalCollider"))
        {
            Vector3 newPos = collision.transform.position + (collision.transform.position - transform.position).normalized;
            SceneData.instance.prevScenePos = newPos;

            SceneManager.LoadScene(sceneName);
        }
    }
}
