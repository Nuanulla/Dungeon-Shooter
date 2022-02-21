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
    private const float moveSpd = 3f;

    public Canvas Canvas;
    private GameObject enemyHealth;
    private Slider healthBarSlider;
    public int health = 100; //remove '100' value and set from each enemy type's individual scripts
    public int currentHealth = 100;

    

    private void Awake()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        enemyHealth = Instantiate(Canvas.transform.GetChild(0).gameObject);
        enemyHealth.transform.SetParent(Canvas.transform, false);
        healthBarSlider = enemyHealth.GetComponent<Slider>();
        enemyHealth.SetActive(true);
        healthBarSlider.minValue = 0;
        healthBarSlider.maxValue = health;
    }

    void Update()
    {
        // Below script calculates current position and vector difference of a current position and target position
        currentPos = transform.position;
        vectorDiff = targetPos - currentPos;

        // Update Health Bar with value of currentHealth at each frame
        healthBarSlider.value = currentHealth;
    }

    private void FixedUpdate()
    {
        enemyHealth.transform.position = new Vector3(currentPos.x, currentPos.y - 1f, currentPos.z);
        var nearestAlly = AllyGroup.FindClosestAlly(transform.position);
        if (nearestAlly != null)
        {
            targetPos = nearestAlly.transform.position;
            LookAtTarget();
            if (Vector3.Distance(currentPos, targetPos) > 1.5f)
            {
                Rigidbody2D.MovePosition(currentPos + (vectorDiff.normalized * Time.fixedDeltaTime * moveSpd));
            }
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
