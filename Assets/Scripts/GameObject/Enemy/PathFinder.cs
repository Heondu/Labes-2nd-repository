using System.Collections.Generic;
using UnityEngine;

public class PathFinder : MonoBehaviour
{
    [SerializeField]
    private Transform target;
    private LayerMask layerMask;
    private float distance = 1;
    private float findAngle = 45;

    private void Awake()
    {
        layerMask = 1 << LayerMask.NameToLayer("Object");
    }

    /// <summary>
    /// Ÿ�ٰ��� �ִܰŸ� ����� ������ ��ȯ�ϴ� �Լ�
    /// </summary>
    /// <param name="lookDir">�ٶ󺸴� ����</param>
    /// <returns></returns>
    public Vector3 GetMoveDir(Vector3 lookDir)
    {
        List<Vector3> possibleDir = new List<Vector3>();
        Vector3[] moveDir = SetMoveDirs(lookDir);

        for (int i = 0; i < moveDir.Length; i++)
        {
            RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, moveDir[i], distance, layerMask);

            if (hits.Length == 0)
            {
                possibleDir.Add(moveDir[i]);
            }
            else if (hits.Length == 1)
            {
                if (hits[0].collider.gameObject == gameObject)
                {
                    possibleDir.Add(moveDir[i]);
                }
            }
        }

        float minDistance = float.MaxValue;
        int possibleDirIndex = -1;

        for (int i = 0; i < possibleDir.Count; i++)
        {
            float distance = Mathf.Abs(Vector3.Distance(target.position, transform.position + possibleDir[i]));
            
            if (distance < minDistance)
            {
                minDistance = distance;
                possibleDirIndex = i;
            }
        }

        if (possibleDirIndex == -1)
        {
            return lookDir * -1;
        }

        Debug.DrawLine(transform.position, transform.position + possibleDir[possibleDirIndex] * 1.5f, Color.red);
        return possibleDir[possibleDirIndex];
    }

    /// <summary>
    /// �ٶ󺸴� ������ �������� ������ ������ŭ �̵��� ������ ���ϴ� �Լ�
    /// </summary>
    /// <param name="lookDir">����</param>
    /// <returns></returns>
    private Vector3[] SetMoveDirs(Vector3 lookDir)
    {
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
        angle = (int)(angle / 45) * 45;
        float leftAngle = (angle - findAngle) * Mathf.Deg2Rad;
        float rightAngle = (angle + findAngle) * Mathf.Deg2Rad;
        Vector3 left = new Vector3(Mathf.Cos(leftAngle), Mathf.Sin(leftAngle), 0);
        Vector3 right = new Vector3(Mathf.Cos(rightAngle), Mathf.Sin(rightAngle), 0);

        Vector3[] moveDir = new Vector3[]
        {
            lookDir, left, right
        };

        return moveDir;
    }

    /// <summary>
    /// Ÿ�� ���̿� �ٸ� ��ü�� �ִ��� Ȯ���ϴ� �Լ�
    /// </summary>
    /// <param name="target">Ÿ�� ������Ʈ</param>
    /// <returns>�ٸ� ��ü�� ���� �� false�� ��ȯ</returns>
    public bool IsEmpty(Transform target)
    {
        Vector3 dir = (target.position - transform.position).normalized;
        float distance = Mathf.Abs(Vector3.Distance(target.position, transform.position));
        RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, dir, distance, layerMask);

        foreach (RaycastHit2D hit in hits)
        {
            if (hit.collider.gameObject != target.gameObject && hit.collider.CompareTag("Enemy") == false)
            {
                return false;
            }
        }

        return true;
    }

    /// <summary>
    /// �̵� ������ �����ִ��� Ȯ���ϴ� �Լ�
    /// </summary>
    /// <param name="dir">�̵� ����</param>
    /// <returns>�������� �� false�� ��ȯ</returns>
    public bool IsEmpty(Vector3 dir)
    {
        RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, dir, distance, layerMask);

        foreach (RaycastHit2D hit in hits)
        {
            if (hit.collider.gameObject != gameObject)
            {
                return false;
            }
        }

        return true;
    }

    public void SetTarget(Transform target)
    {
        this.target = target;
    }
}
