using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    private TextMeshProUGUI scoreText;
    public static int score = 0;
    void Start()
    {
        scoreText = GetComponent<TextMeshProUGUI>();
    }
    // Update is called once per frame
    void Update()
    {
        scoreText.text = "Score: " + score;
    }
}
