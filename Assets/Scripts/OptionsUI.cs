using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsUI : MonoBehaviour
{
    public GameObject controlsPanel;
    public GameObject volumePanel;

    public void Controls()
    {
        FindObjectOfType<AudioManager>().PlaySFX("Submit");
        controlsPanel.SetActive(true);
    }

    public void Preferences()
    {
        //coming soon
    }

    public void Volume()
    {
        FindObjectOfType<AudioManager>().PlaySFX("Submit");
        volumePanel.SetActive(true);
    }

    public void Back()
    {
        FindObjectOfType<AudioManager>().PlaySFX("Cancel");

        controlsPanel.GetComponent<ControlsUI>().Back();
        volumePanel.GetComponent<VolumeUI>().Back();

        gameObject.SetActive(false);
    }
}


