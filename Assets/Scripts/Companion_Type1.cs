using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Companion_Type1 : MonoBehaviour
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
    private float attackRate = 1f;
    private float attackDelay;
    public GameObject Projectile;
    private GameObject cast_projectile;
    private GameObject nearestEnemy;
    private GameObject nearestFriendly;

    public AudioClip attack;

    void Awake()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();
        CompanionController = gameObject.GetComponent<CompanionController>();
        CompanionStats = gameObject.GetComponent<CompanionStats>();
        CompanionStats.health = 100;
        CompanionStats.mana = 150;
        CompanionStats.companionNumber = 1;
        CompanionStats.CloneCompanionDisplayOverlay();
        CompanionStats.image.sprite = portrait;
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerController.toggle1 == true)
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
            RangedAttackBehavior();
        }
        else
        {
            nearestFriendly = FriendlyGroup.FindClosestFriendly(transform.position);
            if ((nearestFriendly != null) && (nearestFriendly.GetComponent<CompanionStats>().currentHealth < nearestFriendly.GetComponent<CompanionStats>().health))
            {
                CompanionController.state = 0;
                targetPos = nearestFriendly.transform.position;
                LookAtTarget();
                CastBehavior();
            }
        }
    }

    void LookAtTarget()
    {
        targetRot = Quaternion.LookRotation(Vector3.forward, vectorDiff.normalized);
        targetRot = Quaternion.RotateTowards(transform.rotation, targetRot, 360 * Time.fixedDeltaTime);
        Rigidbody2D.MoveRotation(targetRot);
    }

    void CastBehavior()
    {
        if (Vector3.Distance(currentPos, targetPos) > 5f)
        {
            Move();
        }
        if ((Vector3.Distance(currentPos, targetPos) <= 15f) && Time.fixedTime > attackDelay)
        {
            attackDelay = Time.fixedTime + attackRate;
            nearestFriendly.GetComponent<CompanionStats>().RecoverHealth(3);
            GetComponent<CompanionStats>().SapMana(3);
        }
    }

    void RangedAttackBehavior()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position + transform.up, transform.up);
        if (Vector3.Distance(currentPos, targetPos) > 5f)
        {
            Rigidbody2D.MovePosition(currentPos + (vectorDiff.normalized * Time.fixedDeltaTime * moveSpd));
        }
        if ((EnemyGroup.Pool.Contains(hit.collider.gameObject)) && (Vector3.Distance(currentPos, targetPos) <= 10f) && (Time.fixedTime > attackDelay))
        {
            attackDelay = Time.fixedTime + attackRate;
            AudioSource.PlayClipAtPoint(attack, transform.position);
            cast_projectile = Instantiate(Projectile, currentPos + transform.up + (transform.right / 4), transform.rotation);
            cast_projectile.GetComponent<StandardProjectile_Type1>().damage = 10;
            cast_projectile.SetActive(true);
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
