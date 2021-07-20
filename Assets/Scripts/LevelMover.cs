using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelMover : MonoBehaviour
{
    //ATTACHED TO CAMERA
    
    public float speed; 
     
    void LateUpdate()
    {
        transform.Translate(Vector2.up * speed * Time.deltaTime);
    }
}
