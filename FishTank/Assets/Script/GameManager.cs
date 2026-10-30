using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using TMPro;
using System;

// Class to update score
public class GameManager : MonoBehaviour
{
    public static int score;
    [SerializeField] TextMeshProUGUI score_text;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        score = 0;
    }

    // Update is called once per frame
    void Update()
    {
        score_text.text = "Score: " + score;
    }

    // Add score to current score
    public static void AddScore(int _score)
    {
        score += _score;
    }
}
