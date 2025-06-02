using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeeleeHitbox : MonoBehaviour
{
    [SerializeField] private Collider m_weaponCollider;
    [SerializeField] private bool isPlayer = false;
    [SerializeField] private MonsterModel mosnterModel;
    [SerializeField] private PlayerModel playerModel;
    void Awake()
    {
        m_weaponCollider.enabled = false;
        if (isPlayer)
        {
            playerModel = GetComponentInParent<PlayerModel>();
        }
        else
        {
            mosnterModel = GetComponentInParent<MonsterModel>();
        }

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
        //몬스터가 공격하는 경우
        if (other.CompareTag("Player") && !isPlayer)
        {
            // other.GetComponent<PlayerHp>().TakeDamage(m_monsterBase.AttackDMG);
            Debug.Log($"{mosnterModel.gameObject.name}이 플레이어에게 {mosnterModel.Damage.Value} 데미지를 입힘");
            other.GetComponent<PlayerController>().TakeDamage(mosnterModel.Damage.Value);
        }
        //플레이어가 공격하는 경우
        if (other.CompareTag("Monster") && isPlayer)
        {
            Debug.Log($"{playerModel.gameObject.name}이 몬스터에게 {playerModel.Damage.Value} 데미지를 입힘");
            other.GetComponent<MonsterController>().TakeDamage(playerModel.Damage.Value);
            
        }
    }

  
}
