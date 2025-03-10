using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    private List<GameObject> TotalBeats = new List<GameObject>();
    public static float TotalScore;
    private int totalBeatCount;
    private float scorePercent;
    [SerializeField] private Text scorePercentText;

    private void Start()
    {
        TotalBeats.Clear();
        string[] tags = { "J Beat", "L Beat", "K Beat" };

        foreach (string tag in tags)
        {
            TotalBeats.AddRange(GameObject.FindGameObjectsWithTag(tag));
        }
        totalBeatCount = TotalBeats.Count;
    }

    void Update()
    {
        if (TotalScore > 0)
        {
            scorePercent = TotalScore / totalBeatCount;
        }
        else
        {
            scorePercent = 0;
        }

        scorePercentText.text = $"Score: {scorePercent * 100f:F2}";

    }
}