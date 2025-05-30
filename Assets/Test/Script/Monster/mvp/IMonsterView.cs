using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Util;

public interface IMonsterView
{
    // void UpdateHpBar(int hp, int maxhp);

    void PlayAttackAnimation(MonsterModel.AttackType type);
    void PlayHitAnimation();
    void PlayDeathAnimation();
    void PlayMoveAnimation(float speed);
    // void ShootArrow(Vector3 direction);
    
}