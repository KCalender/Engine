using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class GameUI : MonoBehaviour
{
    public Text txtScore;

    private int totScore = 0;
    // Start is called before the first frame update
    void Start()
    {
        if(PlayerPrefs.HasKey("TOT_SCORE"))
            totScore = PlayerPrefs.GetInt("TOT_SCORE", 0);
        DispScore(0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DispScore(int score)
    {
        totScore += score;
        txtScore.text = "score <color=#ff0000>" + totScore.ToString() + "</color>";

        PlayerPrefs.SetInt("TOT_SCORE", totScore);
    }
}
