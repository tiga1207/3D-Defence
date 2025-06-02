using UnityEngine;
public class MeelAttack: MonoBehaviour
{
    MonsterModel monsterModel;
    PlayerModel playerModel;
    MeeleeHitbox meeleeHitbox;
    [SerializeField] private bool isPlayer = false;


    void Awake()
    {
        if (isPlayer)
        {
            playerModel = GetComponent<PlayerModel>();
        }
        else
        {
            monsterModel = GetComponent<MonsterModel>();
        }
        meeleeHitbox = GetComponentInChildren<MeeleeHitbox>();
    }
    public void HitboxStart()
    {
        if (isPlayer)
        {
            playerModel.isAttacking = true;
        }
        else
        {
            monsterModel.isAttacking = true;
        }

        meeleeHitbox.EnableHitbox();
    }
    //공격 애니메이션 마지막 프레임에 호출
    public void HitboxEnd()
    {

        if (isPlayer)
        {
            playerModel.isAttacking = false;
        }
        else
        {
            monsterModel.isAttacking = false;
        }

        meeleeHitbox.DisableHitbox();
    }

}