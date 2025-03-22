using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreChecker : MonoBehaviour
{
    public static float TutorialAccuracy;
    public static float LevelOneAccuracy;

    public Text TutorialTextScore;
    public Text Level1TextScore;

    public Button level1Button;

    private void Update()
    {
        ManageTutorialAccuracy();
        ManageLevelOneAccuracy();
    }

    void ManageLevelOneAccuracy()
    {
        if (LevelOneAccuracy > 80f)
        {
            Level1TextScore.text = "A";
        }
        else if (LevelOneAccuracy > 70f)
        {
            Level1TextScore.text = "B";
        }
        else if (LevelOneAccuracy > 50f)
        {
            Level1TextScore.text = "C";
        }
        else if (LevelOneAccuracy > 15f)
        {
            Level1TextScore.text = "D";
        }
        else if(LevelOneAccuracy > 0)
        {
            Level1TextScore.text = "F";
        }
    }
    void ManageTutorialAccuracy()
    {
        if (TutorialAccuracy > 80f)
        {
            TutorialTextScore.text = "A";
        }
        else if (TutorialAccuracy > 70f)
        {
            TutorialTextScore.text = "B";
        }
        else if (TutorialAccuracy > 50f)
        {
            TutorialTextScore.text = "C";
        }
        else if (TutorialAccuracy > 15f)
        {
            TutorialTextScore.text = "D";
        }
        else if(TutorialAccuracy > 0)
        {
            TutorialTextScore.text = "F";
        }

        if (TutorialAccuracy > 0)
        {
            level1Button.interactable = true;
        }
    }
}
