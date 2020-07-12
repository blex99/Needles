using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Curtains : MonoBehaviour
{
    PlayerController player;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<PlayerController>() != null) 
        {   
            player = other.GetComponent<PlayerController>();
            if (!player.isNearCurtain)
            {
                player.rigidbodyStuckTo = GetComponentInParent<Rigidbody2D>();
                player.isNearCurtain = player.isSpikey;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.GetComponent<PlayerController>() != null)
        {
            player = other.GetComponent<PlayerController>();
            player.isNearCurtain = false;
            player.rigidbodyStuckTo = null;
        }
    }
}
