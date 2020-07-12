using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishLine : MonoBehaviour
{
    public string nextSceneName;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // if the thing it collided with is player load next scene..
        PlayerController player = 
            collision.gameObject.GetComponent<PlayerController>();

        if (player != null)
        {
            if (nextSceneName == null)
            {
                Debug.LogWarning("no scene name given");
            }
            else
            {
                GameManager.instance.LoadNewLevel(nextSceneName);
                GameManager.instance.ResetTimer();
            }
        }
    }
}
