using UnityEngine;

namespace Test
{
    public interface IAggroListener
    {
        void OnTargetDetected(Transform target);
        void OnLoseTarget(Transform target);
    }

    public interface IAttackRangeListener
    {
        void OnInAttackRange(Transform target);
        void OnOutAttackRange(Transform target);
    }
}