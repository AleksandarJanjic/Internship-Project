using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class ConnectionControllerOnlineScene : MonoBehaviourPunCallbacks
{

    [SerializeField] private string versionName = "0.1";

    [SerializeField] private WinnerDeclarer winnerDeclarer;

    const string mainMenuSceneName = "MainMenu";
    private void Awake()
    {
        Player.OnPlayerKilled += LeaveTheRoom;
    }
    public override void OnPlayerLeftRoom(Photon.Realtime.Player otherPlayer)
    {
        Debug.Log("Player have left the room !!");
        winnerDeclarer.WonGame();
        PhotonNetwork.LeaveRoom();
    }
    public override void OnLeftRoom()
    {
        Debug.Log("We have left the room ");
    }
    public void LeaveTheRoom()
    {
        Time.timeScale = 1;
        winnerDeclarer.LostGame();
        PhotonNetwork.LeaveRoom();
    }
    public void goToMainMenu()
    {
        PhotonNetwork.LoadLevel(mainMenuSceneName);
    }

}
