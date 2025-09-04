using System;
using TMPro;
using UnityEngine;

public class FInalScoreCounterScript : MonoBehaviour
{
    [SerializeField, Tooltip("Score text that will be used to display the final score.")]
    private TMP_Text scoreText;
    private void OnEnable()
    {
        if (scoreText != null && GameManager.instance != null)
        {
            scoreText.SetText(GameManager.instance.score.ToString());
        }
    }
}
