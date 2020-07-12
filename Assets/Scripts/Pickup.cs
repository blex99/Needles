using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public Vector2 positionFromPlayer;

    new Rigidbody2D rigidbody2D;
    PlayerController player;
    Vector3 playerVelocity;
    Vector3 playerTransform;
    bool letGoNeeded;
    bool madeNoise;

    public GameObject maskObject;

    private void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        madeNoise = false;
    }

    private void Update()
    {
        player = maskObject.GetComponent<PickupTrigger>().player;
        if (player != null && player.isSpikey)
        {
            if (!madeNoise)
            {
                FindObjectOfType<AudioManager>().PlaySFX("Pickup");
                madeNoise = true;
            }
            GetComponent<Rigidbody2D>().gravityScale = 0f;

            Vector3 velocity = rigidbody2D.velocity;
            Vector3 position = rigidbody2D.position;
            playerVelocity = maskObject.GetComponent<PickupTrigger>().player.velocity;
            playerTransform = maskObject.GetComponent<PickupTrigger>().player.transform.position;

            int dir = -1 * (int) Mathf.Sign(playerTransform.x - GetComponent<Transform>().position.x);
            //Debug.Log((dir == -1) ? "pickup is to player's left" : "pickup is to player's right");

            velocity.x = playerVelocity.x;
            velocity.y = playerVelocity.y;
            position.y = playerTransform.y + positionFromPlayer.y;
            position.x = playerTransform.x + (dir * positionFromPlayer.x);

            rigidbody2D.velocity = velocity;
            rigidbody2D.position = position;
            letGoNeeded = true;
        }
        else if (letGoNeeded)
        {
            ReleaseHold();
        }
    }

    private void ReleaseHold()
    {
        GetComponent<Rigidbody2D>().gravityScale = 1f;
        rigidbody2D.velocity = Vector3.zero;
        player = null;
        letGoNeeded = false;
        madeNoise = false;
    }
}
