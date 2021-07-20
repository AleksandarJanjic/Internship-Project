using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SidePlatform : Platform
{
    [SerializeField]
    private bool isRight;
    private Rigidbody2D rigidbody;
    private Vector3 LEFT = new Vector3(-1f, 1f, 0);
    private Vector3 RIGHT = new Vector3(1f, 1f, 0);
    [SerializeField]
    float force;
    public override void OnCollisionEnter2D(Collision2D collision)
    {
        Player player = collision.collider.gameObject.GetComponent<Player>();
        ForceMovement forceMovement = player.GetComponent<ForceMovement>();
        rigidbody = player.GetComponent<Rigidbody2D>();
        float fallingDistance = base.CalculatePlayerFallDistance(player);
        if(fallingDistance > 1.5f)
        {
          player.KillPlayer();
        }
        if(isRight && player.transform.position.y > transform.position.y)
        {
            rigidbody.AddForce(RIGHT * force, ForceMode2D.Impulse);
            return;
        }
        if(player.transform.position.y > transform.position.y)
        {
            rigidbody.AddForce(LEFT * force, ForceMode2D.Impulse);
        }
    }

}
