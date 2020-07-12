using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySong : MonoBehaviour
{
    public string m_name;
    public bool stopAllInstead;

    void Start()
    {
        if (stopAllInstead) AudioManager.instance.StopAllMusic();
        else AudioManager.instance.PlayMusic(m_name);
    }
}
