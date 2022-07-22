using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class SpawnPlayers : MonoBehaviourPunCallbacks
{
    public GameObject playerPrefab;
    public Transform cartTransform;
    public List<GameObject> spawnPoints;
    public int currentSpawnPoint = 0;

    public float minX = -8f;
    public float maxX = 8f;
    public float minY = -4f;
    public float maxY = 4f;
    void Awake()
    {
        spawnPoints.AddRange(GameObject.FindGameObjectsWithTag("SpawnPoint"));
        PhotonNetwork.SerializationRate = 20;
        PhotonNetwork.SendRate = 40;
        Vector2 spawnPosition = spawnPoints[currentSpawnPoint].transform.localPosition;
        GameObject player = PhotonNetwork.Instantiate(playerPrefab.name, spawnPosition, Quaternion.identity);
        player.transform.SetParent(spawnPoints[currentSpawnPoint].transform);
        player.transform.localPosition = new Vector3(0, 0, 0);
        currentSpawnPoint++;
        /*player.transform.SetParent(GameObject.FindGameObjectWithTag("cart").transform);
        player.transform.localPosition = new Vector3(0,3,0);*/

    }
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Vector2 spawnPosition = spawnPoints[currentSpawnPoint].transform.localPosition;
        GameObject player = PhotonNetwork.Instantiate(playerPrefab.name, spawnPosition, Quaternion.identity);
        player.transform.SetParent(spawnPoints[currentSpawnPoint].transform);
        player.transform.localPosition = new Vector3(0, 0, 0);
        currentSpawnPoint++;
    }
}
