using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private Rigidbody2D Rigidbody2D;
    private Vector3 moveDir;
    private const float moveSpd = 7f;

    private void Awake()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        float moveX = 0f;
        float moveY= 0f;

        if (Input.GetKey(KeyCode.W))
        {
            moveY = +1f;
        }
        if (Input.GetKey(KeyCode.A))
        {
            moveX = -1f;
        }
        if (Input.GetKey(KeyCode.S))
        {
            moveY = -1f;
        }
        if (Input.GetKey(KeyCode.D))
        {
            moveX = +1f;
        }

        moveDir = new Vector3(moveX, moveY).normalized;

        if (Input.GetKey(KeyCode.Alpha1))
        {
            
        }
        if (Input.GetKey(KeyCode.Alpha2))
        {
            
        }
        if (Input.GetKey(KeyCode.Alpha3))
        {
            
        }
        if (Input.GetKey(KeyCode.Alpha4))
        {
            
        }
    }

    private void FixedUpdate()
    {
        Rigidbody2D.velocity = moveDir * moveSpd;
    }

}
