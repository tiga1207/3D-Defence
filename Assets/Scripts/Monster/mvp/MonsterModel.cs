using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Util;

public class MonsterModel : MonoBehaviour
{
    public enum MonsterState { MoveToNexus = 0, ChaseTarget, Attacking }
    public enum AttackType { Meelee = 0, Ranged = 1 }
    public enum MonsterType { Goblin, Skeleton, Etc }

    public bool HasTarget() => attackTarget != null;
    public bool IsDead => HP.Value <= 0;
    // public enum AttackType { Meelee, Ranged, None }


    [Header("스탯")]
    [SerializeField] private int initHP;
    [SerializeField] private int initMaxHP;
    public Stat<int> HP { get; private set; }
    public Stat<int> MaxHP { get; private set; }
    public GameObject coinObj;
    public static event Action OnMonsterDied;


    [Header("공격 설정")]
    // public int atkDmg =2;
    [SerializeField] private int initAtkDmg = 2;
    public Stat<int> Damage;
    public AttackType attackType;
    public float attackCooldown = 1f;
    public bool canAttack = true;
    // public bool CanAttack { get; set; } = true;
    public bool isAttacking = false;
    // public bool IsAttacking { get; set; } = false;
    public bool isAttackInterrupted = false;

    [Header("이동 및 추적")]
    // [SerializeField] private Transform nexusTarget;
    private Transform nexusTarget;
    public MonsterState currentState;
    // public MonsterState CurrentState { get; private set; }
    public Transform attackTarget;
    // public Transform AttackTarget { get; private set; }
    private NavMeshAgent agent;
    public NavMeshAgent Agent => agent;
    // public IDamageable targetDamageable;

    // public Rigidbody Rb { get; private set; }

    void Awake()
    {
        // Rb = GetComponent<Rigidbody>();
        HP = new(initHP);
        MaxHP = new(initMaxHP);
        Damage = new(initAtkDmg);

        agent = GetComponent<NavMeshAgent>();
    }
    public void Init()
    {
        nexusTarget = GameManager.Instance.NexusTransform;
        if (agent.isOnNavMesh)
            MoveToNexus();
    }

    public void MoveToNexus()
    {
        currentState = MonsterState.MoveToNexus;
        attackTarget = null;
        agent.isStopped = false;
        agent.SetDestination(nexusTarget.position);
    }

    public void ChaseTarget(Transform target)
    {
        currentState = MonsterState.ChaseTarget;
        attackTarget = target;
        agent.isStopped = false;
    }

    public void StartAttack()
    {
        if (attackTarget == null) return;

        currentState = MonsterState.Attacking;
        agent.isStopped = true;

        LookTarget();

    }
    public void LookTarget()
    {
        if (attackTarget == null) return;

        Vector3 dir = attackTarget.position - transform.position;
        dir.y = 0;
        if (dir != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(dir);
        }
    }
    public void UpdateDes()
    {
        if (currentState == MonsterState.ChaseTarget && attackTarget != null)
            agent.SetDestination(attackTarget.position);
    }

    public void Stop()
    {
        agent.isStopped = true;
    }
    public void Die()
    {
        agent.isStopped = true;
        agent.enabled = false;
        OnMonsterDied?.Invoke();
    }

}
