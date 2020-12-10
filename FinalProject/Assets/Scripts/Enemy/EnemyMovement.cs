using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    #region VARIABLES
    // public AudioClip deathClip;
    public float speed = 5.0f;
    public float maxSpeed = 10.0f;
    public Transform waypoint01;
    public Transform waypoint02;
    public float maxHealth = 100.0f;
    public enum EnemyState
    {
        Idle,
        Patrol,
        Chase,
        Attack,
        Death
    }

    private Transform destination;
    private NavMeshAgent _agent;
    private WaypointManager.Path _path;
    private Animator _enemyAnimator;
    // private AudioSource audioSource;
    private bool isDeath = false;
    private float _currentHealth = 0.0f;
    private float dividedSpeed = 0.0f;
    private float idleClipLength;
    private float deathClipLength;
    private EnemyState _enemyState;

    [Range(1.0f, 10.0f)]
    public float lookRadius = 5.0f;
    public GameObject player;
    Transform target;
    private float distance = 0.0f;

    #endregion

    #region ON DRAW GIZMO SELECTED
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }
    #endregion

    #region AWAKE
    private void Awake()
    {
        _enemyAnimator = GetComponent<Animator>();
        // audioSource = GetComponent<AudioSource>();
        _currentHealth = maxHealth;
        _enemyState = EnemyState.Patrol;
    }
    #endregion

    #region START
    private void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        target = player.transform;

        AnimationClip[] animations = _enemyAnimator.runtimeAnimatorController.animationClips;
        if(animations == null || animations.Length <= 0)
        {
            Debug.LogError("No animations");
            return;
        }
        for(int i = 0; i < animations.Length; ++i)
        {
            if(animations[i].name.Equals("Idle"))
            {
                idleClipLength = animations[i].length;
            }
            else if(animations[i].name.Equals("Falling Back Death"))
            {
                deathClipLength = animations[i].length;
            }
        }

        _enemyAnimator.SetBool("isPatroling", true);
        dividedSpeed = 1 / maxSpeed;
        destination = waypoint01;
    }
    #endregion

    #region UPDATE
    private void Update()
    {
        distance = Vector3.Distance(target.position, transform.position);

        if (distance <= lookRadius)
        {
            Chase();
        }

        UpdateAnimation();
    }
    #endregion

    #region UPDATE ANIMATION
    private void UpdateAnimation()
    {
        if(_enemyState.Equals(EnemyState.Idle))
        {
            _enemyAnimator.SetBool("isPatroling", false);
            StartCoroutine("IdleTime");
        }
        else if(_enemyState.Equals(EnemyState.Patrol))
        {
            PatrolArea();
        }
        else if (_enemyState.Equals(EnemyState.Chase))
        {
            ChasePlayer();
        }
        else if (_enemyState.Equals(EnemyState.Attack))
        {
            AttackPlayer();
        }
    }
    #endregion

    #region PATROL
    public void Patrol()
    {
        _enemyState = EnemyState.Patrol;
        _enemyAnimator.SetBool("isAttacking", false);
        _enemyAnimator.SetBool("isChasing", false);
        _enemyAnimator.SetBool("isPatroling", true);
    }
    #endregion

    #region PATOL AREA
    private void PatrolArea()
    {
        if (_agent != null)
        {
            _agent.SetDestination(destination.position);
            _agent.speed = maxSpeed;
        }
        _enemyAnimator.SetFloat("enemySpeed", _agent.velocity.magnitude * dividedSpeed * 0.5f);

        if ((transform.position - destination.position).sqrMagnitude < 3.0f * 3.0f)
        {
            _enemyState = EnemyState.Idle;
            _enemyAnimator.SetBool("isPatroling", false);
            destination = destination.Equals(waypoint01) ? waypoint02 : waypoint01;
        }
    }
    #endregion

    #region CHASE
    public void Chase()
    {
        _enemyState = EnemyState.Chase;
        _enemyAnimator.SetBool("isPatroling", false);
        _enemyAnimator.SetBool("isChasing", true);
    }
    #endregion

    #region CHASE PLAYER
    private void ChasePlayer()
    {
        _agent.SetDestination(target.position);

        if ((transform.position - target.position).sqrMagnitude < 3.0f * 3.0f)
        {
            _enemyAnimator.SetBool("isAttacking", true);
            _enemyState = EnemyState.Attack;
        }
        else
        {
            Patrol();
        }
        /*
        _enemyAnimator.SetBool("isChasing", true);

        Vector3 playerPosition = player.transform.position;
        Vector3 enemyPlayerDistance = playerPosition - this.transform.position;

        playerPosition.y = this.transform.position.y;
        this.transform.LookAt(playerPosition);
        _enemyController.Move(enemyMoveSpeed * transform.TransformDirection(Vector3.forward));

        if(enemyPlayerDistance.magnitude < 1.5f)
        {
            _enemyState = EnemyState.Attack;
        }
        */
    }
    #endregion

    #region IDLE TIME
    private IEnumerator IdleTime()
    {
        yield return new WaitForSeconds(idleClipLength * 2.0f);
        Patrol();
    }
    #endregion

    #region ATTACK PLAYER
    private void AttackPlayer()
    {
        
    }
    #endregion

    #region RECEIVE DAMAGE
    public void ReceiveDamage(float damage)
    {
        _currentHealth -= damage;
        if(_currentHealth <= 0.0f)
        {
            StartCoroutine("Defeated");
        }
    }
    #endregion

    #region DEFEATED
    public IEnumerator Defeated()
    {
        isDeath = true;
        _enemyAnimator.SetBool("isDeath", isDeath);
        yield return new WaitForSeconds(deathClipLength * 1.5f);
        SelfDestroy(gameObject);
    }
    #endregion

    #region SELF DESTROY
    private void SelfDestroy(GameObject gameObject)
    {
        gameObject.SetActive(false);
    }
    #endregion
}
