using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreScript : MonoBehaviour
{
    public Text scoreText; //Score text element refference
    int playerScore = 0; //Player score at start

    private void Start()
    {
        Platform.OnPlayerLanded += ChangePlayerScore; 
    }

    void ChangePlayerScore(float playerPosition)
    {
        if(playerPosition > 0)
        {
            playerScore = (int)playerPosition + 1;
            scoreText.text = playerScore.ToString();
        }
        
    }

    private void OnDestroy()
    {
        Platform.OnPlayerLanded -= ChangePlayerScore; //Unsubscribe from an event
    }
}
