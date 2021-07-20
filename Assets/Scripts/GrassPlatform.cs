using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrassPlatform : Platform
{
      public override void OnCollisionEnter2D(Collision2D collision) {
        base.OnCollisionEnter2D(collision);
    }
}
