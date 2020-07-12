using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class YouWinScreen_Script : MonoBehaviour
{
    public void BackToMainMenu()
    {
        FindObjectOfType<AudioManager>().PlaySFX("Submit");
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitGame()
    {
        FindObjectOfType<AudioManager>().PlaySFX("Cancel");
        Application.Quit();
    }

    public void Credits()
    {
        FindObjectOfType<AudioManager>().PlaySFX("Submit");
        SceneManager.LoadScene("CreditsScreen");
    }
}
