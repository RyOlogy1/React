using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Items : MonoBehaviour
{
    public bool BeingThrown { get; set; }
    public bool fragile;
    public float criticalVelocity;



    protected Rigidbody2D rb;

    //For calculating acceleration
    protected Vector2 lastVelocity;

    // Start is called before the first frame update
    void Start()
    {
        BeingThrown = false;
        rb = GetComponent<Rigidbody2D>();
        lastVelocity = Vector2.zero;
    }

    // Update is called once per frame
    void Update()
    {
        if (BeingThrown)
        {
            if (rb.velocity.magnitude < criticalVelocity)
            {
                if (fragile)
                {
                    Break();
                } else
                {
                    BeingThrown = false;
                }
            }
            
        }
    }

    private void LateUpdate()
    {
        lastVelocity = rb.velocity;
    }

    virtual protected void Break()
    {

    }

    virtual protected void OnCollisionEnter2D(Collision2D collision)
    {
        
    }
}
