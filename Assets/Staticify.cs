using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Staticify : MonoBehaviour
{
    bool operated;
    Rigidbody2D rb;
    float time = 1f;
    // Start is called before the first frame update
    void Start()
    {
        operated = false;
        rb = GetComponent<Rigidbody2D>();
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (time > 0)
        {
            time -= Time.deltaTime;
        } else if (!operated && rb.velocity.sqrMagnitude < .1)
        {
            operated = true;
            Destroy(GetComponent<BoxCollider2D>());
            Destroy(rb);
        }
    }
}
