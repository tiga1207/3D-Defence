using UnityEngine;

namespace Test
{
    public class AttackRangeTrigger : MonoBehaviour
    {
        private MonsterTest monster;

        void Awake()
        {
            monster = GetComponentInParent<MonsterTest>();
        }

        void OnTriggerEnter(Collider other)
        {
            if (other.transform == monster.attackTarget)
            {
                monster.StartAttack();
            }
        }

        void OnTriggerExit(Collider other)
        {
            if (other.transform == monster.attackTarget)
            {
                monster.ChaseTarget(monster.attackTarget);
            }
        }
    }
}
