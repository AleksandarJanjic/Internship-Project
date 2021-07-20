using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public int movementTime;
    public static float unitOfMovement = 1;

    public float movementSpeed;
    
    public SmoothMovement smoothMovement;
    public ForceMovement forceMovement;

    private Queue<Vector3> movementQueue = new Queue<Vector3>();

    private bool movementAllowed = true;

    private Vector3 up = new Vector3(0, unitOfMovement, 0);
    private Vector3 down = new Vector3(0, -unitOfMovement, 0);
    private Vector3 left = new Vector3(-unitOfMovement, 0, 0);
    private Vector3 right = new Vector3(unitOfMovement, 0, 0);

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            movementQueue.Enqueue(up);
        }
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            movementQueue.Enqueue(left);
        }
        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            movementQueue.Enqueue(down);
        }
        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            movementQueue.Enqueue(right);
        }
    }

    private void FixedUpdate()
    {
        if(movementQueue.Count > 0 && movementAllowed)
        {
            for(int i = 0; i < movementQueue.Count; i++)
            {
                if(movementQueue.Peek() == left || movementQueue.Peek() == right) {
                    MovePlayerSideways(movementQueue.Dequeue());
                    movementAllowed = false;
                    StartCoroutine(DelayMovement());
                }
                else {
                    MovePlayerVertically(movementQueue.Dequeue());
                    movementAllowed = false;
                    StartCoroutine(DelayMovement());
                }
            }
        }
    }

    private void MovePlayerSideways(Vector3 direction)
    {
        Vector3[] movementPoints = new Vector3[3];
        movementPoints[0] = transform.position;
        movementPoints[1] = new Vector3((transform.position.x + direction.x/2), transform.position.y + 0.5f, 0);
        movementPoints[2] = transform.position + direction;
        smoothMovement.StartMovement(movementPoints);
    }

    private void MovePlayerVertically(Vector3 direction) {
        Vector3[] movementPoints = new Vector3[3];
        movementPoints[0] = transform.position;
        movementPoints[1] = new Vector3(transform.position.x, transform.position.y + 0.5f, 0);
        movementPoints[2] = transform.position + direction;
        smoothMovement.StartMovement(movementPoints);
    }

    //listen for event to allow further movement
    private IEnumerator DelayMovement()
    {
        yield return new WaitForSeconds(movementTime);
        movementAllowed = true;
    }

}
