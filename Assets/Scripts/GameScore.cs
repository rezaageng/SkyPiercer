using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameScore : MonoBehaviour
{
    public Text scoreText; // Assign lewat Inspector
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
            UpdateScoreUI(); // Update UI setiap kali score berubah
        }
    }

    void Start()
    {
        UpdateScoreUI(); // Pastikan skor awal ditampilkan
    }

    void UpdateScoreUI()
    {
        scoreText.text = score.ToString("D7");
    }
}
