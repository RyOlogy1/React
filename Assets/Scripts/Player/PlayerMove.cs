using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMove : MonoBehaviour
{
    [HideInInspector]
    List<char> que = new List<char>();
    public List<char> Que
    {
        get
        {
            return que;
        }

        set
        {
            que = value;
        }
    }

    Rigidbody2D rb;
    public float initialBurst;
    public float accel;
    public float maxRecoverableSpeed;
    public float maxSpeed;
    public float stableDrag;

    Vector2 lastDirec;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.angularVelocity = 0;
        SetPlayerVel();
        CheckForRestart();
        CheckForQuit();
    }

    private void CheckForRestart()
    {
        if (Input.GetKeyDown("r"))
        {
            SceneManager.LoadScene(0);
        }
    }

    private void CheckForQuit()
    {
        if (Input.GetKeyDown("escape"))
        {
            Application.Quit();
        }
    }

    void SetPlayerVel()
    {
        Vector2 chosen = Vector2.zero;
        if (movementKeyDown()) // Player is to move
        {

            if (Input.GetKey("w"))
            {
                chosen += Vector2.up;
            }
            if (Input.GetKey("a"))
            {
                chosen += Vector2.left;
            }
            if (Input.GetKey("s"))
            {
                chosen += Vector2.down;
            }
            if (Input.GetKey("d"))
            {
                chosen += Vector2.right;
            }

            // Player can quickly push in the new direction if he's slow enough to plant his feet in the ground
            if (lastDirec != chosen && rb.velocity.magnitude <= maxRecoverableSpeed)
            {
                rb.velocity = chosen * initialBurst;
            }
            rb.SetRotation(Vector2.SignedAngle(Vector2.up, chosen));
            // When you run, your feet don't create drag. 
            rb.drag = 1;

        }
        else // Player is to stand still, because movement button is pressed
        {
            chosen = Vector2.zero;

            // When you're standing still, your feet plant into the ground, creating drag
            rb.drag = stableDrag;
        }


        // Actually move the player in the chosen direction
        rb.AddForce(chosen * accel);
        if (rb.velocity.magnitude > maxSpeed) // The player can't get faster forever. 
        {
            rb.velocity = rb.velocity.normalized * maxSpeed;
        }

        // Save data about which direction the player is currently travelling in, so that
        // the player knows that there is change in direction and can plant his feet in response
        lastDirec = chosen;

    }

    private bool movementKeyDown()
    {
        return Input.GetKey("w") || Input.GetKey("a") || Input.GetKey("d") || Input.GetKey("s");
    }
}
