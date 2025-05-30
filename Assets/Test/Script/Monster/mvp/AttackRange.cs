using UnityEngine;

namespace Test
{
    public class AttackRange : MonoBehaviour
    {
        private IAttackRangeListener listener;

        void Awake()
        {
            listener = GetComponentInParent<IAttackRangeListener>();
        }

        void OnTriggerStay(Collider other)
        {
            if (other.CompareTag("Player") || other.CompareTag("Tower"))
            {
                Debug.Log("공격범위");
                listener.OnInAttackRange(other.transform);
            }
        }

        void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player") || other.CompareTag("Tower"))
            { 
                listener.OnOutAttackRange(other.transform); 
                Debug.Log("공격범위 밖으로 나감.");
            }
        }
    }
}
