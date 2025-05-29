using UnityEngine;
using UnityEngine.AI;

namespace Test
{
    public enum EnemyState { MoveToNexus, ChaseTarget, Attacking }

    public class MonsterTest : MonoBehaviour
    {
        public EnemyState currentState = EnemyState.MoveToNexus;
        private NavMeshAgent agent;

        [SerializeField] private Transform nexusTarget;
        public Transform attackTarget;

        void Start()
        {
            agent = GetComponent<NavMeshAgent>();
            if (agent.isOnNavMesh)
                MoveToNexus();
        }

        void Update()
        {
            switch (currentState)
            {
                case EnemyState.ChaseTarget:
                    if (attackTarget != null)
                        agent.SetDestination(attackTarget.position);
                    else
                        MoveToNexus();
                    break;

                case EnemyState.Attacking:
                    if (attackTarget == null)
                        MoveToNexus();
                    break;
            }
        }

        public void MoveToNexus()
        {
            currentState = EnemyState.MoveToNexus;
            attackTarget = null;
            agent.isStopped = false;
            agent.SetDestination(nexusTarget.position);
        }

        public void ChaseTarget(Transform target)
        {
            currentState = EnemyState.ChaseTarget;
            attackTarget = target;
            agent.isStopped = false;
        }

        public void StartAttack()
        {
            if (attackTarget == null) return;

            currentState = EnemyState.Attacking;
            agent.isStopped = true;

            // TODO: 공격 애니메이션 및 공격 처리
            Debug.Log("공격 중!");
        }

        public void StopAttack()
        {
            MoveToNexus();
        }
    }
}
