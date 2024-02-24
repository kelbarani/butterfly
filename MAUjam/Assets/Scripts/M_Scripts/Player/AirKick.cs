using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirKick : MonoBehaviour
{
    [SerializeField] private float airKickDamage=30f;
    private CircleCollider2D airKickCollider;
    
    void Start()
    {
        airKickCollider = GetComponent<CircleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
