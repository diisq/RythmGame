using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public class LevelEnder : MonoBehaviour
{
    [SerializeField] private AudioSource songProgress;
    [SerializeField] private float progressInSong;
    [SerializeField, Range(1, 5)] private int levelId;

    private void Update()
    {
        if (!songProgress.isPlaying)
        {
            BringBackToLevelSelect();
        }
    }

    void BringBackToLevelSelect()
    {
        // Store Accuracy
        switch (levelId)
        {
            case 1:
                ScoreChecker.TutorialAccuracy = ScoreManager.scorePercent * 100f;
                Debug.Log(ScoreChecker.TutorialAccuracy);
                break;
            case 2:
                ScoreChecker.LevelOneAccuracy = ScoreManager.scorePercent * 100f;
                Debug.Log(ScoreChecker.LevelOneAccuracy);
                break;
        }
        // Bring back to main menu
        SceneManager.LoadScene("Level Selection");
    }
}
