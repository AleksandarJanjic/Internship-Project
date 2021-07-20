using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class LevelGenerator : MonoBehaviour
{
    public GameObject rowPrefab; 
    public int howManyRowsToSpawn;
    private GameObject obj;

    private List<GameObject> createdRows = new List<GameObject>(); //List of created rows
    public Vector3 currentPosition; //Position where next row will be spawned

    private GameObject objectToMoveUp;

    private void OnEnable()
    {
        currentPosition.y = transform.position.y;
        ForceMovement.PlayerHaveJumped += MoveBottomPlatformToTop;
    }
    private void OnDisable()
    {
        ForceMovement.PlayerHaveJumped -= MoveBottomPlatformToTop;
    }

    public void SpawnRows()
    {
        for (int i = 0; i < howManyRowsToSpawn; i++)
        {
            Vector3 rowPosition = new Vector3(0, transform.position.y, 0);
            obj = PhotonNetwork.Instantiate(rowPrefab.name, rowPosition, Quaternion.identity);
            obj.transform.position = currentPosition;

            createdRows.Add(obj);
            currentPosition.y++;
        }
    }
    void MoveBottomPlatformToTop(float playerPosition)
    {
        if (playerPosition >= currentPosition.y-4f)
        {
            objectToMoveUp = createdRows[0]; //Take the lowest spawned row
            createdRows.RemoveAt(0);
            createdRows.Add(objectToMoveUp);
            objectToMoveUp.SetActive(false);

            objectToMoveUp.SetActive(true);
            objectToMoveUp.transform.position = currentPosition;

            currentPosition.y++;
        }
    }


}
