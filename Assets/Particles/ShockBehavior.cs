using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShockBehavior : MonoBehaviour
{

    
    // Start is called before the first frame update
    void Start()
    {
        Invoke("DestroySelf", .3f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void DestroySelf()
    {
        Destroy(gameObject);
    }
}
