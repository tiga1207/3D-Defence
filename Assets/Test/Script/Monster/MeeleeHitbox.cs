using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeeleeHitbox : MonoBehaviour
{
    [SerializeField] private Collider m_weaponCollider;
    [SerializeField] private MonsterModel model;
    void Awake()
    {
        m_weaponCollider.enabled = false;
        model = GetComponentInParent<MonsterModel>();
    }

    public void EnableHitbox()
    {
        m_weaponCollider.enabled = true;
    }

    public void DisableHitbox()
    {
        m_weaponCollider.enabled = false;
    }
    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            // other.GetComponent<PlayerHp>().TakeDamage(m_monsterBase.AttackDMG);
            Debug.Log($"{model.gameObject.name}이 플레이어에게 {model.atkDmg} 데미지를 입힘");

        }
    }

  
}
