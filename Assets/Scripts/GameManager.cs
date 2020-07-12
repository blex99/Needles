using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public float timer = 0f;
    public float timeStart;

    public Vector2 checkpointPos;
    public Vector2 firstCheckpointPos;

    //for setting keybinds
    public KeyCode left { get; set; }
    public KeyCode right { get; set; }
    public KeyCode jump { get; set; }
    public KeyCode spikey { get; set; }
    public KeyCode pause { get; set; }

    public bool usingController { get; private set; }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance);
        }
        else
        {
            Destroy(gameObject);
        }

        usingController = false;

        //sets keybinds according to player preferences
        left = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("leftKey", "LeftArrow"));
        right = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("rightKey", "RightArrow"));
        jump = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("jumpKey", "C"));
        spikey = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("spikeyKey", "X"));
        pause = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("pauseKey", "Escape"));

        ResetTimer();
    }

    private void Update()
    {
        timer = Time.time - timeStart;
    }

    // reload current scene
    public void Die()
    {
        StartCoroutine(LoadScene(SceneManager.GetActiveScene().name)); 
    }

    // called by finish line object or other...
    public void LoadNewLevel(string name)
    {
        checkpointPos = Vector2.zero;
        StartCoroutine(LoadScene(name));
    }

    // will transition to new scene in 1.5 seconds
    IEnumerator LoadScene(string name)
    {
        InGameUI.instance.FadeOut();
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(name);
    }

    public void RestartLevel()
    {
        checkpointPos = firstCheckpointPos;
        StartCoroutine(LoadScene(SceneManager.GetActiveScene().name));
    }

    public void toggleUsingController()
    {
        usingController = !usingController;
        Debug.Log("using controller? " + usingController);
    }

    public void ResetTimer()
    {
        timeStart = Time.time;
    }
}
