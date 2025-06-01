using UnityEngine;
using UnityEngine.UI;

public class MonsterView : MonoBehaviour, IMonsterView
{
    [SerializeField] private Slider hpBarSlider;

    [SerializeField] Animator anim;

    public void UpdateHpBar(int hp, int maxhp)
    {
        hpBarSlider.value = hp;
        hpBarSlider.maxValue = maxhp;
    }

    public void PlayAttackAnimation(MonsterModel.AttackType type)
    {
        Debug.Log($"[애니메이션 호출] AttackType: {type}");
        anim.SetInteger("AttackType", (int)type);
        anim.SetTrigger("isAttack");
    }

    public void PlayHitAnimation() => anim.SetTrigger("isHit");

    public void PlayDeathAnimation() => anim.SetBool("isMonsterDie", true);

    public void PlayMoveAnimation(float speed) => anim.SetFloat("Speed", speed, 0.2f, Time.deltaTime);


}