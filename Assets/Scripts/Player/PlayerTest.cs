using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Test
{

    public class PlayerTest : MonoBehaviour
    {
        // [SerializeField] private float moveSpeed = 5f;
        public int gold = 10;
        // private Rigidbody rb;
        // private Vector3 moveDirection;


        [SerializeField] private bool isCanInteract;
        public bool IsCanInteract { get => isCanInteract; set => isCanInteract = value; }


        // void Awake()
        // {
        //     rb = GetComponent<Rigidbody>();
        // }

        // void Update()
        // {
        //     // 입력 받기
        //     float moveX = Input.GetAxisRaw("Horizontal");
        //     float moveZ = Input.GetAxisRaw("Vertical");
        //     moveDirection = new Vector3(moveX, 0f, moveZ).normalized;

        //     // 바라보는 방향 설정
        //     if (moveDirection != Vector3.zero)
        //     {
        //         Quaternion toRotation = Quaternion.LookRotation(moveDirection, Vector3.up);
        //         transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, 720f * Time.deltaTime); // 부드럽게 회전
        //     }
        //     if (Input.GetKey(KeyCode.Space))
        //     {
        //         Destroy(gameObject);
        //     }
        // }

        // void FixedUpdate()
        // {
        //     rb.velocity = moveDirection * moveSpeed;
        // }
    }
}

