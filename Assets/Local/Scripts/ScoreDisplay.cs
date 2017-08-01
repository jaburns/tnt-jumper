using UnityEngine;

/// <summary>
/// Displays the values stored in ScoreTracker in an attached TextMesh.
/// </summary>
public class ScoreDisplay : MonoBehaviour 
{
    void Start() 
    {
        var timeStr = formatTime(ScoreTracker.EndTime - ScoreTracker.StartTime);
        var text = GetComponent<TextMesh>();
        text.text = "   Time:   " + timeStr + 
            "\nDeaths:   " + ScoreTracker.DeathCount +
            "\n  Shots:   " + ScoreTracker.ShotCount;
    }

    static string formatTime(float seconds)
    {
        var totalSecs = Mathf.FloorToInt(seconds);
        var secs = totalSecs % 60;
        var mins = totalSecs / 60;

        var secsString = secs.ToString();
        if (secs < 10) secsString = "0" + secsString;

        return mins + ":" + secsString;
    }
}