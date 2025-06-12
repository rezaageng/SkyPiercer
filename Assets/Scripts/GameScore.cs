using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameScore : MonoBehaviour
{
    public Text scoreText;
    private int score;

    public int Score
    {
        get
        {
            return score;
        }
        set
        {
            score = value;
            UpdateScoreUI();
        }
    }

    void Start()
    {
        UpdateScoreUI();
    }

    void UpdateScoreUI()
    {
        scoreText.text = score.ToString("D7");
    }
}
