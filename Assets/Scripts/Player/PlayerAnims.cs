using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnims : MonoBehaviour
{
    // Start is called before the first frame update

    Animator anim;
    bool running;
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (( Input.GetKeyDown("w") || Input.GetKeyDown("a") || Input.GetKeyDown("s") || Input.GetKeyDown("d") ) && !running)
        {
            anim.SetTrigger("MakeRun");
            running = true;
        } else if (!Input.anyKey && running)
        {
            running = false;
            anim.SetTrigger("MakeRun");
        }
    }
}
