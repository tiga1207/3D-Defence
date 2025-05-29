using System.Collections;
using UnityEngine;
using DesignPattern;

namespace Test
{
    public class TowerTest : MonoBehaviour
    {
        public int level;
        public int dmg;

        [SerializeField] private Transform shootingTransform;
        [SerializeField] private Bullet bulletPrefab;

        [SerializeField] private float delayTime = 0.3f;
        YieldInstruction fireDelay;

        public Transform targetTransform;
        public Coroutine shootCoroutine;
        private ObjectPool bulletPool;
        void Start()
        {
            fireDelay = new WaitForSeconds(delayTime);

             bulletPool = new ObjectPool(gameObject.transform, bulletPrefab, 10);
        }

        void Update()
        {
            if (targetTransform != null)
                RotateTurret();
        }

        void OnDestroy()
        {
            if (shootCoroutine != null)
            {
                StopCoroutine(shootCoroutine);
                shootCoroutine = null;
            }
            targetTransform = null;
        }

        private void RotateTurret()
        {
            Vector3 target = new(targetTransform.position.x, transform.position.y, targetTransform.position.z);
            transform.LookAt(target);
        }

        public IEnumerator CoShoot()
        {
            while (targetTransform != null)
            {
                if (targetTransform != null)
                {
                    yield return fireDelay;
                    Shoot();
                }
            }
            shootCoroutine = null;
        }

        private void Shoot()
        {
            if (bulletPrefab == null || shootingTransform == null || targetTransform == null)
                return;
            Vector3 direction = targetTransform.position - shootingTransform.position;
            Bullet bullet = bulletPool.PopPool() as Bullet;
            bullet.transform.position = shootingTransform.position;
            bullet.transform.rotation = Quaternion.LookRotation(direction);
            bullet.BulletInit(direction);

        }

    }
}
