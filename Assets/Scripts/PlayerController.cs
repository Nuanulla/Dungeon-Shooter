using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{


    private Rigidbody2D Rigidbody2D;
    private CompanionStats PlayerStats;
    private Vector3 moveDir;
    private const float moveSpd = 7f;
    private Vector3 mousePos;
    private Vector3 worldPos;
    private Vector3 vectorDiff;
    private Quaternion targetRot;
    private CompanionController selectedCompanion = null;
    public bool toggle1 = false;
    public bool toggle2 = false;

    public CompanionController Companion1;
    public CompanionController Companion2;

    private float attackRate = 0.4f;
    private float attackDelay;
    public GameObject Projectile;
    private GameObject cast_projectile;

    public AudioClip attack;

    private void Awake()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();
        PlayerStats = gameObject.GetComponent<CompanionStats>();
        PlayerStats.health = 100;
        PlayerStats.mana = 100;
        PlayerStats.InitiatePlayerDisplayOverlay();
    }

    private void Update()
    {

        mousePos = Mouse.current.position.ReadValue();
        worldPos = Camera.main.ScreenToWorldPoint(mousePos);
        worldPos.z = 0f;
        vectorDiff = worldPos - transform.position;


        // Tell game script which Companion you want to control
        if (Keyboard.current.digit1Key.wasPressedThisFrame)
        {
            selectedCompanion = Companion1;
            toggle1 = !toggle1;
            toggle2 = false;
        }
        if (Keyboard.current.digit2Key.wasPressedThisFrame)
        {
            selectedCompanion = Companion2;
            toggle2 = !toggle2;
            toggle1 = false;
        }
        if (toggle1 == false && toggle2 == false) // reset variable if no Companion button is toggled on
        {
            selectedCompanion = null;
        }
        
        
        // Tell Companion where to look
        if (selectedCompanion != null && Mouse.current.leftButton.isPressed)
        {
            selectedCompanion.state = 1;
            selectedCompanion.targetPos = worldPos;
        }

        // Tell Companion where to move
        if (selectedCompanion != null && Mouse.current.rightButton.isPressed)
        {
            selectedCompanion.state = 2;
            selectedCompanion.targetPos = worldPos;
        }

        if (selectedCompanion == null && Mouse.current.leftButton.isPressed && Time.fixedTime > attackDelay)
        {
            attackDelay = Time.fixedTime + attackRate;
            AudioSource.PlayClipAtPoint(attack, transform.position);
            cast_projectile = Instantiate(Projectile, transform.position + transform.up + (transform.right / 4), transform.rotation);
            cast_projectile.SetActive(true);
        }
    }

    private void FixedUpdate()
    {

        Rigidbody2D.velocity = moveDir * moveSpd;
        targetRot = Quaternion.LookRotation(Vector3.forward, vectorDiff.normalized);
        targetRot = Quaternion.RotateTowards(transform.rotation, targetRot, 1080 * Time.fixedDeltaTime);
        Rigidbody2D.MoveRotation(targetRot);
    }

    public void Move(InputAction.CallbackContext context)
    {
        float inputX = context.ReadValue<Vector2>().x;
        float inputY = context.ReadValue<Vector2>().y;
        moveDir = new Vector2(inputX, inputY);
    }

}
