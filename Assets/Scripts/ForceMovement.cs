using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Photon.Pun;

public class ForceMovement : MonoBehaviourPun
{
    [SerializeField] Rigidbody2D rigidbody;
    [SerializeField] Player player;
    [SerializeField] float horizontalForce;
    [SerializeField] float verticalForce;
    [SerializeField] DustParticles dustParticles;

    private Vector3 direction;
    private Queue<Vector3> movementQueue = new Queue<Vector3>();

    //Movement Vectors
    private Vector3 UP = new Vector3(0, 1f, 0);
    private Vector3 DOWN = new Vector3(0, -1f, 0);
    private Vector3 LEFT = new Vector3(-1f, 1f, 0);
    private Vector3 RIGHT = new Vector3(1f, 1f, 0);
    //Stuck in Slime variable
    private int stuckMovesCounter = 0;

    public static event Action<float> PlayerHaveJumped;

    private Vector2 networkPosition;
      
    // Update is called once per frame
    void Update()
    {   
        if(photonView.IsMine) {
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            {
                movementQueue.Enqueue(UP);
            }
            if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
            {
                movementQueue.Enqueue(LEFT);
            }
            if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
            {
                movementQueue.Enqueue(DOWN);
            }
            if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
            {
                movementQueue.Enqueue(RIGHT);
            }
        }
        
    }

    private void FixedUpdate()
    {
        float horizontalVelocity = rigidbody.velocity.x;
        float verticalVelocity = rigidbody.velocity.y;
        if(movementQueue.Count > 0 && horizontalVelocity == 0 && verticalVelocity == 0)
        {
            Vector3 nextMove = movementQueue.Dequeue();
            dustParticles.DustCloud();
            MovePlayer(nextMove);
        }
        if(!photonView.IsMine)
        {
            rigidbody.position = Vector2.MoveTowards(rigidbody.position, networkPosition, Time.fixedDeltaTime);
        }
    }

    public void MovePlayer(Vector3 direction)
    {
        if(stuckMovesCounter > 0)
        {
            stuckMovesCounter--;
            return;
        }
        Platform currentPlatform = player.GetCurrentPlatform();
        if(direction == UP)
        {
            rigidbody.AddForce(direction * verticalForce, ForceMode2D.Impulse);
            if(PhotonNetwork.IsMasterClient)
            {
                PlayerHaveJumped?.Invoke(transform.position.y);
            }
        }
        if(direction == DOWN)
        {
            StartCoroutine(currentPlatform.AllowDownwardMove());
            rigidbody.AddForce(direction * verticalForce, ForceMode2D.Impulse);
        }
        if(direction == LEFT || direction == RIGHT)
        {
            rigidbody.AddForce(direction * horizontalForce, ForceMode2D.Impulse);
        }
    }

    public void PlayerStuck()
    {
        stuckMovesCounter = 2;
    }

    public void PlayerUnstuck()
    {
        stuckMovesCounter = 0;
    }

    public Rigidbody2D GetRigidbody() {
        return rigidbody;
    }

}
