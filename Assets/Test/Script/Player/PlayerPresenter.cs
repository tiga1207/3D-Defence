using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerPresenter
{
    private PlayerModel model;
    private IPlayerView view;
    private Coroutine attackCooldownCoroutine;
    private MonoBehaviour mono;



    public PlayerPresenter(PlayerModel _model, IPlayerView _view, MonoBehaviour _mono)
    {
        model = _model;
        view = _view;
        mono = _mono;
    }

    public void OnMove(InputValue value)
    {
        model.inputDir = value.Get<Vector2>();
    }

    public void OnRotate(InputValue value)
    {
        model.mouseDelta = value.Get<Vector2>() * model.mouseSensitivity;
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
    public void Test()
    {
        Debug.Log(model.HP.Value);
    }
    public void TakeDamage(int _dmg)
    {
        model.HP.Value -= _dmg;
        view.PlayHitAnimation();
        if (model.HP.Value <= 0)
        {
            view.PlayDeathAnimation();
        }
    }
}
