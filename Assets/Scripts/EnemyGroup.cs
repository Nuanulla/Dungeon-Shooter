using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGroup : MonoBehaviour
{
    public static readonly HashSet<EnemyGroup> Pool = new HashSet<EnemyGroup>();
 
    void Awake()
    {
        EnemyGroup.Pool.Add(this);
    }
 
    void OnDestroy()
    {
        EnemyGroup.Pool.Remove(this);
    }

    public static EnemyGroup FindClosestEnemy(Vector3 pos)
    {
        EnemyGroup result = null;
        float dist = 15f;
        var e = EnemyGroup.Pool.GetEnumerator();
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
