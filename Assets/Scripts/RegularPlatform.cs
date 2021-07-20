using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegularPlatform : Platform
{
      public override void OnCollisionEnter2D(Collision2D collision) {
        Player player = collision.collider.gameObject.GetComponent<Player>();
        float fallingDistance = base.CalculatePlayerFallDistance(player);
        if(fallingDistance > 1.5f)
        {
          player.KillPlayer();
        }
        base.OnCollisionEnter2D(collision);
    }
}
