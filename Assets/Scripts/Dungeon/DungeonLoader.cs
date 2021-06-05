using UnityEngine;
using UnityEngine.SceneManagement;

public class DungeonLoader : MonoBehaviour
{
    [SerializeField]
    private string sceneName;
    [SerializeField]
    private bool destroyPortalAtCollision = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PortalCollider"))
        {
            Vector3 newPos = collision.transform.position + (collision.transform.position - transform.position).normalized;
            
            SceneData.instance.prevScenePos = newPos;
            SceneData.instance.prevScene = SceneManager.GetActiveScene().name;
            SceneData.instance.mapdata = LazyCamera.instance.GetMapData();

            if (SceneData.instance.prevScene == SceneData.instance.mainScene)
            {
                LoadingSceneManager.LoadScene(sceneName);
            }
            else
            {
                SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
            }

            if (destroyPortalAtCollision)
            {
                gameObject.SetActive(false);
            }
        }
    }
}
