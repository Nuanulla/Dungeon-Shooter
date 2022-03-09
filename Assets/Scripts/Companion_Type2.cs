using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Companion_Type2 : MonoBehaviour
{

    public Sprite portrait;
    private CompanionStats CompanionStats;
    public PlayerController PlayerController;
    private CompanionController CompanionController;

    private Rigidbody2D Rigidbody2D;
    private Vector3 vectorDiff;
    public Vector3 targetPos;
    public Quaternion targetRot;
    private Vector3 currentPos;
    private const float moveSpd = 3f;
    private float attackRate = 0.5f;
    private float attackDelay;
    private GameObject nearestEnemy;

    public AudioClip attack;

    void Awake()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();
        CompanionController = gameObject.GetComponent<CompanionController>();
        CompanionStats = gameObject.GetComponent<CompanionStats>();
        CompanionStats.health = 200;
        CompanionStats.mana = 100;
        CompanionStats.companionNumber = 2;
        CompanionStats.CloneCompanionDisplayOverlay();
        CompanionStats.image.sprite = portrait;
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerController.toggle2 == true)
        {
            CompanionStats.image.color = new Color(0.5f, 1f, 0.5f, 1f);
        }
        else
        {
            CompanionStats.image.color = Color.white;
        }
        // Below script calculates current position and vector difference of a current position and target position
        currentPos = transform.position;
        vectorDiff = targetPos - currentPos;
    }

    void FixedUpdate()
    {
        nearestEnemy = EnemyGroup.FindClosestEnemy(transform.position);
        if (nearestEnemy != null)
        {
            CompanionController.state = 0;
            targetPos = nearestEnemy.transform.position;
            LookAtTarget();
            PhysicalAttackBehavior();
        }
    }

    void LookAtTarget()
    {
        targetRot = Quaternion.LookRotation(Vector3.forward, vectorDiff.normalized);
        targetRot = Quaternion.RotateTowards(transform.rotation, targetRot, 360 * Time.fixedDeltaTime);
        Rigidbody2D.MoveRotation(targetRot);
    }

    void PhysicalAttackBehavior()
    {
        if (Vector3.Distance(currentPos, targetPos) > 1.5f)
        {
            Move();
        }
        if ((Vector3.Distance(currentPos, targetPos) <= 1.5f) && Time.fixedTime > attackDelay)
        {
            attackDelay = Time.fixedTime + attackRate;
            AudioSource.PlayClipAtPoint(attack, transform.position);
            nearestEnemy.GetComponent<EnemyStats>().TakeDamage(15);
        }
    }

    void Move()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position + transform.up, transform.up);
        if (EnemyGroup.Pool.Contains(hit.collider.gameObject))
        {
            Rigidbody2D.MovePosition(currentPos + (vectorDiff.normalized * Time.fixedDeltaTime * moveSpd));
        }
    }
}
