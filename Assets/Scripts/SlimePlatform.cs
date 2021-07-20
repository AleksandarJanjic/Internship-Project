using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimePlatform : Platform
{
    public override void OnCollisionEnter2D(Collision2D collision) {
      collision.collider.GetComponent<ForceMovement>().PlayerStuck();
      Player player = collision.collider.gameObject.GetComponent<Player>();
      float fallingDistance = base.CalculatePlayerFallDistance(player);
      if(fallingDistance > 1.5f)
      {
        player.KillPlayer();
      }
      base.OnCollisionEnter2D(collision);
    }

    void OnCollisionExit2D(Collision2D other)
    {
      other.collider.GetComponent<ForceMovement>().PlayerUnstuck();
    }
}
