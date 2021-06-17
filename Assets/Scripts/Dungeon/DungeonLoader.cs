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
            collision.GetComponentInParent<Player>().SetMapData(null);

            Vector3 newPos = collision.transform.position + (collision.transform.position - transform.position).normalized;
            
            SceneData.instance.prevScenePos = newPos;
            SceneData.instance.prevScene = SceneManager.GetActiveScene().name;
            SceneData.instance.mapdata = LazyCamera.instance.GetMapData();

            if (sceneName == SceneData.attackDungeon || sceneName == SceneData.guardDungeon)
            {
                SceneData.instance.regenArea = FindObjectOfType<RegenManager>().regens[0];
                SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
            }
            else
            {
                LoadingSceneManager.LoadScene(sceneName);
            }

            if (destroyPortalAtCollision)
            {
                gameObject.SetActive(false);
            }
        }
    }
}
