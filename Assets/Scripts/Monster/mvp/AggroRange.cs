using UnityEngine;

namespace Test
{
    public class AggroRange : MonoBehaviour
    {
        private IAggroListener listener;

        void Awake()
        {
            listener = GetComponentInParent<IAggroListener>();
        }

        void OnTriggerStay(Collider other)
        {
            if (other.CompareTag("Player") || other.CompareTag("Tower"))
            {
                listener.OnTargetDetected(other.transform);
            }
        }

        void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player") || other.CompareTag("Tower"))
            { listener.OnLoseTarget(other.transform); }
        }
    }
}