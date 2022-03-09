using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandardProjectile_Type1 : MonoBehaviour
{

    private Rigidbody2D Rigidbody2D;
    private const float moveSpd = 9f;
    public int damage = 10;

    // Start is called before the first frame update
    private void Awake()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();
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

    void OnTriggerEnter2D(Collider2D col)
    {
        if (FriendlyGroup.Pool.Contains(col.gameObject) || EnemyGroup.Pool.Contains(col.gameObject))
        {
            col.gameObject.SendMessage("TakeDamage", damage);
        }
        if (!col.isTrigger)
        {
            Destroy(gameObject);
        }
    }
}
