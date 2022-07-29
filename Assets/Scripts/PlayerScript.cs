using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerScript : MonoBehaviour
{
    private float speed = 0.2f;
    private GameObject cart;
    private Rigidbody2D cartRigidbody;
    public PhotonView view;
    public List<GameObject> spawnPoints;
    public int currentSpawnPoint = 0;
    public int playerID = PhotonNetwork.LocalPlayer.ActorNumber - 1;
    public Vector2 networkPosition;
    public Vector2 networkRotation;
    public Vector2 networkVelocity;
    public TextMeshPro textMesh;
    private void Awake()
    {
        spawnPoints.Add(GameObject.FindGameObjectWithTag("SpawnPoint1"));
        spawnPoints.Add(GameObject.FindGameObjectWithTag("SpawnPoint2"));
        spawnPoints.Add(GameObject.FindGameObjectWithTag("SpawnPoint3"));
        spawnPoints.Add(GameObject.FindGameObjectWithTag("SpawnPoint4"));
        cart = GameObject.FindGameObjectWithTag("Cart");
        cartRigidbody = cart.GetComponent<Rigidbody2D>();
    }
    private void Start()
    {
        foreach (GameObject player in GameObject.FindGameObjectsWithTag("PlayerInstance"))
        {
            PlayerScript playerScript = player.GetComponent<PlayerScript>();
            playerScript.textMesh.text = playerScript.view.Owner.NickName;
            player.transform.SetParent(spawnPoints[(playerScript.view.OwnerActorNr - 1)].transform);
            player.transform.localPosition = new Vector3(0, 0, 0);
        }
    }
    private void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("dziala");
            PhotonNetwork.LeaveRoom();
            SceneManager.LoadScene("Lobby");
        }
    }
    void FixedUpdate()
    {
        if (view.IsMine)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Debug.Log("dziala");
                PhotonNetwork.LeaveRoom();
            }
            Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            view.RPC("AddForceAtPosition", RpcTarget.MasterClient, input, transform.position);
        }
    }
    [PunRPC]
    public void AddForceAtPosition(Vector2 _input, Vector3 _position)
    {
        cartRigidbody.AddForceAtPosition(_input.normalized * speed, _position, ForceMode2D.Impulse);
    }

    //TODO
    //
    /*public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(cartRigidbody.position);
            stream.SendNext(cartRigidbody.rotation);
            stream.SendNext(cartRigidbody.velocity);
        }
        else
        {
            networkPosition = (Vector2)stream.ReceiveNext();
            networkRotation = (Vector2)stream.ReceiveNext();
            networkVelocity = (Vector2)stream.ReceiveNext();

            float lag = Mathf.Abs((float)(PhotonNetwork.Time - info.SentServerTime));
            networkPosition += cartRigidbody.velocity * lag;
        }
    }*/
}
