using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllyGroup : MonoBehaviour
{
    public static readonly HashSet<AllyGroup> Pool = new HashSet<AllyGroup>();
 
    void Awake()
    {
        AllyGroup.Pool.Add(this);
    }
 
    void OnDestroy()
    {
        AllyGroup.Pool.Remove(this);
    }

    public static AllyGroup FindClosestAlly(Vector3 pos)
    {
        AllyGroup result = null;
        float dist = 20f;
        var e = AllyGroup.Pool.GetEnumerator();
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
