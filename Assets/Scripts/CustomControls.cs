using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomControls : MonoBehaviour
{
    public static CustomControls ctrls;

    public KeyCode left { get; set; }
    public KeyCode right { get; set; }
    public KeyCode jump { get; set; }
    public KeyCode spikey { get; set; }
    public KeyCode pause { get; set; }


    private void Awake()
    {
        if (ctrls == null)
        {
            DontDestroyOnLoad(gameObject);
            ctrls = this;
        }
        else if(ctrls != this)
        {
            Destroy(gameObject);
        }

        left = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("leftKey", "LeftArrow"));
        right = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("rightKey", "RightArrow"));
        jump = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("jumpKey", "C"));
        spikey = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("spikeyKey", "X"));
        pause = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("pauseKey", "Escape"));
    }
}
