using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using TMPro;
using System;

public class GameManager : MonoBehaviour
{
    public int score;
    [SerializeField] TextMeshProUGUI score_text;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        score = 0;
    }

    // Update is called once per frame
    void Update()
    {
        score_text.text = "Score: " + score.ToString();
    }

    public void AddScore(int _score)
    {
        score += _score;
    }
}
