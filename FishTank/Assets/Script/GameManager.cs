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
    public static int score;
    public static int high_score;
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
}
