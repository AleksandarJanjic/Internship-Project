using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerInteraction : MonoBehaviourPun
{
    string tagOfCollidedObject;
    private Bomb touchedBomb;
    private Player thisPlayer;
    private BombController bombController;

    private void Start()
    {
        bombController = gameObject.GetComponent<BombController>();
        thisPlayer = gameObject.GetComponent<Player>();
    }

    void OnTriggerEnter2D(Collider2D other) 
    {
        if(photonView.IsMine)
        {
            if (other.gameObject.tag == "Collectable")
            {
                tagOfCollidedObject = other.gameObject.GetComponent<Collectable>().CollectableTag;

                switch(tagOfCollidedObject)
                {
                    case "Bomb":
                        touchedBomb = other.gameObject.GetComponent<Bomb>();
                        if (touchedBomb.isCollectable == true)
                        {
                            bombController.AddBomb();
                            int bombPhotonID = other.gameObject.GetPhotonView().ViewID;
                            photonView.RPC("BombCollected", RpcTarget.All, bombPhotonID);
                        }
                        break;
                }
            }
            else if (other.gameObject.tag == "PlayerDestroyer")
            {
                gameObject.GetComponent<Player>().KillPlayer();

            }
        }
    }
    

    [PunRPC]
    public void BombCollected(int bombID)
    {
        GameObject bomb = PhotonView.Find(bombID).gameObject;
        bomb.SetActive(false);
    }
}
