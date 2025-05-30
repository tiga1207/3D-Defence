using UnityEngine;
public class MeelAttack: MonoBehaviour
{
    MonsterModel model;

    void Awake()
    {
        model = GetComponent<MonsterModel>();
    }
    public void HitboxStart()
        {
            model.isAttacking= true;
            GetComponentInChildren<MeeleeHitbox>().EnableHitbox();
        }
        //공격 애니메이션 마지막 프레임에 호출
        public void HitboxEnd()
        {
            model.isAttacking = false;
            // AttackCoolDownStart();
            GetComponentInChildren<MeeleeHitbox>().DisableHitbox();
        }

}