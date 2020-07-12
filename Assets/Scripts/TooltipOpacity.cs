using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TooltipOpacity : MonoBehaviour
{
    public Rigidbody2D playerRB2D;
    bool started;

    // Start is called before the first frame update
    void Start()
    {
        playerRB2D = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
        started = false;
    }

    // Update is called once per frame
    void Update()
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
            StopAllCoroutines();
            Color c = (GetComponent<Image>() != null) ? GetComponent<Image>().color : GetComponent<Text>().color;
            c.a = 0f;
            if(GetComponent<Image>() != null)
            {
                GetComponent<Image>().color = c;
            }

            if(GetComponent<Text>() != null)
            {
                GetComponent<Text>().color = c;
            }

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
        Color c = (GetComponent<Image>() != null) ? GetComponent<Image>().color : GetComponent<Text>().color;
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

            if (GetComponent<Image>() != null)
            {
                GetComponent<Image>().color = c;
            }

            if (GetComponent<Text>() != null)
            {
                GetComponent<Text>().color = c;
            }
            yield return new WaitForEndOfFrame();
        }
    }
}
