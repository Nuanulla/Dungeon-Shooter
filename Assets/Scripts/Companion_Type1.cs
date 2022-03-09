using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Companion_Type1 : MonoBehaviour
{

    public Sprite portrait;

    void Awake()
    {
        var CompanionStats = gameObject.GetComponent<CompanionStats>();
        CompanionStats.health = 100;
        CompanionStats.companionNumber = 1;
        CompanionStats.CloneDisplayOverlay();
        CompanionStats.image.sprite = portrait;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
