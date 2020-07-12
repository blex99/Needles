using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickyWall : MonoBehaviour
{
    PlayerController player;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<PlayerController>() != null)
        {
            player = other.GetComponent<PlayerController>();
            player.rigidbodyStuckTo = GetComponentInParent<Rigidbody2D>();
            player.isNearStickyWall = true;
        }
    }


    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.GetComponent<PlayerController>() != null)
        {
            player = other.GetComponent<PlayerController>(); 

            player.isNearStickyWall = false;
            player.rigidbodyStuckTo = null;
        }
    }
}
