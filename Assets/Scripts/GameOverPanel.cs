using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverPanel : MonoBehaviour
{
    private void Awake()
    {
        Player.OnPlayerKilled += ActivatePanel;
    }
    private void Start()
    {
        gameObject.SetActive(false);
    }
    void ActivatePanel()
    {
        gameObject.SetActive(true);
        Player.OnPlayerKilled -= ActivatePanel;

    }
}
