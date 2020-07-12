using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum Control
{
    MoveRight,
    MoveLeft,
    Jump,
    Spikey
}

public class TooltipText : MonoBehaviour
{
    public Control control;

    // Start is called before the first frame update
    void Awake()
    {
        switch (control)
        {
            case Control.MoveRight:
                GetComponent<Text>().text = GameManager.instance.right.ToString();
                break;
            case Control.MoveLeft:
                GetComponent<Text>().text = GameManager.instance.left.ToString();
                break;
            case Control.Jump:
                GetComponent<Text>().text = GameManager.instance.jump.ToString();
                break;
            case Control.Spikey:
                GetComponent<Text>().text = GameManager.instance.spikey.ToString();
                break;
        }
    }
}
