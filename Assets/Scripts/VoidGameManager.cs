using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class VoidGameManager : MonoBehaviourPunCallbacks
{
    [SerializeField] private LevelGenerator levelGenerator;
    [SerializeField] private GameObject playerPrefab;
    //[SerializeField] private Camera mainCamera;
    private SpriteRenderer playerSprite;
    [SerializeField] private LevelMover levelMover;
    private Vector3 FIRST_PLAYER_START_POS = new Vector3(-2f, -0.5f, 0);
    private Vector3 SECOND_PLAYER_START_POS = new Vector3(2f, -0.5f, 0);

    [SerializeField]
    private float startTimer;
    public Text startTimerText;

    private ForceMovement localPlayerMovement;

    public override void OnPlayerEnteredRoom(Photon.Realtime.Player newPlayer)
    {
        base.OnPlayerEnteredRoom(newPlayer);
        SpawnRows();
        photonView.RPC("StartLevel", RpcTarget.All);
    }
    private void Start()
    {
        SpawnPlayer();
        levelMover.enabled = false;
    }

    void SpawnRows()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            levelGenerator.SpawnRows();
        }
    }
    void SpawnPlayer()
    {
        Vector3 spawnedPlayerPosition = (PhotonNetwork.IsMasterClient) ? FIRST_PLAYER_START_POS : SECOND_PLAYER_START_POS;
        GameObject player = PhotonNetwork.Instantiate(playerPrefab.name, spawnedPlayerPosition, Quaternion.identity, 0);
        localPlayerMovement = player.GetComponent<ForceMovement>();
        //localPlayerMovement.enabled = false;
    }

    [PunRPC]
    public void StartLevel()
    {
        Invoke("timerToStart", 3f);
    }



    public void timerToStart()
    {
        Time.timeScale = 1;
        photonView.RPC("AllowMovement", RpcTarget.All);
        levelMover.enabled = true;

    }

    [PunRPC]
    public void AllowMovement()
    {
        localPlayerMovement.enabled = true;
    }
}
