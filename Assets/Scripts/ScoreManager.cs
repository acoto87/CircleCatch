using UnityEngine;

public class ScoreManager
{
    public static int score;
    public static int maxScore;
    public static Color color;

    public static void Reset()
    {
        score = 0;
    }

    public static void SaveMaxScore()
    {
        if (score > maxScore)
        {
            PlayerPrefs.SetInt("MaxScore", score);
            maxScore = score;

            StartController.PostScore();
        }
    }

    public static void LoadMaxScore()
    {
        maxScore = PlayerPrefs.GetInt("MaxScore");
    }
}
