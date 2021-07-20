using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Platform : MonoBehaviour
{
    public static event Action <float> OnPlayerLanded;
    PlatformEffector2D platformEffector;


    void Start() {
        platformEffector = GetComponent<PlatformEffector2D>();
    }

    public virtual void OnCollisionEnter2D(Collision2D collision) 
    {
        Player player = collision.collider.GetComponent<Player>();
        player.SetCurrentPlatform(this);
        AlignPlayerToCenter(player);
    }

    public float CalculatePlayerFallDistance(Player player)
    {
        Platform platformLandedFrom = player.GetCurrentPlatform();
        float originalPlatformHeight = 0;
        if(platformLandedFrom != null)
        {
            Transform platformParent = platformLandedFrom.transform.parent;
            originalPlatformHeight = platformParent.transform.position.y;
        }
        float targetPlatformHeight = gameObject.transform.parent.position.y;
        return originalPlatformHeight - targetPlatformHeight;
    }

    public void AlignPlayerToCenter(Player player)
    {
        if(player.transform.position.y > transform.position.y)
        {
            ForceMovement playerMovement = player.gameObject.GetComponent<ForceMovement>();
            Transform playerTransform = player.transform;
            Vector3 playerPosition = player.transform.position;
            Vector3 platformCenterAlignment = new Vector3(Mathf.Round(playerPosition.x), playerPosition.y, 0);
            playerTransform.position = Vector3.MoveTowards(playerTransform.position, platformCenterAlignment, 1f);
            OnPlayerLanded?.Invoke(playerPosition.y);
        }
    }

    public IEnumerator AllowDownwardMove() {
        platformEffector.rotationalOffset = 180f;
        yield return new WaitForSeconds(0.3f);
        platformEffector.rotationalOffset = 0f;
    }
}
