using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerminatorBehavior : MonoBehaviour
{

    public GameObject player;

    private Vector3 home;
    private Quaternion homeRotation;
    public float viewDistance;
    public float attackRange;
    public float speed;

    public GameObject shockParticles;

    private float viewDistSqr;
    private float attackRangeSqr;
    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        viewDistSqr = viewDistance * viewDistance;
        attackRangeSqr = attackRange * attackRange;
        rb = GetComponent<Rigidbody2D>();
        home = transform.position;
        homeRotation = transform.rotation;

        Debug.Log(homeRotation);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 toPlayerVec = player.transform.position - transform.position;
        if (Vector2.SqrMagnitude(toPlayerVec) <= attackRangeSqr)
        {
            Shock();
        } else if (Vector2.SqrMagnitude(toPlayerVec) <= viewDistSqr)
        {
            RaycastHit2D viewRay = Physics2D.Raycast(transform.position, toPlayerVec, viewDistance);
            if (viewRay.collider.gameObject.Equals(player))
            {

                rb.SetRotation(Quaternion.Euler(toPlayerVec));
                rb.AddForce(toPlayerVec.normalized * speed);
            } else
            {
                ReturnHome();
            }


        } else
        {
            ReturnHome();
        }
    }

    void ReturnHome()
    {
        Vector3 toHomeVec = home - transform.position;
        if (toHomeVec.sqrMagnitude < .1)
        {
            rb.SetRotation(homeRotation);
        }else
        {
            rb.SetRotation(Quaternion.Euler(toHomeVec));
            rb.AddForce((toHomeVec).normalized * speed);
        }
            
    }

    void Shock()
    {
        player.SetActive(false);
        rb.bodyType = RigidbodyType2D.Static;
        Instantiate(shockParticles, transform.position, Quaternion.identity);
        Destroy(this);
    }
}
