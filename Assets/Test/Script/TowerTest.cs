using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Test
{

    public class TowerTest : MonoBehaviour
    {
        public int level;
        public int dmg;

        //총구 위치
        [SerializeField] private Transform shootingTransform;
        [SerializeField] private Transform targetTransform;

        //포신 머리
        // [SerializeField] private GameObject turretHead;

        void Start()
        {

        }

        void Update()
        {
            if (targetTransform != null)
            {
                RotateTarret();
                Vector3 direction =  (targetTransform.position - transform.position).normalized;
                
            }
        }

        //포탑 몬스터 방향으로 회전
        private void RotateTarret()
        {
            Vector3 target = new(targetTransform.position.x, transform.position.y, targetTransform.position.z);
            transform.LookAt(target);
        }

        void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Monster"))
            {
                if (targetTransform == null)
                {

                    targetTransform = other.transform;
                }
            }
        }


        void OnTriggerExit(Collider other)
        {
            targetTransform = null;
        }



    }

}