using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySong : MonoBehaviour
{
    public string m_name;

    void Start()
    {
        AudioManager.instance.PlayMusic(m_name);
    }
}
