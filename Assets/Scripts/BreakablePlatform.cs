using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakablePlatform : Platform
{
    EdgeCollider2D edgeCollider2D;
    SpriteRenderer spriteRenderer;
    public override void OnCollisionEnter2D(Collision2D collision) {
        Player player = collision.collider.GetComponent<Player>();
        float fallingDistance = base.CalculatePlayerFallDistance(player);
        if(fallingDistance > 0.5f)
        {
            StartCoroutine(DestroyBreakablePlatform());
        }
        if(fallingDistance > 1.5f) {
          player.KillPlayer();
        }
        base.OnCollisionEnter2D(collision);
    }

    private IEnumerator DestroyBreakablePlatform() {
        edgeCollider2D = GetComponent<EdgeCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        edgeCollider2D.enabled = false;
        spriteRenderer.enabled = false;
        yield return new WaitForSeconds(3);
        edgeCollider2D.enabled = true;
        spriteRenderer.enabled = true;
    }
}
