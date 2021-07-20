using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class RowPlatformGenerator : MonoBehaviourPun
{
    [SerializeField] GameObject[] platformPrefabs;
    public List<Vector3> availablePositions;
    public List<Vector3> usablePositions;


    public List<GameObject> RegularPlatforms;
    public List<GameObject> UnUsualPlatforms;

    int chanceToSpawn;
    int chanceRamp = 70;
    int randomPlatform;
    int randomPosition;

    public int howManyRegulars;

    void Awake()
    {
        if(PhotonNetwork.IsMasterClient)
        {
            MakeAllPlatforms();
        }
    }

    private void OnEnable()
    {
        if(PhotonNetwork.IsMasterClient)
        {
            SpawnPlatforms();
        }
    }
    private void OnDisable()
    {
        if(PhotonNetwork.IsMasterClient)
        {
            DestroyChilds();
        }
    }

    private void SpawnPlatforms()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            MakeAvailablePositionList();

            SpawnRegularPlatforms();
            SpawnOtherPlatforms();

            ClearList();
        }
    }

    private void MakeAllPlatforms()
    {
        Vector3 thisObjectPosition = gameObject.transform.position;
        Quaternion rotation = gameObject.transform.rotation;

        GameObject currentPlatformInstance;
        //Make regulars
        for (int i = 0; i < howManyRegulars; i++)
        {
            currentPlatformInstance = PhotonNetwork.Instantiate(platformPrefabs[0].name, thisObjectPosition, rotation, 0);
            RegularPlatforms.Add(currentPlatformInstance);
            SetPlatformParent(currentPlatformInstance);
        }
        //Make others
        for (int i = 1; i < platformPrefabs.Length; i++)
        {
            currentPlatformInstance = PhotonNetwork.Instantiate(platformPrefabs[i].name, thisObjectPosition, rotation, 0);
            UnUsualPlatforms.Add(currentPlatformInstance);
            SetPlatformParent(currentPlatformInstance);
        }
    }

    void SetPlatformParent(GameObject currentPlatform)
    {
        int platformID = currentPlatform.GetPhotonView().ViewID;
        int parentID = gameObject.GetPhotonView().ViewID;
        currentPlatform.SetActive(false);
        photonView.RPC("RPCSetParent", RpcTarget.All, platformID, parentID);
    }
    private void MakeAvailablePositionList()
    {
        Vector3 position = gameObject.transform.position;
        usablePositions = new List<Vector3>();
        for (int i = 0; i < availablePositions.Count; i++)
        {
            Vector3 newPos = new Vector3(availablePositions[i].x, position.y, 0f);
            usablePositions.Add(newPos);
        }
        //Debug.Log("Usable positions at START: " + usablePositions.Count);
    }
    private void SpawnRegularPlatforms()
    {
        for (int i = 0; i < RegularPlatforms.Count; i++)
        {
            randomPosition = Random.Range(0, usablePositions.Count);
            RegularPlatforms[i].SetActive(true);
            int platformPhotonID = RegularPlatforms[i].GetPhotonView().ViewID;
            RegularPlatforms[i].transform.position = usablePositions[randomPosition];
            photonView.RPC("RPCActivateClientPlatforms", RpcTarget.Others, platformPhotonID, true);
            usablePositions.RemoveAt(randomPosition);
        }
        //Debug.Log("Usable positions after NORMALS: " + usablePositions.Count);
    }
    private void SpawnOtherPlatforms()
    {
        for (int i = 0; i < usablePositions.Count; i++)
        {
            chanceToSpawn = Random.Range(0, 100);
            if (chanceToSpawn > chanceRamp)
            {
                print("other " + i);
                randomPlatform = Random.Range(0, UnUsualPlatforms.Count);
                randomPosition = Random.Range(0, usablePositions.Count);

                UnUsualPlatforms[randomPlatform].SetActive(true);
                UnUsualPlatforms[randomPlatform].transform.position = usablePositions[randomPosition];

                int platformPhotonID = UnUsualPlatforms[randomPlatform].GetPhotonView().ViewID;
                photonView.RPC("RPCActivateClientPlatforms", RpcTarget.Others, platformPhotonID, true);

                UnUsualPlatforms.RemoveAt(randomPlatform);
                usablePositions.RemoveAt(randomPosition);
            }
        }
        for(int i = 0; i < UnUsualPlatforms.Count; i++)
        {
            UnUsualPlatforms[i].SetActive(false);
            int platformPhotonID = UnUsualPlatforms[i].GetPhotonView().ViewID;
            photonView.RPC("RPCActivateClientPlatforms", RpcTarget.Others, platformPhotonID, false);
        }
        //Debug.Log("Usable positions after OTHERS: " + usablePositions.Count);
    }
    private void ClearList()
    {
        usablePositions.Clear();
        //Debug.Log("Usable positions after CLEARING: " + usablePositions.Count);
    }
    private void DestroyChilds()
    {
        foreach(Transform child in transform)
        {
            child.gameObject.SetActive(false);
        }
    }

    [PunRPC]
    public void RPCSetParent(int platformID, int parentID)
    {
        GameObject platformGameObject = PhotonView.Find(platformID).gameObject;
        GameObject parentGameObject = PhotonView.Find(parentID).gameObject;
        platformGameObject.transform.SetParent(parentGameObject.transform);
        platformGameObject.SetActive(false);
    }

    [PunRPC]
    public void RPCActivateClientPlatforms(int platformID, bool status)
    {
        GameObject targetPlatform = PhotonView.Find(platformID).gameObject;
        targetPlatform.SetActive(status);
    }
    
}
