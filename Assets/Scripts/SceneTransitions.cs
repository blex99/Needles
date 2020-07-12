using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitions : MonoBehaviour
{
    // warning: please avoid updating this prefab, since it may mess up 
    //          other instances in other scenes.

    // will be accessible by FinishLine, with this
    public static SceneTransitions instance { get; private set; }

    public Animator panelAnimations;
    public string nextSceneName;

    private void Awake()
    {
        instance = this; 
    }

    // called by finish line object or other...
    public void LoadNewLevel()
    {
        StartCoroutine(LoadScene());
    }

    // will transition to new scene in 1.5 seconds
    IEnumerator LoadScene()
    {
        panelAnimations.SetTrigger("FadeOut");
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(nextSceneName);
    }
}
