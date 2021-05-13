using System.Collections;
using UnityEngine;

public enum EnemyState { STATE_NULL = 0, STATE_PATROL, STATE_CHASE, STATE_ATTACK, STATE_COMEBACK }

public class EnemyController : MonoBehaviour
{
    private GameObject target;
    private EnemyState state = EnemyState.STATE_PATROL;
    private const float FIND_DISTANCE = 8f;
    private const float CAHSE_DISTANCE = 13f;
    private const float ATTACK_DISTANCE = 10f;
    private const float COMEBACK_DISTANCE = 5f;
    private const float PATROL_TIME = 1f;
    private Vector3[] patrolDir = { Vector3.right, Vector3.zero, Vector3.down, Vector3.zero,
                                    Vector3.left, Vector3.zero, Vector3.up, Vector3.zero };
    private Timer timer = new Timer();
    private int currentDirNum = 0;
    public bool isSwarmAttack = false;
    private bool isStop = false;
    private bool canChange = true;
    private float changeCool = 1f;

    private PathFinder pathFinder;
    private Vector3 moveDir = Vector3.zero;
    private Vector3 originPos;

    private void Awake()
    {
        target = FindObjectOfType<Player>().gameObject;

        pathFinder = GetComponent<PathFinder>();

        originPos = transform.position;

        RandSort();
    }

    private void OnEnable()
    {
        isSwarmAttack = false;
        isStop = false;
        canChange = true;
    }

    public void Update()
    {
        FSM();
    }

    private void FSM()
    {
        if (isStop) return;

        if (state == EnemyState.STATE_PATROL)
        {
            if (Distance(originPos) > COMEBACK_DISTANCE) SetState(EnemyState.STATE_COMEBACK);
            if (Distance(target.transform.position) <= FIND_DISTANCE || isSwarmAttack == true) SetState(EnemyState.STATE_CHASE);
            if (Distance(target.transform.position) <= ATTACK_DISTANCE && pathFinder.IsEmpty(target) == true) SetState(EnemyState.STATE_ATTACK);
        }
        if (state == EnemyState.STATE_CHASE)
        {
            if (Distance(target.transform.position) > CAHSE_DISTANCE && isSwarmAttack == false) SetState(EnemyState.STATE_PATROL);
            if (Distance(target.transform.position) <= ATTACK_DISTANCE && pathFinder.IsEmpty(target) == true) SetState(EnemyState.STATE_ATTACK);
        }
        if (state == EnemyState.STATE_ATTACK)
        {
            if (Distance(target.transform.position) > CAHSE_DISTANCE && isSwarmAttack == false) SetState(EnemyState.STATE_PATROL);
            if (Distance(target.transform.position) > ATTACK_DISTANCE || pathFinder.IsEmpty(target) == false) SetState(EnemyState.STATE_CHASE);
        }
        if (state == EnemyState.STATE_COMEBACK)
        {
            if (Distance(originPos) <= COMEBACK_DISTANCE) SetState(EnemyState.STATE_PATROL);
            if (Distance(target.transform.position) <= FIND_DISTANCE || isSwarmAttack == true) SetState(EnemyState.STATE_CHASE);
            if (Distance(target.transform.position) <= ATTACK_DISTANCE && pathFinder.IsEmpty(target) == true) SetState(EnemyState.STATE_ATTACK);
        }
    }

    public void SetPos(Vector3 pos)
    {
        transform.position = pos;
        originPos = pos;
    }

    public Vector3 GetAxis()
    {
        if (isStop) return Vector3.zero;
        if (state == EnemyState.STATE_COMEBACK)
        {
            moveDir = (originPos - transform.position).normalized;
            return moveDir;
        }
        if (state == EnemyState.STATE_PATROL || state == EnemyState.STATE_ATTACK)
        {
            if (timer.IsTimeOut(PATROL_TIME))
            {
                RandSort();
                currentDirNum = (currentDirNum + 1) % patrolDir.Length;

                if (pathFinder.IsEmpty(patrolDir[currentDirNum]))
                {
                    moveDir = patrolDir[currentDirNum];
                }
                else
                {
                    moveDir = patrolDir[currentDirNum] * -1;
                }
            }
            return moveDir;
        }
        else if (state == EnemyState.STATE_CHASE)
        {
            moveDir = pathFinder.GetMoveDir(moveDir);
            return moveDir;
        }
        return Vector3.zero;
    }

    public float Distance(Vector3 targetPos)
    {
        return Mathf.Abs(Vector2.Distance(transform.position, targetPos));
    }

    private void RandSort()
    {
        if (currentDirNum != 0) return;
        for (int i = 0; i < patrolDir.Length; i++)
        {
            int randNum = Random.Range(0, patrolDir.Length);
            Vector3 temp = patrolDir[randNum];
            patrolDir[randNum] = patrolDir[i];
            patrolDir[i] = temp;
        }
    }

    public EnemyState GetState()
    {
        return state;
    }

    public void SetState(EnemyState state)
    {
        if (canChange)
        {
            this.state = state;
            StartCoroutine(ChangeCool());

            if (state == EnemyState.STATE_CHASE)
            {
                moveDir = (target.transform.position - transform.position).normalized;
            }
        }
    }

    public IEnumerator Stop(float duration)
    {
        isStop = true;

        yield return new WaitForSeconds(duration);

        isStop = false;
    }

    private IEnumerator ChangeCool()
    {
        canChange = false;

        yield return new WaitForSeconds(changeCool);

        canChange = true;
    }

    public Vector2 GetAttackDir()
    {
        return (target.transform.position - transform.position).normalized;
    }
}
