using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class SpawnPlayers : MonoBehaviourPunCallbacks
{
    public GameObject playerPrefab;
    public Transform cartTransform;
    public PhotonView view;

    public float minX = -8f;
    public float maxX = 8f;
    public float minY = -4f;
    public float maxY = 4f;

    public List<GameObject> spawnPoints;

    private void Awake()
    {
        PhotonNetwork.SerializationRate = 40;
        PhotonNetwork.SendRate = 20;
        spawnPoints.Add(GameObject.FindGameObjectWithTag("SpawnPoint1"));
        spawnPoints.Add(GameObject.FindGameObjectWithTag("SpawnPoint2"));
        spawnPoints.Add(GameObject.FindGameObjectWithTag("SpawnPoint3"));
        spawnPoints.Add(GameObject.FindGameObjectWithTag("SpawnPoint4"));
    }
    public void SetParents(){
        
    }
    private void Start()
    {
        Vector2 position = new Vector2(0, 0);
        PhotonNetwork.Instantiate(playerPrefab.name, position, Quaternion.identity);
        //
        //
        //TODO: NAJPIERW ZALADUJ WSZYSTKICH GRACZY, A WTEDY USTAW ICH RODZICOW NA PUNKT SPAWNU
        //
        //
        //newPlayer.transform.SetParent(spawnPoints[playerID].transform);
        //newPlayer.transform.localPosition = new Vector3(0, 0, 0);
    }
}
