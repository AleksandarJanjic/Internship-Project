using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothMovement : MonoBehaviour
{
    private Vector3 currentPosition;
    private Vector3 targetPosition;

   

    private Vector3[] movementPoints;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(currentPosition != targetPosition) {
            transform.position = Vector3.MoveTowards(currentPosition, targetPosition, 40f * Time.deltaTime);
        }
        if(targetPosition == transform.position) {
            currentPosition = transform.position;
            targetPosition = movementPoints[2];
        }
    }

    public void StartMovement(Vector3[] movePoints) {
        movementPoints = movePoints;
        currentPosition = movementPoints[0];
        targetPosition = movementPoints[1];
    }
}
