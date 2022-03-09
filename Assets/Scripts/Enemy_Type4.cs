using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Type4 : MonoBehaviour
{
    private Rigidbody2D Rigidbody2D;
    private Vector3 vectorDiff;
    public Vector3 targetPos;
    public Quaternion targetRot;
    private Vector3 currentPos;
    private const float moveSpd = 4f;

    private float attackRate = 2.5f;
    private float attackDelay;

    public GameObject ProjectileType1;
    public GameObject ProjectileType2;
    private GameObject cast_projectile1;
    private GameObject cast_projectile2;

    private GameObject nearestFriendly;


    private void Awake()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();
        var EnemyStats = gameObject.GetComponent<EnemyStats>();
        EnemyStats.health = 500;
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
        if (Vector3.Distance(currentPos, targetPos) > 10f)
        {
            Rigidbody2D.MovePosition(currentPos + (vectorDiff.normalized * Time.fixedDeltaTime * moveSpd));
        }
        if ((FriendlyGroup.Pool.Contains(hit.collider.gameObject)) && (Vector3.Distance(currentPos, targetPos) <= 45f) && (Time.fixedTime > attackDelay))
        {
            attackDelay = Time.fixedTime + attackRate;
            if (Random.Range(0,3) < 2)
            {
                RangedAttackType1();
            }
            else
            {
                RangedAttackType2();
            }
        }
    }

    void RangedAttackType1()
    {
        cast_projectile1 = Instantiate(ProjectileType1, currentPos + (transform.up), transform.rotation);
        cast_projectile1.GetComponent<StandardProjectile_Type1>().damage = 25;
        cast_projectile1.SetActive(true);
    }
    void RangedAttackType2()
    {
        cast_projectile2 = Instantiate(ProjectileType2, currentPos + (transform.up * 1.5f), transform.rotation);
        cast_projectile2.GetComponent<StandardProjectile_Type2>().damage = 50;
        cast_projectile2.SetActive(true);
    }
}
