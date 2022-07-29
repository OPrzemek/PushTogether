using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class PlayerEntry : MonoBehaviourPun
{
    public Text playerText;
    private int playerId;
    public Button playerButton;
    public Image playerButtonImage;
    private bool playerReady;
    public void Initialize(string playerName, int _playerId)
    {
        playerText.text = playerName;
        playerId = _playerId;
    }
    private void Start()
    {
        playerReady = false;
        playerButtonImage.color = Color.red;
        if (PhotonNetwork.LocalPlayer.ActorNumber != playerId)
        {
            playerButton.interactable = false;
        }
        else
        {
            Hashtable initialProps = new Hashtable() { { LobbyManager.PLAYER_READY, playerReady } };
            PhotonNetwork.LocalPlayer.SetCustomProperties(initialProps);

            playerButton.onClick.AddListener(() =>
            {
                playerReady = !playerReady;
                SetReady(playerReady);

                Hashtable props = new Hashtable() { { LobbyManager.PLAYER_READY, playerReady } };
                PhotonNetwork.LocalPlayer.SetCustomProperties(props);
            });
        }
    }
    public void SetReady(bool _playerReady)
    {
        playerButtonImage.color = _playerReady ? Color.green : Color.red;
    }
}