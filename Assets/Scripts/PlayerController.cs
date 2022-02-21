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
    private CompanionController selectedCompanion = null;
    private bool toggle1 = false;
    private bool toggle2 = false;
    private bool toggle3 = false;
    private bool toggle4 = false;

    public EnemyController targetObj;

    public CompanionController Companion1;
    public CompanionController Companion2;
    public CompanionController Companion3;
    public CompanionController Companion4;

    private void Awake()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        
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

        


        // Convert mouse position to a location within the game worldspace
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = Camera.main.nearClipPlane;
        worldPos = Camera.main.ScreenToWorldPoint(mousePos);
        worldPos.z = 0f;

        // Tell game script which Companion you want to control
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            selectedCompanion = Companion1;
            toggle1 = !toggle1;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            selectedCompanion = Companion2;
            toggle2 = !toggle2;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            selectedCompanion = Companion3;
            toggle3 = !toggle3;
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            selectedCompanion = Companion4;
            toggle4 = !toggle4;
        }
        if (toggle1 == false && toggle2 == false && toggle3 == false && toggle4 == false) // reset variable if no Companion button is toggled on
        {
            selectedCompanion = null;
        }
        
        
        // Tell Companion where to look
        if (selectedCompanion != null && Input.GetMouseButton(0))
        {
            selectedCompanion.state = 1;
            selectedCompanion.targetPos = worldPos;
            if (targetObj != null) // if targetObj exists, then...
            {
                Debug.Log(selectedCompanion.name + " wants to attack " + targetObj.name);
            }
            
        }

        // Tell Companion where to move
        if (selectedCompanion != null && Input.GetMouseButton(1))
        {
            selectedCompanion.state = 2;
            selectedCompanion.targetPos = worldPos;
        }
        targetObj = null; // reset targetObj after each frame update
    }

    private void FixedUpdate()
    {
        Rigidbody2D.velocity = moveDir * moveSpd;

        // If GameObject is moving, affect rotation and rotate towards movement direction
        if (moveDir != Vector3.zero)
        {
            targetRot = Quaternion.LookRotation(Vector3.forward, moveDir);
            targetRot = Quaternion.RotateTowards(transform.rotation, targetRot, 720 * Time.fixedDeltaTime);
            Rigidbody2D.MoveRotation(targetRot);
        }
    }
}
