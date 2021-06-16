using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackPlayerEffect : MonoBehaviour
{
    [SerializeField] private GameObject Effect;
    [SerializeField] private GameObject player;
    GameObject dsaf;

    private void OnEnable()
    {
        if (Effect) //����Ʈ�� �����ϰ� �÷��̾ ���󰡰� ����
        {
            dsaf = Instantiate(Effect);
            dsaf.transform.position = player.transform.position;
            dsaf.transform.parent = player.transform;
        }
    }

    private void SetDisable()
    {
        gameObject.SetActive(false);
    }
}
