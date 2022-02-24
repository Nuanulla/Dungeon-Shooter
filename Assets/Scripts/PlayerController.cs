using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{


    private Rigidbody2D Rigidbody2D;
    private Vector3 moveDir;
    private const float moveSpd = 7f;
    private Vector2 mousePos;
    private Vector2 worldPos;
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

        mousePos = Mouse.current.position.ReadValue();
        worldPos = Camera.main.ScreenToWorldPoint(mousePos);

        // Tell game script which Companion you want to control
        if (Keyboard.current.digit1Key.wasPressedThisFrame)
        {
            selectedCompanion = Companion1;
            toggle1 = !toggle1;
        }
        if (Keyboard.current.digit2Key.wasPressedThisFrame)
        {
            selectedCompanion = Companion2;
            toggle2 = !toggle2;
        }
        if (Keyboard.current.digit3Key.wasPressedThisFrame)
        {
            selectedCompanion = Companion3;
            toggle3 = !toggle3;
        }
        if (Keyboard.current.digit4Key.wasPressedThisFrame)
        {
            selectedCompanion = Companion4;
            toggle4 = !toggle4;
        }
        if (toggle1 == false && toggle2 == false && toggle3 == false && toggle4 == false) // reset variable if no Companion button is toggled on
        {
            selectedCompanion = null;
        }
        
        
        // Tell Companion where to look
        if (selectedCompanion != null && Mouse.current.leftButton.isPressed)
        {
            selectedCompanion.state = 1;
            selectedCompanion.targetPos = worldPos;
            if (targetObj != null) // if targetObj exists, then...
            {
                Debug.Log(selectedCompanion.name + " wants to attack " + targetObj.name);
            }
            
        }

        // Tell Companion where to move
        if (selectedCompanion != null && Mouse.current.rightButton.isPressed)
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

    public void Move(InputAction.CallbackContext context)
    {
        float inputX = context.ReadValue<Vector2>().x;
        float inputY = context.ReadValue<Vector2>().y;
        moveDir = new Vector2(inputX, inputY);
    }
}
