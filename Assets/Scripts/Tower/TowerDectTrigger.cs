using System.Collections;
using System.Collections.Generic;
using Test;
using UnityEngine;

public class TowerDectTrigger : MonoBehaviour
{
    private TowerTest tower;

    void Awake()
    {
        tower = GetComponentInParent<TowerTest>();
    }
    //TODO: 여기서 몬스터 감지시, 해당 몬스터 HP컴포넌트 획득 하기.
    void OnTriggerStay(Collider other)
    {

        if (other.CompareTag("Monster"))
        {
            Debug.Log("몬스터 발견");
            if (tower.targetTransform == null)
            {
                tower.targetTransform = other.transform;
            }

            if (tower.shootCoroutine == null)
            {
                tower.shootCoroutine = StartCoroutine(tower.CoShoot());
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Monster") && other.transform == tower.targetTransform)
        {
            tower.targetTransform = null;

            if (tower.shootCoroutine != null)
            {
                StopCoroutine(tower.shootCoroutine);
                tower.shootCoroutine = null;
            }
        }
    }
}
