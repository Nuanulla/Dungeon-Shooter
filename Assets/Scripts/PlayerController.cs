using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private Rigidbody2D Rigidbody2D;
    private Vector3 moveDir;
    private const float moveSpd = 7f;
    private Vector3 worldPos;
    private Quaternion targetRot;

    public CompanionController Companion1;
    public CompanionController Companion2;
    public CompanionController Companion3;
    public CompanionController Companion4;

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

        if (moveDir != Vector3.zero)
        {
            targetRot = Quaternion.LookRotation(Vector3.forward, moveDir);
            targetRot = Quaternion.RotateTowards(transform.rotation, targetRot, 360 * Time.fixedDeltaTime);
        }


        Vector3 mousePos = Input.mousePosition;
        // mousePos.z = Camera.main.nearClipPlane;
        worldPos = Camera.main.ScreenToWorldPoint(mousePos);
        worldPos.z = 0f;

        if (Input.GetKey(KeyCode.Alpha1) && Input.GetMouseButton(1))
        {
            Companion1.atRest = false;
            Companion1.targetPos = worldPos;
        }
        if (Input.GetKey(KeyCode.Alpha2) && Input.GetMouseButton(1))
        {
            Companion2.atRest = false;
            Companion2.targetPos = worldPos;
        }
        if (Input.GetKey(KeyCode.Alpha3) && Input.GetMouseButton(1))
        {
            Companion3.atRest = false;
            Companion3.targetPos = worldPos;
        }
        if (Input.GetKey(KeyCode.Alpha4) && Input.GetMouseButton(1))
        {
            Companion4.atRest = false;
            Companion4.targetPos = worldPos;
        }
        
    }

    private void FixedUpdate()
    {
        Rigidbody2D.velocity = moveDir * moveSpd;
        Rigidbody2D.MoveRotation(targetRot);
    }

}
