using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireExtinguisher : Items
{

    public GameObject cloud;

    // Update is called once per frame
    void Update()
    {
        
    }

    protected override void Break()
    {
        Instantiate(cloud, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (collision.collider.name != "Player" && BeingThrown && rb.velocity.sqrMagnitude > criticalVelocity * criticalVelocity)
        {
            Break();
        }
    }
}
