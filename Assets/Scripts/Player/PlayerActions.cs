using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerActions : MonoBehaviour
{
    public int range;
    public KeyCode actionKey;
    public KeyCode cycleLeftKey;
    public KeyCode cycleRightKey;
    private int selectedSlot = 0;
    private Inventory i;

    private Transform playerTrans;
    private GameObject currentSelected;
    private List<GameObject> itemsInRange = new List<GameObject>();

    string[] mask = { "PlayerGrab" };

    // Start is called before the first frame update
    void Start()
    {
        i = new Inventory(3);
        playerTrans = GetComponentInParent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {

        if (i.getSlot(selectedSlot) == null)
        {
            GameObject closestObject = null;
            //Get the closest of all objects in the list of objects currently in the collider
            foreach (GameObject item in itemsInRange)
            {

                if (closestObject == null || (transform.position - item.transform.position).sqrMagnitude < (transform.position - closestObject.transform.position).sqrMagnitude)
                {
                    closestObject = item;
                }
            }
            if (currentSelected != closestObject)
            {
                if (currentSelected != null)
                {
                    currentSelected.GetComponent<SpriteRenderer>().color = Color.white;
                }
                currentSelected = closestObject;
                if (currentSelected != null)
                {
                    currentSelected.GetComponent<SpriteRenderer>().color = Color.red;
                }
            }
        } 
        
        

        if (Input.GetKeyDown(cycleRightKey))
        {
            selectedSlot = (selectedSlot + 1) % 3;
        }

        if (Input.GetKeyDown(cycleLeftKey))
        {
            selectedSlot = selectedSlot == 0 ? 2 : selectedSlot - 1;
        }
        if (Input.GetKeyDown(actionKey))
        {
            if (i.getSlot(selectedSlot) == null)
            {
                if (currentSelected != null)
                {

                    //Add object to slot
                    i.setSlot(selectedSlot, currentSelected);

                    //Disable object in world
                    currentSelected.SetActive(false);
                } else
                {
                    Debug.Log("No objects in range");
                }

            } else
            {
                if (Physics2D.OverlapPoint(transform.position, LayerMask.GetMask("Map")) == null)
                {
                    GameObject item = i.getSlot(selectedSlot);

                    //Teleport object to player
                    item.transform.position = this.transform.position;


                    //Enable object 
                    item.SetActive(true);
                    item.GetComponent<Items>().BeingThrown = true;
                    //Tell object it is being thrown

                    //Calculate Vector of force
                    Vector2 direction = transform.parent.rotation * Vector2.up;
                    //Add acceleration to object in direction being faced

                    item.transform.rotation = GetComponentInParent<Transform>().rotation;
                    item.GetComponent<Rigidbody2D>().AddTorque(.9f, ForceMode2D.Impulse);
                    item.GetComponent<Rigidbody2D>().AddForce(range * direction, ForceMode2D.Impulse);

                    //Remove object from slot
                    i.setSlot(selectedSlot, null);
                    
                } 
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
            itemsInRange.Add(collision.gameObject);
        
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        
        itemsInRange.Remove(collision.gameObject);
    }
}
