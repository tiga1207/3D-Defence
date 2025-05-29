using UnityEngine;

namespace Test
{
    public class AggroTrigger : MonoBehaviour
    {
        private MonsterTest monster;

        void Awake()
        {
            monster = GetComponentInParent<MonsterTest>();
        }

        void OnTriggerStay(Collider other)
        {
            if ((other.CompareTag("Player") || other.CompareTag("Tower")) && monster.attackTarget == null)
            {
                monster.ChaseTarget(other.transform);
            }
        }

        void OnTriggerExit(Collider other)
        {
            if (other.transform == monster.attackTarget)
            {
                monster.StopAttack(); // 타겟을 놓쳤을 때
            }
        }
    }
}
