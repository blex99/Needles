using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public bool firstCheckpoint = false; //by default
    bool current;
    SpriteRenderer spriteRenderer;
    Animator animator;
    Color color;

    private void Start()
    {
        current = firstCheckpoint;
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        color = spriteRenderer.color;

        if (firstCheckpoint) GameManager.instance.firstCheckpointPos = transform.position;

        if (current == true && GameManager.instance.checkpointPos == Vector2.zero)
            GameManager.instance.checkpointPos = transform.position;
    }

    private void Update()
    {
        // if active checkpoint, its fully colored
        if (current)
        {
            color.a = 1f;
            animator.SetBool("isOn", true);
        }
        else
        {
            color.a = 0.5f;
            animator.SetBool("isOn", false);
        }

        spriteRenderer.color = color;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // make all other checkpoints inactive
            GameObject []checkpoints = GameObject.FindGameObjectsWithTag("Checkpoint");
            for (int i = 0; i < checkpoints.Length; i++)
            {
                checkpoints[i].GetComponent<Checkpoint>().current = false;
            }

            // make this checkpoint active
            current = true;
            GameManager.instance.checkpointPos = transform.position;

            // TEST (light)
            if (GetComponentInChildren<Light>() != null) 
            { 
                GetComponentInChildren<Light>().enabled = true;
            }
        }
    }
}
