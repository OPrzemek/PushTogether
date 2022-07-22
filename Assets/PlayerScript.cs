using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;

public class PlayerScript : MonoBehaviour
{
    private float speed = 1f;
    private Rigidbody2D cartRigidbody;

    PhotonView view;
    // Start is called before the first frame update
    void Start()
    {
        cartRigidbody = GetComponentInParent<Rigidbody2D>();
        view = GetComponent<PhotonView>();
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
    /*public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(rb2D.position);
            stream.SendNext(rb2D.velocity);
        }
        else
        {
            rb2D.position = (Vector3) stream.ReceiveNext();
            rb2D.velocity = (Vector3)stream.ReceiveNext();

            float lag = Mathf.Abs((float)(PhotonNetwork.Time - info.SentServerTime));
            rb2D.position += rb2D.velocity * lag;
        }
    }*/

    // Update is called once per frame
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
            view.RPC("AddForceAtPosition", RpcTarget.AllViaServer, input, transform.position);
        }
    }
    [PunRPC]
    public void AddForceAtPosition(Vector2 _input, Vector3 _position)
    {
        cartRigidbody = GetComponentInParent<Rigidbody2D>();
        cartRigidbody.AddForceAtPosition(_input.normalized * speed, _position, ForceMode2D.Impulse);
    }
    /*private void OnCollisionStay2D(Collision2D collision)
    {
        Rigidbody2D rigidbody = collision.rigidbody;
        if (rigidbody == null) return;
        Vector2 forceDirection = collision.gameObject.transform.position - transform.position;
        rigidbody.AddForceAtPosition(forceDirection * forceMagnitude, transform.position, ForceMode2D.Impulse);
    }*/
}
