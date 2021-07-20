using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseButton : MonoBehaviour
{
    public GameObject pausePanel;
    
    public void PauseTheGame()
    {
        Time.timeScale = 0;
        pausePanel.SetActive(true);
    }

    public void ResumeToGame()
    {
        Time.timeScale = 1;
        pausePanel.SetActive(false);
    }
}
