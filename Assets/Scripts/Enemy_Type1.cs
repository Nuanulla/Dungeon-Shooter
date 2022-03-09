using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Type1 : MonoBehaviour
{

    private Rigidbody2D Rigidbody2D;
    private Vector3 vectorDiff;
    public Vector3 targetPos;
    public Quaternion targetRot;
    private Vector3 currentPos;
    private const float moveSpd = 3f;

    private float attackRate = 1.0f;
    private float attackDelay;

    private GameObject nearestFriendly;

    private void Awake()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();
        var EnemyStats = gameObject.GetComponent<EnemyStats>();
        EnemyStats.health = 250;
        EnemyStats.CloneStatOverlay();
    }

    void Update()
    {
        // Below script calculates current position and vector difference of a current position and target position
        currentPos = transform.position;
        vectorDiff = targetPos - currentPos;
    }

    private void FixedUpdate()
    {
        nearestFriendly = FriendlyGroup.FindClosestFriendly(transform.position);
        if (nearestFriendly != null)
        {
            targetPos = nearestFriendly.transform.position;
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
            Rigidbody2D.MovePosition(currentPos + (vectorDiff.normalized * Time.fixedDeltaTime * moveSpd));
        }
        if ((Vector3.Distance(currentPos, targetPos) <= 1.5f) && Time.fixedTime > attackDelay)
        {
            attackDelay = Time.fixedTime + attackRate;
            nearestFriendly.SendMessage("TakeDamage", 10);
        }
    }
}
