using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    public PlayerController player;
    private Rigidbody2D Rigidbody2D;
    private Vector3 vectorDiff;
    public Vector3 targetPos;
    public Quaternion targetRot;
    private Vector3 currentPos;
    

    private void Awake()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    void Update()
    {
        currentPos = transform.position;
        vectorDiff = targetPos - currentPos;
    }

    private void FixedUpdate()
    {
        var nearestAlly = AllyGroup.FindClosestAlly(transform.position);
        if (nearestAlly != null)
        {
            targetPos = nearestAlly.transform.position;
            LookAtTarget();
        }
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
