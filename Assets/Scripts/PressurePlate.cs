using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    public GameObject doorObject;
    public float doorSpeed;

    bool isOpen;
    Vector3 openPosition; // position for closer
    Vector3 closedPosition; // position for open door

    private void Awake()
    {
        isOpen = false;
        float doorHeight = doorObject.GetComponent<BoxCollider2D>().size.y;
        closedPosition = doorObject.transform.position;
        openPosition = doorObject.transform.position - (Vector3.up * doorHeight);
    }

    private void Update()
    {
        // if door should be open and its not open, open it
        if (isOpen && Vector3.Distance(openPosition, doorObject.transform.position) >= 0.001f)
        {
            doorObject.transform.position = 
                Vector3.MoveTowards(doorObject.transform.position, openPosition, doorSpeed);
        }
        // same for closed
        if (!isOpen && Vector3.Distance(closedPosition, doorObject.transform.position) >= 0.001f)
        {
            doorObject.transform.position = 
                Vector3.MoveTowards(doorObject.transform.position, closedPosition, doorSpeed);
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Pickup>() != null)
        {
            isOpen = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Pickup>() != null)
        {
            isOpen = false;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(gameObject.transform.position, doorObject.transform.position);
    }
}
















