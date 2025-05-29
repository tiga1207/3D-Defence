using UnityEngine;
using DesignPattern;

public class Bullet : PooledObject
{
    private Vector3 _direction;
    [SerializeField] private float speed = 5f;
    [SerializeField] private float lifeTime = 3f;

    [SerializeField] private float _timer;
    Rigidbody rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void OnEnable()
    {
        _timer = 0f;
        enabled = true;
    }
    public void BulletInit(Vector3 direction)
    {
        _direction = direction.normalized;
        _timer = 0f; // 수명 타이머 초기화
        if (rb != null)
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            rb.position = transform.position;
            rb.rotation = transform.rotation;
        }

    }

    void Update()
    {
        transform.position += _direction * speed * Time.deltaTime;

        _timer += Time.deltaTime;
        if (_timer >= lifeTime)
        {
            ReturnPool(); // 일정 시간 지나면 풀로 반환
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Monster"))
        {
            // 데미지 처리 등 가능
            ReturnPool(); // 풀로 반환
        }
    }
}