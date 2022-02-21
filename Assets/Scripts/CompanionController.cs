using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompanionController : MonoBehaviour
{

    private Rigidbody2D Rigidbody2D;
    private const float moveSpd = 3f;
    public Vector3 targetPos;
    private Vector3 currentPos;
    public int state; // 0 = at rest; 1 = change rotation; 2 = change position
    private Vector3 vectorDiff;
    public Quaternion targetRot;
    private Quaternion currentRot;
    public EnemyGroup Enemy;

    private void Awake()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();
        targetPos = transform.position;
        state = 0;
    }

    private void Start()
    {

    }

    private void Update()
    {
        // Below script calculates current position and vector difference of a current position and target position
        currentPos = transform.position;
        vectorDiff = targetPos - currentPos;
        currentRot = transform.rotation;
    }

    private void FixedUpdate()
    {
        var nearestEnemy = EnemyGroup.FindClosestEnemy(transform.position);
        
        if (state == 1)
        {
            LookAtTarget();
        }
        if ((state == 2) && ((Vector3.Distance(currentPos, targetPos) > 0.1f)))
        {
            Rigidbody2D.MovePosition(currentPos + (vectorDiff.normalized * Time.fixedDeltaTime * moveSpd));
            LookAtTarget();
        }
        if ((Vector3.Distance(currentPos, targetPos) <= 0.1f))
        {
            state = 0;
        }
        if ((state == 0) && (nearestEnemy != null))
        {
            targetPos = nearestEnemy.transform.position;
            LookAtTarget();
        }
    }

    void LookAtTarget()
    {
        targetRot = Quaternion.LookRotation(Vector3.forward, vectorDiff.normalized);
        targetRot = Quaternion.RotateTowards(transform.rotation, targetRot, 360 * Time.fixedDeltaTime);
        Rigidbody2D.MoveRotation(targetRot);
    }
}
