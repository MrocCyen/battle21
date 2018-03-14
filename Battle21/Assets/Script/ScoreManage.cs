using System;
using UnityEngine;
using System.Collections;

public class ScoreManage : MonoBehaviour
{

    public static void SetScore(int score)
    {
        GameObject scoreObj = GameObject.FindGameObjectWithTag("Score");
        string currentScore = scoreObj.GetComponent<UILabel>().text;
        scoreObj.GetComponent<UILabel>().text = (Convert.ToInt32(currentScore) + score).ToString();
    }

    private void Start()
    {

    }

    private void Update()
    {

    }
}
