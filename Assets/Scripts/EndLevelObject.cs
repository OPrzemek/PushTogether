using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class EndLevelObject : MonoBehaviourPun
{
    public Button endLevelButton;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Cart")
        {
            endLevelButton.gameObject.SetActive(true);
            endLevelButton.interactable = PhotonNetwork.IsMasterClient ? true : false;
            endLevelButton.onClick.AddListener(EndLevel);
        }
    }
    public void EndLevel()
    {
        PhotonNetwork.LoadLevel("Game2");
    }
}
