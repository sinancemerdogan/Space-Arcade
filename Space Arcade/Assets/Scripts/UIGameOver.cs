using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIGameOver : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText;
    ScoreKeeper scoreKeeper;

    private void Awake() {
        
    }
    void Start()
    {
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
    }

    void Update()
    {
        scoreText.text = "You Scored: \n" + scoreKeeper.GetScore().ToString();
    }
}
