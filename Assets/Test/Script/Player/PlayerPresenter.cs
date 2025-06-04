using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerPresenter
{
    private PlayerModel model;
    private IPlayerView view;
    private Coroutine attackCooldownCoroutine;
    private Coroutine invincibleTimeCoroutine;
    private MonoBehaviour mono;



    public PlayerPresenter(PlayerModel _model, IPlayerView _view, MonoBehaviour _mono)
    {
        model = _model;
        view = _view;
        mono = _mono;
    }

    public void Init()
    {
        model.HP.Onchanged += OnHpChanged;
        model.MaxHP.Onchanged += OnHpChanged;
        OnHpChanged();
    }

    public void OnMove(InputValue value)
    {
        model.inputDir = value.Get<Vector2>();
    }

    public void OnRotate(InputValue value)
    {
        // model.mouseDelta = value.Get<Vector2>() * model.mouseSensitivity;
        model.mouseDelta = value.Get<Vector2>() * PlayerData.Instance.mouseSensitivity;
        model.mouseDelta.y *= -1f;
    }

    public void Move()
    {
        Vector3 move = model.transform.right * model.inputDir.x + model.transform.forward * model.inputDir.y;
        Vector3 velocity = move * model.moveSpeed;
        velocity.y = model.Rb.velocity.y;
        model.Rb.velocity = velocity;
    }

    public void Rotate()
    {
        if (GameTimeManager.Instance.IsPaused) return;

        model.currentRotation.x += model.mouseDelta.x;
        model.currentRotation.y += model.mouseDelta.y;

        view.ApplyRotation(model.transform, model.Aim, model.currentRotation, model.minPitch, model.maxPitch);
    }

    public void OnHpChanged()
    {
        int currHp = model.HP.Value;
        int maxHp = model.MaxHP.Value;

        view.UpdateHpBar(currHp, maxHp);
    }

    private IEnumerator AttackCooldown()
    {
        yield return new WaitForSeconds(model.attackCooldown);
        model.canAttack = true;
        model.isAttacking = false;
        attackCooldownCoroutine = null;
    }

    public void Update()
    {
        float speed = model.Rb.velocity.magnitude;
        view.PlayMoveAnimation(speed);
    }
    public void TryAttack()
    {
        Debug.Log("공격로직 외부");
        if (model == null)
        {
            Debug.Log("모델이 없습니다");
            return;
        }
        if (model.IsDead || model.isAttacking || !model.canAttack) return;
        Debug.Log("공격로직 내부");

        model.isAttacking = true;
        model.canAttack = false;

        view.PlayAttackAnimation();

        if (attackCooldownCoroutine == null)
            attackCooldownCoroutine = mono.StartCoroutine(AttackCooldown());
    }
    public void TakeDamage(int _dmg)
    {
        if (model.IsDead || model.isInvincible) return;

        model.HP.Value -= _dmg;
        view.PlayHitAnimation();

        if (model.HP.Value <= 0)
        {
            view.PlayDeathAnimation();
            model.Die();
        }
        model.isInvincible = true;
        
        if (invincibleTimeCoroutine == null)
            invincibleTimeCoroutine = mono.StartCoroutine(IE_invincibleTime());
        
    }
    private IEnumerator IE_invincibleTime()
    {
        yield return new WaitForSeconds(model.invincibleTime);
        model.isInvincible = false;
        invincibleTimeCoroutine = null;
    }
    
}
