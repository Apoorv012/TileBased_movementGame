using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

    [SerializeField] private TMP_Text RocketScoreText;
    int RocketScore = 0;

    private void Awake()
    {
        Instance = this;
        RocketScoreText.text = $"Score: {RocketScore}";
    }

    public void AddRocketScore(int _score)
    {
        RocketScore += _score;
        RocketScoreText.text = $"Score: {RocketScore}";
    }
}
