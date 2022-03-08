using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    private Rigidbody2D Rigidbody2D;
    private const float moveSpd = 7f;

    // Start is called before the first frame update
    private void Awake()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();
    }

    void Start()
    {

    }

    void OnEnable()
    {
        Destroy(gameObject, 2);
    }

    // Update is called once per frame
    void Update()
    {
        Rigidbody2D.velocity = transform.up * moveSpd;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        Destroy(gameObject);
        if (FriendlyGroup.Pool.Contains(col.gameObject))
        {
            col.gameObject.SendMessage("TakeDamage", 10);
        }
    }
}
