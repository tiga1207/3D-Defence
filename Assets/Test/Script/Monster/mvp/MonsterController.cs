using System;
using System.Collections;
using System.Collections.Generic;
using Test;
using UnityEngine;


public class MonsterController : MonoBehaviour, IAggroListener, IAttackRangeListener
{
    private MonsterPresenter presenter;
    // public event Action OnMonsterDied;

    void Start()
    {
        var model = GetComponent<MonsterModel>();
        var view = GetComponent<IMonsterView>();
        presenter = new(model, view, this);
    }
    void Update()
    {
        presenter.Update();
        // presenter.TryAttack();
    }
    public void TakeDamage(int dmg)
    {
        presenter.TakeDamage(dmg);
    }
    

    //인터페이스 구현
    public void OnTargetDetected(Transform target) => presenter.OnTargetDetected(target);
    public void OnLoseTarget(Transform target) => presenter.OnLoseTarget(target);
    public void OnInAttackRange(Transform target) => presenter.OnInAttackRange(target);
    public void OnOutAttackRange(Transform target) => presenter.OnOutAttackRange(target);

    // public void SetTarget(IDamageable target) =>presenter.SetTarget(target);

    // public void ClearTager(IDamageable target) =>presenter.ClearTarget(target);


}
