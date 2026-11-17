/***********************************************************************
Author      :	Zane Jackson
Studio      :	FishTank games
Description :   Keeps track of score / high score
**************************************************************************/

using System;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

// Class to update score
public class GameManager : MonoBehaviour
{
    private static int score = 0;
    private static int locked_score = 0;
    private static int high_score = 0;

    [SerializeField] TextMeshProUGUI score_text;
    InputAction pause_action;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        pause_action = InputSystem.actions.FindAction("Pause");
        score = 0;
    }

    // Update is called once per frame
    void Update()
    {
        score_text.text = "Score: " + score;

        // New high score
        if (score > high_score)
        {
            high_score = score;
        }

        // Quit / pause
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("Menu");
        }
    }

    // Add score to current score
    public static void AddScore(int _score)
    {
        score += _score;
    }

    // Set the score happens when player resets wave
    public static void SetScore()
    {
        locked_score = score; // Player keeps this score even if they die
    }

    // Reset the score when the player reaches a new wave
    public static void ResetScore()
    {
        score = locked_score; 
    }
}
