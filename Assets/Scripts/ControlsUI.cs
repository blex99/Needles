using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlsUI : MonoBehaviour
{
    public Image ApplyImage;
    public Toggle controllerToggle;

    public Transform controlsPanel;
    Event keyEvent;
    Text buttonText;
    //for Default Controls:
    Text leftBT;
    Text rightBT;
    Text jumpBT;
    Text spikeyBT;
    Text pauseBT;

    KeyCode tempLeft,
        tempRight,
        tempJump,
        tempSpikey,
        tempPause;

    KeyCode newKey;

    bool waitingForKey;
    bool unappliedKeybinds;

    void Awake()
    {
        waitingForKey = false;
        controllerToggle.isOn = GameManager.instance.usingController;
        SetAllText();

        SetTempKeys();

        UnappliedCheck();
    }


    void KeybindsPanel()
    {
        controlsPanel.gameObject.SetActive(false);
    }

    private void OnGUI()
    {
        keyEvent = Event.current;

        if (keyEvent.isKey && waitingForKey == true)
        {
            newKey = keyEvent.keyCode;
            waitingForKey = false;
        }
    }

    public void UpdateButtonText(Text newKeyText)
    {
        buttonText = newKeyText;
    }

    public void StartAssignment(string keyName)
    {
        if (waitingForKey == false)
        {
            StartCoroutine(AssignKey(keyName));
        }
    }

    IEnumerator WaitForKey()
    {
        while (!keyEvent.isKey)
        {
            yield return null;
        }
    }

    public IEnumerator AssignKey(string keyName)
    {
        waitingForKey = true;

        yield return WaitForKey();

        switch (keyName)
        {
            case "left":
                tempLeft = newKey;
                buttonText.text = tempLeft.ToString();
                break;
            case "right":
                tempRight = newKey;
                buttonText.text = tempRight.ToString();
                break;
            case "jump":
                tempJump = newKey;
                buttonText.text = tempJump.ToString();
                break;
            case "spikey":
                tempSpikey = newKey;
                buttonText.text = tempSpikey.ToString();
                break;
            case "pause":
                tempPause = newKey;
                buttonText.text = tempPause.ToString();
                break;
        }

        UnappliedCheck();

        yield return null;
    }

    public void ResetToDefault(string keyName)
    {
        StartCoroutine(DefaultKey(keyName));
        controllerToggle.isOn = false;
        if (GameManager.instance.usingController)
        {
            toggleUsingController();
        }
        FindObjectOfType<AudioManager>().PlaySFX("Submit");
    }

    public IEnumerator DefaultKey(string keyName)
    {

        switch (keyName)
        {
            case "left":
                tempLeft = KeyCode.LeftArrow;
                buttonText.text = tempLeft.ToString();
                break;
            case "right":
                tempRight = KeyCode.RightArrow;
                buttonText.text = tempRight.ToString();
                break;
            case "jump":
                tempJump = KeyCode.C;
                buttonText.text = tempJump.ToString();
                break;
            case "spikey":
                tempSpikey = KeyCode.X;
                buttonText.text = tempSpikey.ToString();
                break;
            case "pause":
                tempPause = KeyCode.Escape;
                buttonText.text = tempPause.ToString();
                break;
        }

        UnappliedCheck();

        yield return null;
    }

    public void Apply()
    {
        if (tempLeft != KeyCode.None)
        {
            GameManager.instance.left = tempLeft;
        }
        else
        {
            GameManager.instance.left = KeyCode.LeftArrow;
        }
        PlayerPrefs.SetString("leftKey", GameManager.instance.left.ToString());

        if (tempRight != KeyCode.None)
        {
            GameManager.instance.right = tempRight;
        }
        else
        {
            GameManager.instance.right = KeyCode.RightArrow;
        }
        PlayerPrefs.SetString("rightKey", GameManager.instance.right.ToString());

        if (tempJump != KeyCode.None)
        {
            GameManager.instance.jump = tempJump;
        }
        else
        {
            GameManager.instance.jump = KeyCode.C;
        }
        PlayerPrefs.SetString("jumpKey", GameManager.instance.jump.ToString());

        if (tempSpikey != KeyCode.None)
        {
            GameManager.instance.spikey = tempSpikey;
        }
        else
        {
            GameManager.instance.spikey = KeyCode.X;
        }
        PlayerPrefs.SetString("spikeyKey", GameManager.instance.spikey.ToString());

        if (tempPause != KeyCode.None)
        {
            GameManager.instance.pause = tempPause;
        }
        else
        {
            GameManager.instance.pause = KeyCode.Escape;
        }
        PlayerPrefs.SetString("pauseKey", GameManager.instance.pause.ToString());

        if(unappliedKeybinds == true)
        {
            FindObjectOfType<AudioManager>().PlaySFX("Submit");
        }
        else
        {
            FindObjectOfType<AudioManager>().PlaySFX("Cancel");
        }

        UnappliedCheck();
    }

    void UnappliedCheck()
    {
        if (tempLeft == GameManager.instance.left &&
            tempRight == GameManager.instance.right &&
            tempJump == GameManager.instance.jump &&
            tempSpikey == GameManager.instance.spikey &&
            tempPause == GameManager.instance.pause)
        {
            Color c = ApplyImage.color;
            c.a = 0.3f;
            ApplyImage.color = c;
            unappliedKeybinds = false;
        }
        else
        {
            Color c = ApplyImage.color;
            c.a = 1f;
            ApplyImage.color = c;
            unappliedKeybinds = true;
        }
    }

    public void Back()
    {
        SetAllText();

        SetTempKeys();

        this.gameObject.SetActive(false);
        FindObjectOfType<AudioManager>().PlaySFX("Cancel");
    }

    void SetAllText()
    {
        for (int i = 0; i < controlsPanel.childCount; i++)
        {
            if (controlsPanel.GetChild(i).name == "leftKey")
            {
                controlsPanel.GetChild(i).GetComponentInChildren<Text>().text = GameManager.instance.left.ToString();
            }
            else if (controlsPanel.GetChild(i).name == "rightKey")
            {
                controlsPanel.GetChild(i).GetComponentInChildren<Text>().text = GameManager.instance.right.ToString();
            }
            else if (controlsPanel.GetChild(i).name == "jumpKey")
            {
                controlsPanel.GetChild(i).GetComponentInChildren<Text>().text = GameManager.instance.jump.ToString();
            }
            else if (controlsPanel.GetChild(i).name == "spikeyKey")
            {
                controlsPanel.GetChild(i).GetComponentInChildren<Text>().text = GameManager.instance.spikey.ToString();
            }
            else if (controlsPanel.GetChild(i).name == "pauseKey")
            {
                controlsPanel.GetChild(i).GetComponentInChildren<Text>().text = GameManager.instance.pause.ToString();
            }
        }
    }

    void SetTempKeys()
    {
        tempLeft = GameManager.instance.left;
        tempRight = GameManager.instance.right;
        tempJump = GameManager.instance.jump;
        tempSpikey = GameManager.instance.spikey;
        tempPause = GameManager.instance.pause;
    }

    public void toggleUsingController()
    {
        GameManager.instance.toggleUsingController();
    }
}
