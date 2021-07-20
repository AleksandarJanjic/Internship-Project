using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffectsScript : MonoBehaviour
{
    public AudioSource ouchSound;
    public AudioSource jumpSound;

    private void Awake()
    {
        Player.OnPlayerKilled += playOuchSound;

        ForceMovement.PlayerHaveJumped += playjumpSound;
    }

    public void playOuchSound()
    {
        Debug.Log("Played sound : " + ouchSound);
        ouchSound.Play();
    }
    public void playjumpSound(float none)
    {
        //We do nothing with this float 
        Debug.Log("Played sound : " + jumpSound);
        jumpSound.Play();
    }
}
