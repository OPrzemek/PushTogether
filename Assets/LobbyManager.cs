using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class LobbyManager : MonoBehaviourPunCallbacks
{
    public const string PLAYER_READY = "IsPlayerReady";

    //Login Panel
    public GameObject loginPanel;

    public InputField loginField;

    //Create and Join Room Panel
    public GameObject createAndJoinRoomPanel;

    public InputField createField;
    public InputField joinField;

    //Room Panel
    public GameObject roomPanel;

    public Text roomText;
    public GameObject playerList;
    public GameObject playerEntryPrefab;
    public Button startGameButton;
    private Dictionary<int, GameObject> playerListEntries;

    private void Start()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
    }
    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby();
        SetActivePanel(createAndJoinRoomPanel.name);
    }
    public void Login()
    {
        string playerName = loginField.text;

        if (!playerName.Equals(""))
        {
            PhotonNetwork.LocalPlayer.NickName = playerName;
            PhotonNetwork.ConnectUsingSettings();
        }
        else
        {
            Debug.LogError("Player Name is invalid.");
        }
    }
    private void SetActivePanel(string activePanel)
    {
        loginPanel.SetActive(activePanel.Equals(loginPanel.name));
        createAndJoinRoomPanel.SetActive(activePanel.Equals(createAndJoinRoomPanel.name));
        roomPanel.SetActive(activePanel.Equals(roomPanel.name));
    }

    public void CreateRoom()
    {
        PhotonNetwork.CreateRoom(createField.text);
    }
    public void JoinRoom()
    {
        PhotonNetwork.JoinRoom(joinField.text);
    }
    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel("Game");
        /*SetActivePanel(roomPanel.name);

        if (playerListEntries == null)
        {
            playerListEntries = new Dictionary<int, GameObject>();
        }

        foreach (Player p in PhotonNetwork.PlayerList)
        {
            GameObject currentPlayer = Instantiate(playerEntryPrefab);
            currentPlayer.transform.SetParent(playerList.transform);
            currentPlayer.transform.localScale = Vector3.one;
            currentPlayer.GetComponent<PlayerEntry>().Initialize(p.NickName, p.ActorNumber);

            object playerReady;
            if (p.CustomProperties.TryGetValue(PLAYER_READY, out playerReady))
            {
                currentPlayer.GetComponent<PlayerEntry>().SetReady((bool)playerReady);
            }

            playerListEntries.Add(p.ActorNumber, currentPlayer);
        }

        startGameButton.gameObject.SetActive(CheckPlayersReady());*/
    }
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        GameObject newPlayerEntry = Instantiate(playerEntryPrefab);
        newPlayerEntry.transform.SetParent(playerList.transform);
        newPlayerEntry.transform.localScale = Vector3.one;
        newPlayerEntry.GetComponent<PlayerEntry>().Initialize(newPlayer.NickName, newPlayer.ActorNumber);

        playerListEntries.Add(newPlayer.ActorNumber, newPlayerEntry);

        startGameButton.gameObject.SetActive(CheckPlayersReady());
    }
    public override void OnPlayerPropertiesUpdate(Player targetPlayer, Hashtable changedProps)
    {
        if (playerListEntries == null)
        {
            playerListEntries = new Dictionary<int, GameObject>();
        }

        GameObject playerEntry;
        if (playerListEntries.TryGetValue(targetPlayer.ActorNumber, out playerEntry))
        {
            object playerReady;
            if (changedProps.TryGetValue(PLAYER_READY, out playerReady))
            {
                playerEntry.GetComponent<PlayerEntry>().SetReady((bool)playerReady);
            }
        }

        startGameButton.gameObject.SetActive(CheckPlayersReady());
    }
    private bool CheckPlayersReady()
    {
        if (!PhotonNetwork.IsMasterClient)
        {
            return false;
        }

        foreach (Player p in PhotonNetwork.PlayerList)
        {
            object isPlayerReady;
            if (p.CustomProperties.TryGetValue(PLAYER_READY, out isPlayerReady))
            {
                if (!(bool)isPlayerReady)
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        return true;
    }
    public void OnPlayersReady()
    {
        PhotonNetwork.CurrentRoom.IsOpen = false;
        PhotonNetwork.CurrentRoom.IsVisible = false;

        PhotonNetwork.LoadLevel("Game");
    }
}
