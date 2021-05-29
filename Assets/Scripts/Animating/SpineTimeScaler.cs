using UnityEngine;
using Spine.Unity;
using Spine;

public class SpineTimeScaler : MonoBehaviour
{
    private SkeletonAnimation skeletonAnimation;
    [SerializeField]
    private bool ignoreTimeScale = true;
    private float lastInterval;


    private void Awake()
    {
        skeletonAnimation = GetComponent<SkeletonAnimation>();
    }

    private void Update()
    {
        float delta = Time.deltaTime;

        if (ignoreTimeScale && Application.isPlaying)
        {
            float timeNow = Time.realtimeSinceStartup;
            delta = timeNow - lastInterval;
            lastInterval = timeNow;
        }

        UpdateSkeleton(delta);
    }

    public void UpdateSkeleton(float deltaTime)
    {
        skeletonAnimation.Update(deltaTime * skeletonAnimation.timeScale);
    }
}
