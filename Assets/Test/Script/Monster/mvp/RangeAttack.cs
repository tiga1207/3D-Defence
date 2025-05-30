using DesignPattern;
using UnityEngine;

public class RangeAttack : MonoBehaviour
{
    [SerializeField] Transform somwhere;
    private MonsterModel model;

    [SerializeField] private Arrow arrowPrefab;
    private ObjectPool arrowPool;

    //화살 쏘는 위치
    [SerializeField] private Transform arrowShootingTransform;

    void Awake()
    {
        model = GetComponent<MonsterModel>();
    }
    void Start()
    {
        arrowPool = new ObjectPool(gameObject.transform, arrowPrefab, 10);
    }
    public void ShotingArrow()
    {
       
        if (arrowPrefab == null || arrowShootingTransform == null || model.attackTarget == null) return;

        Vector3 dircetionToPlayer = model.attackTarget.position - arrowShootingTransform.position;
        Arrow arrow = arrowPool.PopPool() as Arrow;
        arrow.transform.position = arrowShootingTransform.position;

        arrow.ArrowInit(dircetionToPlayer);
    }

    //공격 애니메이션 첫 프레임에 호출
    public void ShootArrowStart()
    {
        // m_isAttacking= true;
        model.isAttacking = true;
    }
    //공격 애니메이션 마지막 프레임에 호출
    public void ShootArrowEnd()
    {
        model.isAttacking = false;
        // m_isAttacking = false;
        // AttackCoolDownStart();
    }
}