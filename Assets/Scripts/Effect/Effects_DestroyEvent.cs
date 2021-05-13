using UnityEngine;

public class Effects_DestroyEvent : MonoBehaviour
{
    //오브젝트 파괴는 판정 밸런싱으로 바꿀 수 있기 때문에 애니메이션에 넣어서 이펙트만 파괴하기 위함. 
    public void destroyEvent()
    {
        Destroy(gameObject);
    }
}
