using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikePlatform : Platform
{
      public override void OnCollisionEnter2D(Collision2D collision) {
        collision.collider.GetComponent<Player>().KillPlayer();
    }
}
