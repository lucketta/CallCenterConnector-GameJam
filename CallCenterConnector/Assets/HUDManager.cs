using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HUDManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timeText;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI numLives;
    [SerializeField] private CablePortsManager gameManager;
    private float currentTime;
    private int numberOccupied;
    private int lives;

    public void Update()
    {
        currentTime = gameManager.currentTime;
        numberOccupied = gameManager.numberOccupied;
        lives = gameManager.lives;

        timeText.text = $"{currentTime}";
        scoreText.text = $"{numberOccupied}/{(gameManager.ports.Length/2)}";
        numLives.text = $"{lives}/3";
    }
}
