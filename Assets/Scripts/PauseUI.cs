using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseUI : MonoBehaviour
{
    public static bool paused = false;

    public GameObject pauseUI;
    public GameObject quitConfirmationPanel;
    public GameObject optionsPanel;

    GameManager gm;

    private void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(gm.pause) || Input.GetKeyDown(KeyCode.Escape))
        {
            if(paused)
            {
                optionsPanel.GetComponent<OptionsUI>().Back();

                pauseUI.SetActive(false);
                Time.timeScale = 1f;
                paused = false;
            }
            else
            {
                Pause();
            }
        }
    }

    public void Pause()
    {
        pauseUI.SetActive(true);
        Time.timeScale = 0f;
        paused = true;
    }

    public void Resume()
    {
        FindObjectOfType<AudioManager>().PlaySFX("Submit");
        pauseUI.SetActive(false);
        Time.timeScale = 1f;
        paused = false;
    }

    public void BackToMainMenu()
    {
        Resume();
        FindObjectOfType<AudioManager>().PlaySFX("Cancel");
        SceneManager.LoadScene("MainMenu");
    }

    public void Options()
    {
        FindObjectOfType<AudioManager>().PlaySFX("Submit");
        optionsPanel.SetActive(true);
    }

    public void RestartLevel()
    {
        FindObjectOfType<AudioManager>().PlaySFX("Submit");
        gm.RestartLevel();
        Resume();
        gm.ResetTimer();
    }

    public void QuitGame()
    {
        FindObjectOfType<AudioManager>().PlaySFX("Submit");
        quitConfirmationPanel.SetActive(true);
    }
}

