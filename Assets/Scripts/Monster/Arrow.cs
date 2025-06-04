using System.Collections;
using System.Collections.Generic;
using DesignPattern;
using Test;
using UnityEngine;

public class Arrow : PooledObject
{
    // private ArrowPool m_arrowPool;
    // private int m_damage;
    [SerializeField] private float speed = 5f;

    //타이머 변수
    [SerializeField] private float timer;

    [SerializeField] private float lifeTime = 3f;
    private int damage;

    //화살 위치
    private Vector3 _direction;

    void Awake()
    {
        //화살끼리의 충돌 무시
        Physics.IgnoreLayerCollision(this.gameObject.layer, this.gameObject.layer, true);
        //화살과 몬스터의 충돌 무시
        Physics.IgnoreLayerCollision(this.gameObject.layer, LayerMask.NameToLayer("Monster"), true);
    }

    private void OnEnable()
    {
        timer = 0f;
    }

    public void ArrowInit(Vector3 direction, int _damage)
    {
        _direction = direction.normalized;
        timer = 0f; // 수명 타이머 초기화
        transform.rotation = Quaternion.LookRotation(_direction);
        damage =_damage;
    }
    void Update()
    {
        //화살 앞 방향 세팅

        transform.position += _direction * speed * Time.deltaTime;
        //타이머 동작
        timer += Time.deltaTime;
        //타이머가 화살이 사라지는 시간과 같다면 ->화살 파괴
        if (timer >= lifeTime)
        {
            //풀에 반납하기

            ReturnPool();
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController player = other.GetComponent<PlayerController>();
            player.TakeDamage(damage);

            // 데미지 처리 등 가능
        }
        if (other.CompareTag("Tower"))
        {
            TowerTest tower = other.GetComponent<TowerTest>();
            tower.TakeDamage(damage);
        }
        ReturnPool(); // 풀로 반환

    }

}
