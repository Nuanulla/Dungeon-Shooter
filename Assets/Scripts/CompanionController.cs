using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompanionController : MonoBehaviour
{

    private Rigidbody2D Rigidbody2D;
    private const float moveSpd = 3f;
    public Vector3 targetPos;
    public bool atRest;
    private Vector3 vectorDiff;
    private Quaternion targetRot;

    private void Awake()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();
        targetPos = transform.position;
        atRest = true;
    }

    private void Update()
    {
        vectorDiff = targetPos - transform.position;
        targetRot = Quaternion.LookRotation(Vector3.forward, vectorDiff.normalized);
        targetRot = Quaternion.RotateTowards(transform.rotation, targetRot, 360 * Time.fixedDeltaTime);
    }

    private void FixedUpdate()
    {
        if ((vectorDiff.magnitude > Time.fixedDeltaTime * moveSpd) && atRest == false)
        {
            Rigidbody2D.MovePosition(transform.position + (vectorDiff.normalized * Time.fixedDeltaTime * moveSpd));
            Rigidbody2D.MoveRotation(targetRot);
        }
        else
        {
            atRest = true;
        }
        
    }
}
