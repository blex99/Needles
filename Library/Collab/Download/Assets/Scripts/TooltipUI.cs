using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TooltipUI : MonoBehaviour
{
    public GameObject controlsImageObject;


    public void HideControls()
    {
        controlsImageObject.SetActive(false);
    }
}
