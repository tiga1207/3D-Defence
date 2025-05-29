using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    [SerializeField] PlayerModel model;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            model.HP -= 1;
        }

        Move();
    }

    private void Move()
    {
        float xInput = Input.GetAxis("Horizontal");
        float zInput = Input.GetAxis("Vertical");

        Vector3 dir = new Vector3(xInput, 0, zInput);
        if (dir.sqrMagnitude > 1)
        {
            dir = dir.normalized;
        }

        model.Velocity = Vector3.MoveTowards(model.Velocity, dir * 5, 10 * Time.deltaTime);
    }
}
