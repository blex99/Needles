using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{
    public GameObject optionsPanel,
        quitConfirmationPanel;

    public void PlayGame()
    {
        FindObjectOfType<AudioManager>().PlaySFX("Submit");
        GameManager.instance.checkpointPos = Vector2.zero;
        GameManager.instance.firstCheckpointPos = Vector2.zero;
        SceneManager.LoadScene("Level1_Tutorial");
        GameManager.instance.ResetTimer();
    }

    public void HowToPlay()
    {
        FindObjectOfType<AudioManager>().PlaySFX("Submit");
        GameManager.instance.checkpointPos = Vector2.zero;
        GameManager.instance.firstCheckpointPos = Vector2.zero;
        SceneManager.LoadScene("Level_0");
        GameManager.instance.ResetTimer();
    }

    public void Options()
    {
        FindObjectOfType<AudioManager>().PlaySFX("Submit");
        optionsPanel.SetActive(true);
    }

    public void Credits()
    {
        FindObjectOfType<AudioManager>().PlaySFX("Submit");
        SceneManager.LoadScene("CreditsScreen");
    }

    public void QuitGame()
    {
        FindObjectOfType<AudioManager>().PlaySFX("Submit");
        quitConfirmationPanel.SetActive(true);
    }
}
