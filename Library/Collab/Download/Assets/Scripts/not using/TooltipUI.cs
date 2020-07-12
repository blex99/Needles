using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TooltipUI : MonoBehaviour
{
    public Rigidbody2D playerRB2D;
    bool started;

    private void Start()
    {
        playerRB2D = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
        started = false;
    }

    private void Update()
    {
        if (playerRB2D.velocity.magnitude <= 0.1f)
        {
            if (started == false)
            {
                StartCoroutine(StillTimer());
            }
        }
        else
        {
            StopCoroutine(StillTimer());
            Color c = GetComponent<Image>().color;
            c.a = 0f;
            GetComponent<Image>().color = c;
            started = false;
        }
    }

    // Wait 5 Seconds and then fade in the tooltip image
    IEnumerator StillTimer()
    {       
        started = true;
        yield return new WaitForSeconds(5f);

        yield return StartCoroutine(FadeIn());
    }

    // Tooltip fades in slowly
    IEnumerator FadeIn()
    {
        Color c = GetComponent<Image>().color;
        c.a = 0f;
        var startTime = Time.time;
        bool whileSwitch = true;

        while (whileSwitch == true)
        {
            float u = (Time.time - startTime) / 1f;

            if (u == 1f)
            {
                u = 1f;
                whileSwitch = false;
            }

            c.a = (1 - u) * 0 + (u * 1);

            GetComponent<Image>().color = c;
            yield return new WaitForEndOfFrame();
        }
    }
}
