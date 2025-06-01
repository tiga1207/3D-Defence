using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMoveTest : MonoBehaviour
{
     [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float mouseSensitivity = 1f;
    [SerializeField][Range(-90f, 0f)] private float minPitch = -60f;
    [SerializeField][Range(0f, 90f)] private float maxPitch = 60f;

    private Rigidbody rb;
    private Vector2 inputDir;
    private Vector2 mouseDelta;
    private Vector2 currentRotation;

    [Header("References")]
    [SerializeField] private Transform _avatar;
    [SerializeField] private Transform _aim;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void LateUpdate()
    {
        Rotate();
    }

    public void OnMove(InputValue value)
    {
        inputDir = value.Get<Vector2>();
    }

    public void OnRotate(InputValue value)
    {
        mouseDelta = value.Get<Vector2>() * mouseSensitivity;
        mouseDelta.y *= -1f;
    }

    private void Move()
    {
        Vector3 move = transform.right * inputDir.x + transform.forward * inputDir.y;
        Vector3 velocity = move * moveSpeed;
        velocity.y = rb.velocity.y;
        rb.velocity = velocity;
    }

    private void Rotate()
    {
        if(GameTimeManager.Instance.IsPaused) return;
        // if(GameManager.Instance.GameTime.IsPaused) return;
        
        currentRotation.x += mouseDelta.x;
        currentRotation.y = Mathf.Clamp(currentRotation.y + mouseDelta.y, minPitch, maxPitch);

        // 플레이어는 좌우 회전
        transform.rotation = Quaternion.Euler(0f, currentRotation.x, 0f);

        // 카메라 피벗은 상하 회전
        if (_aim != null)
        {
            _aim.localRotation = Quaternion.Euler(currentRotation.y, 0f, 0f);
        }
    }


}
