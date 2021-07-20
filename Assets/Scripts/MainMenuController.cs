using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class MainMenuController : MonoBehaviourPunCallbacks
{
    [SerializeField] private string versionName = "0.1";
    [SerializeField] private InputField UsernameInput;
    [SerializeField] private InputField JoinGameInput;
    [SerializeField] private InputField CreateGameInput;

    const string onlineGameSceneName = "OnlineTestGame";


    private void Start()
    {
        if(!PhotonNetwork.IsConnected)
        {
            ConnectToServer();
        }
        
    }
    private void Awake()
    {
        if(PhotonNetwork.IsConnected)
        {
            Debug.Log("We are connected");
        }
        else
        {

            Debug.Log("We are  NOT connected");
        }
        
    }
    private static void ConnectToServer()
    {
        PhotonNetwork.ConnectUsingSettings();
        Debug.Log("Trying to connect to server");
    }
    

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby();
        Debug.Log("Connected to master server");
    }
    public void CreateTheGame()
    {
        PickTheUsername();
        PhotonNetwork.CreateRoom(CreateGameInput.text, new Photon.Realtime.RoomOptions() {MaxPlayers = 2}, null);
        Debug.Log("Created a game room !");
    }
    public void JoinTheGame()
    {
        PickTheUsername();
        PhotonNetwork.JoinRoom(JoinGameInput.text, null);
        Debug.Log("Joining a game room !");
    }
    private void PickTheUsername()
    {
        PhotonNetwork.NickName = UsernameInput.text;
        Debug.Log("Your username is " + PhotonNetwork.NickName);
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Loading the level scene !");
        PhotonNetwork.LoadLevel(onlineGameSceneName);
    }

}
