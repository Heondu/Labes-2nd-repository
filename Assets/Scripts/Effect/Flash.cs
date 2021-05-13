using System.Collections;
using UnityEngine;

public class Flash : MonoBehaviour
{
    [SerializeField]
    private Material whiteMat;
    private Material[] originMats;
    private SpriteRenderer[] renderers;
    private bool isFlash = false;
    private float duration = 0.02f;

    private void Awake()
    {
        renderers = GetComponentsInChildren<SpriteRenderer>();
        originMats = new Material[renderers.Length];
        for (int i = 0; i < originMats.Length; i++)
        {
            if (renderers[i].gameObject.layer == LayerMask.NameToLayer("Minimap")) continue;
            originMats[i] = renderers[i].material;
        }
    }

    public IEnumerator Execute()
    {
        if (isFlash == false)
        {
            isFlash = true;
            for (int i = 0; i < renderers.Length; i++)
            {
                if (renderers[i].gameObject.layer == LayerMask.NameToLayer("Minimap")) continue;
                renderers[i].material = whiteMat;
            }
            yield return new WaitForSeconds(duration);
            for (int i = 0; i < originMats.Length; i++)
            {
                if (renderers[i].gameObject.layer == LayerMask.NameToLayer("Minimap")) continue;
                renderers[i].material = originMats[i];
            }
        }
        isFlash = false;
    }
}
