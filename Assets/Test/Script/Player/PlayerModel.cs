using UnityEngine;
using Util;
public class PlayerModel : MonoBehaviour
{
    // public enum AttackType { Meelee, Ranged, None }


    [Header("스탯")]
    [SerializeField] private int initHP;
    [SerializeField] private int initMaxHP;
    public Stat<int> HP { get; private set; }
    public Stat<int> MaxHP { get; private set; }
    public bool IsDead => HP.Value <= 0;


    [Header("공격 설정")]
    // [SerializeField] private int initAtkDmg =2;
    public Stat<int> Damage;
    public float attackCooldown = 1f;
    public bool canAttack = true;
    public bool isAttacking = false;

    [Header("이동/회전 설정")]
    public float moveSpeed = 5f;
    public float mouseSensitivity = 1f;
    [Range(-90f, 0f)] public float minPitch = -60f;
    [Range(0f, 90f)] public float maxPitch = 60f;

    public Vector2 inputDir;
    public Vector2 mouseDelta;
    public Vector2 currentRotation;
    [SerializeField] private bool isCanInteract;
    public bool IsCanInteract { get => isCanInteract; set => isCanInteract = value; }

    public Rigidbody Rb { get; private set; }

    public Transform Avatar;
    public Transform Aim;

    void Awake()
    {
        HP = new(initHP);
        MaxHP = new(initMaxHP);
        // Damage =new(initAtkDmg);
        Damage = new(PlayerData.Instance.atk);
        Rb = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

    }
}
