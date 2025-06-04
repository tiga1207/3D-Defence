using UnityEngine;
using UnityEngine.UI;

public class PlayerView : MonoBehaviour, IPlayerView
{
    [SerializeField] Animator anim;
    [SerializeField] Slider hpSlider;

    public void ApplyRotation(Transform player, Transform aim, Vector2 rotation, float minPitch, float maxPitch)
    {
        player.rotation = Quaternion.Euler(0f, rotation.x, 0f);
        if (aim != null)
        {
            aim.localRotation = Quaternion.Euler(Mathf.Clamp(rotation.y, minPitch, maxPitch), 0f, 0f);
        }
    }

    public void UpdateHpBar(int currentHp, int maxHp)
    {
        hpSlider.maxValue = maxHp;
        hpSlider.value = currentHp;
    }

    //애니메이션

    public void PlayAttackAnimation()
    {
        anim.SetTrigger("isAttack");
    }

    public void PlayHitAnimation() => anim.SetTrigger("isHit");

    public void PlayDeathAnimation() => anim.SetBool("isMonsterDie", true);

    public void PlayMoveAnimation(float speed) => anim.SetFloat("Speed", speed, 0.2f, Time.deltaTime);
    public void Died()
    {
        Destroy(gameObject);
    }

    
}
