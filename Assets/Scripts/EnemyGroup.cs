using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGroup : MonoBehaviour
{
    public static readonly HashSet<GameObject> Pool = new HashSet<GameObject>();
 
    void Awake()
    {
        Pool.Add(gameObject);
    }
 
    void OnDestroy()
    {
        Pool.Remove(gameObject);
    }

    public static GameObject FindClosestEnemy(Vector3 pos)
    {
        GameObject result = null;
        float dist = 50f;
        var e = Pool.GetEnumerator();
        while(e.MoveNext())
        {
            float d = (e.Current.transform.position - pos).sqrMagnitude;
            if(d < dist)
            {
                result = e.Current;
                dist = d;
            }
        }
        return result;
    }
}
