using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitConfirmationUI : MonoBehaviour
{
    public void Yes()
    {
        FindObjectOfType<AudioManager>().PlaySFX("Submit");
        Application.Quit();
    }

    public void No()
    {
        FindObjectOfType<AudioManager>().PlaySFX("Cancel");
        this.gameObject.SetActive(false);
    }
}
