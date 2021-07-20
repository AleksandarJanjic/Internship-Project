using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerScript : MonoBehaviour
{
    const string mainMenuName = "MainMenu";
    string onlineSceneName = "OnlineGame";
    string offlineSceneName = "OfflineGame";

    public void loadMainMenu()
    {
        SceneManager.LoadScene(mainMenuName);
    }
    public void restartTheGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void playOnlineGame()
    {
        SceneManager.LoadScene(onlineSceneName);
    }
    public void playOfflineGame()
    {
        SceneManager.LoadScene(offlineSceneName);
    }
}
