using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    #region VARIABLES
    public Animator _enemyAnimator;
    [Range(0.01f, 1.0f)]
    public float enemyMoveSpeed;
    public float dividedSpeed = 0.0f;
    public static float health;

    private CharacterController _enemyController;
    private GameObject player;
    private float mEnemyCurrentSpeed;
    private float maxEnemyHP;

    public enum EnemyState
    {
        Idle,
        Patrol,
        Chase,
        Attack,
        Death
    }

    EnemyState _enemyState;

    #endregion

    #region AWAKE
    private void Awake()
    {
        _enemyController = GetComponent<CharacterController>();
        _enemyAnimator = GetComponent<Animator>();
        _enemyState = EnemyState.Patrol;
        maxEnemyHP = 100.0f;
    }
    #endregion

    #region START
    private void Start()
    {
        health = maxEnemyHP;
        player = GameObject.FindGameObjectWithTag("Player");
    }
    #endregion

    #region UPDATE
    private void Update()
    {
        if(_enemyState.Equals(EnemyState.Idle))
        {
            _enemyAnimator.SetFloat("moveSpeed", 0.0f);
        }

        if(_enemyState.Equals(EnemyState.Patrol))
        {
            _enemyAnimator.SetFloat("moveSpeed", 0.5f);
        }

        if (_enemyState.Equals(EnemyState.Chase))
        {
            _enemyAnimator.SetFloat("moveSpeed", 1.0f);
            ChasePlayer();
        }

        if(_enemyState.Equals(EnemyState.Attack))
        {
            AttackPlayer();
        }
        
    }
    #endregion

    #region CHASE
    public void Chase()
    {
        _enemyState = EnemyState.Chase;
    }
    #endregion

    #region PATROL
    public void Patrol()
    {
        _enemyState = EnemyState.Idle;
        _enemyAnimator.SetBool("isChasing", false);
        _enemyAnimator.SetBool("isAttacking", false);
    }
    #endregion

    #region CHASE PLAYER
    private void ChasePlayer()
    {
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
    }
    #endregion

    #region ATTACK PLAYER
    private void AttackPlayer()
    {
        _enemyAnimator.SetBool("isAttacking", true);
    }
    #endregion

    #region SELF DESTROY
    private void SelfDestroy()
    {
        Destroy(this.gameObject);
    }
    #endregion
}
