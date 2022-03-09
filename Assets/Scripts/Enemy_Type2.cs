using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Type2 : MonoBehaviour
{
    private Rigidbody2D Rigidbody2D;
    private Vector3 vectorDiff;
    public Vector3 targetPos;
    public Quaternion targetRot;
    private Vector3 currentPos;
    private const float moveSpd = 3f;

    private float attackRate = 1.0f;
    private float attackDelay;

    public GameObject Projectile;
    private GameObject cast_projectile;

    private GameObject nearestFriendly;


    private void Awake()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();
        var EnemyStats = gameObject.GetComponent<EnemyStats>();
        EnemyStats.health = 100;
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
            RangedAttackBehavior();
        }
    }

    void LookAtTarget()
    {
        targetRot = Quaternion.LookRotation(Vector3.forward, vectorDiff.normalized);
        targetRot = Quaternion.RotateTowards(transform.rotation, targetRot, 360 * Time.fixedDeltaTime);
        Rigidbody2D.MoveRotation(targetRot);
    }

    void RangedAttackBehavior()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position + transform.up, transform.up);
        if (Vector3.Distance(currentPos, targetPos) > 5f)
        {
            Rigidbody2D.MovePosition(currentPos + (vectorDiff.normalized * Time.fixedDeltaTime * moveSpd));
        }
        if ((FriendlyGroup.Pool.Contains(hit.collider.gameObject)) && (Vector3.Distance(currentPos, targetPos) <= 10f) && (Time.fixedTime > attackDelay))
        {
            attackDelay = Time.fixedTime + attackRate;
            cast_projectile = Instantiate(Projectile, currentPos + (transform.up), transform.rotation);
            cast_projectile.GetComponent<StandardProjectile_Type1>().damage = 10;
            cast_projectile.SetActive(true);
        }
    }
}
