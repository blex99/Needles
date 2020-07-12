using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderFillController : MonoBehaviour
{
    public void Filler(float newFillAmount)
    {
        GetComponent<Image>().fillAmount = newFillAmount;
    }
}
