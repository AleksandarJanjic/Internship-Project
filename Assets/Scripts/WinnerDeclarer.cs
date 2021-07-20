using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class WinnerDeclarer : MonoBehaviourPunCallbacks
{
    [SerializeField] private GameObject WonLostPanel;
    [SerializeField] private Text wonLostText;
    
    public void LostGame()
    {
        wonLostText.text = "YOU LOST!";
        WonLostPanel.SetActive(true);
    }

    public void WonGame()
    {
        wonLostText.text = "YOU WON!";
        WonLostPanel.SetActive(true);
    }
}
