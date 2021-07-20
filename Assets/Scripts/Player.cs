using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Player : MonoBehaviour
{
    public string playerName;
    public bool canDieByBomb = true;
    private Platform currentPlatform;
    public static event Action OnPlayerKilled;


    public string GetName()
    {
        return playerName;
    }

    public void KillPlayer() 
    {

        OnPlayerKilled?.Invoke();
        Destroy(gameObject);
    }

    public void SetCurrentPlatform(Platform platform)
    {
        currentPlatform = platform;
    }

    public Platform GetCurrentPlatform()
    {
        return currentPlatform;
    }


}
