using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MonsterPresenter
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
        view.Init(this);
    }

    public void Update()
    {
        if (model.IsDead) return;
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
    public void TakeDamage(int _dmg)
    {
        model.HP.Value -= _dmg;
        view.PlayHitAnimation();
        if (model.HP.Value <= 0)
        {
            view.PlayDeathAnimation();
        }
    }
    public void AfterDied()
    {
        // 코인 생성
        Vector3 coinTransform = model.transform.position + Vector3.up;
        GameObject.Instantiate(model.coinObj, coinTransform, Quaternion.identity);

        // 사망 알림
        model.Die();

        // 파괴
        GameObject.Destroy(model.gameObject);
    }

    public void TryAttack()
    {
        if (model.IsDead || model.isAttacking || !model.canAttack || !model.HasTarget()) return;

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
