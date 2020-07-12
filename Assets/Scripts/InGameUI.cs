using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InGameUI : MonoBehaviour
{
    public static InGameUI instance { get; private set; }
    public Image healthMask;
    public TextMeshProUGUI timeText;
    public Animator panelAnimations;
    float originalSize;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        originalSize = healthMask.rectTransform.rect.width; 
    }

    private void Update()
    {
        timeText.text = Mathf.FloorToInt(GameManager.instance.timer).ToString();
            //Mathf.FloorToInt(Time.timeSinceLevelLoad).ToString();
    }

    public void SetHealthValue(float health, float healthMax)
    {
        float value = Mathf.Max(0, health / healthMax);
        healthMask.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, originalSize * value);
    }

    // fade out screen
    public void FadeOut()
    {
        panelAnimations.SetTrigger("FadeOut");
    }
}
