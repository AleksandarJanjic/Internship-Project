using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombExplosion : MonoBehaviour
{
    private Player player;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.gameObject.tag == "Player")      //Zameni string sa const string
        {
            player = collision.collider.gameObject.GetComponent<Player>();
            Debug.Log("collided with player " + player.playerName);
            if(player.canDieByBomb)
            {
                player.KillPlayer();
            }
        }

    }

}
