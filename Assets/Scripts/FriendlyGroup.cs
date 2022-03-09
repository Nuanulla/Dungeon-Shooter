using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FriendlyGroup : MonoBehaviour
{
    public static readonly HashSet<GameObject> Pool = new HashSet<GameObject>();

    void Awake()
    {
        Invoke("Add", 0.1f);
    }

    void Add()
    {
        Pool.Add(gameObject);
    }
 
    void OnDestroy()
    {
        Pool.Remove(gameObject);
    }

    public static GameObject FindClosestFriendly(Vector3 pos)
    {
        GameObject result = null;
        float dist = 225f;
        var e = FriendlyGroup.Pool.GetEnumerator();
        while(e.MoveNext())
        {
            float d = (e.Current.transform.position - pos).sqrMagnitude;
            if(d < dist && d != 0f)
            {
                result = e.Current;
                dist = d;
            }
        }
        return result;
    }
}
