using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class BombController : MonoBehaviourPun
{
    public Bomb bombPrefab;     //Koristi SerializeField ovde, nema potrebe da bude public
    public int bombsInStack = 0;   //Ne koristi public

    private GameObject droppedBomb;


    void Update()
    {
        if (Input.GetKeyUp(KeyCode.B) && photonView.IsMine)
        {
            DropABomb();
        }
    }

    public void DropABomb()
    {
        if (bombsInStack > 0 && photonView.IsMine)
        {
            bombsInStack--;
            Vector3 bombSpawnLocation = gameObject.transform.position;
            droppedBomb = PhotonNetwork.Instantiate(bombPrefab.name, bombSpawnLocation, Quaternion.identity);
            Bomb droppedBombComponent = droppedBomb.GetComponent<Bomb>();       //Direktno pristupi boji, nema potrebe da izvlacenje ref
            Color bombColor = new Color(1f,.3f,.3f, 1f);
            droppedBombComponent.GetComponent<SpriteRenderer>().color = bombColor;
            int bombPhotonID = droppedBomb.GetPhotonView().ViewID;
            photonView.RPC("ActivateBomb", RpcTarget.All, bombPhotonID);
        }

    }

    [PunRPC]
    public void ActivateBomb(int droppedBombID) 
    {
        GameObject droppedBombGameObject = PhotonView.Find(droppedBombID).gameObject;
        Bomb droppedBomb = droppedBombGameObject.GetComponent<Bomb>();
        droppedBomb.isCollectable = false;
        droppedBomb.ActivateBomb();
    }

    public void AddBomb()
    {
        bombsInStack++;
    }
}
