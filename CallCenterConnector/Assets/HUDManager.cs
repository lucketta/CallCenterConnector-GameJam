using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HUDManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timeText;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private CablePortsManager gameManager;

    public void Update()
    {

        timeText.text = $"Time Left: {gameManager.currentTime}";
        scoreText.text = $"Score: {gameManager.numberOccupied}/{(gameManager.ports.Length/2)}";
    }
}
