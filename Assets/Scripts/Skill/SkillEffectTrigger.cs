using UnityEngine;
using UnityEngine.Events;

public class SkillEffectTrigger : MonoBehaviour
{
    public UnityEvent onStart = new UnityEvent();
    public UnityEvent onHit = new UnityEvent();

    private Transform target;

    private void Awake()
    {
        target = transform;
    }

    public void CreateObjectInWorld(GameObject obj)
    {
        Instantiate(obj, target.position, Quaternion.identity);
    }

    public void CreateObjectInParent(GameObject obj)
    {
        Instantiate(obj, target.position, Quaternion.identity, target);
    }

    public void CreateSoundEffect(AudioClip aud)
    {
        SoundEffectManager.SoundEffect(aud);
    }

    public void SetTarget(Transform target)
    {
        this.target = target;
    }

}
