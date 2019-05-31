using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beer : Items
{
    public GameObject puddle;
    public Sprite[] puddleSprites;

    public GameObject shattered;
    
    protected override void Break()
    {
        GameObject spawnedPuddle = Instantiate(puddle, transform.position, Quaternion.Euler(0, 0, Random.Range(0, 360)));
        spawnedPuddle.GetComponent<SpriteRenderer>().sprite = puddleSprites[Random.Range(0, puddleSprites.Length)];
        GameObject shatteredBeer = Instantiate(shattered, transform.position, transform.rotation);
        Rigidbody2D[] childRBs = shatteredBeer.GetComponentsInChildren<Rigidbody2D>();

        string test = "";
        
        foreach (Rigidbody2D child in childRBs)
        {
            test += child.name;
            child.transform.SetParent(null, true);
            Debug.Log(rb.velocity + " - " + lastVelocity + " = " + (rb.velocity - lastVelocity));
            Debug.Log((rb.velocity - lastVelocity) + " / " + Time.deltaTime + " = " + (rb.velocity - lastVelocity) / Time.deltaTime);
            Debug.Log(rb.mass + " * accel = " + rb.mass * (rb.velocity - lastVelocity) / Time.deltaTime);
            child.AddForce(rb.mass * (rb.velocity - lastVelocity) / Time.deltaTime / -5);
        }

        Debug.Log(test);
        Destroy(this.gameObject);
    }

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        if (rb.velocity.magnitude > criticalVelocity)
        {
            Break();
        }
    }
}
