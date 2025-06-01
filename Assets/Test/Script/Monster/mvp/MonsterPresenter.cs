using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MonsterPresenter : MonoBehaviour
{
    private MonsterModel model;
    private IMonsterView view;

    private MonoBehaviour mono;
    private Coroutine attackCooldownCoroutine;



    public MonsterPresenter(MonsterModel _model, IMonsterView _view, MonoBehaviour _mono)
    {
        model = _model;
        view = _view;
        mono = _mono;

        model.Init();
        model.HP.Onchanged += OnHpChanged;
        model.MaxHP.Onchanged += OnHpChanged;
    }

    public void Update()
    {
        switch (model.currentState)
        {
            case MonsterModel.MonsterState.ChaseTarget:
                if (model.HasTarget())
                    model.UpdateDes();
                else
                    model.MoveToNexus();
                break;

            case MonsterModel.MonsterState.Attacking:
                if (!model.HasTarget())
                    model.MoveToNexus();
                break;
        }

        float speed = model.Agent.velocity.magnitude;
        view.PlayMoveAnimation(speed);
    }

    private void OnHpChanged()
    {
        int hp = model.HP.Value;
        int maxHp = model.MaxHP.Value;
        // view.UpdateHpBar(hp, maxHp);

        // if(hp <0)
        //사망 UI에 적용
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

    public void TryAttack()
    {
        Debug.Log("공격로직 외부");
        if (model.IsDead || model.isAttacking || !model.canAttack || !model.HasTarget()) return;
        Debug.Log("공격로직 내부");

        model.StartAttack();
        model.isAttacking = true;
        model.canAttack = false;
        // 공격 전 방향 회전
        model.LookTarget();

        view.PlayAttackAnimation(model.attackType);

        if (attackCooldownCoroutine == null)
            attackCooldownCoroutine = mono.StartCoroutine(AttackCooldown());
    }


    private IEnumerator AttackCooldown()
    {
        yield return new WaitForSeconds(model.attackCooldown);
        model.canAttack = true;
        model.isAttacking = false;
        attackCooldownCoroutine = null;
        if (model.isAttackInterrupted && model.HasTarget())
        {
            model.ChaseTarget(model.attackTarget);
            model.isAttackInterrupted = false;
        }
    }

    public void OnTargetDetected(Transform target)
    {
        if (!model.HasTarget()) //공격 타겟이 null 일때
            model.ChaseTarget(target);

    }

    public void OnLoseTarget(Transform target)
    {
        if (target == model.attackTarget)
            model.MoveToNexus();
    }
    public void OnInAttackRange(Transform target)
    {
        if (target == model.attackTarget)
        {
            // model.StartAttack();
            TryAttack();
        }
    }
    public void OnOutAttackRange(Transform target)
    {
        if (target == model.attackTarget)
        {
            Debug.Log("OnOutAttackRange 내부 로직 실행");
            //     model.isAttacking = false;
            //     model.canAttack = true;
            //     model.ChaseTarget(target);

            if (model.isAttacking)
            {
                // 공격 도중 이탈: 공격 끝나고 추적하도록 표시
                model.isAttackInterrupted = true;
            }
            else
            {
                // 공격 중이 아닐 때는 바로 추적
                model.ChaseTarget(target);
            }
        }
    }

}
