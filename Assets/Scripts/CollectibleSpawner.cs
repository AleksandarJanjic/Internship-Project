using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class CollectibleSpawner : MonoBehaviourPun
{//KOMENTARISATI
    public GameObject[] spawnableObjectPrefabs;     //Koristi serialize field
    public List<GameObject> usableObjects;          //Prebaci na private

    Vector3 spawnPosition;

    GameObject objectToWorkWith;

    float chanceRamp = 90f;



    void Start()
    {
        if(PhotonNetwork.IsMasterClient)
        {
        spawnPosition = new Vector3(transform.position.x, transform.position.y + 0.2f, 0);
        if(objectToWorkWith == null)
            {
                for (int i = 0; i < spawnableObjectPrefabs.Length; i++)
                {
                    objectToWorkWith = PhotonNetwork.Instantiate(spawnableObjectPrefabs[i].name, spawnPosition, Quaternion.identity, 0);
                    int photonViewID = objectToWorkWith.GetPhotonView().ViewID;
                    photonView.RPC("SetActiveStatus", RpcTarget.All, false, photonViewID);
                    usableObjects.Add(objectToWorkWith);
                }

                float chanceToSpawn = Random.Range(0f, 100f);
                int randomObjectToUse = Random.Range(0, spawnableObjectPrefabs.Length);
                if (chanceToSpawn > chanceRamp)
                {
                    objectToWorkWith = usableObjects[randomObjectToUse];
                    int photonViewID = objectToWorkWith.GetPhotonView().ViewID;
                    photonView.RPC("SetActiveStatus", RpcTarget.All, true, photonViewID);
                }

            }
        }
    }

    private void OnDisable()
    {
        if(objectToWorkWith != null) 
        {
            int photonViewID = objectToWorkWith.GetPhotonView().ViewID;
            photonView.RPC("SetActiveStatus", RpcTarget.All, false, photonViewID);
            objectToWorkWith = null;
        }
    }

    [PunRPC]
    public void SetActiveStatus(bool status, int photonViewID)
    {
        GameObject collectable = PhotonView.Find(photonViewID).gameObject;
        collectable.SetActive(status);
    }
}
