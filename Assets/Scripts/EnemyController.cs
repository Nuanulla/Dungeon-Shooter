using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour
{

    public PlayerController player;
    private Rigidbody2D Rigidbody2D;
    private Vector3 vectorDiff;
    public Vector3 targetPos;
    public Quaternion targetRot;
    private Vector3 currentPos;

    public GameObject healthBar;
    public Slider healthBarSlider;
    public int health = 100; //remove '100' value and set from each enemy type's individual scripts
    public int currentHealth = 100;

    

    private void Awake()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();
        healthBarSlider = healthBar.GetComponent<Slider>();
    }

    // Start is called before the first frame update
    void Start()
    {
        healthBar.SetActive(true);
        healthBarSlider.minValue = 0;
        healthBarSlider.maxValue = health;
    }

    void Update()
    {
        // Below script calculate current position and vector difference of a current position and target position
        currentPos = transform.position;
        vectorDiff = targetPos - currentPos;

        // Update Health Bar with value of currentHealth at each frame
        healthBarSlider.value = currentHealth;
    }

    private void FixedUpdate()
    {
        healthBarSlider.transform.position = new Vector3(currentPos.x, currentPos.y - 1f, currentPos.z);
        var nearestAlly = AllyGroup.FindClosestAlly(transform.position);
        if (nearestAlly != null)
        {
            targetPos = nearestAlly.transform.position;
            LookAtTarget();
        }
    }

    void TakeDamage(int damage)
    {
        currentHealth -= damage;
    }

    void OnMouseDown()
    {
        player.targetObj = this;
    }

    void LookAtTarget()
    {
        targetRot = Quaternion.LookRotation(Vector3.forward, vectorDiff.normalized);
        targetRot = Quaternion.RotateTowards(transform.rotation, targetRot, 360 * Time.fixedDeltaTime);
        Rigidbody2D.MoveRotation(targetRot);
    }
}
