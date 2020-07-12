using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerSpawner : MonoBehaviour
{
    public static PlayerSpawner instance;
    public GameObject playerPrefab;
    GameObject vcam;

    private void Awake()
    {
        if (instance == null) instance = this;
        vcam = GameObject.Find("CM vcam1");
        SpawnPlayer();
    }

    void SpawnPlayer()
    {
        GameObject player = Instantiate(playerPrefab, this.transform);
        vcam.GetComponent<CinemachineVirtualCamera>().Follow = player.transform;
    }

    // called when player dies, delete old player and make a new one
    public void SpawnNewPlayer(GameObject oldPlayer)
    {
        Destroy(oldPlayer);
        GameObject player = Instantiate(playerPrefab, this.transform);
        vcam.GetComponent<CinemachineVirtualCamera>().Follow = player.transform;
    }
}










