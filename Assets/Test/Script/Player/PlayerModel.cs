using UnityEngine;

public class PlayerModel : MonoBehaviour
{
    [Header("이동/회전 설정")]
    public float moveSpeed = 5f;
    public float mouseSensitivity = 1f;
    [Range(-90f, 0f)] public float minPitch = -60f;
    [Range(0f, 90f)] public float maxPitch = 60f;

    public Vector2 inputDir;
    public Vector2 mouseDelta;
    public Vector2 currentRotation;
    public int hp;
    [SerializeField] private bool isCanInteract;
    public bool IsCanInteract { get => isCanInteract; set => isCanInteract = value; }

    public Rigidbody Rb { get; private set; }

    public Transform Avatar;
    public Transform Aim;

    void Awake()
    {
        Rb = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
